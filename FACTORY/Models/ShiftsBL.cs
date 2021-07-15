using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FACTORY.Models
{
    public class ShiftsBL
    {
        FactoryEntities db = new FactoryEntities();
        private static LoginBL bl = new LoginBL();

		public List<ExtendedShift> GetShifts( int uid )
		{
			List<ExtendedShift> extended_shift_list = new List<ExtendedShift>();

			if ( bl.UserHasActionsLeft(uid) )
			{
				foreach ( var shft in db.shifts )
				{

					ExtendedShift extended_shift = new ExtendedShift();
					extended_shift.ID = shft.ID;
					extended_shift.starttime = shft.starttime;
					extended_shift.endtime = shft.endtime;
					extended_shift.date = shft.date;
					extended_shift.employees = (from eXs in db.employees_shifts
											   join e in db.employees on eXs.EmployeeID equals e.ID
											   where eXs.ShiftID == shft.ID
											   select e).ToList();

					extended_shift_list.Add(extended_shift);
				}

				return extended_shift_list;
			}
			else
			{
				return null;
			}

		}



		public shift AddShift( int uid, shift s )
		{
			if ( bl.UserHasActionsLeft(uid) )
			{

				shift shft = new shift();
				shft.starttime = s.starttime;
				shft.endtime = s.endtime;
				shft.date = s.date;

				db.shifts.Add(shft);
				db.SaveChanges();
				return shft;
			}

			return null;

		}

		public employees_shifts CreateShiftForEmployee( int uid, int eid, shift s )
		{
			if ( bl.UserHasActionsLeft(uid) )
			{

				var shft = new shift();
				shft.date = s.date;
				shft.endtime = s.endtime;
				shft.starttime = s.starttime;
				db.shifts.Add(shft);
				db.SaveChanges();

				var currShift = db.shifts.Find(shft);
				var eXs = new employees_shifts();

				eXs.ShiftID = currShift.ID;
				eXs.EmployeeID = eid;

				db.employees_shifts.Add(eXs);
				db.SaveChanges();
				return eXs;
			}

			return null;

		}


	}
}