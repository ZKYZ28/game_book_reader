namespace GBReaderMahyF.Infrastructures.Dto;

public record IsbnDto
{
    private  String isbnNumber;

    public IsbnDto(String isbnNumber)
    {
        this.isbnNumber = isbnNumber;
    }
    
    
    public String getIsbnNumber()
    {
        return this.isbnNumber;
    }
}