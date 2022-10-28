using GBReaderMahyF.Infrastructures.Dto;
using GBReaderMahyF.Domains;


namespace GBReaderMahyF.Infrastructures.Mapper;
public class BookMapping
{
    /// <summary>
    /// Méthode qui permet de transformer un BookDto en Book
    /// </summary>
    /// <param name="bookDto">mon objet BookDto</param>
    /// <returns>Book qui est le BookDto convertit</returns>
    public Book GetBookFromBookDto(BookDto bookDto)
    {
        Author author = new Author(bookDto.getAuthor().getFirstName(), bookDto.getAuthor().getName(), bookDto.getAuthor().getMatricule());
        Isbn isbn = new Isbn(bookDto.getIsbn().getIsbnNumber());
        Book book = new Book(bookDto.getTitle(), author, isbn, bookDto.getResume());
        return book;
    }
}