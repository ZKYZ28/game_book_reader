namespace GBReaderMahyF.Domains;

public class Author
{
    private String _firstName;
    private String _name;
    private int _matricule;

   /// <summary>
   /// Constructeur de l'Author
   /// </summary>
   /// <param name="firstName">String qui est le prénom de l'auteur</param>
   /// <param name="name">String qui est le nom de l'auteur</param>
   /// <param name="matricule">int qui est le matricule de l'auteur</param>
    public Author(String firstName, String name, int matricule)
    {
        this._firstName = firstName;
        this._name = name;
        this._matricule = _matricule;
    }

    public String getFirstName()
    {
        return this._firstName;
    }

    public String getName()
    {
        return this._name;
    }

    public int getMatricule()
    {
        return this._matricule;
    }

    /// <summary>
    /// Méthode qui permet de récupérer le prénom + nom de l'auteur sous forme de String
    /// </summary>
    /// <returns>String qui est le nom complet de l'auteur</returns>
    public String getFullName()
    {
        return this._firstName + " " + this._name;
    }
}