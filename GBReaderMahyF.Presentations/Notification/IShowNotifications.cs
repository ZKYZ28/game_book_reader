namespace GBReaderMahyF.Presentations.Notification;

public interface IShowNotifications
{
    /// <summary>
    /// Méthode qui permet d'afficher une notification à l'utilisateur
    /// </summary>
    /// <param name="serverity">NotificationSeverity qui est le type de notification que l'on veut afficher</param>
    /// <param name="title">string qui va être le titre de la notification</param>
    /// <param name="message">string qui va être le message de la notification</param>
    void Push(NotificationSeverity serverity, string title, string message);
}
