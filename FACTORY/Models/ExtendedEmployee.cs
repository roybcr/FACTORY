using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FACTORY.Models
{
    public class ExtendedEmployee
    {
        public int ID { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string start_work_year { get; set; }
        public int departmentID { get; set; }
        public string departmentName { get; set; }
        public bool isManager { get; set; }
        public List<shift> employeesWithShifts { get; set; }
    }
}