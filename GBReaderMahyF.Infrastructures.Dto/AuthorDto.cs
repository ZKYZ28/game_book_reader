namespace GBReaderMahyF.Infrastructures.Dto;

public record AuthorDto
{
    private  String firstName;
    private  String name;
    private int matricule;

    public AuthorDto(String firstName, String name, int matricule)
    {
        this.firstName = firstName;
        this.name = name;
        this.matricule = matricule;
    }
    
    public String getFirstName()
    {
        return this.firstName;
    }

    public String getName()
    {
        return this.name;
    }

    public int getMatricule()
    {
        return this.matricule;
    }
}