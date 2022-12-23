using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GBReaderMahyF.Presentations.ModelView;

namespace GBReaderMahyF.Control;

public partial class StatisticView : UserControl
{
    /// <summary>
    /// Constucteur de la StatisticView
    /// </summary>
    public StatisticView()
    {
        InitializeComponent(true);
    }

    /// <summary>
    /// Méthode qui permet de mettre à jour les inforamtions affichées pour une statistic
    /// </summary>
    /// <param name="isbn">String qui est le numéro isbn du libre</param>
    /// <param name="session">ModelViewSession qui est la session du livre en question</param>
    public void SetStatisticInformation(string isbn, ModelViewSession session)
    {
        IsbnBook.Text = isbn;
        SessionStartDate.Text = session.StartReadingDate;
        SessionLastUpdateDate.Text = session.EndReadingDate;
        LastPage.Text = session.NumSessionPage;
    }
}