using System.Data.Common;
using GBReaderMahyF.Domains;
using GBReaderMahyF.Infrastructures.BD;
using NUnit.Framework;

namespace GBReaderMahyF.Tests.Infrastructures.BD;

public class BookStorageFactoryTests
{
    [OneTimeSetUp]
    public static void BeforeAll()
    {
        DbProviderFactories.RegisterFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);
    }
    
    [Test]
    public void RejectsUnknowProviders()
    {
        Assert.That(
            () => new BookStorageFactory("This.Does.Not.Exists", ""), 
            Throws.InstanceOf<ProviderNotFoundException>());
    }

    [Test]
    public void RejectsInvalidConnectionString()
    {
        var factory = new BookStorageFactory("System.Data.SqlClient", "bad connection string");

        Assert.That(
            () => factory.NewStorage(new ManagerReader()),
            Throws.InstanceOf<InvalidConnectionStringException>()
        );
    }

    [Test]
    public void RejectsConnectionStringToNonExistingDb()
    {
        var factory = new BookStorageFactory("System.Data.SqlClient", "Data Source=(localdb)\\MsSqlLocalDb; Initial Catalog=sad.ado.test");

        Assert.That(
            () => factory.NewStorage(new ManagerReader()),
            Throws.InstanceOf<UnableToConnectException>()
        );
    }
}