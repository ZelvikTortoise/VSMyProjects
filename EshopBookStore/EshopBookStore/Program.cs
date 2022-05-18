using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("TestClass")]
namespace EshopBookStore
{
    //
    // Model
    //
        
    class ModelStore
    {
        private List<Book> books = new List<Book>();
        private List<Customer> customers = new List<Customer>();

        public IList<Book> GetBooks()
        {
            return books;
        }

        public Book GetBook(int id)
        {
            return books.Find(b => b.Id == id);
        }

        public Customer GetCustomer(int id)
        {
            return customers.Find(c => c.Id == id);
        }

        public static ModelStore LoadFrom(TextReader reader)
        {
            var store = new ModelStore();

            try
            {
                if (reader.ReadLine() != "DATA-BEGIN")
                {
                    return null;
                }
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        return null;
                    }
                    else if (line == "DATA-END")
                    {
                        break;
                    }

                    string[] tokens = line.Split(';');
                    switch (tokens[0])
                    {
                        case "BOOK":
                            store.books.Add(new Book
                            {
                                Id = int.Parse(tokens[1]),
                                Title = tokens[2],
                                Author = tokens[3],
                                Price = decimal.Parse(tokens[4])
                            });
                            break;
                        case "CUSTOMER":
                            store.customers.Add(new Customer
                            {
                                Id = int.Parse(tokens[1]),
                                FirstName = tokens[2],
                                LastName = tokens[3]
                            });
                            break;
                        case "CART-ITEM":
                            var customer = store.GetCustomer(int.Parse(tokens[1]));
                            if (customer == null)
                            {
                                return null;
                            }
                            customer.ShoppingCart.Items.Add(new ShoppingCartItem
                            {
                                BookId = int.Parse(tokens[2]),
                                Count = int.Parse(tokens[3])
                            });
                            break;
                        default:
                            return null;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is FormatException || ex is IndexOutOfRangeException)
                {
                    return null;
                }
                throw;
            }

            return store;
        }
    }

    class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
    }

    class Customer
    {
        private ShoppingCart shoppingCart;

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ShoppingCart ShoppingCart
        {
            get
            {
                if (shoppingCart == null)
                {
                    shoppingCart = new ShoppingCart();
                }
                return shoppingCart;
            }
            set
            {
                shoppingCart = value;
            }
        }
    }

    class ShoppingCartItem
    {
        public int BookId { get; set; }
        public int Count { get; set; }
    }

    class ShoppingCart
    {
        public int CustomerId { get; set; }
        public List<ShoppingCartItem> Items = new List<ShoppingCartItem>();
    }

    //
    // End of Model
    //

    //
    // View
    //

    class ViewStore
    {
        /// <summary>
        /// Prints the indicator that the answer from the server has ender.
        /// </summary>
        /// <param name="writer"></param>
        public void ViewEnd(TextWriter writer)
        {
            const string end = "====";
            writer.WriteLine(end);
        }

        /// <summary>
        /// Prints html code for the text at the beginning of each Nezarka.net website.
        /// </summary>
        /// <param name="store"></param>
        /// <param name="writer"></param>
        /// <param name="custId">ID of the customer that sent a command to load a new page</param>
        public void ViewMenu(ModelStore store, TextWriter writer, int custId)
        {
            writer.WriteLine("<!DOCTYPE html>");
            writer.WriteLine("<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
            writer.WriteLine("<head>");
            writer.WriteLine("    <meta charset=\"utf-8\" />");
            writer.WriteLine("    <title>Nezarka.net: Online Shopping for Books</title>");
            writer.WriteLine("</head>");
            writer.WriteLine("<body>");
            writer.WriteLine("    <style type=\"text/css\">");
            writer.WriteLine("        table, th, td {");
            writer.WriteLine("            border: 1px solid black;");
            writer.WriteLine("            border-collapse: collapse;");
            writer.WriteLine("        }");
            writer.WriteLine("        table {");
            writer.WriteLine("            margin-bottom: 10px;");
            writer.WriteLine("        }");
            writer.WriteLine("        pre {");
            writer.WriteLine("            line-height: 70%;");
            writer.WriteLine("        }");
            writer.WriteLine("    </style>");
            writer.WriteLine("    <h1><pre>  v,<br />Nezarka.NET: Online Shopping for Books</pre></h1>");
            writer.WriteLine("    {0}, here is your menu:", store.GetCustomer(custId).FirstName);
            writer.WriteLine("    <table>");
            writer.WriteLine("        <tr>");
            writer.WriteLine("            <td><a href=\"/Books\">Books</a></td>");
            writer.WriteLine("            <td><a href=\"/ShoppingCart\">Cart ({0})</a></td>", store.GetCustomer(custId).ShoppingCart.Items.Count.ToString());
            writer.WriteLine("        </tr>");
            writer.WriteLine("    </table>");
        }

        /// <summary>
        /// Prints html code for a website with the overview of all available books.
        /// </summary>
        /// <param name="store"></param>
        /// <param name="writer"></param>
        /// <param name="custId">ID of the customer that sent this command</param>
        public void ViewBooks(ModelStore store, TextWriter writer, int custId)
        {            
            IList<Book> library = store.GetBooks();
            const int booksOnLine = 3;  // How many books we want on one full line.
            int fullLines = library.Count / booksOnLine;    // Integer division: how many full lines of books we have.
            int lastLineBooks = library.Count % booksOnLine;    // Modulo: how many books will be on the last non-full line. (If all lines are full, 0 books will be on the last non-full line.)
            int currentBook = -1; // Indexing of our library List.

            ViewMenu(store, writer, custId);
            writer.WriteLine("    Our books for you:");
            writer.WriteLine("    <table>");
            // Full lines
            for (int i = 1; i <= fullLines; i++)
            {
                writer.WriteLine("        <tr>");
                for (int j = 1; j <= booksOnLine; j++)
                {
                    currentBook++;
                    writer.WriteLine("            <td style=\"padding: 10px;\">");
                    writer.WriteLine("                <a href=\"/Books/Detail/{0}\">{1}</a><br />", library[currentBook].Id.ToString(), library[currentBook].Title);
                    writer.WriteLine("                Author: {0}<br />", library[currentBook].Author);
                    writer.WriteLine("                Price: {0} EUR &lt;<a href=\"/ShoppingCart/Add/{1}\">Buy</a>&gt;", library[currentBook].Price.ToString(), library[currentBook].Id.ToString());
                    writer.WriteLine("            </td>");
                }
                writer.WriteLine("        </tr>");
            }
            // The last (non-full) line
            if (lastLineBooks != 0)
            {
                writer.WriteLine("        <tr>");
                for (int i = 1; i <= lastLineBooks; i++)
                {
                    currentBook++;
                    writer.WriteLine("            <td style=\"padding: 10px;\">");
                    writer.WriteLine("                <a href=\"/Books/Detail/{0}\">{1}</a><br />", library[currentBook].Id.ToString(), library[currentBook].Title);
                    writer.WriteLine("                Author: {0}<br />", library[currentBook].Author);
                    writer.WriteLine("                Price: {0} EUR &lt;<a href=\"/ShoppingCart/Add/{1}\">Buy</a>&gt;", library[currentBook].Price.ToString(), library[currentBook].Id.ToString());
                    writer.WriteLine("            </td>");
                }
                writer.WriteLine("        </tr>");
            }                        
            writer.WriteLine("    </table>");
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
            ViewEnd(writer);
        }

        /// <summary>
        /// Prints html code for a website with details of a specific book.
        /// </summary>
        /// <param name="store"></param>
        /// <param name="writer"></param>
        /// <param name="custId">ID of the customer that sent this command</param>
        /// <param name="bookId">Specific book ID</param>
        public void ViewBookDetails(ModelStore store, TextWriter writer, int custId, int bookId)
        {
            ViewMenu(store, writer, custId);
            writer.WriteLine("    Book details:");
            writer.WriteLine("    <h2>{0}</h2>", store.GetBook(bookId).Title);
            writer.WriteLine("<p style=\"margin-left: 20px\">");
            writer.WriteLine("Author: {0}<br />", store.GetBook(bookId).Author);
            writer.WriteLine("Price: {0} EUR<br />", store.GetBook(bookId).Price.ToString());
            writer.WriteLine("</p>");
            writer.WriteLine("<h3>&lt;<a href=\"/ShoppingCart/Add/{0}\">Buy this book</a>&gt;</h3>", bookId);
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
            ViewEnd(writer);
        }

        /// <summary>
        /// Prints html code for a website with overview of the shopping cart of the current customer.
        /// </summary>
        /// <param name="store"></param>
        /// <param name="writer"></param>
        /// <param name="custId">ID of the customer that sent this command</param>
        public void ViewShoppingCart(ModelStore store, TextWriter writer, int custId)
        {
            ViewMenu(store, writer, custId);
            int itemsCount = store.GetCustomer(custId).ShoppingCart.Items.Count;    // Number of items in the shopping cart.
            if (itemsCount == 0)    // The shopping cart is empty.
            {
                writer.WriteLine("    Your shopping cart is EMPTY.");
            }
            else    // There are some items in the shopping cart.
            {
                decimal totalPrice = 0;
                writer.WriteLine("    Your shopping cart:");
                writer.WriteLine("    <table>");
                writer.WriteLine("        <tr>");
                writer.WriteLine("            <th>Title</th>");
                writer.WriteLine("            <th>Count</th>");
                writer.WriteLine("            <th>Price</th>");
                writer.WriteLine("            <th>Actions</th>");
                writer.WriteLine("        </tr>");
                for (int i = 0; i < itemsCount; i++)    // We'll use i to index the List Items.
                {
                    int currentBookId = store.GetCustomer(custId).ShoppingCart.Items[i].BookId;
                    int currentBookCount = store.GetCustomer(custId).ShoppingCart.Items[i].Count;
                    decimal currentBookPrice = store.GetBook(currentBookId).Price;
                    writer.WriteLine("        <tr>");
                    writer.WriteLine("            <td><a href=\"/Books/Detail/{0}\">{1}</a></td>", currentBookId.ToString(), store.GetBook(currentBookId).Title);
                    writer.WriteLine("            <td>{0}</td>", currentBookCount.ToString());
                    if (currentBookCount == 1)
                    {
                        writer.WriteLine("            <td>{0} EUR</td>", currentBookPrice.ToString());
                        totalPrice += currentBookPrice;
                    }
                    else   // currentBookCount > 1
                    {
                        decimal currentTotal = currentBookCount * currentBookPrice;
                        totalPrice += currentTotal;
                        writer.WriteLine("            <td>{0} * {1} = {2} EUR</td>", currentBookCount.ToString(), currentBookPrice.ToString(), (currentTotal).ToString());
                    }                    
                    writer.WriteLine("            <td>&lt;<a href=\"/ShoppingCart/Remove/{0}\">Remove</a>&gt;</td>", currentBookId.ToString());
                    writer.WriteLine("        </tr>");
                }
                writer.WriteLine("    </table>");
                writer.WriteLine("    Total price of all items: {0} EUR", totalPrice.ToString());
            }
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
            ViewEnd(writer);
        }

        /// <summary>
        /// Prints html code for a website with a text: "Invalid request.".
        /// </summary>
        /// <param name="writer"></param>
        public void ViewInvalidRequest(TextWriter writer)
        {
            writer.WriteLine("<!DOCTYPE html>");
            writer.WriteLine("<html lang=\"en\" xmlns=\"http://www.w3.org/1999/xhtml\">");
            writer.WriteLine("<head>");
            writer.WriteLine("    <meta charset=\"utf-8\" />");
            writer.WriteLine("    <title>Nezarka.net: Online Shopping for Books</title>");
            writer.WriteLine("</head>");
            writer.WriteLine("<body>");
            writer.WriteLine("<p>Invalid request.</p>");
            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
            ViewEnd(writer);
        }
    }

    //
    // End of View
    //

    //
    // Control
    //

    class ControlStore
    {
        const string endOfInput = null;

        public void ControlInput(ModelStore store, TextReader reader, TextWriter writer)
        {
            string line = reader.ReadLine();
            while (line != endOfInput)
            {
                ControlCommand(store, writer, line);
                line = reader.ReadLine();
            }
        }

        public void ControlCommand(ModelStore store, TextWriter writer, string command)
        {
            ViewStore view = new ViewStore();
            const string websiteAddress = "http://www.nezarka.net";
            const char separator = ' ';
            string[] tokens = command.Split(separator);
            bool valid = false;     // Note: All following 'valid = false' commands aren't necessary. They're there just for clarity.
            try
            {
                if (tokens.Length == 3 && tokens[0] == "GET" && tokens[2].StartsWith(websiteAddress))
                {
                    tokens[2] = tokens[2].Remove(0, websiteAddress.Length);
                    int custId = int.Parse(tokens[1]);
                    int bookId;
                    if (store.GetCustomer(custId) == null)
                    {
                        // The customer isn't registered on our website. Invalid customer's ID.
                        valid = false;
                    }
                    else if (tokens[2].StartsWith("/Books"))
                    {
                        if (tokens[2].Equals("/Books"))
                        {
                            view.ViewBooks(store, writer, custId);
                            valid = true;
                        }
                        else if (tokens[2].StartsWith("/Books/Detail/"))
                        {
                            if (store.GetBook(bookId = int.Parse(tokens[2].Remove(0, "/Books/Detail/".Length))) == null)
                            {
                                // The book doesn't exist in the Eshop.
                                valid = false;
                            }
                            else
                            {
                                view.ViewBookDetails(store, writer, custId, bookId);
                                valid = true;
                            }
                        }
                    }
                    else if (tokens[2].StartsWith("/ShoppingCart"))
                    {
                        if (tokens[2].Equals("/ShoppingCart"))
                        {
                            view.ViewShoppingCart(store, writer, custId);
                            valid = true;
                        }
                        else if (tokens[2].StartsWith("/ShoppingCart/Add/"))
                        {
                            if (store.GetBook(bookId = int.Parse(tokens[2].Remove(0, "/ShoppingCart/Add/".Length))) == null)
                            {
                                // The book doesn't exist in the Eshop.
                                valid = false;
                            }
                            else
                            {
                                int index;
                                int itemsCount = store.GetCustomer(custId).ShoppingCart.Items.Count;
                                bool found = false;
                                // Trying to find the book in the shopping cart.
                                for (index = 0; index < itemsCount; index++)
                                {
                                    // The book is already in the shopping cart.
                                    if (store.GetCustomer(custId).ShoppingCart.Items[index].BookId == bookId)
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                                // The book has already been in the shopping cart.
                                if (found)
                                {
                                    store.GetCustomer(custId).ShoppingCart.Items[index].Count++;
                                }
                                // The book isn't in the shopping cart.
                                else
                                {
                                    store.GetCustomer(custId).ShoppingCart.Items.Add(new ShoppingCartItem
                                    {
                                        BookId = bookId,
                                        Count = 1
                                    });
                                }
                                view.ViewShoppingCart(store, writer, custId);
                                valid = true;
                            }
                        }
                        else if (tokens[2].StartsWith("/ShoppingCart/Remove/"))
                        {
                            if (store.GetBook(bookId = int.Parse(tokens[2].Remove(0, "/ShoppingCart/Remove/".Length))) == null)
                            {
                                // The book doesn't exist in the Eshop.
                                valid = false;
                            }
                            else
                            {
                                int index;
                                int itemsCount = store.GetCustomer(custId).ShoppingCart.Items.Count;
                                bool found = false;
                                // Trying to find the book in the shopping cart.
                                for (index = 0; index < itemsCount; index++)
                                {
                                    // The book has to already be in the shopping cart.
                                    if (store.GetCustomer(custId).ShoppingCart.Items[index].BookId == bookId)
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                                if (found)
                                {
                                    if (store.GetCustomer(custId).ShoppingCart.Items[index].Count == 1)
                                    {
                                        // Removing the book.
                                        store.GetCustomer(custId).ShoppingCart.Items.RemoveAt(index);
                                    }
                                    else   // The book is more than once in the shopping cart.
                                    {
                                        store.GetCustomer(custId).ShoppingCart.Items[index].Count--;
                                    }
                                    view.ViewShoppingCart(store, writer, custId);   // Needed to remove the book with this bookId!
                                    valid = true;
                                }
                                else
                                {
                                    // The book wasn't in the shopping cart so it cannot be removed from it.
                                    valid = false;
                                }
                            }
                        }
                    }

                }
            }
            catch (FormatException) // Because of Parse().
            {
                valid = false;
            }
            if (!valid)
            {
                view.ViewInvalidRequest(writer);
            }
        }
    }

    //
    // End of Control
    //



    // Program
    class Program
    {
        static void Main(string[] args)
        {
            TextReader tin = Console.In;
            TextWriter tout = Console.Out;

            ModelStore myStore = ModelStore.LoadFrom(tin);
            if (myStore == null)    // There was an error in the input data.
            {
                Console.Write("Data error.");
            }
            else   // The input data was alright.
            {
                ControlStore myControl = new ControlStore();
                myControl.ControlInput(myStore, tin, tout);
            }
        }
    }
}