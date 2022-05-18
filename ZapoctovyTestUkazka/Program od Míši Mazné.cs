using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Zapoctak4
{
	class Program
	{
		static void Main(string[] args)
		{
			Environment environment = new Environment();
			environment.Live();
		}
	}
	class Environment
	{
		public Dictionary<string, Table> Tables = new Dictionary<string, Table>();
		public bool TableExists(string tableName)
		{
				if (!Tables.ContainsKey(tableName))
				{
					Console.WriteLine("Invalid table name " + tableName);
					return false;
				}
			return true;
		}
		public List<Command> commands = new List<Command> { new CreateTable(), new SelectFrom(), new InsertInto(), new Update(), new Load(), new Save() };
		public void ProcessCommand(string line, List<Command> commands)
		{
			string[] lineSplitted = line.Split(' ');
			if (line != "" && line != null)
			{
				foreach (Command command in commands)
				{
					if (command.Name == lineSplitted[0])
					{
						command.DoCommand(this, line.Remove(0, command.Name.Length + 1));
						break;
					}
				}
			}
		}
		public void Live()
		{
			while (true)
			{
				try
				{
					string line = Console.ReadLine();
					ProcessCommand(line, commands);
				}
				catch
				{
					//already handled
				}
			}
		}

	}
	class Table
	{
		public string Name { get;  set; }
		public Dictionary<string, Column> columns = new Dictionary<string, Column>();
		public bool ColumnsExist(List<string> columns)
		{
			for (int i = 0; i < columns.Count; i++)
			{
				if (!this.columns.ContainsKey(columns[i]))
				{
					Console.WriteLine("Invalid column name " + columns[i]);
					return false;
				}
			}
			return true;
		}
	}

	class Column
	{
		public List<int> rows = new List<int>();
		public string Name { get; set; }
	}

	abstract class Command
	{
		public abstract string Name { get; }

		public abstract void DoCommand(Environment environment, string line);			
	}

	class CreateTable : Command
	{
		public override string Name { get { return "CREATE_TABLE"; } }
		public override void DoCommand(Environment environment, string line)
		{
			List<string> args = Parser.ParseArgs(line);
			if (!environment.TableExists(args[0]))
			{
				Table table = new Table() { Name = args[0] };
				List<Column> columns = new List<Column>();
				for (int i = 1; i < args.Count; i++)
				{
					table.columns.Add(args[i], new Column() { Name = args[i] });
				}
				environment.Tables.Add(args[0], table);
			}
			else
				throw new FormatException();
			//TODO: else -> exception??

		}

	}
	class SelectFrom : Command
	{
		public override string Name { get { return "SELECT_FROM"; } }

		public override void DoCommand(Environment environment, string line)
		{
			List<string> args = Parser.ParseArgs(line);

			if (environment.TableExists(args[0]))
			{
				List<string> columns = args.GetRange(1, args.Count - 1);
				if (environment.Tables[args[0]].ColumnsExist(columns))
				{
					Console.WriteLine(Parser.WriteWithChar(columns, ',')); // names of columns
					StringBuilder sb = new StringBuilder();
					for (int i = 0; i < environment.Tables[args[0]].columns[columns[0]].rows.Count; i++)
					{
						foreach (string column in columns)
						{
							sb.Append(environment.Tables[args[0]].columns[column].rows[i] + ",");
						}
						sb.Remove(sb.Length - 1, 1);
						Console.WriteLine(sb.ToString());
						sb.Clear();
					}
				}
				else
				{
					throw new FormatException();
				}

			}
			else
				throw new FormatException();
		}
	}
	class InsertInto : Command
	{
		public override string Name { get => "INSERT_INTO"; }

		public override void DoCommand(Environment environment, string line)
		{
			List<string> args = Parser.ParseArgs(line);

			if (environment.TableExists(args[0]))
			{
				List<string> values = args.GetRange(1, args.Count - 1);
				int i = 0;
				foreach (KeyValuePair<string, Column> column in environment.Tables[args[0]].columns)
				{
					column.Value.rows.Add(Convert.ToInt32(values[i]));
					i++;
				}

			}
			else
				throw new FormatException();
		}
	}

	class Update : Command
	{

		public override string Name { get { return "UPDATE"; } }


		public override void DoCommand(Environment environment, string line)
		{
			List<string> args = Parser.ParseArgs(line);

			if (environment.TableExists(args[0]))
			{
				List<string> equation = Parser.GetEquation(args[3]);
				if(environment.Tables[args[0]].ColumnsExist(new List<string> { args[1], equation[0] }))
				{
					List<int> indexes = new List<int>();
					int i = 0;
					int num = Convert.ToInt32(equation[2]);
					switch (equation[1])
					{
						case ">":
							foreach (int row in environment.Tables[args[0]].columns[equation[0]].rows)
							{
								if (row > num)
								{
									indexes.Add(i);
								}
								i++;
							}
							break;
						case "<":
							foreach (int row in environment.Tables[args[0]].columns[equation[0]].rows)
							{
								if (row < num)
								{
									indexes.Add(i);
								}
								i++;
							}
							break;
						case "=":
							foreach (int row in environment.Tables[args[0]].columns[equation[0]].rows)
							{
								if (row == num)
								{
									indexes.Add(i);
								}
								i++;
							}
							break;
					}
					for (int j = 0; j < environment.Tables[args[0]].columns[equation[0]].rows.Count; j++)
					{
						if (indexes.Contains(j))
						{
							environment.Tables[args[0]].columns[args[1]].rows[j] = Convert.ToInt32(args[2]);
						}
					}
				}
				else
				{
					throw new FormatException();
				}
			}
			else
				throw new FormatException();
		}
	}

	class Load : Command
	{
		public override string Name { get { return "LOAD"; } }


		public override void DoCommand(Environment environment, string line)
		{
			try
			{
				using (StreamReader sr = new StreamReader(line))
				{
					string lineFile;
					while ((lineFile=sr.ReadLine())!=null)
					{
						environment.ProcessCommand(lineFile, environment.commands);
					}
				}
			}
			catch (FormatException){
				// already handled
			}
			catch
			{
				Console.WriteLine("Could not find file " + line);
			}
		}
	}

	class Save : Command
	{
		public override string Name { get { return "SAVE"; } }


		public override void DoCommand(Environment environment, string line)
		{
			try
			{
				using (StreamWriter sw = new StreamWriter(line))
				{
					foreach(KeyValuePair<string, Table> table in environment.Tables)
					{
						StringBuilder sb = new StringBuilder();
						sb.Append("CREATE_TABLE " + table.Key + " ");
						string rndName="rnd";
						foreach (KeyValuePair<string,Column> column in table.Value.columns)
						{
							sb.Append(column.Key + " ");
							 rndName= column.Key;
						}
						sb.Remove(sb.Length - 1, 1);
						sw.WriteLine(sb.ToString());
						sb.Clear();
						
						for(int i=0; i < table.Value.columns[rndName].rows.Count; i++)
						{
							sb.Append("INSERT_INTO " + table.Key + " ");
							foreach (KeyValuePair<string, Column> column in table.Value.columns)
							{
								sb.Append(column.Value.rows[i] + " ");
							}
							sb.Remove(sb.Length - 1, 1);
							sw.WriteLine(sb.ToString());
							sb.Clear();
						}
					}
				}
			}
			catch
			{
				Console.WriteLine("Could not find file " + line);
			}
		}
	}
	class Parser
	{
		public static List<string> ParseArgs(string line)
		{
			StringBuilder sb = new StringBuilder();
			List<string> args = new List<string>();
			for (int i = 0; i < line.Length; i++)
			{
				if (line[i] == ' ')
				{
					args.Add(sb.ToString());
					sb.Clear();
				}
				else
				{
					sb.Append(line[i]);
				}
			}
			if (sb.Length > 0)
			{
				args.Add(sb.ToString());
				sb.Clear();
			}
			return args;
		}

		public static string WriteWithChar(List<string> words, char separator)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < words.Count; i++)
			{
				sb.Append(words[i] + separator);
			}
			sb.Remove(sb.Length - 1, 1);
			return sb.ToString();
			
		}
		public static string WriteWithChar(List<int> nums, char separator)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 1; i < nums.Count; i++)
			{
				sb.Append(nums[i].ToString() + separator);
			}
			sb.Remove(sb.Length - 1, 1);
			return sb.ToString();
		}
		public static List<string> GetEquation(string word)
		{
			List<string> words = new List<string>();
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < word.Length; i++)
			{
				if (word[i] == '>' || word[i] == '<' || word[i] == '=')
				{
					words.Add(sb.ToString());
					sb.Clear();
					words.Add(word[i].ToString());
				}
				else
				{
					sb.Append(word[i]);
				}
			}
			words.Add(sb.ToString());
			return words;
		}		
	}
}
