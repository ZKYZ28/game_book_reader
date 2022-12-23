using GBReaderMahyF.Domains;
using NUnit.Framework;

namespace GBReaderMahyF.Tests.Domains;

public class IsbnTests
{
    [Test]
    public void IsbnConstruct()
    {
        Isbn isbn = new Isbn("2-210208-01-7");
        Assert.That(isbn.IsbnNumber(), Is.EqualTo("2-210208-01-7"));
    }
    
    [Test]
    public void IsbnConstructBadLastNumber()
    {
        Isbn isbn = new Isbn("2-210208-01-8");
        Assert.That(isbn.IsbnNumber(), Is.EqualTo("0-000000-00-0"));
    }
    
    [Test]
    public void IsbnConstructBadFormat()
    {
        Isbn isbn = new Isbn("2-210208-01-");
        Assert.That(isbn.IsbnNumber(), Is.EqualTo("0-000000-00-0"));
    }
    
    [Test]
    public void IsbnConstructBadSize()
    {
        Isbn isbn = new Isbn("2-210208");
        Assert.That(isbn.IsbnNumber(), Is.EqualTo("0-000000-00-0"));
    }
}