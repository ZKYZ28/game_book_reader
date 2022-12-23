namespace GBReaderMahyF.Domains;

/// <summary>
/// Class qui représente l'auteur d'un livre
/// </summary>
public record Author
{
    private readonly string _firstName;
    private readonly string _name;

    /// <summary>
   /// Constructeur de l'Author
   /// </summary>
   /// <param name="firstName">String qui est le prénom de l'auteur</param>
   /// <param name="name">String qui est le nom de l'auteur</param>
    public Author(string firstName, string name)
    {
        this._firstName = firstName;
        this._name = name;
    }

   /// <summary>
    /// Méthode qui permet de récupérer le prénom + nom de l'auteur sous forme de String
    /// </summary>
    /// <returns>String qui est le nom complet de l'auteur</returns>
    public string GetFullName()
    {
        return this._firstName + " " + this._name;
    }
}