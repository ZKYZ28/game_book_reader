using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using GBReaderMahyF.Domains;
using GBReaderMahyF.Infrastructures.BD;
using GBReaderMahyF.Respositories.Exception;
using NUnit.Framework;
using Moq;
namespace GBReaderMahyF.Tests.Infrastructures.BD;

public class SqlBookStorageTests
{
    private static string DbConnectionString2 = Path.GetFullPath(Path.Combine("..", "..", "..", "..", "GBReaderMahyF.Tests", "Infrastructures", "Ressources", "DBTests.mdf"));
    private static string DbConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""" + DbConnectionString2 +  @""";Integrated Security=True";
    private static DbProviderFactory factory;
    
    
    private IDbConnection NewConnection()
    {
        IDbConnection con = factory.CreateConnection();
        String s = Directory.GetCurrentDirectory();
        
        con.ConnectionString = DbConnectionString;
        con.Open();

        return con;
    }
    

    [OneTimeSetUp]
    public static void BeforeAll()
    {
        DbProviderFactories.RegisterFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);
        factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
    }

    [Test]
    public void AutoClosesHisConnection()
    {
        Mock<IDbConnection> con = new Mock<IDbConnection>();

        using (SqlBookStorage storage = new SqlBookStorage(con.Object, new ManagerReader()))
        {

        }
        con.Verify(c => c.Dispose(), Times.Once);
    }

    [Test]
    public void LoadCoverBook()
    {
        ManagerReader manager = new ManagerReader();
        using (SqlBookStorage storage = new SqlBookStorage(NewConnection(), manager))
        {
            storage.LoadCoverBook();
            Book? book = manager.LibraryBooks.LibraryBooksAssociation["2-210208-01-7"];
            Assert.That(new Author( "Francois", "Mahy"), Is.EqualTo(book.Author));
            Assert.That("Titre du livre", Is.EqualTo(book.Title));
            Assert.That("2-210208-01-7", Is.EqualTo(book.Isbn.IsbnNumber()));
            Assert.That("Ma description", Is.EqualTo(book.Resume));
            Assert.True(0 == book.ListPage.Count);
        }
    }
    
    
    [Test]
    public void LoadPagesWithoutChoices()
    {
        ManagerReader manager = new ManagerReader();
        manager.LibraryBooks.AddBook("2-210208-02-8", new Book("Mon deuxième livre", new Author("François", "Mahy"), new Isbn("2-210208-02-8"), "Ma deuxième description"));
        using (SqlBookStorage storage = new SqlBookStorage(NewConnection(), manager))
        {
            storage.LoadPages("2-210208-02-8");
            Book? book = manager.LibraryBooks.LibraryBooksAssociation["2-210208-02-8"];
            Assert.True(book.ListPage.Count > 0);
            Assert.That("Vous êtes plongés dans la lecture du journal, quand vous entendez un bruit derrière vous.", Is.EqualTo(book.ListPage[0].TextPage));
            Assert.That("Le tueur vous dégomme : c’est perdu !", Is.EqualTo(book.ListPage[1].TextPage));
            Assert.That("Vous apercevez une silhouette en fuite", Is.EqualTo(book.ListPage[2].TextPage));
            Assert.That("Bravo, vous rattrapez et menottez un dangereux criminel !", Is.EqualTo(book.ListPage[3].TextPage));
            Assert.That("Ce texte ne sert à rien", Is.EqualTo(book.ListPage[4].TextPage));
            
           Assert.True(book.ListPage[0].ListChoices.Count == 0);
           Assert.True(book.ListPage[1].ListChoices.Count == 0);
           Assert.True(book.ListPage[2].ListChoices.Count == 0);
           Assert.True(book.ListPage[3].ListChoices.Count == 0);
           
        }
    }
    
    [Test]
    public void LoadPagesWithChoices()
    {
        ManagerReader manager = new ManagerReader();
        manager.LibraryBooks.AddBook("2-210208-01-7", new Book("Mon livre", new Author("François", "Mahy"), new Isbn("2-210208-01-7"), "Mon résumé"));
        using (SqlBookStorage storage = new SqlBookStorage(NewConnection(), manager))
        {
            storage.LoadPages("2-210208-01-7");
            Book? book = manager.LibraryBooks.LibraryBooksAssociation["2-210208-01-7"];
            Assert.True(book.ListPage.Count > 0);
            Assert.That("Vous êtes plongés dans la lecture du journal, quand vous entendez un bruit derrière vous.", Is.EqualTo(book.ListPage[0].TextPage));
            Assert.That("Le tueur vous dégomme : c’est perdu !", Is.EqualTo(book.ListPage[1].TextPage));
            Assert.That("Vous apercevez une silhouette en fuite", Is.EqualTo(book.ListPage[2].TextPage));
            Assert.That("Bravo, vous rattrapez et menottez un dangereux criminel !", Is.EqualTo(book.ListPage[3].TextPage));
            Assert.That("Ce texte ne sert à rien", Is.EqualTo(book.ListPage[4].TextPage));
            
            Assert.That("Vous continuez à lire : allez en page 2", Is.EqualTo(book.ListPage[0].ListChoices[0].TextChoice));
            Assert.That("vous vous retournez pour jeter un œil :", Is.EqualTo(book.ListPage[0].ListChoices[1].TextChoice));
            Assert.That("Vous recommencez à lire : allez en page 2,", Is.EqualTo(book.ListPage[2].ListChoices[0].TextChoice));
            Assert.That("vous poursuivez la silhouette en lui criant de s’arrêter : allez en page 4", Is.EqualTo(book.ListPage[2].ListChoices[1].TextChoice));
        }
    }
    
    
    [Test]
    public void LoadPagesFromBookNotExist()
    {
        ManagerReader manager = new ManagerReader();
        using (SqlBookStorage storage = new SqlBookStorage(NewConnection(), manager))
        {
            Assert.That(
                () => storage.LoadPages("2-667667-99-1"), 
                Throws.InstanceOf<SqlStorageException>());
        }
    }
}