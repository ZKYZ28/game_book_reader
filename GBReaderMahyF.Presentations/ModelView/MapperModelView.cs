using GBReaderMahyF.Domains;

namespace GBReaderMahyF.Presentations.ModelView;

public static class MapperModelView
{
    
    /// <summary>
    /// Méthode qui permet de convertir une liste de Book en en liste de ModelViewBook
    /// </summary>
    /// <param name="listBook">List<Book> qui est la liste de Book que l'on veut transformer</param>
    /// <returns>List<ModelViewBook> la liste de Book transformée en ModelViewBook</returns>
    public static List<ModelViewBook> ConvertListBookToListBookMv(List<Book?> listBook)
    {
        List<ModelViewBook> listBookMv = new List<ModelViewBook>();

        foreach (var book in listBook)
        {
            listBookMv.Add(new ModelViewBook(book));
        }

        return listBookMv;
    }

    /// <summary>
    /// Méthode qui permet de convertir une dictionnaire de Session en un dictionnaire de ModelViewSession
    /// </summary>
    /// <param name="sessions">(Dictionary<string, Session> qui est le dictionnaire de toutes les sessions, key => numIsbn du livre, value => session liée au livre</param>
    /// <returns>Dictionary<string, ModelViewSession> qui est le dictionnaire de Session convertit en ModelViewSession</returns>
    public static Dictionary<string, ModelViewSession> ConvertSessionsToSessionsMv(Dictionary<string, Session> sessions)
    {
        Dictionary<string, ModelViewSession> sessionsModelView = new Dictionary<string, ModelViewSession>();
        
        foreach(KeyValuePair<string, Session> entry in sessions)  
        {
            sessionsModelView.Add(entry.Key, new ModelViewSession(entry.Value));
        }

        return sessionsModelView;
    }
}