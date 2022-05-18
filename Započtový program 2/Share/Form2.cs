using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Share
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            // Optimalizace.        
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            KeyDown += new KeyEventHandler(Form2_KeyDown);      // Pro používání šipek na klávesnici.
        }
            
        // Informační pole:
        // Legenda: (0/1/2/3/4/5) points/Jidlo/Bonusu/gameTimeMin/gameTimeSec/Difficulty
        // Poznámka: Difficulty: (easy/medium/hard) mají kód 1/2/3
        public static int[] info = new int[6];

        // !!!DŮLEŽITÉ!!!
        // panel.Location == Point(12, 12);
        // pictureBoxNyanCat.Location == Point(-2, -2);

        // Kolik snědl hráč instancí jídla.
        public int Jidla
        {
            set;
            get;    
        }

        // Kolik snědl hráč bonusů.
        public int Bonusu
        {
            set;
            get;
        }


        // Obtížnost (easy, medium, hard).
        public string Difficulty
        {
            set;
            get;
        }

        // Proměnné na pohyb:
        Random random = new Random();

        int points = 0;
        int pointFoodGain = 100;        // Kolik dostane hráč bodů za snězení jednoho jídla. (Jsou na tom závislé i hodnoty bodů za PowerUpy.) Mění se s obtížností.
        int gameTime = 0;   // Pro timerSpeed.
        int gameTimeSec = 0;
        int gameTimeMin = 0;
        Point snakeStartingLocation = new Point(223, 223);  // Nelze consts...
        // Pomyslná mřížka:
            // Kraje -2 a 448.
            // Strana 1 čtverečku je 25.
            // Snyanke cat vždy je ve 4 čtverečkách najednou s tím, že levý horní roh levého horního čtverečku jsou souřadnice pictureBoxNyanCat.
            // Mřížka je tedy 19 čtverečků × 19 čtverečků.
            // Poznámka: Rozšířená mřížka obsahuje ještě 2 čtverečky do každé strany, tj. je 21 čtverečků × 21 čtverečků.
       
        Point enemyStartingLocation = new Point(4, 4);

        const int maxSpeed = 50;    // Minimální interval timeru timerSnake.
        const int stepSnake = 25; // NEMĚNIT! (uzpůsobeno prostředí číslu 25)
        const int stepMagnet = 5;   

        int foodNum = 0;
        int foodX = 0;
        int foodY = 0;
        Image foodImage = food1;

        string direction = "right";
        bool zleva = false;
        bool enemyZleva = false;

        int powerUpNum = 0;
        int powerX = 0;
        int powerY = 0;
        bool powerSpawned = false;
        bool powerTaken = false;
        int gameTimePower = 0;
        int powerUpSpawnDelay = 0;
        bool blink = false;
        const int powerUpMinSpawnTime = 10;     // Minimální čas pro spawnutí PowerUpu od vypršení posledního PowerUpu (v sekundách).
        const int powerUpMaxSpawnTime = 20;     // Maximální čas pro spawnutí PowerUpu od vypršení posledního PowerUpu (v sekundách).
        const int powerUpDespawnTime = 13;      // Jak dlouho vydrží PowerUp na mapě, než zmizí (v sekunádch).
        const int powerUpBlinkTime = 3;         // Jak dlouho bude blikat (v sekundách).
        const int powerUpDurationTime = 10;     // Jak dlouho bude PowerUp učinkovat (v sekundách).

        // Proměnné PowerUpů:
        bool powerUpBorders = false;
        bool powerUpMagnet = false;
        bool powerUpreversed = false;

        // Proměnné rychlosti:
        int speedStartingInterval = 4000;  // Jaký je počáteční interval nyan cat. Mění se s obtížností. Hodnota inicializace 4000 k ničemu není.
        const int speedChangeTime = 30;     // Po jaké době se nyan cat zrychlí (v sekundách).
        const int speedChangeInterval = 25;     // O kolik se zmenší interval Timeru timerSnake (nyan cat se tak zrychlí).



        // Nyan Cat obrázky (50; 50):
        static Image doprava = Properties.Resources.nyan_cat_doprava;
        static Image doleva = Properties.Resources.nyan_cat_doleva;
        static Image nahoruZleva = Properties.Resources.nyan_cat_nahoru_zleva;
        static Image nahoruZprava = Properties.Resources.nyan_cat_nahoru_zprava;
        static Image dolůZleva = Properties.Resources.nyan_cat_dolů_zleva;
        static Image dolůZprava = Properties.Resources.nyan_cat_dolů_zprava;

        // Enemy obrázky (50; 50):
        static Image enemyDoprava = Properties.Resources.EnemyDoprava;
        static Image enemyDoleva = Properties.Resources.EnemyDoleva;
        static Image enemyNahoruZleva = Properties.Resources.EnemyNahoruZleva;
        static Image enemyNahoruZprava = Properties.Resources.EnemyNahoruZprava;
        static Image enemyDolůZleva = Properties.Resources.EnemyDolůZleva;
        static Image enemyDolůZprava = Properties.Resources.EnemyDolůZprava;

        // Jídlo obrázky (40; 40):
        static Image food1 = Properties.Resources.Cookie;
        static Image food2 = Properties.Resources.Dort;
        static Image food3 = Properties.Resources.Dortík;
        static Image food4 = Properties.Resources.Jahoda;
        static Image food5 = Properties.Resources.Lízátko;
        static Image food6 = Properties.Resources.Pepermint;
        static Image food7 = Properties.Resources.Zmrzlina;
        static Image food8 = Properties.Resources.Želé;

        // PowerUpy obrázky (40; 40):
        static Image magnet = Properties.Resources.Stříbrná_hvězda;
        static Image borders = Properties.Resources.Zlatá_hvězda;
        static Image reversed = Properties.Resources.Reversed;
        static Image swap = Properties.Resources.Swap;

        // Při spuštění Form2.
        private void Form2_Shown(object sender, EventArgs e)
        {
            Jidla = 0;
            Bonusu = 0;
            switch (Difficulty)
            {
                case "easy":
                    speedStartingInterval = 400;
                    pointFoodGain = 100;
                    labelDifficulty.Text = "Snadná";

                    break;
                case "medium":
                    speedStartingInterval = 275;
                    pointFoodGain = 200;
                    labelDifficulty.Text = "Střední";
                    break;
                case "hard":
                    speedStartingInterval = 150;
                    pointFoodGain = 300;
                    labelDifficulty.Text = "Těžká";
                    break;
            }
            MessageBox.Show("Vítejte ve hře Snyanke cat! Přejeme příjemnou zábavu. =)", "Snyanke cat", MessageBoxButtons.OK, MessageBoxIcon.Information);
            panelSnake.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxFood.Visible = true;

            

            timerSpeed.Interval = 1000;     // const
            timerSpeed.Enabled = true;
            timerGameTime.Interval = 1000;  // const
            timerGameTime.Enabled = true;
            timerSnake.Interval = speedStartingInterval;  // mění se s časem
            timerSnake.Enabled = true;
            timerPowerSpawn.Interval = 1000;    // const
            timerPowerSpawn.Enabled = true;

            timerPowerDespawn.Interval = 1000;  // const
            timerPowerDespawn.Enabled = false;
            timerBlink.Interval = 250;          // const
            timerBlink.Enabled = false;
            timerDuration.Interval = 10 * powerUpDurationTime;      // const
            timerDuration.Enabled = false;

            Food();
        }


        // Metody na ovládání Nyan Cat:
        // Metoda pro pohyb nahoru.
        public void Nahoru()
        {
            if (!powerUpreversed)
            {
                direction = "up"; 
            }
            else
            {
                direction = "down";
            }
        }

        // Metoda pro pohyb dolů.
        public void Dolů()
        {
            if (!powerUpreversed)
            {
                direction = "down";              
            }
            else
            {
                direction = "up";              
            }
        }

        // Metoda pro pohyb doleva.
        public void Doleva()
        {
            if (!powerUpreversed)
            {
                direction = "left";
                zleva = true;
            }
            else
            {
                direction = "right";
                zleva = false;
            }            
        }

        // Metoda pro pohyb doprava.
        public void Doprava()
        {
            if (!powerUpreversed)
            {
                direction = "right";
                zleva = false;
            }
            else
            {
                direction = "left";
                zleva = true;
            }
        }

        // Pro fungování šipek.
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                case Keys.W:
                    Nahoru();
                    return true;
                case Keys.Down:
                case Keys.S:
                    Dolů();
                    return true;
                case Keys.Left:
                case Keys.A:
                    Doleva();
                    return true;
                case Keys.Right:
                case Keys.D:
                    Doprava();
                    return true;
            }
            return false;
        }

        // Smoother movement.
        private void Form2_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;     // pro lepší plynulost
        }

        // Mačkání šipek na klávesnici a „e.Handeled = true;“.
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;       // Možná ani není třeba.
        }

        // Umožňuje používat šipky.
        private void Form2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        // Tlačítka:
        // Po potvrzení ukončí program.
        private void buttonKonec_Click(object sender, EventArgs e)
        {           
            bool[] inf = new bool[7]; // Technické informační pole pro správné pokračování timerů.
            // (0/1/2/3/4/5/6) GameTime/Snake/Speed/Duration/Blink/PowerSpawn/PowerDespawn
            inf[0] = timerGameTime.Enabled;
            timerGameTime.Enabled = false;  // norm: true
            inf[1] = timerSnake.Enabled;
            timerSnake.Enabled = false;     // norm: true
            inf[2] = timerSpeed.Enabled;
            timerSpeed.Enabled = false;     // norm: true
            inf[3] = timerDuration.Enabled;
            timerDuration.Enabled = false;   // norm: false
            inf[4] = timerBlink.Enabled;
            timerBlink.Enabled = false;  // norm: false
            inf[5] = timerPowerSpawn.Enabled;
            timerPowerSpawn.Enabled = false;  // norm: true
            inf[6] = timerPowerDespawn.Enabled;
            timerPowerDespawn.Enabled = false; // norm: false

            DialogResult result = MessageBox.Show("Opravdu chceš aplikaci ukončit?", "Potvrzení", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                timerGameTime.Enabled = inf[0];
                timerSnake.Enabled = inf[1];
                timerSpeed.Enabled = inf[2];
                timerDuration.Enabled = inf[3];
                timerBlink.Enabled = inf[4];
                timerPowerSpawn.Enabled = inf[5];
                timerPowerDespawn.Enabled = inf[6];
            }
        }

        // Změnit obtížnost (vyvolá Form2Úvod).
        private void buttonDifficultyChange_Click(object sender, EventArgs e)
        {
            // Vypnout timery:
            timerBlink.Enabled = false;
            timerDuration.Enabled = false;
            timerGameTime.Enabled = false;
            timerPowerDespawn.Enabled = false;
            timerPowerSpawn.Enabled = false;
            timerSnake.Enabled = false;
            timerSpeed.Enabled = false;

            Form2Úvod úvod = new Form2Úvod();       // Nutné, aby nebylo static (došlo by k StackOverflowException).
            úvod.Closed += (s, args) => this.Close();   // Stačilo by jednou, podobně jako proměnná k ve Form2Úvod.cs.
            this.Hide();
            úvod.Show();
        }

        // Tlačítko nahoru.
        private void buttonUp_Click(object sender, EventArgs e)
        {
            Nahoru();
        }

        // Tlačítko dolů.
        private void buttonDown_Click(object sender, EventArgs e)
        {
            Dolů();
        }

        // Tlačítko doleva.
        private void buttonLeft_Click(object sender, EventArgs e)
        {
            Doleva();
        }

        // Tlačítko doprava.
        private void buttonRight_Click(object sender, EventArgs e)
        {
            Doprava();
        }      


        // Ovládá pohyb jídla při spuštěném magnetu.
        public void Magnet(int x, int y)
        {
            if ((x + pictureBoxFood.Width) <= pictureBoxNyanCat.Location.X)
            {
                x += stepMagnet;
            }
            else if (x >= (pictureBoxNyanCat.Location.X + pictureBoxNyanCat.Width))
            {
                x -= stepMagnet;
            }

            if ((y + pictureBoxFood.Height) <= pictureBoxNyanCat.Location.Y)
            {
                y += stepMagnet;
            }
            else if (y >= (pictureBoxNyanCat.Location.Y + pictureBoxNyanCat.Height))
            {
                y -= stepMagnet;
            }

            pictureBoxFood.Location = new Point(x, y);
        }

        // Tvoří jídlo.
        public void Food()
        {
            foodNum = random.Next(1, 9);

            do
            {
                foodX = random.Next(-2, 449);    // Původně u obou 461. Chci, aby bonus Swap (Výměna) nemohl hráče nikdy zabít pouhým vyměněním.
                foodY = random.Next(-2, 449);    // Lepší asi bylo udělat konstantu, ale teď už se to nevyplatí.
            }
            while ((foodX + pictureBoxFood.Width) >= pictureBoxNyanCat.Location.X && foodX <= (pictureBoxNyanCat.Location.X + pictureBoxNyanCat.Width) && (foodY + pictureBoxFood.Height) >= pictureBoxNyanCat.Location.Y && foodY <= (pictureBoxNyanCat.Location.Y + pictureBoxNyanCat.Height));

            switch (foodNum)
            {
                case 1:
                    foodImage = food1;
                    break;
                case 2:
                    foodImage = food2;
                    break;
                case 3:
                    foodImage = food3;
                    break;
                case 4:
                    foodImage = food4;
                    break;
                case 5:
                    foodImage = food5;
                    break;
                case 6:
                    foodImage = food6;
                    break;
                case 7:
                    foodImage = food7;
                    break;
                case 8:
                    foodImage = food8;
                    break;
            }

            pictureBoxFood.Image = foodImage;
            pictureBoxFood.Location = new Point(foodX, foodY);
        }
        
        // Metoda, která zajišťuje mizení snězených a objevování nových jídel.
        public void Ham()
        {
            if (((pictureBoxNyanCat.Location.X + pictureBoxNyanCat.Width) > pictureBoxFood.Location.X && pictureBoxNyanCat.Location.X < (pictureBoxFood.Location.X + pictureBoxFood.Width)) && ((pictureBoxNyanCat.Location.Y + pictureBoxNyanCat.Height) > pictureBoxFood.Location.Y && pictureBoxNyanCat.Location.Y < (pictureBoxFood.Location.Y + pictureBoxFood.Height)))
            {
                points += pointFoodGain;
                labelSkóre.Text = points.ToString();
                //Score++;  Bylo by mnohem lepší, kdyby neexistovala proměnná points (nechci ale nic rozbít). :x
                Jidla++;
                Food();
            }
        }

        // Metoda, která vyvolává buffy a debuffy.
        public void PowerUp()
        {
            powerUpNum = random.Next(1, 5);     // 4 powerUpy

            switch (powerUpNum)
            {
                case 1:
                    pictureBoxPower.Image = magnet;
                    break;
                case 2:
                    pictureBoxPower.Image = borders;
                    break;
                case 3:
                    pictureBoxPower.Image = reversed;
                    break;
                case 4:
                    pictureBoxPower.Image = swap;
                    break;
            }

            do
            {
                powerX = random.Next(0, 461);
                powerY = random.Next(0, 461);
            }
            while ((powerX + pictureBoxPower.Width) >= pictureBoxNyanCat.Location.X && powerX <= (pictureBoxNyanCat.Location.X + pictureBoxNyanCat.Width) && (powerY + pictureBoxPower.Height) >= pictureBoxNyanCat.Location.Y && powerY <= (pictureBoxNyanCat.Location.Y + pictureBoxNyanCat.Height));

            pictureBoxPower.Location = new Point(powerX, powerY);
            pictureBoxPower.Visible = true;
            powerSpawned = true;
        }

        // Zajišťuje zmizení buffu / debuffu, spouští jeho efekt a zapíná progress box.
        public void HamPower()
        {
            if (((pictureBoxNyanCat.Location.X + pictureBoxNyanCat.Width) > pictureBoxPower.Location.X && pictureBoxNyanCat.Location.X < (pictureBoxPower.Location.X + pictureBoxPower.Width)) && ((pictureBoxNyanCat.Location.Y + pictureBoxNyanCat.Height) > pictureBoxPower.Location.Y && pictureBoxNyanCat.Location.Y < (pictureBoxPower.Location.Y + pictureBoxPower.Height)))      // zkontrolovat a opravit
            {
                Bonusu++;
                powerTaken = true;
                pictureBoxPower.Visible = false;
                powerSpawned = false;
                progressBar.Value = 100;            // Kdyby se vzalo víc PowerUpů naráz, tak ten další plně zruší ten předchozí a bude mít plnou Duration.
                timerDuration.Interval = 10 * powerUpDurationTime;
                timerDuration.Enabled = true;

                if (pictureBoxPower.Image == magnet)
                {
                    powerUpMagnet = true;
                    powerUpBorders = false;
                    powerUpreversed = false;
                    pictureBoxPowerTaken.Image = magnet;
                    labelPowerTakenPopis.Location = new Point (389, 569);
                    labelPowerTakenPopis.Text = "Magnetické jídlo";
                    points += 3 * pointFoodGain / 2;    // *1,5
                    labelSkóre.Text = points.ToString();
                }
                else if (pictureBoxPower.Image == borders)
                {
                    panelSnake.BorderStyle = BorderStyle.Fixed3D;
                    powerUpMagnet = false;
                    powerUpBorders = true;
                    powerUpreversed = false;
                    pictureBoxPowerTaken.Image = borders;
                    labelPowerTakenPopis.Location = new Point(381, 569);
                    labelPowerTakenPopis.Text = "Otevřené kraje";
                    points += 2 * pointFoodGain;    // *2.0
                    labelSkóre.Text = points.ToString();
                }
                else if (pictureBoxPower.Image == reversed)
                {
                    powerUpMagnet = false;
                    powerUpBorders = false;
                    powerUpreversed = true;
                    pictureBoxPowerTaken.Image = reversed;
                    labelPowerTakenPopis.Location = new Point(380, 569);
                    labelPowerTakenPopis.Text = "Opačné ovládání";
                    points += 3 * pointFoodGain;    // *3.0
                    labelSkóre.Text = points.ToString();
                }
                else if (pictureBoxPower.Image == swap)
                {
                    powerUpMagnet = false;
                    powerUpBorders = false;
                    powerUpreversed = false;
                    pictureBoxPowerTaken.Image = swap;
                    labelPowerTakenPopis.Location = new Point(419, 569);
                    labelPowerTakenPopis.Text = "Výměna";
                    points += 2 * pointFoodGain;    // *2.0
                    labelSkóre.Text = points.ToString();
                    timerDuration.Interval = 1000;  // symbolické trvání 1 sekundy
                    progressBar.Value = 0;  // s prázdným progressBarem

                    int zbytekX = (pictureBoxFood.Location.X + 2) % 25;
                    int zbytekY = (pictureBoxFood.Location.Y +2) % 25;
                    int newSnakeX;
                    int newSnakeY;

                    // Snaynke cat nesmí vypadnout s pomyslné čtvercové mřížky o straně 1 čtverečku 25 pixelů a krajích -2 a 448.
                    if (zbytekX <= 12)  // 0-24: průměr je 11,5.
                    {
                        newSnakeX = pictureBoxFood.Location.X - zbytekX;    // Posune se na levé políčko mřížky.
                    }
                    else
                    {
                        newSnakeX = pictureBoxFood.Location.X + 25 - zbytekX;   // Posune se na pravé políčko mřížky.
                    }

                    if (zbytekY <= 12)  // 0-24: průměr je 11,5.
                    {
                        newSnakeY = pictureBoxFood.Location.Y - zbytekY;    // Posune se na horní políčko mřížky.
                    }
                    else
                    {
                        newSnakeY = pictureBoxFood.Location.Y + 25 - zbytekY;   // Posune se na dolní políčko mřížky.
                    }

                    foodX = pictureBoxNyanCat.Location.X;
                    foodY = pictureBoxNyanCat.Location.Y;
                    pictureBoxNyanCat.Location = new Point(newSnakeX, newSnakeY);
                    pictureBoxFood.Location = new Point(foodX, foodY);
                }
                pictureBoxPowerTaken.Visible = true;
                labelPowerTakenPopis.Visible = true;
                progressBar.Visible = true;
            }
        }

        // Vyzkouší, jestli náhodou Výsledky hledání Tackenayns nechytil Snyanke cat.
        private bool ChycenNepritelem()
        {
            if (((pictureBoxNyanCat.Location.X + pictureBoxNyanCat.Width) > pictureBoxEnemy.Location.X && pictureBoxNyanCat.Location.X < (pictureBoxEnemy.Location.X + pictureBoxEnemy.Width)) && ((pictureBoxNyanCat.Location.Y + pictureBoxNyanCat.Height) > pictureBoxEnemy.Location.Y && pictureBoxNyanCat.Location.Y < (pictureBoxEnemy.Location.Y + pictureBoxEnemy.Height)))           
                return true;
            else
                return false;
        }

        // Kontroluje, jestli Nyan cat nenabourala do zdi.
        public void Kontrola()
        {
            if (ChycenNepritelem())
            {
                timerSnake.Enabled = false;
                timerGameTime.Enabled = false;
                timerPowerSpawn.Enabled = false;
                timerPowerDespawn.Enabled = false;
                timerBlink.Enabled = false;
                timerDuration.Enabled = false;

                MessageBox.Show("          PROHRÁLI JSTE. \n " + "Nyan cat byla chycena Tackenaynsem. ", "Duha šedne", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormScoreSubmiting save = new FormScoreSubmiting();
                save.Closed += (s, args) => this.Close();
                this.Hide();

                // Jídla a bonusy už jsou načtené v Jidla a Bonusu.

                // Poznámka: Kdybych to dělal od znovu, tak by proměnné gameTimesSec, gameTimeMin a points neexistovaly. Nahradil bych je těmi vlastnostmi přímo.
                // Poznámka2: Vzhledem k tomu, že už vím, jak přesně funguje vytváření instancí tříd v pozadí, pravděpodobně bych si dobře rozmyslel příští skládání proměnných na začátek formuláře do meziprostoru.
                //            V každém případě vlastnosti nefungovaly, protože se inicializovaly na 0, a tak vytvářím veřejné pole info na transport informací.
                info[0] = points;
                info[1] = Jidla;
                info[2] = Bonusu;
                info[3] = gameTimeMin;
                info[4] = gameTimeSec;
                // Switch.
                if (Difficulty == "easy")
                {
                    info[5] = 1;
                }
                else if (Difficulty == "medium")
                {

                    info[5] = 2;
                }
                else
                {
                    info[5] = 3;
                }

                save.Show();
            }
            else if (!powerUpBorders)
            {
                if (pictureBoxNyanCat.Location.X < -2 || pictureBoxNyanCat.Location.X > 448 || pictureBoxNyanCat.Location.Y < -2 || pictureBoxNyanCat.Location.Y > 448)
                {
                    timerSnake.Enabled = false;
                    timerGameTime.Enabled = false;
                    timerPowerSpawn.Enabled = false;
                    timerPowerDespawn.Enabled = false;
                    timerBlink.Enabled = false;
                    timerDuration.Enabled = false;

                    MessageBox.Show("          PROHRÁLI JSTE. \n " + "Nyan cat narazila do stěny. ", "Kočka na měkko", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FormScoreSubmiting save = new FormScoreSubmiting();
                    save.Closed += (s, args) => this.Close();
                    this.Hide();

                    // Jídla a bonusy už jsou načtené v Jidla a Bonusu.

                    // Poznámka: Kdybych to dělal od znovu, tak by proměnné gameTimesSec, gameTimeMin a points neexistovaly. Nahradil bych je těmi vlastnostmi přímo.
                    // Poznámka2: Vzhledem k tomu, že už vím, jak přesně funguje vytváření instancí tříd v pozadí, pravděpodobně bych si dobře rozmyslel příští skládání proměnných na začátek formuláře do meziprostoru.
                    //            V každém případě vlastnosti nefungovaly, protože se inicializovaly na 0, a tak vytvářím veřejné pole info na transport informací.
                    info[0] = points;
                    info[1] = Jidla;
                    info[2] = Bonusu;
                    info[3] = gameTimeMin;
                    info[4] = gameTimeSec;
                    // Switch.
                    if (Difficulty == "easy")
                    {
                        info[5] = 1;
                    }
                    else if (Difficulty == "medium")
                    {

                        info[5] = 2;
                    }
                    else
                    {
                        info[5] = 3;
                    }
                    
                    save.Show();
                }
            }
            else
            {
                if ((pictureBoxNyanCat.Location.X + pictureBoxNyanCat.Width) < -27)
                {
                    pictureBoxNyanCat.Location = new Point(473, pictureBoxNyanCat.Location.Y);
                }
                else if (pictureBoxNyanCat.Location.X > 473)
                {
                    pictureBoxNyanCat.Location = new Point(-27 - pictureBoxNyanCat.Width, pictureBoxNyanCat.Location.Y);
                }

                if ((pictureBoxNyanCat.Location.Y + pictureBoxNyanCat.Height) < -27)
                {
                    pictureBoxNyanCat.Location = new Point(pictureBoxNyanCat.Location.X, 473);
                }
                else if (pictureBoxNyanCat.Location.Y > 473)
                {
                    pictureBoxNyanCat.Location = new Point(pictureBoxNyanCat.Location.X, -27 - pictureBoxNyanCat.Height);
                }
            }
        }

        // Spustí hru od začátku.
        public void Restart()   // Poznámka: Dříve bylo ve hře přímo tlačítko na restartování. Nyní se už toho využívá pouze při spuštění a při změně obtížnosti.
        {
            // Timery už jsou zastavené.

            gameTime = 0;
            gameTimePower = 0;
            powerUpSpawnDelay = 0;
            gameTimeSec = 0;
            gameTimeMin = 0;
            points = 0;
            Jidla = 0;
            Bonusu = 0;
            labelSkóre.Text = points.ToString();
            labelGameTime.Text = "00:00";
            panelSnake.BorderStyle = BorderStyle.FixedSingle;

            switch (Difficulty)
            {
                case "easy":
                    speedStartingInterval = 400;
                    pointFoodGain = 100;
                    labelDifficulty.Text = "Snadná";
                    break;
                case "medium":
                    speedStartingInterval = 275;
                    pointFoodGain = 200;
                    labelDifficulty.Text = "Střední";
                    break;
                case "hard":
                    speedStartingInterval = 150;
                    pointFoodGain = 300;
                    labelDifficulty.Text = "Těžká";
                    break;
            }

            powerUpNum = 0;
            powerTaken = false;
            powerSpawned = false;
            pictureBoxPower.Visible = false;
            pictureBoxPowerTaken.Visible = false;
            labelPowerTakenPopis.Visible = false;
            progressBar.Visible = false;
            progressBar.Value = 100;
            blink = false;

            pictureBoxNyanCat.Image = doprava;
            pictureBoxNyanCat.Location = snakeStartingLocation;
            zleva = false;
            direction = "right";

            pictureBoxEnemy.Image = enemyDoprava;
            pictureBoxEnemy.Location = enemyStartingLocation;
            enemyZleva = false;

            powerUpBorders = false;
            powerUpMagnet = false;
            powerUpreversed = false;

            // True restarting:
            Food();

            timerPowerDespawn.Interval = 1000;
            timerPowerDespawn.Enabled = false;
            timerBlink.Interval = 250;
            timerBlink.Enabled = false;
            timerDuration.Interval = 10 * powerUpDurationTime;
            timerDuration.Enabled = false;

            timerSpeed.Interval = 1000;
            timerSpeed.Enabled = true;
            timerGameTime.Interval = 1000;
            timerGameTime.Enabled = true;
            timerSnake.Interval = speedStartingInterval;      // mění se v průběhu hry
            timerSnake.Enabled = true;
            timerPowerSpawn.Interval = 1000;
            timerPowerSpawn.Enabled = true;
        }

        private void PohybNepritele()
        {
            // Pohyb nepritele.
            int x = pictureBoxEnemy.Location.X;
            int y = pictureBoxEnemy.Location.Y;
            bool changed = false;

            if ((x + pictureBoxEnemy.Width) <= pictureBoxNyanCat.Location.X)
            {
                x += 2*stepMagnet;
                pictureBoxEnemy.Image = enemyDoprava;
                enemyZleva = false;
                changed = true;
            }
            else if (x >= (pictureBoxNyanCat.Location.X + pictureBoxNyanCat.Width))
            {
                x -= 2*stepMagnet;
                pictureBoxEnemy.Image = enemyDoleva;
                enemyZleva = true;
                changed = true;
            }

            if ((y + pictureBoxEnemy.Height) <= pictureBoxNyanCat.Location.Y)
            {
                y += 2*stepMagnet;

                if (!changed)
                {
                    if (enemyZleva)
                        pictureBoxEnemy.Image = enemyDolůZleva;
                    else
                        pictureBoxEnemy.Image = enemyDolůZprava;

                    // changed = true;
                }
            }
            else if (y >= (pictureBoxNyanCat.Location.Y + pictureBoxNyanCat.Height))
            {
                y -= 2*stepMagnet;

                if (!changed)
                {
                    if (enemyZleva)
                        pictureBoxEnemy.Image = enemyNahoruZleva;
                    else
                        pictureBoxEnemy.Image = enemyNahoruZprava;

                    // changed = true;
                }
            }

            pictureBoxEnemy.Location = new Point(x, y);
        }

        // Timer hry Snake (zajišťuje pohyb nyan cat).
        private void timerSnake_Tick(object sender, EventArgs e)
        {
            switch (direction)  // Pohyb
            {
                case "up":
                    if (zleva)
                    {
                        pictureBoxNyanCat.Image = nahoruZleva;
                    }
                    else
                    {
                        pictureBoxNyanCat.Image = nahoruZprava;
                    }
                    pictureBoxNyanCat.Location = new Point(pictureBoxNyanCat.Location.X, pictureBoxNyanCat.Location.Y - stepSnake);                   
                    break;
                case "down":
                    if (zleva)
                    {
                        pictureBoxNyanCat.Image = dolůZleva;
                    }
                    else
                    {
                        pictureBoxNyanCat.Image = dolůZprava;
                    }
                    pictureBoxNyanCat.Location = new Point(pictureBoxNyanCat.Location.X, pictureBoxNyanCat.Location.Y + stepSnake);
                    break;

                case "left":
                    pictureBoxNyanCat.Image = doleva;
                    pictureBoxNyanCat.Location = new Point(pictureBoxNyanCat.Location.X - stepSnake, pictureBoxNyanCat.Location.Y);
                    break;

                case "right":
                    pictureBoxNyanCat.Image = doprava;
                    pictureBoxNyanCat.Location = new Point(pictureBoxNyanCat.Location.X + stepSnake, pictureBoxNyanCat.Location.Y);
                    break;                    
            }

            PohybNepritele();

            if (powerUpMagnet)  // Pohyb jídla (pouze s magnetem)
            {
                Magnet(pictureBoxFood.Location.X, pictureBoxFood.Location.Y);
            }

            Ham();          // Snědla nějaké jídlo?
            if (powerSpawned)
            {
                HamPower(); // Snědla nějaký PowerUp?
            }
            Kontrola();     // Nenabourala? (Popř. powerUpBorders)  
        }

        // Timer, který zajišťuje postupné zrychlování nyan cat.
        private void timerSpeed_Tick(object sender, EventArgs e)
        {
            gameTime++;     // celkový čas v sekundách
            /* if (labelGameTime.ForeColor == Color.Red)       
            {
                labelGameTime.ForeColor = SystemColors.ControlText; // Zpětná změna na černou.
            }*/

            if (gameTime % speedChangeTime == 0)
            {
                if (timerSnake.Interval > (maxSpeed + speedChangeInterval))
                {
                    timerSnake.Interval -= speedChangeInterval;
                    //labelGameTime.ForeColor = Color.Red;    // Při změně rychlosti čas zčervená na 1 sekundu.
                }
                else
                {
                    gameTime = 0;
                    timerSpeed.Enabled = false;
                }
            }
        }

        // Ukazuje, jak dlouho hra trvá.
        private void timerGameTime_Tick(object sender, EventArgs e)
        {
            // GameTime:
            gameTimeSec++;     // sekundy
            if (gameTimeSec >= 60)  //  šlo udělat líp
            {
                gameTimeSec = 0;
                gameTimeMin++;  // minuty
            }

            if (gameTimeMin < 10)
            {
                if (gameTimeSec < 10)
                {
                    labelGameTime.Text = "0" + gameTimeMin.ToString() + ":0" + gameTimeSec.ToString();
                }
                else
                {
                    labelGameTime.Text = "0" + gameTimeMin.ToString() + ":" + gameTimeSec.ToString();
                }
            }
            else
            {
                if (gameTimeSec < 10)
                {
                    labelGameTime.Text = gameTimeMin.ToString() + ":0" + gameTimeSec.ToString();
                }
                else
                {
                    labelGameTime.Text = gameTimeMin.ToString() + ":" + gameTimeSec.ToString();
                }
            }
        }       


        // Timer, který spawnuje PowerUpy.
        private void timerPowerSpawn_Tick(object sender, EventArgs e)
        {
            if (powerUpSpawnDelay == 0)
            {
                gameTimePower++;
                if (gameTimePower % powerUpMinSpawnTime == 0)        // Nutné toto pořadí (nebude se randomovat na začátku hry.)
                {
                    powerUpSpawnDelay = random.Next(1, powerUpMaxSpawnTime - powerUpMinSpawnTime + 2);     // od 0 do 10 sekund delaye.
                }
            }
            else if (powerUpSpawnDelay == 1)
            {
                PowerUp();      // už obsahuje powerUpSpawned = true;
                gameTimePower = 0;
                powerUpSpawnDelay = 0;
                timerPowerSpawn.Enabled = false;
                timerPowerDespawn.Enabled = true;
            }
            else
            {
                powerUpSpawnDelay--;
            }
        }

        // Timer, který despawnuje PowerUpy na mapě po určité době.
        private void timerPowerDespawn_Tick(object sender, EventArgs e)
        {
            if (!powerTaken)
            {
                gameTimePower++;

                if (gameTimePower >= (powerUpDespawnTime - powerUpBlinkTime))
                {
                    gameTimePower = 0;
                    timerPowerDespawn.Enabled = false;
                    timerBlink.Enabled = true;
                }
            }
            else    // Proběhla událost HamPower().
            {
                gameTimePower = 0;
                timerPowerDespawn.Enabled = false;
            }
        }

        // Timer, který zajišťuje blikání powerUpů při vyprchávání.
        private void timerBlink_Tick(object sender, EventArgs e)
        {
            if (!powerTaken)
            {
                gameTimePower++;

                if (!blink)  // Pro 250 interval.
                {
                    pictureBoxPower.Visible = true;
                    blink = true;
                }
                else
                {
                    pictureBoxPower.Visible = false;
                    blink = false;
                }

                if (gameTimePower >= ((1000 / timerBlink.Interval) * powerUpBlinkTime))      // nebo >? Otestovat! (myslím, že >=)
                {
                    gameTimePower = 0;
                    pictureBoxPower.Visible = false;
                    powerSpawned = false;
                    timerBlink.Enabled = false;
                    timerPowerSpawn.Enabled = true;
                }
            }
            else   // Proběhla událost HamPower().
            {
                gameTimePower = 0;
                timerBlink.Enabled = false;
            }

            // Interval je 250 (zkouška), jinak dát 500.
        }

        // Timer, který dává pozor na vypršení sebraného PowerUpu. Je propojen s ProgressBarem.
        private void timerDuration_Tick(object sender, EventArgs e)
        {
            if (progressBar.Value > 0)
            {
                progressBar.Value--;
            }
            else
            {
                if (powerUpMagnet)
                {
                    powerUpMagnet = false;
                }
                else if (powerUpBorders)        // Po skončení případně nyan cat vyplyvne z borderů (radši nechávám větší mezeru a měním směr).
                {
                    if (pictureBoxNyanCat.Location.X < -2) // Původně -27.
                    {
                        pictureBoxNyanCat.Location = new Point(23, pictureBoxNyanCat.Location.Y);
                        direction = "right";
                        zleva = false;
                    }
                    else if ((pictureBoxNyanCat.Location.X + pictureBoxNyanCat.Width) > 448)    // Původně 473.
                    {
                        pictureBoxNyanCat.Location = new Point(423 - pictureBoxNyanCat.Width, pictureBoxNyanCat.Location.Y);
                        direction = "left";
                        zleva = true;
                    }

                    if (pictureBoxNyanCat.Location.Y < -2) // Původně -27.
                    {
                        pictureBoxNyanCat.Location = new Point(pictureBoxNyanCat.Location.X, 23);
                        direction = "down";
                    }
                    else if ((pictureBoxNyanCat.Location.Y + pictureBoxNyanCat.Height) > 448)   // Původně 473.
                    {
                        pictureBoxNyanCat.Location = new Point(pictureBoxNyanCat.Location.X, 423 - pictureBoxNyanCat.Height);
                        direction = "up";
                    }

                    powerUpBorders = false;
                    panelSnake.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (powerUpreversed)   // if nechávám pouze pro přehlednost, rychlost programu se (téměř) nezmění
                {
                    powerUpreversed = false;
                }

                pictureBoxPowerTaken.Visible = false;
                labelPowerTakenPopis.Visible = false;
                progressBar.Visible = false;
                powerTaken = false;
                timerDuration.Enabled = false;
                timerPowerSpawn.Enabled = true;
            }
        }
    }
}
