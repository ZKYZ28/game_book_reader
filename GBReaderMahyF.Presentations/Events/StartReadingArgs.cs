namespace GBReaderMahyF.Presentations.Events;


/// <summary>
/// Argument => Lorsque l'on commence la lecture d'un livre
/// </summary>
/// <param name="Isbn">string qui est le numéro isbn du livre que l'on souhaite lire</param>
public record StartReadingArgs(string Isbn);