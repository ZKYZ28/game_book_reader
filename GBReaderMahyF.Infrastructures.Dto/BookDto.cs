namespace GBReaderMahyF.Infrastructures.Dto;

public record BookDto
{
    private  AuthorDto author;
    private  String title;
    private  IsbnDto isbn;
    private  String resume;


    public BookDto(AuthorDto author, String title, IsbnDto isbn, String resume)
    {
        this.author = author;
        this.title = title;
        this.isbn = isbn;
        this.resume = resume;
    }
    
    public AuthorDto getAuthor()
    {
        return this.author;
    }

    public String getTitle()
    {
        return this.title;
    }

    public IsbnDto getIsbn()
    {
        return this.isbn;
    }

    public String getResume()
    {
        return this.resume;
    }
}