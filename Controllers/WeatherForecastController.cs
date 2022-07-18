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
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "mid1", "DC=mid1,DC=proj");
            UserPrincipal u = new UserPrincipal(ctx);
            u.GivenName = "Alon";
            u.Surname = "Arkush";
            PrincipalSearcher ps = new PrincipalSearcher();
            ps.QueryFilter = u;
            PrincipalSearchResult<Principal> psr = ps.FindAll();
            
            return Enumerable.Range(1, psr.Count()).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = psr.ElementAt(index - 1).Sid + ""
            })
            .ToArray();
        }
    }
}