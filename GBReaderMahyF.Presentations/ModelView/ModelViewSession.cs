using GBReaderMahyF.Domains;

namespace GBReaderMahyF.Presentations.ModelView;

/// <summary>
/// Représente une Session sous la forme d'un ModelView
/// </summary>
public record ModelViewSession
{
    private readonly string _numLastPage;
    private readonly string _startReadingDate;
    private readonly string _endReadingDate;

    /// <summary>
    /// Constructeur de ModelViewSession
    /// </summary>
    /// <param name="session">Session qui est la session que l'on veut transformer en ModelViewSession</param>
    public ModelViewSession(Session session)
    {
        this._numLastPage = session.NumSessionPage.ToString();
        this._startReadingDate = session.StartReadingDate.ToString();
        this._endReadingDate = session.EndReadingDate.ToString();
    }
    
    public string NumSessionPage
        => this._numLastPage;
    
    public string EndReadingDate
        => this._endReadingDate;
    
    public string StartReadingDate
        => this._startReadingDate;
}