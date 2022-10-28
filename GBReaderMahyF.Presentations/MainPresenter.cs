using System;
using GBReaderMahyF.Respositories;
using GBReaderMahyF.Domains;

namespace GBReaderMahyF.Presentations
{
    public class MainPresenter
    {
        private IView _mainWindow;
        private IManagerRepository IRepo;
        
        /// <summary>
        /// Constructeur du MainPresenter
        /// </summary>
        /// <param name="mainWindow">IView qui est l'Interface implémentée par mainWindow</param>
        /// <param name="IRepo">IRepo qui est l'Interface implémentée par ManagerRepository </param>
        public MainPresenter(IView mainWindow, IManagerRepository IRepo)
        {
            mainWindow.setPresenter(this);
            this._mainWindow = mainWindow;
            this.IRepo = IRepo;
            SetupMainWindow();
        }

        public IManagerRepository getIReop()
        {
            return this.IRepo;
        }
        
        /// <summary>
        ///Méthode qui permet de mettre en place la mainWindow
        /// Si il y a des erreurs elles seront affichées et l'application sera quittée après 3secondes
        /// Sinon on affichera l'ensemble des livres lus ainsi que la vue détaillée du premier livre
        /// </summary>
        private void SetupMainWindow()
        {
            if (!FindError())
            {
                _mainWindow.DisplayAllBooks(IRepo.ReadBooks());
                _mainWindow.DisplayFirstDetailBook();
            }
            else
            {
                Task.Delay(3000).ContinueWith(t =>  Environment.Exit(0));
            }
        }

        /// <summary>
        /// Méthode qui permet de rechercher un livre sur base d'une chaine de caractères et de les afficher
        /// Si toFind est une chaine vide alors on réaffichera tous les livres
        /// </summary>
        /// <param name="toFind">String qui est ce que l'on souhaite rechercher</param>
        public void SearchBook(String toFind)
        {
            if (toFind.Equals(""))
            {
                _mainWindow.DisplayAllBooks(IRepo.ReadBooks());
            }
            else
            {
                List<Book> booksList = FindBooks(toFind);
                if (booksList.Capacity == 0)
                {
                    _mainWindow.DisplayError( "Aucun résultat");
                }
                _mainWindow.DisplayAllBooks(booksList);
            }
        }

        /// <summary>
        /// Méthode qui permet de chercher les livres correspondant à la recherche encodée par l'utilisateur
        /// </summary>
        /// <param name="toFind">String qui est ce que l'on souhaite rechercher</param>
        /// <returns></returns>
        private List<Book> FindBooks(String toFind)
        {
            List<Book> findBooks = new List<Book>();
            foreach (var book in IRepo.ReadBooks())
            {
                String bookTitle = book.getTitle().ToUpper();
                toFind = toFind.ToUpper().Trim();
                if (bookTitle.Contains(toFind) || book.getIsbn().getIsbnNumber().Equals(toFind))
                {
                    findBooks.Add(book);
                }
            }
            return findBooks;
        }
        
        /// <summary>
        /// Méthode qui permet de vérifier que le fichier que le fichier existe bien et si c'est le cas qu'il n'est pas vide.
        /// En cas d'erreur il affichera un message
        /// </summary>
        /// <returns>Boolean, true si une erreur est trouvée sinon false</returns>
        public Boolean FindError()
        {
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "ue36", "q210208.json");
            
            if (!File.Exists(filePath))
            {
                _mainWindow.DisplayError( "/!\\ Le fichier n'existe pas /!\\ ");
                return true;
            }
            
            if(File.ReadAllText(filePath) == "")
            {
                _mainWindow.DisplayError("/!\\ Le fichier est vide /!\\ ");
                return true;
            }

            return false;
        }
    }
}