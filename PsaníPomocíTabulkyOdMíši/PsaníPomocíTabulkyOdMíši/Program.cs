using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alphabet
{
	class Program
	{
		class CteckaCisel
		{
			public static int PrectiInt()
			{
				int x = 0;
				int znak = Console.Read();
				bool Zaporny = false;


				// vsechno, co nejsou cisla, preskoc
				while ((znak < '0') || (znak > '9'))
				{
					// záporný čísla
					if (znak == '-')
					{
						znak = Console.Read();
						if ((znak >= '0') && (znak <= '9'))
						{
							Zaporny = true;
							break;
						}
					}
					else znak = Console.Read();

				}

				while ((znak >= '0') && (znak <= '9'))
				{
					x = 10 * x + znak - '0';
					znak = Console.Read();
				}
				if (Zaporny) return -x;
				else return x;
			}
		}
		struct Position
		{
			public int X;
			public int Y;
			public Position(int x, int y)
			{
				X = x;
				Y = y;
			}
		}
		static void Main(string[] args)
		{
			#region Nacteni_vstupu
			int n = CteckaCisel.PrectiInt(); // pocet sloupcu; sirka tabulky
			int m = CteckaCisel.PrectiInt(); // pocet radku; vyska tabulky
			Console.ReadLine();
			string tabulkaVstup = Console.ReadLine(); // bude to čtečkou!!!!!!
			int pom = 0;
			char[,] tabulkaPismen = new char[m, n];
			for (int i = 0; i < m; i++)
			{
				for (int j = 0; j < n; j++)
				{
					tabulkaPismen[i, j] = tabulkaVstup[pom++];
				}
			}
			//VypisTabulku(tabulkaPismen);
			pom = 0;
			string slovo = Console.ReadLine();
			#endregion

			int pocetKlikuPoslNalez = 0;
			int currentPocetKliku = 0; 
			int[,] tabulkaKliku = new int[m, n];

			Queue<Position> fronta = new Queue<Position>(); 
			Position vychozi = new Position(0, 0);
			fronta.Enqueue(vychozi);
			int index = 0;

			Position nahoru = new Position(0, 1);
			Position dolu = new Position(0, -1);
			Position vpravo = new Position(1, 0);
			Position vlevo = new Position(-1, 0);
			List<Position> okoli = new List<Position>() { nahoru, dolu, vpravo, vlevo };

			//VypisTabulku(tabulkaPismen);
			List<Position> KdeMaCenuPokracovat = new List<Position>();
			List<Position> NajiteOznackovanimOkoli = new List<Position>();
			while (fronta.Count > 0) 
			{
				KdeMaCenuPokracovat.Clear();
				foreach (Position pos in fronta)
				{
					if (tabulkaPismen[pos.X, pos.Y] == slovo[index])
					{
						pocetKlikuPoslNalez = currentPocetKliku + 1;
						tabulkaKliku[pos.X, pos.Y] = pocetKlikuPoslNalez; //pozor
					}
					KdeMaCenuPokracovat.Add(pos); // blbý, když je jen jedna vhodná a ostatní ne, a stejně i ty ostatní přidám 
				}
				currentPocetKliku++;
				//tady jsem skoncila 19:35
				if (pocetKlikuPoslNalez == currentPocetKliku)
					index++; // ten se musí zvýšit jen pokud pocetklikuPoslNalezeno je tsejny nebo o 1 mensi (?) nez currpocet
				
				currentPocetKliku++;
				fronta.Clear();
				if(index < slovo.Length)
				{
					//VYTEKLAS Z TABULKY UVNITŘ PROHLEDÁVÁNÍ
					foreach (Position pos in KdeMaCenuPokracovat)
					{
						if (ProhledejOkoli(pos, slovo[index]) == false)
						{
							OznackujOkoli(pos, currentPocetKliku);
							//VypisTabulku(tabulkaKliku);
						} //pech je, že tu měnim frontu: napraveno Listem, ale je to humáč
					}

					if (fronta.Count == 0)
					{
						foreach (Position pos in NajiteOznackovanimOkoli)
						{
							if (!fronta.Contains(pos))
								fronta.Enqueue(pos);
						}
					}
					NajiteOznackovanimOkoli.Clear(); //smazala jsem tu else, snad to nebude moc vadit 
					//VypisTabulku(tabulkaPismen);
				}
				
			}

			int vysledek = pocetKlikuPoslNalez + slovo.Length; // delku slova pridavam kvuli nezapocitavani stisku Enterů
			Console.WriteLine(vysledek);

			void VypisTabulku <T>(T[,] tabulka)
			{
				for (int i  = 0; i  <m; i ++)
				{
					for (int j = 0; j < n; j++)
					{
						Console.Write(tabulka[i, j]);
					}
					Console.WriteLine();
				}
			}
			

			void OznackujOkoli(Position currPos,int pocetKliku)
			{
				foreach(Position smer in okoli)
				{
					int x = currPos.X + smer.X;
					int y = currPos.Y + smer.Y;
					if(((x >= 0) && (x < m)) && ((y >= 0) && (y < n)))
					{
						if (tabulkaKliku[x, y] < pocetKlikuPoslNalez)
						{
							tabulkaKliku[x, y] = pocetKliku;
							Position pos = new Position(x, y);
							NajiteOznackovanimOkoli.Add(pos);
						}
					}
					
				}
			}
			
			
			bool ProhledejOkoli(Position currPoss, char hledanePismeno)
			{
				//int[] okoli = new int[2] { -1, 1 };
				int x;
				int y;
				bool najitoAsponNeco = false;

				if (tabulkaPismen[currPoss.X,currPoss.Y] == hledanePismeno)
				{
					if (!fronta.Contains(currPoss))
						 fronta.Enqueue(currPoss);
					najitoAsponNeco=true;
				}


				foreach(Position smer in okoli)
				{
					x = currPoss.X + smer.X;
					y = currPoss.Y + smer.Y;
					if (((x >= 0) && (x < m)) && ((y >= 0) && (y < n)))
					{
						if (tabulkaPismen[x, y] == hledanePismeno)
						{
							Position position = new Position(x, y);
							if (!fronta.Contains(position))
								fronta.Enqueue(position);
							najitoAsponNeco = true;
						}
					}
				}

				
				return najitoAsponNeco;
			}
		}		
	}
}
