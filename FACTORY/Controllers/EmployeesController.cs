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
    public class EmployeesController : ApiController
    {
        private static EmployeesBL bl = new EmployeesBL();

        [Route("api/Employees/{uid}")]
        [HttpGet]
        // GET: api/Employees
        public IEnumerable<ExtendedEmployee> Get(int uid)
        {
            return bl.GetExtendedEmployees(uid);
        }

        [Route("api/Employees/{uid}/{eid}")]
        [HttpGet]
        // GET: api/Employees/5
        public ExtendedEmployee Get(int uid, int eid)
        {
            return bl.GetEmployee(uid, eid);
        }


        [Route("api/Employees/{uid}/{filter}/{input}")]
        [HttpGet]
        public IEnumerable<ExtendedEmployee> Get( int uid, string filter, string input )
        {
            return bl.GetSearchResults(uid, filter, input);
        }


        [Route("api/Employees/{uid}")]
        [HttpPost]
        // POST: api/Employees
        public employee Post(int uid, employee emp)
        {
           return bl.AddEmployee(uid, emp);
        }


        [Route("api/Employees/{uid}/{eid}")]
        [HttpPut]
        // PUT: api/Employees/5
        public employee Put(int uid, int eid, employee emp)
        {
            return bl.EditEmployee(uid, eid, emp);
        }

        [Route("api/Employees/{uid}/{eid}")]
        [HttpDelete]
        // DELETE: api/Employees/5
        public bool Delete(int uid, int eid)
        {
            return bl.DeleteEmployee(uid, eid);
        }
    }
}
