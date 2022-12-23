namespace GBReaderMahyF.Presentations.Events;


/// <summary>
/// Argument => Lorsqye l'on cherche un livre
/// </summary>
/// <param name="ToFind">string qui est ce que l'on cherche</param>
public record SearchBookArgs(string ToFind);