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
    public class DepartmentsController : ApiController
    {

        private static DepartmentsBL bl = new DepartmentsBL();

        [Route("api/Departments/{uid}")]
        [HttpGet]
        // GET: api/Departments
        public IEnumerable<ExtendedDepartment> Get(int uid)
        {
            return bl.GetDepartments(uid);
        }


        [Route("api/Departments/{uid}/{did}")]
        [HttpGet]
        // GET: api/Departments/5
        public department Get( int uid, int did )
        {
            return bl.GetDepartment(uid, did);
        }

        [Route("api/Departments/{uid}")]
        [HttpPost]
        // POST: api/Departments
        public department Post( int uid, department dep )
        {
            return bl.CreateDepartment(uid, dep);
        }


        [Route("api/Departments/{uid}/{did}")]
        [HttpPut]
        // PUT: api/Departments/5
        public department Put( int uid, int did, department dep )
        {
            return bl.EditDepartment(uid, did, dep);
        }

        [Route("api/Departments/{uid}/{did}")]
        [HttpDelete]
        // DELETE: api/Departments/5
        public bool Delete( int uid, int did )
        {
            return bl.DeleteDepartment(uid, did);
        }
    }
}
