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
    public class ShiftsController : ApiController
    {
        static private ShiftsBL bl = new ShiftsBL();

        [Route("api/Shifts/{uid}")]
        [HttpGet]
        // GET: api/Shifts
        public IEnumerable<ExtendedShift> Get( int uid )
        {
            return bl.GetShifts(uid);
        }

        [Route("api/Shifts/{uid}")]
        [HttpPost]
        // POST: api/Shifts
        public shift Post( int uid, shift s )
        {
            return bl.AddShift(uid, s);
        }

        [Route("api/Shifts/{uid}/{eid}")]
        [HttpPost]
        // POST: api/Shifts/5
        public employees_shifts Post( int uid, int eid, shift s )
        {
            return bl.CreateShiftForEmployee(uid, eid, s);
        }
    }
}
