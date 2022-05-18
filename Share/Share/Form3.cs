using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Share
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        List<Button> seznamTlačítek = new List<Button>();
        Random random = new Random();

        Image norm = Properties.Resources.Miny_norm;
        Image vict = Properties.Resources.Mini_victory;
        Image loss = Properties.Resources.Miny_loss;
        Image mine = Properties.Resources.Miny_fekálie;

        int doba = 0;
        int neoznačenéMiny = 10;

        // TabIndex znamená počet sousedních min.
        // UseMnemotic je true, pokud políčko obsahuje minu.

        // Vlastnost, která zaznamenává, jaké tlačítko myši bylo naposledy zmáčknuto (zajímá nás levé a pravé).
        public MouseButtons MouseButton
        {
            get;
            set;
        }

        // Událost MouseDown, která zjišťuje, jaké tlačítko myši bylo zmáčknuto, a ukládá tuto informaci do vlastnosti MouseButton.
        private void Form3_MouseDown(object sender, MouseEventArgs e)
        {
            MouseButton = e.Button;
        }

        // Co se stane, když hráč zmáčkne tlačítko (jak levým, tak pravým; jak číslo, tak 0, tak minu).
        public void TlačítkoKlik(Button tlačítko)
        {
            tlačítko.Enabled = false;
            // Levým: pokud číslo, tak Trefa(); pokud 0, tak NulaMin(), a to i u všech sousedních (i před rohy); pokud mina, tak Boom().
            // Pravým: pokud poprvé, tak praporek a nelze left click, pokud podruhé, tak ?, pokud potřetí, tak nic a možné left click
        }

        // Co se stane, když hráč zmáčkne levým tlačítkem minu.
        public void Boom()
        {
            timerDoba.Enabled = false;
            buttonRestart.Image = loss;
        }

        // Odstartuje lavinové mačkání vedlejších tlačítek, kde je 0 sousedních min (zavolat rekurzivně). Není potřeba dělat podmínku s minou, protože žádné sousední tlačítko minu neobsahuje.
        public void NulaMin()
        {

        }

        // Disablne tlačítko a zobrazí na něm počet sousedních min (i přes rohy).
        public void Trefa()
        {

        }

        // Vyrandomuje miny a čísla.
        public void Miny()
        {
            // Cyklus, který každému tlačítku přiřadí číslo, nebo minu.
            foreach (Button tlačítko in seznamTlačítek)
            {
                // Pak s testem optimalizovat zbytečné podmínky ve větvení.

                // Levý horní roh.
                if (seznamTlačítek.IndexOf(tlačítko) == 0)
                {
                    if (seznamTlačítek[1].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[10].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[11].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                }
                // Pravý horní roh.
                else if (seznamTlačítek.IndexOf(tlačítko) == 9)
                {
                    if (seznamTlačítek[8].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[18].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[19].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                }
                // Levý dolní roh.
                else if (seznamTlačítek.IndexOf(tlačítko) == 90)
                {
                    if (seznamTlačítek[80].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[81].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[91].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                }
                // Pravý dolní roh.
                else if (seznamTlačítek.IndexOf(tlačítko) == 99)
                {
                    if (seznamTlačítek[88].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[89].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[98].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                }
                // Horní kraj.
                else if (seznamTlačítek.IndexOf(tlačítko) < 9 && seznamTlačítek.IndexOf(tlačítko) > 0)
                {
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) - 1].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) + 1].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) + 9].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) + 10].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) + 11].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                }
                // Dolní kraj.
                else if (seznamTlačítek.IndexOf(tlačítko) > 90 && seznamTlačítek.IndexOf(tlačítko) < 99)
                {
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) - 1].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) + 1].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) - 9].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) - 10].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) - 11].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                }
                // Levý kraj.
                else if (seznamTlačítek.IndexOf(tlačítko) > 0 && seznamTlačítek.IndexOf(tlačítko) % 10 == 0 && seznamTlačítek.IndexOf(tlačítko) < 90)
                {
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) - 10].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) - 9].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) + 1].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) + 10].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) + 11].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                }
                // Pravý kraj.
                else if (seznamTlačítek.IndexOf(tlačítko) > 9 && seznamTlačítek.IndexOf(tlačítko) < 99 && (seznamTlačítek.IndexOf(tlačítko) - 9) % 10 == 0)
                {
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) - 11].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) - 10].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) - 1].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) + 9].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) + 10].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                }
                // Prostředek.
                else
                {
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) - 11].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) - 10].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) - 9].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) - 1].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) + 1].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) + 9].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) + 10].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                    if (seznamTlačítek[seznamTlačítek.IndexOf(tlačítko) + 11].UseMnemonic)
                    {
                        tlačítko.TabIndex++;
                    }
                }
            }
        }

        // Shown
        private void Form3_Shown(object sender, EventArgs e)
        {
            // Všechna tlačítka se přidají do seznamu tlačítek.
            seznamTlačítek.Add(button1);
            seznamTlačítek.Add(button2);
            seznamTlačítek.Add(button3);
            seznamTlačítek.Add(button4);
            seznamTlačítek.Add(button5);
            seznamTlačítek.Add(button6);
            seznamTlačítek.Add(button7);
            seznamTlačítek.Add(button8);
            seznamTlačítek.Add(button9);
            seznamTlačítek.Add(button10);
            seznamTlačítek.Add(button11);
            seznamTlačítek.Add(button12);
            seznamTlačítek.Add(button13);
            seznamTlačítek.Add(button14);
            seznamTlačítek.Add(button15);
            seznamTlačítek.Add(button16);
            seznamTlačítek.Add(button17);
            seznamTlačítek.Add(button18);
            seznamTlačítek.Add(button19);
            seznamTlačítek.Add(button20);
            seznamTlačítek.Add(button21);
            seznamTlačítek.Add(button22);
            seznamTlačítek.Add(button23);
            seznamTlačítek.Add(button24);
            seznamTlačítek.Add(button25);
            seznamTlačítek.Add(button26);
            seznamTlačítek.Add(button27);
            seznamTlačítek.Add(button28);
            seznamTlačítek.Add(button29);
            seznamTlačítek.Add(button30);
            seznamTlačítek.Add(button31);
            seznamTlačítek.Add(button32);
            seznamTlačítek.Add(button33);
            seznamTlačítek.Add(button34);
            seznamTlačítek.Add(button35);
            seznamTlačítek.Add(button36);
            seznamTlačítek.Add(button37);
            seznamTlačítek.Add(button38);
            seznamTlačítek.Add(button39);
            seznamTlačítek.Add(button40);
            seznamTlačítek.Add(button41);
            seznamTlačítek.Add(button42);
            seznamTlačítek.Add(button43);
            seznamTlačítek.Add(button44);
            seznamTlačítek.Add(button45);
            seznamTlačítek.Add(button46);
            seznamTlačítek.Add(button47);
            seznamTlačítek.Add(button48);
            seznamTlačítek.Add(button49);
            seznamTlačítek.Add(button50);
            seznamTlačítek.Add(button51);
            seznamTlačítek.Add(button52);
            seznamTlačítek.Add(button53);
            seznamTlačítek.Add(button54);
            seznamTlačítek.Add(button55);
            seznamTlačítek.Add(button56);
            seznamTlačítek.Add(button57);
            seznamTlačítek.Add(button58);
            seznamTlačítek.Add(button59);
            seznamTlačítek.Add(button60);
            seznamTlačítek.Add(button61);
            seznamTlačítek.Add(button62);
            seznamTlačítek.Add(button63);
            seznamTlačítek.Add(button64);
            seznamTlačítek.Add(button65);
            seznamTlačítek.Add(button66);
            seznamTlačítek.Add(button67);
            seznamTlačítek.Add(button68);
            seznamTlačítek.Add(button69);
            seznamTlačítek.Add(button70);
            seznamTlačítek.Add(button71);
            seznamTlačítek.Add(button72);
            seznamTlačítek.Add(button73);
            seznamTlačítek.Add(button74);
            seznamTlačítek.Add(button75);
            seznamTlačítek.Add(button76);
            seznamTlačítek.Add(button77);
            seznamTlačítek.Add(button78);
            seznamTlačítek.Add(button79);
            seznamTlačítek.Add(button80);
            seznamTlačítek.Add(button81);
            seznamTlačítek.Add(button82);
            seznamTlačítek.Add(button83);
            seznamTlačítek.Add(button84);
            seznamTlačítek.Add(button85);
            seznamTlačítek.Add(button86);
            seznamTlačítek.Add(button87);
            seznamTlačítek.Add(button88);
            seznamTlačítek.Add(button89);
            seznamTlačítek.Add(button90);
            seznamTlačítek.Add(button91);
            seznamTlačítek.Add(button92);
            seznamTlačítek.Add(button93);
            seznamTlačítek.Add(button94);
            seznamTlačítek.Add(button95);
            seznamTlačítek.Add(button96);
            seznamTlačítek.Add(button97);
            seznamTlačítek.Add(button98);
            seznamTlačítek.Add(button99);
            seznamTlačítek.Add(button100);

            Miny();
        }

        // Restart tlačítko.
        private void buttonRestart_Click(object sender, EventArgs e)
        {
            doba = 0;
            textBoxDoba.Text = doba.ToString();
            neoznačenéMiny = 10;
            textBoxMiny.Text = neoznačenéMiny.ToString();
            buttonRestart.Image = norm;

            button1.TabIndex = 0;
            button2.TabIndex = 0;
            button3.TabIndex = 0;
            button4.TabIndex = 0;
            button5.TabIndex = 0;
            button6.TabIndex = 0;
            button7.TabIndex = 0;
            button8.TabIndex = 0;
            button9.TabIndex = 0;
            button10.TabIndex = 0;
            button11.TabIndex = 0;
            button12.TabIndex = 0;
            button13.TabIndex = 0;
            button14.TabIndex = 0;
            button15.TabIndex = 0;
            button16.TabIndex = 0;
            button17.TabIndex = 0;
            button18.TabIndex = 0;
            button19.TabIndex = 0;
            button20.TabIndex = 0;
            button21.TabIndex = 0;
            button22.TabIndex = 0;
            button23.TabIndex = 0;
            button24.TabIndex = 0;
            button25.TabIndex = 0;
            button26.TabIndex = 0;
            button27.TabIndex = 0;
            button28.TabIndex = 0;
            button29.TabIndex = 0;
            button30.TabIndex = 0;
            button31.TabIndex = 0;
            button32.TabIndex = 0;
            button33.TabIndex = 0;
            button34.TabIndex = 0;
            button35.TabIndex = 0;
            button36.TabIndex = 0;
            button37.TabIndex = 0;
            button38.TabIndex = 0;
            button39.TabIndex = 0;
            button40.TabIndex = 0;
            button41.TabIndex = 0;
            button42.TabIndex = 0;
            button43.TabIndex = 0;
            button44.TabIndex = 0;
            button45.TabIndex = 0;
            button46.TabIndex = 0;
            button47.TabIndex = 0;
            button48.TabIndex = 0;
            button49.TabIndex = 0;
            button50.TabIndex = 0;
            button51.TabIndex = 0;
            button52.TabIndex = 0;
            button53.TabIndex = 0;
            button54.TabIndex = 0;
            button55.TabIndex = 0;
            button56.TabIndex = 0;
            button57.TabIndex = 0;
            button58.TabIndex = 0;
            button59.TabIndex = 0;
            button60.TabIndex = 0;
            button61.TabIndex = 0;
            button62.TabIndex = 0;
            button63.TabIndex = 0;
            button64.TabIndex = 0;
            button65.TabIndex = 0;
            button66.TabIndex = 0;
            button67.TabIndex = 0;
            button68.TabIndex = 0;
            button69.TabIndex = 0;
            button70.TabIndex = 0;
            button71.TabIndex = 0;
            button72.TabIndex = 0;
            button73.TabIndex = 0;
            button74.TabIndex = 0;
            button75.TabIndex = 0;
            button76.TabIndex = 0;
            button77.TabIndex = 0;
            button78.TabIndex = 0;
            button79.TabIndex = 0;
            button80.TabIndex = 0;
            button81.TabIndex = 0;
            button82.TabIndex = 0;
            button83.TabIndex = 0;
            button84.TabIndex = 0;
            button85.TabIndex = 0;
            button86.TabIndex = 0;
            button87.TabIndex = 0;
            button88.TabIndex = 0;
            button89.TabIndex = 0;
            button90.TabIndex = 0;
            button91.TabIndex = 0;
            button92.TabIndex = 0;
            button93.TabIndex = 0;
            button94.TabIndex = 0;
            button95.TabIndex = 0;
            button96.TabIndex = 0;
            button97.TabIndex = 0;
            button98.TabIndex = 0;
            button99.TabIndex = 0;
            button100.TabIndex = 0;

            button1.Image = null;
            button2.Image = null;
            button3.Image = null;
            button4.Image = null;
            button5.Image = null;
            button6.Image = null;
            button7.Image = null;
            button8.Image = null;
            button9.Image = null;
            button10.Image = null;
            button11.Image = null;
            button12.Image = null;
            button13.Image = null;
            button14.Image = null;
            button15.Image = null;
            button16.Image = null;
            button17.Image = null;
            button18.Image = null;
            button19.Image = null;
            button20.Image = null;
            button21.Image = null;
            button22.Image = null;
            button23.Image = null;
            button24.Image = null;
            button25.Image = null;
            button26.Image = null;
            button27.Image = null;
            button28.Image = null;
            button29.Image = null;
            button30.Image = null;
            button31.Image = null;
            button32.Image = null;
            button33.Image = null;
            button34.Image = null;
            button35.Image = null;
            button36.Image = null;
            button37.Image = null;
            button38.Image = null;
            button39.Image = null;
            button40.Image = null;
            button41.Image = null;
            button42.Image = null;
            button43.Image = null;
            button44.Image = null;
            button45.Image = null;
            button46.Image = null;
            button47.Image = null;
            button48.Image = null;
            button49.Image = null;
            button50.Image = null;
            button51.Image = null;
            button52.Image = null;
            button53.Image = null;
            button54.Image = null;
            button55.Image = null;
            button56.Image = null;
            button57.Image = null;
            button58.Image = null;
            button59.Image = null;
            button60.Image = null;
            button61.Image = null;
            button62.Image = null;
            button63.Image = null;
            button64.Image = null;
            button65.Image = null;
            button66.Image = null;
            button67.Image = null;
            button68.Image = null;
            button69.Image = null;
            button70.Image = null;
            button71.Image = null;
            button72.Image = null;
            button73.Image = null;
            button74.Image = null;
            button75.Image = null;
            button76.Image = null;
            button77.Image = null;
            button78.Image = null;
            button79.Image = null;
            button80.Image = null;
            button81.Image = null;
            button82.Image = null;
            button83.Image = null;
            button84.Image = null;
            button85.Image = null;
            button86.Image = null;
            button87.Image = null;
            button88.Image = null;
            button89.Image = null;
            button90.Image = null;
            button91.Image = null;
            button92.Image = null;
            button93.Image = null;
            button94.Image = null;
            button95.Image = null;
            button96.Image = null;
            button97.Image = null;
            button98.Image = null;
            button99.Image = null;
            button100.Image = null;

            button1.UseMnemonic = false;
            button2.UseMnemonic = false;
            button3.UseMnemonic = false;
            button4.UseMnemonic = false;
            button5.UseMnemonic = false;
            button6.UseMnemonic = false;
            button7.UseMnemonic = false;
            button8.UseMnemonic = false;
            button9.UseMnemonic = false;
            button10.UseMnemonic = false;
            button11.UseMnemonic = false;
            button12.UseMnemonic = false;
            button13.UseMnemonic = false;
            button14.UseMnemonic = false;
            button15.UseMnemonic = false;
            button16.UseMnemonic = false;
            button17.UseMnemonic = false;
            button18.UseMnemonic = false;
            button19.UseMnemonic = false;
            button20.UseMnemonic = false;
            button21.UseMnemonic = false;
            button22.UseMnemonic = false;
            button23.UseMnemonic = false;
            button24.UseMnemonic = false;
            button25.UseMnemonic = false;
            button26.UseMnemonic = false;
            button27.UseMnemonic = false;
            button28.UseMnemonic = false;
            button29.UseMnemonic = false;
            button30.UseMnemonic = false;
            button31.UseMnemonic = false;
            button32.UseMnemonic = false;
            button33.UseMnemonic = false;
            button34.UseMnemonic = false;
            button35.UseMnemonic = false;
            button36.UseMnemonic = false;
            button37.UseMnemonic = false;
            button38.UseMnemonic = false;
            button39.UseMnemonic = false;
            button40.UseMnemonic = false;
            button41.UseMnemonic = false;
            button42.UseMnemonic = false;
            button43.UseMnemonic = false;
            button44.UseMnemonic = false;
            button45.UseMnemonic = false;
            button46.UseMnemonic = false;
            button47.UseMnemonic = false;
            button48.UseMnemonic = false;
            button49.UseMnemonic = false;
            button50.UseMnemonic = false;
            button51.UseMnemonic = false;
            button52.UseMnemonic = false;
            button53.UseMnemonic = false;
            button54.UseMnemonic = false;
            button55.UseMnemonic = false;
            button56.UseMnemonic = false;
            button57.UseMnemonic = false;
            button58.UseMnemonic = false;
            button59.UseMnemonic = false;
            button60.UseMnemonic = false;
            button61.UseMnemonic = false;
            button62.UseMnemonic = false;
            button63.UseMnemonic = false;
            button64.UseMnemonic = false;
            button65.UseMnemonic = false;
            button66.UseMnemonic = false;
            button67.UseMnemonic = false;
            button68.UseMnemonic = false;
            button69.UseMnemonic = false;
            button70.UseMnemonic = false;
            button71.UseMnemonic = false;
            button72.UseMnemonic = false;
            button73.UseMnemonic = false;
            button74.UseMnemonic = false;
            button75.UseMnemonic = false;
            button76.UseMnemonic = false;
            button77.UseMnemonic = false;
            button78.UseMnemonic = false;
            button79.UseMnemonic = false;
            button80.UseMnemonic = false;
            button81.UseMnemonic = false;
            button82.UseMnemonic = false;
            button83.UseMnemonic = false;
            button84.UseMnemonic = false;
            button85.UseMnemonic = false;
            button86.UseMnemonic = false;
            button87.UseMnemonic = false;
            button88.UseMnemonic = false;
            button89.UseMnemonic = false;
            button90.UseMnemonic = false;
            button91.UseMnemonic = false;
            button92.UseMnemonic = false;
            button93.UseMnemonic = false;
            button94.UseMnemonic = false;
            button95.UseMnemonic = false;
            button96.UseMnemonic = false;
            button97.UseMnemonic = false;
            button98.UseMnemonic = false;
            button99.UseMnemonic = false;
            button100.UseMnemonic = false;

            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
            button10.Enabled = true;
            button11.Enabled = true;
            button12.Enabled = true;
            button13.Enabled = true;
            button14.Enabled = true;
            button15.Enabled = true;
            button16.Enabled = true;
            button17.Enabled = true;
            button18.Enabled = true;
            button19.Enabled = true;
            button20.Enabled = true;
            button21.Enabled = true;
            button22.Enabled = true;
            button23.Enabled = true;
            button24.Enabled = true;
            button25.Enabled = true;
            button26.Enabled = true;
            button27.Enabled = true;
            button28.Enabled = true;
            button29.Enabled = true;
            button30.Enabled = true;
            button31.Enabled = true;
            button32.Enabled = true;
            button33.Enabled = true;
            button34.Enabled = true;
            button35.Enabled = true;
            button36.Enabled = true;
            button37.Enabled = true;
            button38.Enabled = true;
            button39.Enabled = true;
            button40.Enabled = true;
            button41.Enabled = true;
            button42.Enabled = true;
            button43.Enabled = true;
            button44.Enabled = true;
            button45.Enabled = true;
            button46.Enabled = true;
            button47.Enabled = true;
            button48.Enabled = true;
            button49.Enabled = true;
            button50.Enabled = true;
            button51.Enabled = true;
            button52.Enabled = true;
            button53.Enabled = true;
            button54.Enabled = true;
            button55.Enabled = true;
            button56.Enabled = true;
            button57.Enabled = true;
            button58.Enabled = true;
            button59.Enabled = true;
            button60.Enabled = true;
            button61.Enabled = true;
            button62.Enabled = true;
            button63.Enabled = true;
            button64.Enabled = true;
            button65.Enabled = true;
            button66.Enabled = true;
            button67.Enabled = true;
            button68.Enabled = true;
            button69.Enabled = true;
            button70.Enabled = true;
            button71.Enabled = true;
            button72.Enabled = true;
            button73.Enabled = true;
            button74.Enabled = true;
            button75.Enabled = true;
            button76.Enabled = true;
            button77.Enabled = true;
            button78.Enabled = true;
            button79.Enabled = true;
            button80.Enabled = true;
            button81.Enabled = true;
            button82.Enabled = true;
            button83.Enabled = true;
            button84.Enabled = true;
            button85.Enabled = true;
            button86.Enabled = true;
            button87.Enabled = true;
            button88.Enabled = true;
            button89.Enabled = true;
            button90.Enabled = true;
            button91.Enabled = true;
            button92.Enabled = true;
            button93.Enabled = true;
            button94.Enabled = true;
            button95.Enabled = true;
            button96.Enabled = true;
            button97.Enabled = true;
            button98.Enabled = true;
            button99.Enabled = true;
            button100.Enabled = true;

            timerDoba.Enabled = true;

            Miny();
        }

        // Tick.
        private void timerDoba_Tick(object sender, EventArgs e)
        {
            doba++;
            textBoxDoba.Text = doba.ToString();
        }




        // Události zmáčknutí jednotlivých tlačítek (100: 1-100).
        private void button1_Click(object sender, EventArgs e)
        {
            TlačítkoKlik(button1);
        }
    }    
}
