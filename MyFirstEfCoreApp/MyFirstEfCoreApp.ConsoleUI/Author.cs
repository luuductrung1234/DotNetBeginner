using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstEfCoreApp.ConsoleUI
{
    [Table("Authors")]
    public class Author
    {
        public int AuthorId { get; set; } //#D
        public string Name { get; set; }
        public string WebUrl { get; set; }
    }

    /*******************************************************
    #D This holds the Primary Key of the Author row in the database
     * ************************************************/
}