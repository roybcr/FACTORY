using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FACTORY.Models
{
    public class ExtendedShift
    {
        public int ID { get; set; }
        public int starttime { get; set; }
        public int endtime { get; set; }
        public DateTime date { get; set; }
        public List<employee> employees { get; set; }
    }
}