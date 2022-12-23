using GBReaderMahyF.Domains;
using NUnit.Framework;
namespace GBReaderMahyF.Tests.Domains;


public class AuthorTests
{
    [Test]
    public void AuthorConstruct()
    {
        Author author = new Author("Francis", "Ma");
        Assert.That(author.GetFullName(), Is.EqualTo("Francis Ma"));
    }
}