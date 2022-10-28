using Newtonsoft.Json;
using GBReaderMahyF.Infrastructures.Dto;
using GBReaderMahyF.Infrastructures.Mapper;
using GBReaderMahyF.Domains;
using GBReaderMahyF.Respositories;

namespace GBReaderMahyF.Infrastructures;

public class ManagerRepository : IManagerRepository
{
    public List<Book> ReadBooks()
    {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "ue36", "q210208.json");
        string json = File.ReadAllText(path);
        return TransformBookDtoToBook(JsonConvert.DeserializeObject<List<BookDto>>(json));
    }

    /// <summary>
    /// Méthode qui permet de transformer les BookDto en Book et de vérifier que les numéros isbn lus sont valides
    /// </summary>
    /// <param name="booksDtoList">List<BookDto> qui est la liste des livres crées par les utilisateurs</param>
    /// <returns>List<Book> qui est la liste des livres avec un numéro isbn valide</returns>
    private List<Book> TransformBookDtoToBook(List<BookDto> booksDtoList)
    {
        List<Book> booksList = new List<Book>();
        foreach (var bookDto in booksDtoList)
        {
            Book book = new BookMapping().GetBookFromBookDto(bookDto);
            if (book.getIsbn().checkIsbnNumber())
            {
                booksList.Add(book);
            }
        }
        
        return booksList;
    }
}