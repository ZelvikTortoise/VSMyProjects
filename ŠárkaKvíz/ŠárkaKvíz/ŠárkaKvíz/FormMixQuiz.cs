using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ŠárkaKvíz
{
    public partial class FormMixQuiz : Form
    {
        public FormMixQuiz()
        {
            InitializeComponent();

            this.FormClosed +=
           new System.Windows.Forms.FormClosedEventHandler(this.FormMixQuiz_FormClosed);
        }

        // Proměnné, které označují indexy vylosované věci, tlačítka a vylosovaných osobností.
        int indexVěc;
        int indexButton;
        int indexButtonCorrect;
        int indexOsobnostCorrect;
        int indexOsobnostWrong;


        // Metoda, která se provede při zmáčknutí správného tlačítka.
        public void Správně()
        {
            FormWelcoming.skóre += FormWelcoming.přidat;
            FormWelcoming.správných++;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            buttonZměna.Enabled = false;
            buttonKonec.Enabled = false;
            timer.Enabled = true;
            labelResult.Text = "Správně!";
            labelResult.ForeColor = Color.Green;
            labelResult.Visible = true;
            labelSkóre.Text = FormWelcoming.skóre.ToString();
        }

        // Metoda, která se provede při zmáčknutí špatného tlačítka.
        public void Špatně()
        {
            // Přidat indikaci "ŠPATNÁ ODPOVĚĎ"
           
            FormWelcoming.skóre -= FormWelcoming.odebrat;
            FormWelcoming.špatných++;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            buttonZměna.Enabled = false;
            buttonKonec.Enabled = false;
            timer.Enabled = true;
            labelResult.Text = "Špatně...";
            labelResult.ForeColor = Color.DarkRed;
            labelResult.Visible = true;
            labelSkóre.Text = FormWelcoming.skóre.ToString();
        }

        // Timer. (Interval je 1000 ms.)
        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            labelResult.Visible = false;
            Rozlosuj();
        }      

        // Metoda VytvořSeznamy().
        public void VytvořSeznamy()
        {
            FormWelcoming.seznamOsobnostCorrect.Clear();
            FormWelcoming.seznamOsobnostWrong.Clear();

            // !!!Je nutné, aby u každé věci byly aspoň 2 osobnosti s Resultem false a aspoň 1 s Resultem true!!!
            switch (FormWelcoming.seznamVěcNový[indexVěc].Name)    // Switch, který vytvoří podmínky pro vytvoření seznamů seznamOsobnostCorrect a seznamOsobnostWrong.
            {
                case "Cigarety":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = true;
                    FormWelcoming.claire.Result = false;
                    break;

                case "Knížky":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = true;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;

                case "Minecraft":
                    FormWelcoming.share.Result = true;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = true;
                    FormWelcoming.juliet.Result = true;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = true;
                    break;

                case "Panda":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = true;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;

                case "Nemá brýle":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = true;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Hauština":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = true;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Koně":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = true;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = true;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Lama":
                    FormWelcoming.share.Result = true;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Posilování":
                    FormWelcoming.share.Result = true;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = true;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = true;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = true;
                    break;
                case "Brzké vstávání":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = true;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Lak na nehty":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = true;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = true;
                    break;
                case "Šaty":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = true;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = true;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Čajík o páté":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = true;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = true;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Béžová barva":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = true;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Snapback":
                    FormWelcoming.share.Result = true;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = true;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Síťované oblečení":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = true;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Šňupací tabák":
                     FormWelcoming.share.Result = true;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = true;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = true;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = true;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Statek":
                    FormWelcoming.share.Result = true;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = true;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Bro":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = true;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Plyšáci":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = true;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = true;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = true;
                    break;
                case "Práce":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = true;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Úklid":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = true;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Lenost":
                    FormWelcoming.share.Result = true;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Abstinent":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = true;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = true;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = true;
                    break;
                case "The Sims":
                    FormWelcoming.share.Result = true;
                    FormWelcoming.jenny.Result = true;
                    FormWelcoming.jaykie.Result = true;
                    FormWelcoming.juliet.Result = true;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = true;
                    break;
                case "Chrání přírodu":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = true;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Pan Meloun":
                    FormWelcoming.share.Result = true;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Bílá barva":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = true;
                    break;
                case "Pinkie Pie":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = true;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = true;
                    break;
                case "Hudba":
                    FormWelcoming.share.Result = true;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = true;
                    FormWelcoming.juliet.Result = true;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = true;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = true;
                    break;
                case "Černá barva":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = true;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Fialová barva":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = true;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Růžová barva":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = true;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Pennyboard":
                    FormWelcoming.share.Result = true;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = true;
                    FormWelcoming.juliet.Result = true;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = true;
                    FormWelcoming.claire.Result = true;
                    break;
                case "Longboard":
                    FormWelcoming.share.Result = true;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = true;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = true;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = true;
                    break;
                case "Plavání":
                    FormWelcoming.share.Result = true;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = true;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = true;
                    FormWelcoming.amber.Result = true;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Databáze Max":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = true;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Cimbálka":
                    FormWelcoming.share.Result = true;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = true;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = true;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Neumí zpívat":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = true;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Khaki barva":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = true;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Asociál":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = true;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Asexuál":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = true;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Homosexuál":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = true;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Bisexuál":
                    FormWelcoming.share.Result = true;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Častý smutek":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = true;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Levandule":
                    FormWelcoming.share.Result = true;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Kočky":
                    FormWelcoming.share.Result = true;
                    FormWelcoming.jenny.Result = true;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = true;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = true;
                    FormWelcoming.emilie.Result = true;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = true;
                    break;
                case "Bubble bum":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = true;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Mléko":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = true;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Tiger":
                    FormWelcoming.share.Result = true;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = true;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Holo":
                    FormWelcoming.share.Result = true;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Deník":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = true;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Věší se za nohu":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = true;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Pičovina":
                    FormWelcoming.share.Result = true;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Sobecká mrdka":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = true;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Auta":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = true;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                case "Buldozer":
                    FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = true;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                    break;
                    // zde pokračuj s Editem:
                    // case "...":
                /*
                FormWelcoming.share.Result = false;
                    FormWelcoming.jenny.Result = false;
                    FormWelcoming.jaykie.Result = false;
                    FormWelcoming.juliet.Result = false;
                    FormWelcoming.bell.Result = false;
                    FormWelcoming.bow.Result = false;
                    FormWelcoming.emilie.Result = false;
                    FormWelcoming.amber.Result = false;
                    FormWelcoming.claire.Result = false;
                 */
                // break;
            }

            foreach (Osobnost osobnost in FormWelcoming.seznamOsobnost)      // Cyklus foreach, který naplní seznamy podle boolovské hodnoty osobnost.Result.
            {
                if (osobnost.Result)
                {
                    FormWelcoming.seznamOsobnostCorrect.Add(osobnost);
                }
                else
                {
                    FormWelcoming.seznamOsobnostWrong.Add(osobnost);
                }
            }
        }

        // Metoda Rozlosuj().
        public void Rozlosuj()
        {           
            if (FormWelcoming.seznamVěcNový.Count == 0)        // Kdyby už nebyly nepoužité věci.
            {
                MessageBox.Show("Omlouváme se, ale pro zatím jste z typu kvízu „Mix“ odpověděl(a) na všechny otázky. \n Buď si zvolte jiný typ kvízu, nebo hru vypněte a zapněte znovu.", "Hlášení", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                FormWelcoming.returnedOnce = true;
                this.Hide();
                (new FormDividing()).Show();
            }
            else        // V normálním případě.
            {
                indexVěc = FormWelcoming.random.Next(0, FormWelcoming.seznamVěcNový.Count);        //      Random index od 0 do max. v seznamu seznamVěcNový
                pictureBoxVěc.Image = FormWelcoming.seznamVěcNový[indexVěc].Image;         // Obrázek věci ze seznamu seznamVěcNový
                labelInfo.Text = "(" + FormWelcoming.seznamVěcNový[indexVěc].Name + ")";       // Text popisu věci ve formě (Věc)

                // (Aspoň 2 Wrong a aspoň 1 Correct.)
                VytvořSeznamy();

                FormWelcoming.seznamVěcNový.Remove(FormWelcoming.seznamVěcNový[indexVěc]);    // Odstraní věc ze seznamu seznamVěcNový.

                indexButton = FormWelcoming.random.Next(1, 4);
                indexOsobnostCorrect = FormWelcoming.random.Next(0, FormWelcoming.seznamOsobnostCorrect.Count);     // Correct osobnost.
                indexOsobnostWrong = FormWelcoming.random.Next(0, FormWelcoming.seznamOsobnostWrong.Count);         // Wrong osobnost 1.

                switch (indexButton)
                {
                    case 1:
                        indexButtonCorrect = 1;
                        button1.BackgroundImage = FormWelcoming.seznamOsobnostCorrect[indexOsobnostCorrect].Image;      // Correct Button
                        button1.Text = FormWelcoming.seznamOsobnostCorrect[indexOsobnostCorrect].Name;
                        button1.ForeColor = FormWelcoming.seznamOsobnostCorrect[indexOsobnostCorrect].Color;

                        button2.BackgroundImage = FormWelcoming.seznamOsobnostWrong[indexOsobnostWrong].Image;
                        button2.Text = FormWelcoming.seznamOsobnostWrong[indexOsobnostWrong].Name;
                        button2.ForeColor = FormWelcoming.seznamOsobnostWrong[indexOsobnostWrong].Color;

                        indexButton = 3;
                        break;
                    case 2:
                        button1.BackgroundImage = FormWelcoming.seznamOsobnostWrong[indexOsobnostWrong].Image;
                        button1.Text = FormWelcoming.seznamOsobnostWrong[indexOsobnostWrong].Name;
                        button1.ForeColor = FormWelcoming.seznamOsobnostWrong[indexOsobnostWrong].Color;

                        indexButtonCorrect = 2;
                        button2.BackgroundImage = FormWelcoming.seznamOsobnostCorrect[indexOsobnostCorrect].Image;      // Correct Button
                        button2.Text = FormWelcoming.seznamOsobnostCorrect[indexOsobnostCorrect].Name;
                        button2.ForeColor = FormWelcoming.seznamOsobnostCorrect[indexOsobnostCorrect].Color;

                        indexButton = 3;
                        break;
                    case 3:
                        button1.BackgroundImage = FormWelcoming.seznamOsobnostWrong[indexOsobnostWrong].Image;
                        button1.Text = FormWelcoming.seznamOsobnostWrong[indexOsobnostWrong].Name;
                        button1.ForeColor = FormWelcoming.seznamOsobnostWrong[indexOsobnostWrong].Color;

                        indexButton = 2;

                        indexButtonCorrect = 3;
                        button3.BackgroundImage = FormWelcoming.seznamOsobnostCorrect[indexOsobnostCorrect].Image;      // Correct Button
                        button3.Text = FormWelcoming.seznamOsobnostCorrect[indexOsobnostCorrect].Name;
                        button3.ForeColor = FormWelcoming.seznamOsobnostCorrect[indexOsobnostCorrect].Color;
                        break;
                }

                FormWelcoming.seznamOsobnostWrong.Remove(FormWelcoming.seznamOsobnostWrong[indexOsobnostWrong]);    // Odstraní Wrong osobnost, aby se nám nestalo, že budeme mít 2 stejné osobnosti v jedné otázce.
                indexOsobnostWrong = FormWelcoming.random.Next(0, FormWelcoming.seznamOsobnostWrong.Count);     // Wrong osobnost 2.
                if (indexButton == 3)
                {
                    button3.BackgroundImage = FormWelcoming.seznamOsobnostWrong[indexOsobnostWrong].Image;
                    button3.Text = FormWelcoming.seznamOsobnostWrong[indexOsobnostWrong].Name;
                    button3.ForeColor = FormWelcoming.seznamOsobnostWrong[indexOsobnostWrong].Color;
                }
                else
                {
                    button2.BackgroundImage = FormWelcoming.seznamOsobnostWrong[indexOsobnostWrong].Image;
                    button2.Text = FormWelcoming.seznamOsobnostWrong[indexOsobnostWrong].Name;
                    button2.ForeColor = FormWelcoming.seznamOsobnostWrong[indexOsobnostWrong].Color;
                }
            }
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            buttonZměna.Enabled = true;
            buttonKonec.Enabled = true;
        }

        // Clsoed.
        private void FormMixQuiz_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();  // jen dočasně (goto SumbmitingForm)
        }

        // Shown().
        private void FormMixQuiz_Shown(object sender, EventArgs e)
        {
            labelSkóre.Text = FormWelcoming.skóre.ToString();

           /* if (FormWelcoming.quizFirstTime)
            {
                FormWelcoming.quizFirstTime = false;
                Rozlosuj();             // Pak je bug při zmáčknutí tlačítka Zpět a novém kliknutí na tlačítko Mix (ukáže se totiž nová, nerozlosovaná instance třídy FormMixQuiz).
            }
            */
            Rozlosuj();
        }


        // Tlačítka kvízu:
        // Button1:
        private void button1_Click(object sender, EventArgs e)
        {
            if (indexButtonCorrect == 1)
            {
                Správně();
            }
            else
            {
                Špatně();
            }
            return;
        }

        // Button2:
        private void button2_Click(object sender, EventArgs e)
        {
            if (indexButtonCorrect == 2)
            {
                Správně();
            }
            else
            {
                Špatně();
            }
            return;
        }

        // Button3:
        private void button3_Click(object sender, EventArgs e)
        {
            if (indexButtonCorrect == 3)
            {
                Správně();
            }
            else
            {
                Špatně();
            }
            return;
        }
        // Konec tlačítek kvízu.



        private void buttonKonec_Click(object sender, EventArgs e)
        {
            FormWelcoming.formCode = 2;
            this.Hide();
            (new FormKonec()).Show();
        }

        private void buttonZměna_Click(object sender, EventArgs e)
        {
            this.Hide();
            (new FormDividing()).Show();
        }
    }
}
