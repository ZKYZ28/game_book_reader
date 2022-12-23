using GBReaderMahyF.Domains;
using GBReaderMahyF.Infrastructures.DTO;
using NUnit.Framework;

namespace GBReaderMahyF.Tests.Infrastructures.Mapper;

public class MapperDtoTests
{
    [Test]
    public void ConvertAllSessionsToDto()
    {
        Dictionary<string, Session> sessionsModel = new Dictionary<string, Session>();
        sessionsModel.Add("2-210208-01-7", new Session(2, DateTime.MinValue, DateTime.MaxValue));
        sessionsModel.Add("2-210208-02-8", new Session(4, DateTime.MinValue, DateTime.MaxValue));

        AllSessionsDto sessionDto = MapperDto.ConvertAllSessionsToDto(new AllSessions(sessionsModel));
        
        Assert.That(2, Is.EqualTo(sessionDto.Sessions["2-210208-01-7"].NumLastPage));
        Assert.That(4, Is.EqualTo(sessionDto.Sessions["2-210208-02-8"].NumLastPage));
    }
    
    [Test]
    public void ConvertAllSessionsToDtoEmpty()
    {
        Dictionary<string, Session> sessionsModel = new Dictionary<string, Session>();

        AllSessionsDto sessionDto = MapperDto.ConvertAllSessionsToDto(new AllSessions(sessionsModel));
        
        Assert.That(0, Is.EqualTo(sessionDto.Sessions.Count));
    }
    
    [Test]
    public void ConvertAllSessionsDtoToModel()
    {
        Dictionary<string, SessionDto> sessionsDto = new Dictionary<string, SessionDto>();
        sessionsDto.Add("2-210208-01-7", new SessionDto(2, DateTime.MinValue, DateTime.MaxValue));
        sessionsDto.Add("2-210208-02-8", new SessionDto(4, DateTime.MinValue, DateTime.MaxValue));

        AllSessions sessionsModel = MapperDto.ConvertAllSessionsDtoToModel(new AllSessionsDto(sessionsDto));
        
        Assert.That(2, Is.EqualTo(sessionsModel.Sessions["2-210208-01-7"].NumSessionPage));
        Assert.That(4, Is.EqualTo(sessionsModel.Sessions["2-210208-02-8"].NumSessionPage));
    }
    
    [Test]
    public void ConvertAllSessionsDtoToModelEmpty()
    {
        Dictionary<string, SessionDto> sessionsDto = new Dictionary<string, SessionDto>();

        AllSessions sessionsModel = MapperDto.ConvertAllSessionsDtoToModel(new AllSessionsDto(sessionsDto));
        
        Assert.That(0, Is.EqualTo(sessionsModel.Sessions.Count));
    }
    
}