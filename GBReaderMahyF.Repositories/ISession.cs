namespace GBReaderMahyF.Respositories;

public interface ISession
{
    /// <summary>
    /// Méthode qui permet d'écrire les sessions dans le fichier json
    /// </summary>
    public void WriteSession();
}