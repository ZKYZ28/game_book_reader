using GBReaderMahyF.Domains;
using NUnit.Framework;

namespace GBReaderMahyF.Tests.Domains;

public class ChoiceTests
{
    [Test]
    public void ChoiceConstruct()
    {
        Choice choice = new Choice( "Texte", 2);
        
        Assert.That(choice.TextChoice, Is.EqualTo("Texte"));
        Assert.That(2, Is.EqualTo(choice.NumGoPage));
    }
}