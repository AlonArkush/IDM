using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace IDM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "mid1", "OU=OU1,DC=mid1,DC=proj");
            UserPrincipal u = new UserPrincipal(ctx);
        
        
            PrincipalSearcher ps = new PrincipalSearcher();
            ps.QueryFilter = u;
            PrincipalSearchResult<Principal> psr = ps.FindAll();
            UserPrincipal up = psr.ElementAt(0) as UserPrincipal;

            
            return Enumerable.Range(1, psr.Count()).Select(index => new WeatherForecast
            {
                FirstName = (psr.ElementAt(index - 1) as UserPrincipal).GivenName,
                Surname = (psr.ElementAt(index - 1) as UserPrincipal).Surname,
                UserName = (psr.ElementAt(index - 1) as UserPrincipal).UserPrincipalName,
                Phone = "" + (psr.ElementAt(index - 1) as UserPrincipal).VoiceTelephoneNumber,
                Organization = ((psr.ElementAt(index - 1) as UserPrincipal).GetUnderlyingObject() as DirectoryEntry).Properties["department"].Value.ToString()
            
            })
            .ToArray();
        }
    }
}