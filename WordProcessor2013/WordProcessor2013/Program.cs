using System;
using System.Collections.Generic;
// using System.Linq;
using System.Text;
// using System.Threading.Tasks;

using System.IO;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("WordProcessor2013_UnitTests")]

namespace WordProcessor2013 {

	interface IWordReader {
		string ReadWord();
	}

	// Processors are using a push model.
	interface IWordProcessor {
		void ProcessWord(string word);
		void Finish();
	}

	//class WordReader : IWordReader {
	//	public WordReader(TextReader reader) {

	//	}

	//	public string ReadWord() {
	//		throw new NotImplementedException();
	//	}
	//}

	//class WordReader : IWordReader {
	//	private TextReader reader;
	//	private string[] words;
	//	private int nextWord = 0;

	//	public WordReader(TextReader reader) {
	//		this.reader = reader;
	//		words = reader.ReadLine().Split(' ', '\t');
	//	}

	//	public string ReadWord() {
	//		if (nextWord >= words.Length) {
	//			return null;
	//		}

	//		return words[nextWord++];
	//	}
	//}

	//class WordReader : IWordReader {
	//	private TextReader reader;
	//	private string[] words = { };
	//	private int nextWord = 0;

	//	public WordReader(TextReader reader) {
	//		this.reader = reader;
	//	}

	//	public string ReadWord() {
	//		if (nextWord >= words.Length) {
	//			string line = reader.ReadLine();
	//			if (line == null) {
	//				return null;
	//			}
	//			words = line.Split(' ', '\t');
	//			nextWord = 0;
	//		}

	//		return words[nextWord++];
	//	}
	//}

	//class WordReader : IWordReader {
	//	private TextReader reader;
	//	private string[] words = { };
	//	private int nextWord = 0;

	//	public WordReader(TextReader reader) {
	//		this.reader = reader;
	//	}

	//	public string ReadWord() {
	//		while (nextWord >= words.Length) {
	//			string line = reader.ReadLine();
	//			if (line == null) {
	//				return null;
	//			}
	//			words = line.Split(new [] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
	//			nextWord = 0;
	//		}

	//		return words[nextWord++];
	//	}
	//}

	//class WordReader : IWordReader {
	//	private TextReader reader;
	//	private string[] words = { };
	//	private int nextWord = 0;

	//	public WordReader(TextReader reader) {
	//		if (reader == null) {
	//			throw new ArgumentNullException("reader");
	//		}
	//		this.reader = reader;
	//	}

	//	public string ReadWord() {
	//		while (nextWord >= words.Length) {
	//			string line = reader.ReadLine();
	//			if (line == null) {
	//				return null;
	//			}
	//			words = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
	//			nextWord = 0;
	//		}

	//		return words[nextWord++];
	//	}
	//}

	class WordReader : IWordReader, IDisposable {
		private TextReader reader;
		private string[] words = { };
		private int nextWord = 0;

		public WordReader(TextReader reader) {
			if (reader == null) {
				throw new ArgumentNullException("reader");
			}
			this.reader = reader;
		}

		public string ReadWord() {
			while (nextWord >= words.Length) {
				string line = reader.ReadLine();
				if (line == null) {
					return null;
				}
				words = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
				nextWord = 0;
			}

			return words[nextWord++];
		}

		public void Dispose() {
			reader.Dispose();
		}
	}

	//
	// -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
	//

	//class WordCounter : IWordProcessor {
	//	private TextWriter writer;

	//	public WordCounter(TextWriter writer) {
	//		if (writer == null) {
	//			throw new ArgumentNullException("writer");
	//		}
	//		this.writer = writer;
	//	}

	//	public void ProcessWord(string word) {
	//		throw new NotImplementedException();
	//	}

	//	public void Finish() {
	//		throw new NotImplementedException();
	//	}
	//}

	//class WordCounter : IWordProcessor {
	//	private TextWriter writer;
	//	private int wordCount = 0;

	//	public WordCounter(TextWriter writer) {
	//		if (writer == null) {
	//			throw new ArgumentNullException("writer");
	//		}
	//		this.writer = writer;
	//	}

	//	public void ProcessWord(string word) {
	//		throw new NotImplementedException();
	//	}

	//	public void Finish() {
	//		writer.WriteLine(wordCount);
	//	}
	//}

	class WordCounter : IWordProcessor {
		private TextWriter writer;
		private int wordCount = 0;

		public WordCounter(TextWriter writer) {
			if (writer == null) {
				throw new ArgumentNullException("writer");
			}
			this.writer = writer;
		}

		public void ProcessWord(string word) {
			wordCount++;
		}

		public void Finish() {
			writer.WriteLine(wordCount);
		}
	}

	class Program {
		// The main word processing algorithm
		public static void ProcessWords(IWordReader reader, IWordProcessor processor) {
			string word;
			while ((word = reader.ReadWord()) != null) {
				processor.ProcessWord(word);
			}

			processor.Finish();
		}

		//static void Main(string[] args) {
		//}

		//public static void Run(string[] args, TextWriter outputWriter) {

		//}

		//static void Main(string[] args) {
		//	Run(args, Console.Out);
		//}

		static TextWriter output;

		static void ReportFileError() {
			output.WriteLine("File Error");
		}

		static void ReportArgumentError() {
			output.WriteLine("Argument Error");
		}

		//public static void Run(string[] args, TextWriter outputWriter) {
		//	output = outputWriter;

		//	if (args.Length != 1 || args[0] == "") {
		//		ReportArgumentError();
		//		return;
		//	}

		//	WordReader reader = null;
		//	try {
		//		reader = new WordReader(new StreamReader(args[0]));
		//		var counter = new WordCounter(outputWriter);

		//		ProcessWords(reader, counter);
		//	} catch (FileNotFoundException) {
		//		ReportFileError();
		//	} catch (IOException) {
		//		ReportFileError();
		//	} catch (UnauthorizedAccessException) {
		//		ReportFileError();
		//	} catch (System.Security.SecurityException) {
		//		ReportFileError();
		//  }
		//}

		public static void Run(string[] args, TextWriter outputWriter) {
			output = outputWriter;

			if (args.Length != 1 || args[0] == "") {
				ReportArgumentError();
				return;
			}

			WordReader reader = null;
			try {
				reader = new WordReader(new StreamReader(args[0]));
				var counter = new WordCounter(outputWriter);

				ProcessWords(reader, counter);
			} catch (FileNotFoundException) {
				ReportFileError();
			} catch (IOException) {
				ReportFileError();
			} catch (UnauthorizedAccessException) {
				ReportFileError();
			} catch (System.Security.SecurityException) {
				ReportFileError();
			} finally {
				if (reader != null) reader.Dispose();
			}
		}

		static void Main(string[] args) {
			Run(args, Console.Out);
		}
	}
}

