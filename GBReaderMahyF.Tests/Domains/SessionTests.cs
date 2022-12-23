using GBReaderMahyF.Domains;
using NUnit.Framework;

namespace GBReaderMahyF.Tests.Domains;

public class SessionTests
{
    [Test]
    public void SessionConstruct()
    {
        Session session = new Session(3, DateTime.MinValue, DateTime.MinValue);
        
        Assert.That(3, Is.EqualTo(session.NumSessionPage));
        Assert.That(DateTime.MinValue, Is.EqualTo(session.StartReadingDate));
        Assert.That(DateTime.MinValue, Is.EqualTo(session.EndReadingDate));
    }
    
    
    [Test]
    public void UpdateSession()
    {
        Session session = new Session(3, DateTime.MinValue, DateTime.MinValue);
        
        Assert.That(3, Is.EqualTo(session.NumSessionPage));
        Assert.That(DateTime.MinValue, Is.EqualTo(session.StartReadingDate));
        Assert.That(DateTime.MinValue, Is.EqualTo(session.EndReadingDate));
        
        session.UpdateSession(4, DateTime.MaxValue);
        Assert.That(4, Is.EqualTo(session.NumSessionPage));
        Assert.That(DateTime.MaxValue, Is.EqualTo(session.EndReadingDate));
    }
}