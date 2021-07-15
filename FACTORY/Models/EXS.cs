using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FACTORY.Models
{
    // Employees _X_ Shifts Junction Table
    public class EXS 
    {
        public int ID { get; set; }
        public int employeeId { get; set; }
        public int shiftId { get; set; }
    }
}