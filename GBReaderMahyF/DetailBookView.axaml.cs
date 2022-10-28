using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using GBReaderMahyF.Domains;

namespace GBReaderMahyF;

public partial class DetailBookView : UserControl
{
    private TextBlock _title;
    private TextBlock _author;
    private TextBlock _isbn;
    private TextBlock _resume;
    private Button _startOrResumeButton;
    
    public DetailBookView()
    {
        InitializeComponent();
        LocateControls();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void LocateControls()
    {
        _title = this.FindControl<TextBlock>("Title");
        _author = this.FindControl<TextBlock>("Author");
        _isbn = this.FindControl<TextBlock>("Isbn");
        _resume = this.FindControl<TextBlock>("Resume");
        _startOrResumeButton = this.FindControl<Button>("StartOResumeButton");
    }
    
    
    public void SetBookInformation(Book book)
    {
        _title.Text = book.getTitle();
        _author.Text = book.getAuthor().getFullName();
        _isbn.Text = book.getIsbn().getIsbnNumber();
        _resume.Text = book.getResume();
    }
}