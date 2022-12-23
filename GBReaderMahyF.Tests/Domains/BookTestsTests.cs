using GBReaderMahyF.Domains;
using NUnit.Framework;

namespace GBReaderMahyF.Tests.Domains;

public class BookTests
{
    [Test]
    public void BookConstruct()
    {
        Book book = new Book("Titre", new Author("Francis", "Mahy"), new Isbn("2-210208-01-7"), "Résumé");
        Assert.That(book.Title, Is.EqualTo("Titre"));
        Assert.That(book.Author.GetFullName(), Is.EqualTo("Francis Mahy"));
        Assert.That(book.Isbn.IsbnNumber(), Is.EqualTo("2-210208-01-7"));
        Assert.That(book.Resume, Is.EqualTo("Résumé"));
    }

    [Test]
    public void CheckIfPageAlreadyExistTrue()
    {
        List<Page?> listPage = new List<Page?>();
        listPage.Add(new Page(1, "Blabla"));
        Book book = new Book("Titre", new Author("Francis", "Mahy"), new Isbn("2-210208-01-8"), "Résumé");
        book.ListPage = listPage;
        
        Assert.True(book.CheckIfPageAlreadyExist());
    }
    
    [Test]
    public void CheckIfPageAlreadyExistFalse()
    {
        Book book = new Book("Titre", new Author("Francis", "Mahy"), new Isbn("2-210208-01-8"), "Résumé");
        Assert.False(book.CheckIfPageAlreadyExist());
    }
    
    
    [Test]
    public void GetNextPage()
    {
        Page? page1 = new Page(1, "Blabla");
        List<Choice> listChoices = new List<Choice>();
        listChoices.Add(new Choice("choix1", 2));
        page1.ListChoices = listChoices;
        Page? page2 = new Page(2, "Bli");
        Page? page3 = new Page(3, "IOFehz");
        
        List<Page?> listPage = new List<Page?>();
        listPage.Add(page1);
        listPage.Add(page2);
        listPage.Add(page3);
        Book book = new Book("Titre", new Author("Francis", "Mahy"), new Isbn("2-210208-01-8"), "Résumé");
        book.ListPage = listPage;
        
        Assert.That(page2, Is.EqualTo(book.GetNextPage(0, 1)));
    }
    
    [Test]
    public void GetNextPageLastPage()
    {
        Page? page1 = new Page(1, "Blabla");
        List<Choice> listChoices = new List<Choice>();
        listChoices.Add(new Choice("choix1", 2));
        page1.ListChoices = listChoices;
        Page? page2 = new Page(2, "Bli");

        List<Page?> listPage = new List<Page?>();
        listPage.Add(page1);
        listPage.Add(page2);
        Book book = new Book("Titre", new Author("Francis", "Mahy"), new Isbn("2-210208-01-8"), "Résumé");
        book.ListPage = listPage;
        
        Assert.That(page2, Is.EqualTo(book.GetNextPage(0, 1)));
    }
}