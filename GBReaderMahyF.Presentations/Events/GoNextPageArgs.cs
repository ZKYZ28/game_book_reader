namespace GBReaderMahyF.Presentations.Events;


/// <summary>
/// Argument => Lorsqye l'on va à la page suivante
/// </summary>
/// <param name="IndexChoice">int qui est l'index du choix choisit</param>
public record GoNextPageArgs(int IndexChoice);