using GBReaderMahyF.Domains;
using NUnit.Framework;

namespace GBReaderMahyF.Tests.Domains;

public class LibraryBooksTests
{
    [Test]
    public void CheckIfPageAlreadyExistTrue()
         {
             List<Page?> listPage = new List<Page?>();
             listPage.Add(new Page(1, "Blabla"));
             Book? book = new Book("Titre", new Author("Francis", "Mahy"), new Isbn("2-210208-01-8"), "Résumé");
             book.ListPage = listPage;
             
             LibraryBooks libraryBooks = new LibraryBooks();
             libraryBooks.AddBook(book.Isbn.IsbnNumber(), book);

            Assert.True(libraryBooks.CheckIfPageAlreadyExist(book.Isbn.IsbnNumber()));
         }
    
    [Test]
    public void CheckIfPageAlreadyExistFalse()
    {
        Book? book = new Book("Titre", new Author("Francis", "Mahy"), new Isbn("2-210208-01-8"), "Résumé");
        
        LibraryBooks libraryBooks = new LibraryBooks();
        libraryBooks.AddBook(book.Isbn.IsbnNumber(), book);
        
        Assert.False(libraryBooks.CheckIfPageAlreadyExist(book.Isbn.IsbnNumber()));
    }
    
    [Test]
    public void GetBookOnIsbn()
    {
        Book? book = new Book("Titre de mon super livre", new Author("Francis", "Mahy"), new Isbn("2-210208-01-7"), "Résumé");
        
        LibraryBooks libraryBooks = new LibraryBooks();
        libraryBooks.AddBook(book.Isbn.IsbnNumber(), book);
        
        Assert.That("Titre de mon super livre", Is.EqualTo(libraryBooks.GetBookOnIsbn("2-210208-01-7").Title));
    }
    
    [Test]
    public void FindBooksOnTitle()
    {
        Book? book = new Book("Titre de mon super livre", new Author("Francis", "Mahy"), new Isbn("2-210208-01-7"), "Résumé");
        Book? book2 = new Book("Deuxième", new Author("Charles", "Pasqua"), new Isbn("2-667667-02-5"), "Yep");
        LibraryBooks libraryBooks = new LibraryBooks();
        libraryBooks.AddBook(book.Isbn.IsbnNumber(), book);
        libraryBooks.AddBook(book2.Isbn.IsbnNumber(), book2);

        List<Book?> listBookFind = libraryBooks.FindBooks("Deux");
        
        Assert.That("Deuxième", Is.EqualTo(listBookFind[0].Title));
    }
    
    [Test]
    public void FindBooksOnIsbn()
    {
        Book? book = new Book("Titre de mon super livre", new Author("Francis", "Mahy"), new Isbn("2-210208-01-7"), "Résumé");
        Book? book2 = new Book("Deuxième", new Author("Charles", "Pasqua"), new Isbn("2-210208-02-5"), "Yep");
        LibraryBooks libraryBooks = new LibraryBooks();
        libraryBooks.AddBook(book.Isbn.IsbnNumber(), book);
        libraryBooks.AddBook(book2.Isbn.IsbnNumber(), book2);
        
        List<Book?> listBookFind = libraryBooks.FindBooks("2-210208-01-7");
        
        Assert.That("2-210208-01-7", Is.EqualTo(listBookFind[0].Isbn.IsbnNumber()));
    }
    
        
    [Test]
    public void FindBooksMultiple()
    {
        Book? book = new Book("Titre Deuxième de mon super livre", new Author("Francis", "Mahy"), new Isbn("2-210208-01-7"), "Résumé");
        Book? book2 = new Book("Deuxième", new Author("Charles", "Pasqua"), new Isbn("2-210208-02-5"), "Yep");
        Book? book3 = new Book("Charles et les lugumes", new Author("Charles", "Pasqua"), new Isbn("2-210208-03-3"), "Non");
        LibraryBooks libraryBooks = new LibraryBooks();
        libraryBooks.AddBook(book.Isbn.IsbnNumber(), book);
        libraryBooks.AddBook(book2.Isbn.IsbnNumber(), book2);
        libraryBooks.AddBook(book3.Isbn.IsbnNumber(), book3);
        
        List<Book?> listBookFind = libraryBooks.FindBooks("Deux");
        
        Assert.That("Titre Deuxième de mon super livre", Is.EqualTo(listBookFind[0].Title));
        Assert.That("Deuxième", Is.EqualTo(listBookFind[1].Title));
    }
    
    [Test]
    public void FindBooksWithEmptySearch()
    {
        Book? book = new Book("Titre Deuxième de mon super livre", new Author("Francis", "Mahy"), new Isbn("2-210208-01-7"), "Résumé");
        Book? book2 = new Book("Deuxième", new Author("Charles", "Pasqua"), new Isbn("2-210208-02-5"), "Yep");
        Book? book3 = new Book("Charles et les lugumes", new Author("Charles", "Pasqua"), new Isbn("2-210208-03-3"), "Non");
        LibraryBooks libraryBooks = new LibraryBooks();
        libraryBooks.AddBook(book.Isbn.IsbnNumber(), book);
        libraryBooks.AddBook(book2.Isbn.IsbnNumber(), book2);
        libraryBooks.AddBook(book3.Isbn.IsbnNumber(), book3);
        
        List<Book?> listBookFind = libraryBooks.FindBooks("");
        
        Assert.That("Titre Deuxième de mon super livre", Is.EqualTo(listBookFind[0].Title));
        Assert.That("Deuxième", Is.EqualTo(listBookFind[1].Title));
        Assert.That("Charles et les lugumes", Is.EqualTo(listBookFind[2].Title));
    }
}