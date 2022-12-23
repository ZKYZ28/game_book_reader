using GBReaderMahyF.Domains;
using GBReaderMahyF.Infrastructures.JSON;
using GBReaderMahyF.Respositories.Exception;
using NUnit.Framework;

namespace GBReaderMahyF.Tests.Infrastructures.JSON;

public class JsonStorageTests
{
    private string _pathDirectory = Path.Combine("..", "..", "..", "Ressources");

    private string _pathFile =  Path.Combine("..", "..", "..", "Ressources", "test-file.json");

    [TearDown]
    public void CleanDirectoryAndFile()
    {
        File.Delete(_pathFile);
        Directory.Delete(_pathDirectory);
    }

    [Test]
    public void SetupNoDirectoryNoFile()
    {
        JsonSessionStorage jsonStorage = new JsonSessionStorage(new ManagerReader(), _pathDirectory, _pathFile);
        jsonStorage.SetupJson();
        
        Assert.True(Directory.Exists(_pathDirectory));
        Assert.True(File.Exists(_pathFile));
    }
    
    [Test]
    public void SetupNoFile()
    {
        Directory.CreateDirectory(_pathDirectory);
        Assert.True(Directory.Exists(_pathDirectory));
        
        JsonSessionStorage jsonStorage = new JsonSessionStorage(new ManagerReader(), _pathDirectory, _pathFile);
        jsonStorage.SetupJson();
        
        Assert.True(File.Exists(_pathFile));
    }
    
    [Test]
    public void ReadAllSessionsCorrupt()
    {
        JsonSessionStorage jsonStorage = new JsonSessionStorage(new ManagerReader(), _pathDirectory, _pathFile);
        jsonStorage.SetupJson();
        
        File.WriteAllText(_pathFile, "OEBFIUEBfiuebfzeupbfzeuzef;");
        Assert.That(
            () => jsonStorage.ReadAllSessions(), 
            Throws.InstanceOf<SessionStorageException>());
    }

    [Test]
    public void ReadAllSessionsEmpty()
    {
        JsonSessionStorage jsonStorage = new JsonSessionStorage(new ManagerReader(), _pathDirectory, _pathFile);
        jsonStorage.SetupJson();
        try
        {
            jsonStorage.ReadAllSessions();
        }
        catch (SessionStorageException e)
        {
           Assert.Fail();
        }
    }
    
    [Test]
    public void ReadAllSessionsFileNoExist()
    {
        JsonSessionStorage jsonStorage = new JsonSessionStorage(new ManagerReader(), _pathDirectory, _pathFile);
       
        
        Assert.That(
            () => jsonStorage.ReadAllSessions(), 
            Throws.InstanceOf<SessionStorageException>());
        
        //Pour que le taerdown ne produise pas d'erreur
        jsonStorage.SetupJson();
    }
    
    [Test]
    public void ReadAllSessionsBadFormat()
    {
        JsonSessionStorage jsonStorage = new JsonSessionStorage(new ManagerReader(), _pathDirectory, _pathFile);
        jsonStorage.SetupJson();
        
        File.WriteAllText(_pathFile, @"{""Sessions"":{2-210208-01-7:{""NumLastPage"":8,""StartReading"":""2022-12-20"",""EndReading"":""2022-12-20""}}}");
        Assert.That(
            () => jsonStorage.ReadAllSessions(), 
            Throws.InstanceOf<SessionStorageException>());
    }
    
    [Test]
    public void ReadAllSessionsGoodCase()
    {
        ManagerReader managerReader = new ManagerReader();
        JsonSessionStorage jsonStorage = new JsonSessionStorage(managerReader, _pathDirectory, _pathFile);
        jsonStorage.SetupJson();
        File.WriteAllText(_pathFile, @"{""Sessions"":{""2-210208-01-7"":{""NumLastPage"":8,""StartReading"":""2022-12-20"",""EndReading"":""2022-12-20""}}}");
        try
        {
            jsonStorage.ReadAllSessions();
            Assert.That(8, Is.EqualTo(managerReader.AllSessions.Sessions["2-210208-01-7"].NumSessionPage));
            Assert.That(new DateTime(2022, 12, 20), Is.EqualTo(managerReader.AllSessions.Sessions["2-210208-01-7"].StartReadingDate));
            Assert.That(new DateTime(2022, 12, 20), Is.EqualTo(managerReader.AllSessions.Sessions["2-210208-01-7"].EndReadingDate));
        }
        catch (SessionStorageException e)
        {
            Assert.Fail();
        }
    }
    
    [Test]
    public void ReadAllSessionsGoodCase2Sessions()
    {
        ManagerReader managerReader = new ManagerReader();
        JsonSessionStorage jsonStorage = new JsonSessionStorage(managerReader, _pathDirectory, _pathFile);
        jsonStorage.SetupJson();
        File.WriteAllText(_pathFile, @"{""Sessions"":{""2-210208-01-7"":{""NumLastPage"":3,""StartReading"":""2022-12-20"",""EndReading"":""2022-12-20""},""2-667667-01-8"":{""NumLastPage"":8,""StartReading"":""2022-11-20"",""EndReading"":""2022-10-20""}}}");
        try
        {
            jsonStorage.ReadAllSessions();
            Assert.That(3, Is.EqualTo(managerReader.AllSessions.Sessions["2-210208-01-7"].NumSessionPage));
            Assert.That(new DateTime(2022, 12, 20), Is.EqualTo(managerReader.AllSessions.Sessions["2-210208-01-7"].StartReadingDate));
            Assert.That(new DateTime(2022, 12, 20), Is.EqualTo(managerReader.AllSessions.Sessions["2-210208-01-7"].EndReadingDate));
            
            Assert.That(8, Is.EqualTo(managerReader.AllSessions.Sessions["2-667667-01-8"].NumSessionPage));
            Assert.That(new DateTime(2022, 11, 20), Is.EqualTo(managerReader.AllSessions.Sessions["2-667667-01-8"].StartReadingDate));
            Assert.That(new DateTime(2022, 10, 20), Is.EqualTo(managerReader.AllSessions.Sessions["2-667667-01-8"].EndReadingDate));
        }
        catch (SessionStorageException e)
        {
            Assert.Fail();
        }
    }
    
    [Test]
    public void WriteSessionEmpty()
    {
        JsonSessionStorage jsonStorage = new JsonSessionStorage(new ManagerReader(), _pathDirectory, _pathFile);
        jsonStorage.SetupJson();
        try
        {
            jsonStorage.WriteSession();
        }
        catch (SessionStorageException e)
        {
            Assert.Fail();
        }
    }
    
    [Test]
    public void WriteSessionGoodCase()
    {
        JsonSessionStorage jsonStorage = new JsonSessionStorage(new ManagerReader(), _pathDirectory, _pathFile);
        jsonStorage.SetupJson();
        
        File.WriteAllText(_pathFile, @"{""Sessions"":{""2-210208-01-7"":{""NumLastPage"":8,""StartReading"":""2022-12-20"",""EndReading"":""2022-12-20""}}}");
        try
        {
            jsonStorage.WriteSession();
        }
        catch (SessionStorageException e)
        {
            Assert.Fail();
        }
    }
    
    [Test]
    public void WriteSessionGoodCase2Sessions()
    {
        JsonSessionStorage jsonStorage = new JsonSessionStorage(new ManagerReader(), _pathDirectory, _pathFile);
        jsonStorage.SetupJson();
        
        File.WriteAllText(_pathFile, @"{""Sessions"":{""2-210208-01-7"":{""NumLastPage"":3,""StartReading"":""2022-12-20"",""EndReading"":""2022-12-20""},""2-667667-01-8"":{""NumLastPage"":8,""StartReading"":""2022-11-20"",""EndReading"":""2022-10-20""}}}");
        try
        {
            jsonStorage.WriteSession();
        }
        catch (SessionStorageException e)
        {
            Assert.Fail();
        }
    }

    [Test]
    public void WriteSessionNoExistFile()
    {
        JsonSessionStorage jsonStorage = new JsonSessionStorage(new ManagerReader(), _pathDirectory, _pathFile);
        
        Assert.That(
            () => jsonStorage.WriteSession(), 
            Throws.InstanceOf<SessionStorageException>());
        
        //Pour que le taerdown ne produise pas d'erreur
        jsonStorage.SetupJson();
    }
   
    
}