using GBReaderMahyF.Domains;
using GBReaderMahyF.Infrastructures.DTO;
using GBReaderMahyF.Respositories;
using GBReaderMahyF.Respositories.Exception;

namespace GBReaderMahyF.Infrastructures.JSON;
using Newtonsoft.Json;

public class JsonSessionStorage : ISession
{
    private readonly ManagerReader _manager;
    private readonly string _pathDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "ue36");
    private readonly string _pathFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "ue36", "q210208-session.json");

    /// <summary>
    /// Constructeur du JsonSessionStorage
    /// </summary>
    /// <param name="manager">ManagerReader qui est le manager de l'application</param>
    public JsonSessionStorage(ManagerReader manager)
    {
        this._manager = manager;
        SetupJson();
        ReadAllSessions();
    }

    /// <summary>
    /// Constructeur du JsonSessionStorage
    /// POUR LES TESTS
    /// </summary>
    /// <param name="manager">ManagerReader qui est le manager de l'application</param>
    /// <param name="pathDirectory">string qui est le path vers le dossier du json de test</param>
    /// <param name="pathFile">string qui est le path vers le fichier du json de test</param>
    public JsonSessionStorage(ManagerReader manager, string pathDirectory, string pathFile)
    {
        this._manager = manager;
        this._pathDirectory = pathDirectory;
        this._pathFile = pathFile;
    }
    
    /// <summary>
    /// Méthode qui permet de setup le fichier json.
    /// Si il dossier n'existe pas il sera créé, si le fichier n'existe pas il sera créé
    /// Ensuite on va aller lire les éventuelles sessions existantes
    /// </summary>
    /// <exception cref="SessionStorageException">Exception lancée en cas de problème lié de près ou de loin au fichier Json</exception>
    public void SetupJson()
    {
        try
        {
            if (!Directory.Exists(_pathDirectory))
            {
                Directory.CreateDirectory(_pathDirectory);
                using ( File.Create(_pathFile))
                {
                }
            }
            else
            {
                if (!File.Exists(_pathFile))
                {
                    using (File.Create(_pathFile))
                    {
                    }
                   
                }
            }
        }
        catch (IOException ex)
        {
            throw new SessionStorageException("Erreur lors de la lecture du fichier Json.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new SessionStorageException("Erreur lors de la lecture du fichier Json.",ex);
        }
        catch (ArgumentException ex)
        {
            throw new SessionStorageException("Erreur lors de la lecture du fichier Json.", ex);
        }

    }

    /// <summary>
    /// Méthode qui permet de lire toutes les sessions en cours depuis le fichier json
    /// Si aucune session n'est crée alors on crée un object AllSession qui ne comporte aucune session. 
    /// </summary>
    /// <exception cref="SessionStorageException">Exception lancée en cas de problème lié de près ou de loin au fichier Json</exception>
    public void ReadAllSessions()
    {
        try
        {
            string json = File.ReadAllText(_pathFile);

            if (String.IsNullOrEmpty(json))
            {
                this._manager.AllSessions = new AllSessions();
            }
            else
            {
                AllSessionsDto sessionsDto = JsonConvert.DeserializeObject<AllSessionsDto>(json)!;
                this._manager.AllSessions = MapperDto.ConvertAllSessionsDtoToModel(sessionsDto);
            }
        }
        catch (JsonSerializationException ex)
        {
            throw new SessionStorageException("Erreur lors de la lecture du fichier Json.", ex);
        }
        catch (JsonReaderException ex)
        {
            throw new SessionStorageException("Erreur lors de la lecture du fichier Json.", ex);
        }
        catch (IOException ex)
        {
            throw new SessionStorageException("Erreur lors de la lecture du fichier Json.",ex);
        }
    }

    /// <summary>
    /// Méthode qui permet d'écrire les sessions dans le fichier json
    /// </summary>
    /// <exception cref="SessionStorageException">Exception lancée en cas de problème lié de près ou de loin au fichier Json</exception>
    public void WriteSession()
    {
        try
        {
            var sessions = JsonConvert.SerializeObject(MapperDto.ConvertAllSessionsToDto(_manager.AllSessions));
            File.WriteAllText(_pathFile, sessions);
        }
        catch (IOException ex)
        {
            throw new SessionStorageException("Erreur lors de l'écriture dans le fichier Json", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new SessionStorageException("Erreur lors de l'écriture dans le fichier Json",ex);
        }
        catch (ArgumentException ex)
        {
            throw new SessionStorageException("Erreur lors de l'écriture dans le fichier Json", ex);
        }
    }
}