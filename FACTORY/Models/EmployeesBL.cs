using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FACTORY.Models
{
    public class EmployeesBL
    {
        FactoryEntities db = new FactoryEntities();
        private static LoginBL bl = new LoginBL(); // Use The Login Business Logic to check if user has actions left.
        public List<employee> GetEmployees( int uid )
        {
            if ( bl.ReduceOneActionIfAuthorized(uid) )
            {
                return db.employees.ToList();
            }
            else
            {
                return null;
            }

        }

        public ExtendedEmployee GetEmployee( int uid, int eid )
        {

            if ( bl.ReduceOneActionIfAuthorized(uid) )
            {

                var employee = db.employees.Where(e => e.ID == eid).First();
                var dep = db.departments.Where( d  => d.ID == employee.DepartmentID).First();
                ExtendedEmployee extEmp = new ExtendedEmployee();
                extEmp.ID = employee.ID;
                extEmp.firstname = employee.firstname;
                extEmp.lastname = employee.lastname;
                extEmp.departmentID = employee.DepartmentID;
                extEmp.start_work_year = employee.start_work_year;
                extEmp.departmentName = dep.name;
                extEmp.isManager = db.departments.Where( d => d.ID == employee.DepartmentID).First().manager_id == employee.ID;
                extEmp.employeesWithShifts = (from eXs in db.employees_shifts join s in db.shifts on eXs.ShiftID equals s.ID where eXs.EmployeeID == employee.ID select s).ToList();

                return extEmp;
            }
            else
            {
                return null;
            }

        }

        public List<ExtendedEmployee> GetExtendedEmployees( int uid )
        {
            List<ExtendedEmployee> extList = new List<ExtendedEmployee>();

            if ( bl.ReduceOneActionIfAuthorized(uid) )
            {
                foreach ( var e in db.employees )
                {
                    var dep = db.departments.Where(( d ) => d.ID == e.DepartmentID).First();
                    ExtendedEmployee extEmp = new ExtendedEmployee();
                    extEmp.ID = e.ID;
                    extEmp.firstname = e.firstname;
                    extEmp.lastname = e.lastname;
                    extEmp.departmentID = e.DepartmentID;
                    extEmp.start_work_year = e.start_work_year;
                    extEmp.departmentName = dep.name;
                    extEmp.isManager = db.departments.Where(( d ) => d.ID == e.DepartmentID).First().manager_id == e.ID;
                    extEmp.employeesWithShifts = (from eXs in db.employees_shifts join s in db.shifts on eXs.ShiftID equals s.ID where eXs.EmployeeID == e.ID select s).ToList();

                    extList.Add(extEmp);
                }

                return extList;
            }
            else
            {
                return null;
            }

        }

        public employee EditEmployee( int uid, int eid, employee emp )
        {
            if ( bl.ReduceOneActionIfAuthorized(uid) )
            {

                var newEmp = db.employees.Where(e => e.ID == eid).First();
                newEmp.firstname = emp.firstname;
                newEmp.lastname = emp.lastname;
                newEmp.DepartmentID = emp.DepartmentID;
                newEmp.start_work_year = emp.start_work_year;
                db.SaveChanges();
                return newEmp;

            }

            return null;

        }
        public IEnumerable<ExtendedEmployee> GetSearchResults( int uid, string filter, string input )
        {
            List<ExtendedEmployee> searchResultList = new List<ExtendedEmployee>();
            switch ( filter )
            {

                case "dep":
                    searchResultList = GetExtendedEmployees(uid).Where(eXt => eXt.departmentName.Contains(input)).ToList();
                    break;
                case "first":
                    searchResultList = GetExtendedEmployees(uid).Where(eXt => eXt.firstname.Contains(input)).ToList();
                    break;
                case "last":
                    searchResultList = GetExtendedEmployees(uid).Where(eXt => eXt.lastname.Contains(input)).ToList();
                    break;

            }
            return searchResultList;
        }

        public bool DeleteEmployee( int uid, int eid )
        {

            if ( bl.ReduceOneActionIfAuthorized(uid) )
            {
                var shiftsOfEmployee = db.employees_shifts.Where(eXs => eXs.EmployeeID == eid).ToList();
                foreach ( var sid in shiftsOfEmployee )
                {
                    EXS eXs = new EXS();
                    eXs.employeeId = sid.EmployeeID;
                    eXs.shiftId = sid.ShiftID;
                    employees_shifts _eXs = db.employees_shifts.Where(x => x.EmployeeID == eXs.employeeId && x.ShiftID == eXs.shiftId).First();
                    db.employees_shifts.Remove(_eXs);

                    db.SaveChanges();

                }

                employee emp = db.employees.Where(e => e.ID == eid).First();
                db.employees.Remove(emp);
                db.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }



        public employee AddEmployee( int uid, employee emp )
        {
            if ( bl.ReduceOneActionIfAuthorized(uid) )
            {

                employee newEmp = new employee();
                newEmp.firstname = emp.firstname;
                newEmp.lastname = emp.lastname;
                newEmp.DepartmentID = emp.DepartmentID;
                newEmp.start_work_year = emp.start_work_year;
                db.employees.Add(newEmp);
                db.SaveChanges();
                return newEmp;

            }

            return null;

        }
    }
}