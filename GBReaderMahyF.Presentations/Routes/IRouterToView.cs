using GBReaderMahyF.Presentations.Notification;

namespace GBReaderMahyF.Presentations.Routes;

public interface IRouterToView
{
    /// <summary>
    /// Méthode qui permet de passer d'une page à l'autre 
    /// </summary>
    /// <param name="pageName">string qui est le nom de la page vers laquelle on souhaite se rendre</param>
    void GoTo(string pageName);
}