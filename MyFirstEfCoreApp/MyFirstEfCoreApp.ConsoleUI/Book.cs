using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstEfCoreApp.ConsoleUI
{
    [Table("Books")]
    public class Book
    {
        public int BookId { get; set; } //#A

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedOn { get; set; }
        public int AuthorId { get; set; } //#B


        public Author Author { get; set; } //#C
    }

    /*********************************************************
    #A This holds the Primary Key of the Books row in the database
    #B This holds the Foreign Key which references the Author row holding the name of the author
    #C EF Core will create a link to the Author class using the AuthroId foreign key
    * **********************************************************/
}