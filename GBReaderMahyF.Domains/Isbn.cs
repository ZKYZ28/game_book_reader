using System.Text.RegularExpressions;

namespace GBReaderMahyF.Domains;


/// <summary>
/// Class qui représente le numéro isbn d'un livre
///
/// Un numéro ISBN-10 est composé par 10 chiffres. Il commence par le numéro du groupe linguistique 
/// visé (2 dans notre cas pour le français) suivi par l’identifiant de l’auteur qui correspondra aux 6 
/// chiffres de votre matricule, suivi par 2 chiffres identifiant le livre. Le dernier chiffre est un code de 
/// vérification calculé sur ce principe : On prend les 9 premiers chiffres de l’ISBN et on les multiplie par un poids allant de 10 à 2.
/// 
/// </summary>
public class Isbn
{
    private string _isbnNumber;

    /// <summary>
    /// Constucteur du Isbn
    /// </summary>
    /// <param name="isbnNumber">String qui est le numéro isbn</param>
    public Isbn(string isbnNumber)
    {
        this._isbnNumber = isbnNumber;
        CheckIsbnNumber();
    }

    public string IsbnNumber()
	    => _isbnNumber;
    
    
    /// <summary>
    /// Méthode qui permet de vérifier que le numéro Isbn est valide et non vide
    /// </summary>
    /// <returns>Boolean, True si le numéro Isbn est valide sinon false</returns>
    private void CheckIsbnNumber() {
		if(!string.IsNullOrEmpty(_isbnNumber)) {
			if (!CheckFormatIsbnNumber())
			{
				this._isbnNumber = "0-000000-00-0";
			}
		}
    }
	

    /// <summary>
    /// Méthode qui permet de vérifier la validité du numéro Isbn
    /// </summary>
    /// <returns>Boolean, True si le numéro Isbn est valide sinon false</returns>
	private bool CheckFormatIsbnNumber()
	{
		Regex rx = new Regex("[0-9]-[0-9]{6}-[0-9]{2}-[0-9|X|Y]");
		MatchCollection matches = rx.Matches(this._isbnNumber);
		
		if(matches.Count == 0) {
			return false;			
		}
		return CheckLastNumberIsbn();
	}
	
	/// <summary>
	/// Méthode qui permet de calculer et de vérifier la cohérence du dernier chiffre du numéro isbn
	/// </summary>
	/// <returns>Boolean, True si le dernier numéro du numéro Isbn est valide sinon false</returns>
	private bool CheckLastNumberIsbn() {
		var lastCtrl = 0;
		var  last = DeterminateLastNumber(this._isbnNumber);
		var max = 10;
		
		for (int i = 0; i < this._isbnNumber.Length -1; i++) {
			if(i != 1 && i != 8 && i != 11) {
				lastCtrl += (int) Char.GetNumericValue(this._isbnNumber[i]) * max;
				max--;
			}
		}						
		return CompareLastNumberIsbn(lastCtrl, last);
	}
	
	/// <summary>
	///  Méthode qui permet de comparer les derniers chiffres des nombres isbn pour vérifier si ils sont bien égaux.
	/// </summary>
	/// <param name="lastCtrl">int qui est le dernier chiffre isbn calulé</param>
	/// <param name="last">int qui est le dernier chiffre isbn rentré par l'utilisateur</param>
	/// <returns>Boolean, True si le dernier chiffre du numéro isbn est le même que celui de contrôle, sinon false</returns>
	private bool CompareLastNumberIsbn(int lastCtrl,  int last) {
		if (11 - (lastCtrl % 11) != last)
		{
			return false;
		}
		return true;
	}

	/// <summary>
	/// Méthode qui donne la valeur réelle du dernier chiffre du numéro isbn
	/// 0 => 9 = nbr
	/// X => 10
	/// Y => 11
	/// </summary>
	/// <param name="isbnNumber">String qui est le numéro isbn</param>
	/// <returns>int qui est la valeur du dernier chiffre du numéro isbn</returns>
	private int DeterminateLastNumber(string isbnNumber) {	
		
		switch(isbnNumber[isbnNumber.Length-1]) {
		case 'X':
			return 10;
		
		case 'Y':
			return 11;
			
		default :
			return isbnNumber[isbnNumber.Length-1] - '0';
		}		
	}
}