using Microsoft.AspNetCore.Mvc;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace IDM.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdUserController : ControllerBase
    {

        private readonly ILogger<AdUserController> _logger;

        public AdUserController(ILogger<AdUserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<AdUser> Get()
        {
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "mid1", "OU=OU1,DC=mid1,DC=proj");
            PrincipalSearcher ps = new PrincipalSearcher();
            ps.QueryFilter = new UserPrincipal(ctx);
            PrincipalSearchResult<Principal> psr = ps.FindAll();
            UserPrincipal up = psr.ElementAt(0) as UserPrincipal;

            return Enumerable.Range(1, psr.Count()).Select(index => new AdUser
            {
                FirstName = (psr.ElementAt(index - 1) as UserPrincipal).GivenName,
                Surname = (psr.ElementAt(index - 1) as UserPrincipal).Surname,
                UserName = (psr.ElementAt(index - 1) as UserPrincipal).UserPrincipalName,
                Phone = "" + (psr.ElementAt(index - 1) as UserPrincipal).VoiceTelephoneNumber,
                Organization = ((psr.ElementAt(index - 1) as UserPrincipal).GetUnderlyingObject() as DirectoryEntry).Properties["department"].Value.ToString()

            })
            .ToArray();
        }

        [HttpPost]
        public void Post([FromBody] byte[] sqlStr)
        {
            /*AdUser aduser = JsonConvert.DeserializeObject<AdUser>(sqlStr);
            SqlConnection cnn;
            string connectionString = @"Data Source=VM1234MID1SQL1;Initial Catalog=DARK;User=VM1234MID1SQL1\qw;Password=ASDfgh!@#456;Integrated Security=True";
            cnn = new SqlConnection(connectionString);
            cnn.Open();
            string sql = $"insert into dbo.CreateUserRequests(FirstName, Surname, Username, Phone, Organization) Values({aduser.FirstName}, {aduser.Surname}, {aduser.UserName}, {aduser.Phone}, {aduser.Organization})";
            SqlCommand sqlcommand = new SqlCommand(sql, cnn);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.InsertCommand = sqlcommand;
            adapter.InsertCommand.ExecuteNonQuery();
            sqlcommand.Dispose();
            cnn.Close();
            */
        }
    }
}