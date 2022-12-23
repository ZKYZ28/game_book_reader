using System.Data;
using System.Data.SqlClient;
using GBReaderMahyF.Domains;
using GBReaderMahyF.Respositories;
using GBReaderMahyF.Respositories.Exception;
using MySql.Data.MySqlClient;

namespace GBReaderMahyF.Infrastructures.BD;

public class SqlBookStorage : IDisposable, IStorage
{
    private readonly ManagerReader _manager;
    
    private const string LOAD_COVER_BOOK = @"SELECT b.title, b.isbn, b.description, a.firstName, a.name, a.matricule FROM BOOK b JOIn AUTHOR a on b.matricule = a.matricule WHERE b.isPublished = 1;";

    private const string LOAD_PAGES = @"SELECT * FROM PAGE WHERE idBook = @idBook";
        
    private const string LOAD_ID = @"SELECT idBook FROM BOOK WHERE isbn LIKE @isbn";
        
    private const string LOAD_CHOICES = @"SELECT * FROM CHOICE WHERE numFromPage = @numFromPage AND idBook = @idBook";
        
    private readonly IDbConnection _con;

        /// <summary>
        /// Constructeur de SqlBookStorage
        /// </summary>
        /// <param name="con">IDbConnection qui est le connection à la base de données</param>
        /// <param name="manager">ManagerReader qui est le manager de l'application</param>
        public SqlBookStorage(IDbConnection con, ManagerReader manager)
        {
            this._con = con;
            this._manager = manager;
        }

        /// <summary>
        /// Méhtode qui permet de load la cover des livres depuis la base de données
        /// </summary>
        /// <exception cref="SqlStorageException">Exception lancée lorsqu'une exception liée de près ou de loin à la BD est lancée</exception>
        public void LoadCoverBook()
        {
            try
            {
                using IDbCommand selectCommand = _con.CreateCommand();
                selectCommand.CommandText = LOAD_COVER_BOOK;

                using IDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    string title = (string)reader["title"];
                    string firstName = (string)reader["firstName"];
                    string name = (string)reader["name"];
                    Isbn isbn = new Isbn((string)reader["isbn"]);
                    string resume = (string)reader["description"];

                    this._manager.LibraryBooks.AddBook(isbn.IsbnNumber(), 
                        new Book(title, new Author(firstName, name), isbn, resume));
                }
            }
            catch (SqlException ex)
            {
                throw new SqlStorageException("Erreur lors du chargement des livres depuis la base de données.", ex);
            }
            catch (MySqlException ex)
            {
                throw new SqlStorageException("Erreur lors du chargement des livres depuis la base de données.", ex);
            }
        }

        /// <summary>
        /// Méthode qui permet de load les pages d'un livre depuis la base de données
        /// </summary>
        /// <param name="isbn">string qui est le numéro isbn du livre dont on souhaite lire les pages</param>
        /// <exception cref="SqlStorageException">Exception lancée lorsqu'une exception liée de près ou de loin à la BD est lancée</exception>
        public void LoadPages(string isbn)
        {
            try
            {
                int idBook = LoadIdOnIsbn(isbn);
                List<Page?> listPages = new List<Page?>();
                using(IDbCommand selectCommand = _con.CreateCommand())
                {
                    selectCommand.CommandText = LOAD_PAGES;

                    var nameParam = selectCommand.CreateParameter();

                    nameParam.ParameterName = "@idBook";
                    nameParam.Value = idBook;
                    nameParam.DbType = DbType.String;

                    selectCommand.Parameters.Add(nameParam);

                    using (IDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int numPage = (int)reader["numPage"];
                            string textPage = (string)reader["textPage"];
                            listPages.Add(new Page(numPage, textPage));
                        }
                    }

                    AddChoiceToPage(idBook, listPages);
                    _manager.LibraryBooks.GetBookOnIsbn(isbn)!.ListPage = listPages;
                }
            }
            catch (SqlException ex)
            {
                throw new SqlStorageException("Erreur lors du chargement des pages depuis la base de données.", ex);
            }
            catch (MySqlException ex)
            {
                throw new SqlStorageException("Erreur lors du chargement des pages depuis la base de données.", ex);
            }
        }

        /// <summary>
        /// Méthode qui permet d'ajouter les choix à une page
        /// </summary>
        /// <param name="idBook">int qui est l'id du livre qui contient les pages auxquelles on veut ajouter les choix</param>
        /// <param name="listPage">List<Page> qui est la liste de toutes les pages d'un livre</param>
        private void AddChoiceToPage(int idBook, List<Page?> listPage)
        {
            foreach (var page in listPage)
            {
                page!.ListChoices = LoadChoices(idBook, page.NumPage);
            }
        }

        /// <summary>
        /// Méthode qui permet de load les choix d'une page
        /// </summary>
        /// <param name="idBook">int qui est l'id du livre qui contient la page</param>
        /// <param name="numPage">int qui est le numéro de la page à laquelle on veut ajouter les choix</param>
        /// <exception cref="SqlStorageException">Exception lancée lorsqu'une exception liée de près ou de loin à la BD est lancée</exception>
        private List<Choice> LoadChoices(int idBook, int numPage)
        {
            try
            {
                List<Choice> listChoices = new List<Choice>();
                using(IDbCommand selectCommand = _con.CreateCommand())
                {
                    selectCommand.CommandText = LOAD_CHOICES;

                    var nameParam = selectCommand.CreateParameter();
                    
                    nameParam.ParameterName = "@numFromPage";
                    nameParam.Value = numPage;
                    nameParam.DbType = DbType.Int64;
                    
                    selectCommand.Parameters.Add(nameParam);
                    
                    var nameParam2 = selectCommand.CreateParameter();
                    nameParam2.ParameterName = "@idBook";
                    nameParam2.Value = idBook;
                    nameParam2.DbType = DbType.String;

                    selectCommand.Parameters.Add(nameParam2);

                    using (IDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string textChoice = (string)reader["textChoice"];
                            int numGopage = (int)reader["numGopage"];
                            
                            listChoices.Add(new Choice(textChoice, numGopage));
                        }
                    }
                    return listChoices;
                }
            }
            catch (SqlException ex)
            {
                throw new SqlStorageException("Erreur lors du chargement des choix depuis la base de données.", ex);
            }
            catch (MySqlException ex)
            {
                throw new SqlStorageException("Erreur lors du chargement des choix depuis la base de données.", ex);
            }
        }

        /// <summary>
        /// Méthode qui permet de load l'id d'un livre sur base de son numéro isbn
        /// </summary>
        /// <param name="isbn">string qui est le numéro isbn du livre</param>
        /// <returns>int qui est l'id du livre qui correspond au numéro isbn fourni</returns>
        /// <exception cref="SqlStorageException">Exception lancée lorsqu'une exception liée de près ou de loin à la BD est lancée</exception>
        private int LoadIdOnIsbn(string isbn)
        {
            try
            {
                int id = 0;
                using(IDbCommand selectCommand = _con.CreateCommand()) 
                {
                    selectCommand.CommandText = LOAD_ID;

                    var nameParam = selectCommand.CreateParameter();
                    
                    nameParam.ParameterName = "@isbn";
                    nameParam.Value = isbn;
                    nameParam.DbType = DbType.String;

                    selectCommand.Parameters.Add(nameParam);
                    
                    using (IDataReader reader = selectCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = (int)reader["idBook"];
                        }
                    }

                    if (id == 0)
                    {
                        throw new SqlStorageException("Ce livre n'existe pas dans la base de données", new Exception());
                    }
                    
                    return id;
                }
            }
            catch (SqlException ex)
            {
                throw new SqlStorageException("Erreur lors du chargement du livre depuis la base de données.", ex);
            }
            
            catch (MySqlException ex)
            {
                throw new SqlStorageException("Erreur lors du chargement du livre depuis la base de données.", ex);
            }
        }

        /// <summary>
        /// Méthode qui permet de fermer la connexion à la base de données
        /// </summary>
        public void Dispose()
        { 
            _con.Dispose();
        }
    }
