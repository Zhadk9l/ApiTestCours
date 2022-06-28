using ApiTestCours.Clients;
using ApiTestCours.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiTestCours.Controllers;

[ApiController]
[Route("[controller]")]
public class DotaController : ControllerBase
{
    
    
    private readonly DotaClient _dotaClient;
    public DotaController(DotaClient dotaClient)
    {
        _dotaClient = dotaClient;
    }

    [HttpGet("HeroByName/{HeroName}")]
    public async Task<Heroes> GetAllHeroes(string HeroName)
    {
        var Heroe = await _dotaClient.GetHeroes(HeroName);
        return Heroe;
    }

    [HttpGet("Teamtop/{TopCount}")]
    public async Task<string> TeamTop(int TopCount)
    {
        var top = await _dotaClient.GetTop(TopCount);
        return top;
    }

    [HttpGet("CompareHeroes/{TwoHeroNames}")]
    public async Task<string> GetCompareTwoHeroes(string TwoHeroNames)
    {

        var Compare = await _dotaClient.GetCompare(TwoHeroNames);
        return Compare;
    }


    [HttpGet("TeambyName/{TeamName}")]
    public async Task<DotaTeams> GetTeamById(string TeamName)
    {
        var team = await _dotaClient.GetTeam(TeamName);
        return team;
    }

    [HttpGet("LeaguebyName/{LeagueName}")]
    public async Task<Leagues> GetLeagueByName(string LeagueName)
    {
        var League = await _dotaClient.GetLeague(LeagueName);
        return League;
    }


    [HttpGet("TeamsMatchesLive")]
    public async Task<List<LiveMatches>> GetTeamsMatchesLive()
    {
        List<LiveMatches> MatchesLive = await _dotaClient.GetTeammatchesLive();
        return MatchesLive;
    }

    [HttpGet("Tournaments")]
    public async Task<string> GetTournaments()
    {
        var List = await _dotaClient.GetListofTournaments();
        return List;
    }

    [HttpPost("AddTeamName/{TeamName}")]
    public async Task<string> PostTeamName(string TeamName)
    {
        if (TeamName == null)
        {
            return null;
        }
        _dotaClient.Add(TeamName);
        return "Done";
    }
    [HttpDelete("DelTeam/{TeamName}")]
    public async Task<string> DelTeamName(string TeamName)
    {
        if (TeamName == null)
        {
            return null;
        }
        _dotaClient.Del(TeamName);
        return "Done";
    }
}
