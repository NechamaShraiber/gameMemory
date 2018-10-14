using MemoryGame.Models;
using System.Linq;
using System.Web.Http;

namespace MemoryGame

{

    public class UserController : ApiController
    {

        [Route("api/login")]
        [HttpPost]
        public IHttpActionResult Login([FromBody]User user)
        {
            
            if (user.UserName.Length < 2 || user.UserName.Length > 10)
            {
                return Ok("user name must be 2-10 letters");
            }
            if (user.Age < 18 || user.Age > 120)
            {
                return Ok("age must be between 18-120 years");
            }
            user.Score = 0;
            lock (Global.UserList)
            {
                if (Global.UserList.Any(u => u.UserName == user.UserName))
                {
                    return Ok("there is this userName");
                }
                Global.UserList.Add(user);
            }

            return Ok();
        }

        [Route("api/getUsersWaitToPartner")]
        [HttpGet]
        public IHttpActionResult GetUsersWaitToPartner()
        {
            return Ok(Global.UserList.Where(u => u.PartnerName == null).Select(p => new { p.UserName, p.Age }));
        }

        [Route("api/getUserDetails/{userName}")]
        [HttpGet]
        public IHttpActionResult GetUserDetails(string userName)
        {
            return Ok(Global.UserList.FirstOrDefault(u => u.UserName == userName));
        }

        [Route("api/ChoosingPartner/{userName}/{partnerName}")]
        [HttpPut]
        public IHttpActionResult ChoosingPartner(string userName, string partnerName)
        {
            lock (Global.UserList)
            {
                var user = Global.UserList.FirstOrDefault(u => u.UserName == userName);
                var partner = Global.UserList.FirstOrDefault(u => u.UserName == partnerName);
                if (user.PartnerName == null)
                    if (partner.PartnerName == null)
                    {
                        user.PartnerName = partnerName;
                        partner.PartnerName = userName;
                        Game g = new Game()
                        {
                            Player1 = user,
                            Player2 = partner,
                            CurrentTurn = user.UserName,
                            CardArray = { { "1",null}, { "2", null}, { "3", null},
                                      { "4",null}, { "5",null}, { "6",null},
                                      { "7",null}, { "8",null}, { "9",null}}

                        };

                        Global.GameList.Add(g);
                        return Ok();

                    }
                    else
                    {
                        return Ok("the partner catch yet");
                    }
                else return Ok("you has partner yet");
            }
        }

        [Route("api/deleteUser/{userName}")]
        [HttpGet]
        public IHttpActionResult deleteUser(string userName)
        {
            lock (Global.GameList)
            {
                var currentGame = Global.GameList.FirstOrDefault(g => g.Player1.UserName == userName || g.Player2.UserName == userName);
                if (currentGame != null)
                    Global.GameList.Remove(currentGame);
            }
            lock (Global.UserList)
            {
                var player = Global.UserList.FirstOrDefault(u => u.UserName == userName);
                if (player != null)
                    Global.UserList.Remove(player);

            }
            return Ok();
        }
    }
}
