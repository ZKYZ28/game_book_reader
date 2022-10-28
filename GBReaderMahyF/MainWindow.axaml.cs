using System;
using Avalonia.Controls;
using GBReaderMahyF.Domains;
using System.Collections.Generic;
using Avalonia.Interactivity;
using GBReaderMahyF.Presentations;

namespace GBReaderMahyF
{
    public partial class MainWindow : Window, IDetailBookListener, IView
    {
        private TextBlock _errorMessage;
        private WrapPanel _booksContainer;
        private StackPanel _detailBookContainer;
        private TextBox _searchText;
        private TextBlock _detailTitle;
        
        private MainPresenter p;
        
        public MainWindow()
        {
            InitializeComponent();
            LocateControls();
        }

        public void setPresenter(MainPresenter mainPresenter)
        {
            this.p = mainPresenter;
        }

        private void LocateControls()
        {
            _searchText = this.FindControl<TextBox>("SearchText");
            _errorMessage = this.FindControl<TextBlock>("ErrorMessage");
            _booksContainer = this.FindControl<WrapPanel>("BooksContainer");
            _detailBookContainer = this.FindControl<StackPanel>("DetailBookContainer");
            _detailTitle = this.FindControl<TextBlock>("DetailTitle");
        }

        public void DisplayAllBooks(List<Book> BooksList)
        {
            foreach (var book in BooksList)
            {
                BookView bookV = new BookView();
                bookV.SetBookInformation(book);
                bookV.SetListener(this);
                _booksContainer.Children.Add(bookV);
            }
        }

        public void DisplayFirstDetailBook()
        {
            _detailTitle.IsVisible = true;
            DetailBookView detailBookView = new DetailBookView();
            detailBookView.SetBookInformation(p.getIReop().ReadBooks()[0]);
            _detailBookContainer.Children.Add(detailBookView); 
        }
        
        public void Search_OnClick(object? sender, RoutedEventArgs e)
        {
            _errorMessage.Text = "";
            _booksContainer.Children.Clear();
            p.SearchBook(_searchText.Text);
        }

        public void DisplayError(String error)
        {
            _errorMessage.Text = error;
            _errorMessage.IsVisible = true;
        }
        
        public void OnDetailBookSelected(object sender, Book book)
        {
            _detailBookContainer.Children.Clear();
            DetailBookView detailBookView = new DetailBookView();
            detailBookView.SetBookInformation(book);
            _detailBookContainer.Children.Add(detailBookView); 
        }
    }
}