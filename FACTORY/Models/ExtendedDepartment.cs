using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FACTORY.Models
{
    public class ExtendedDepartment
    {
        public int ID { get; set; }
        public string name { get; set; }
        public int manager_id { get; set; }
        public bool hasEmployees { get; set; }
    }
}