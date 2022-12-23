using GBReaderMahyF.Domains;
using GBReaderMahyF.Infrastructures.DTO;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace GBReaderMahyF.Infrastructures.DTO;

public static class MapperDto
{
    /// <summary>
    /// Méthode qui permet de convertir AllSession en AllSessionDto
    /// </summary>
    /// <param name="sessionsModel">AllSessions qui est l'ensemble des sessions en cours</param>
    /// <returns>AllSessionsDto qui est l'ensemble des sessions en cours sous la forme d'un dto </returns>
    public static AllSessionsDto ConvertAllSessionsToDto(AllSessions sessionsModel)
    {
        Dictionary<string, SessionDto> sessionsDto = new Dictionary<string, SessionDto>();
        
        foreach(KeyValuePair<string, Session> entry in sessionsModel.Sessions)
        {
            Session currentSess = entry.Value;
            sessionsDto.Add(entry.Key, new SessionDto(currentSess.NumSessionPage, currentSess.StartReadingDate, currentSess.EndReadingDate));
        }
        
        return new AllSessionsDto(sessionsDto);
    }
    
    /// <summary>
    /// Méthode qui permet de covertir AllSessionsDto en AllSession
    /// </summary>
    /// <param name="sessionsDto">AllSessionsDto qui est l'ensemble des sessions en cours sous la forme d'un dto</param>
    /// <returns>AllSessions qui est l'ensemble des sessions en cours</returns>
    public static AllSessions ConvertAllSessionsDtoToModel(AllSessionsDto sessionsDto)
    {
        Dictionary<string, Session> sessionsModel = new Dictionary<string, Session>();
        
        foreach(KeyValuePair<string, SessionDto> entry in sessionsDto.Sessions)  
        {
            SessionDto currentSess = entry.Value;
            sessionsModel.Add(entry.Key, new Session(currentSess.NumLastPage, currentSess.StartReading, currentSess.EndReading));
        }
        
        return new AllSessions(sessionsModel);
    }
}