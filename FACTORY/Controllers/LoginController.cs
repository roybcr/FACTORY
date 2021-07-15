using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using FACTORY.Models;

namespace FACTORY.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        private static LoginBL bl = new LoginBL();

        [Route("api/Login/{uid}")]
        [HttpGet]
        // GET: api/Login/5
        public bool Get(int uid)
        {
            return bl.UserHasActionsLeft(uid);
        }

        // POST: api/Login
        public user Post(user u)
        {
            return bl.Login(u);
        }
    }
}
