using GBReaderMahyF.Domains;
using NUnit.Framework;

namespace GBReaderMahyF.Tests.Domains;

public class PageTests
{
    [Test]
    public void PageConstruct()
    {
        Page page = new Page(1, "SuperPage");
        
        Assert.That(1, Is.EqualTo(page.NumPage));
        Assert.That("SuperPage", Is.EqualTo(page.TextPage));
        Assert.That(0, Is.EqualTo(page.ListChoices.Count));
    }
    
    [Test]
    public void PageSetChoices()
    {
        List<Choice> listChoice = new List<Choice>();
        listChoice.Add(new Choice("textchoice",  2));
        Page page = new Page(1, "SuperPage");
        page.ListChoices = listChoice;
        
        Assert.That(1, Is.EqualTo(page.ListChoices.Count));
    }
    
    [Test]
    public void IsAEndPageFalse()
    {
        List<Choice> listChoice = new List<Choice>();
        listChoice.Add(new Choice( "textchoice",  2));
        Page page = new Page(1, "SuperPage");
        page.ListChoices = listChoice;
        
        Assert.False(page.IsAEndPage());
    }
    
    [Test]
    public void IsAEndPageTrue()
    {
        Page page = new Page(1, "SuperPage");

        Assert.True(page.IsAEndPage());
    }
}