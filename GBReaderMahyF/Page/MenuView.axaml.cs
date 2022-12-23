using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using GBReaderMahyF.Presentations;
using GBReaderMahyF.Presentations.Events;
using GBReaderMahyF.Presentations.ModelView;

namespace GBReaderMahyF.Page;

  public partial class MenuView : UserControl, IMenuView
    {
        /// <summary>
        /// Constructeur de la MenuView
        /// </summary>
        public MenuView()
        {
            InitializeComponent(true);
        }
        
        /// <summary>
        /// Méthode qui permet d'afficher tous les livres
        /// </summary>
        /// <param name="booksList">List<ModelViewBook> qui est la list de tous les livres que l'on souhaite afficher</param>
        public void DisplayAllBooks(List<ModelViewBook> booksList)
        {
            foreach (var bookMv in booksList)
            {
                BookView bookV = new BookView();
                bookV.SetBookInformation(bookMv);
                bookV.OnBookClick += UpdateDetailBookEvent;
                BooksContainer.Children.Add(bookV);
            }
        }
        
        /// <summary>
        /// Méthode déclanchée lorsque l'on clique sur la cover d'un livre. Cette méthode va invoquer SwitchDetailBook. 
        /// </summary>
        /// <param name="sender">BookView qui est une la vie d'une cover de livre</param>
        /// <param name="bookMv">ModelViewBook Objet Book sous la forme d'une ModelView</param>
        private void UpdateDetailBookEvent(object? sender, ModelViewBook bookMv)
        {
            this.SwitchDetailBook?.Invoke(sender,new SwitchDetailBookArgs(bookMv));
        }

        /// <summary>
        /// Méthode qui permet de mettre à jour toute la vue détaillée d'un livre
        /// </summary>
        /// <param name="bookMv">ModelViewBook qui est le livre que l'on afficher dans la vue détaillée</param>
        /// <param name="textButton">string qui est la statut du livre</param>
        public void UpdateDetailBook(ModelViewBook bookMv)
        {
            this.Title.Text = bookMv.Title;
            this.Author.Text = bookMv.Author;
            this.Isbn.Text = bookMv.Isbn;
            this.Resume.Text = bookMv.Resume;
        }

        /// <summary>
        /// Méthode qui permet de changer le statut du livre à l'affichage
        /// </summary>
        /// <param name="textButton">string qui est la statut du livre</param>
        public void UpdateButtonDetailBook(string textButton)
        {
            this.StartOResumeButton.Content = textButton;
        }
        
        /// <summary>
        /// Méthode qui permet d'afficher un message d'erreur à l'utilisateur
        /// </summary>
        /// <param name="error">String qui est le messae d'erreur que l'on souhaite afficher</param>
        public void DisplayError(String error)
        {
            ErrorMessage.Text = error;
            ErrorMessage.IsVisible = true;
        }

        /// <summary>
        /// Méthode délanchée lorsque l'on appuie sur le bouton qui permet de rechercher un/des livre.s
        /// </summary>
        /// <param name="sender">Button qui permet de rechercher un/des livre.s</param>
        /// <param name="e">Les arguments de l'évenement</param>
        public void Search_OnClick(object? sender, RoutedEventArgs e)
        {
            ErrorMessage.Text = "";
            BooksContainer.Children.Clear();
            SearchBook?.Invoke(sender, new SearchBookArgs(SearchText.Text));
        }

        /// <summary>
        /// Méthode déclanchée lorsque l'on appuie sur le bouton qui permet de se rendre sur la page des statistiques
        /// </summary>
        /// <param name="sender">Button qui permet d'aller sur la page des statistiques</param>
        /// <param name="e">Les arguments de l'évenement</param>
        public void Statistics_OnClick(object? sender, RoutedEventArgs e)
        {
            SeeStatistics?.Invoke(sender, e);
        }

        /// <summary>
        /// Méthode délanchée lorsque l'on appuie sur le bouton qui permet de commencer un livre
        /// </summary>
        /// <param name="sender">Button qui permet de commencer la lecture d'un livre</param>
        /// <param name="e">Les arguments de l'évenement</param>
        public void StartBook_OnClick(object? sender, RoutedEventArgs e)
        {
            StartReadingBook?.Invoke(sender, new StartReadingArgs(Isbn.Text));
        }

        public event EventHandler<SearchBookArgs> SearchBook;
        public event EventHandler<StartReadingArgs> StartReadingBook;
        public event EventHandler SeeStatistics;
        public event EventHandler<SwitchDetailBookArgs> SwitchDetailBook;
    }
