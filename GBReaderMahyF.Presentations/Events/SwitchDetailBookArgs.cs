using GBReaderMahyF.Presentations.ModelView;

namespace GBReaderMahyF.Presentations.Events;

/// <summary>
/// Argument => Lorsque l'on change la vue détaillée d'un livre
/// </summary>
/// <param name="BookMv">ModelViewBook qui est le livre que dont on souhaite afficher les détails sour la forme d'un ModelView</param>
public record SwitchDetailBookArgs(ModelViewBook BookMv);