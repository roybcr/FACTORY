using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FACTORY.Models
{

    
    public class DepartmentsBL
    {
        FactoryEntities db = new FactoryEntities();
        private static LoginBL bl = new LoginBL();

        public List<ExtendedDepartment> GetDepartments( int uid )
        {

            if ( bl.UserHasActionsLeft(uid) )
            {

                var departmentList = db.departments.ToArray();
                var dXe = new List<ExtendedDepartment>();

                foreach ( var d in departmentList )
                {
                    List<employee> employeeList = db.employees.Where(e => e.DepartmentID == d.ID).ToList();
                    int total = employeeList.Count();
                    dXe.Add(new ExtendedDepartment
                    {
                        ID = d.ID,
                        name = d.name,
                        manager_id = d.manager_id,
                        hasEmployees = !!(total > 0)
                    });
                }

                return dXe;
            }
            else
            {
                return null;
            }

        }



        public department GetDepartment(int uid, int did)
        {
            if(bl.UserHasActionsLeft(uid))
            {
                var dep = db.departments.FirstOrDefault(d => d.ID == did);
                return dep;
            }

            return null;
        }


        public department EditDepartment( int uid, int did, department dep )
        {

            if ( bl.UserHasActionsLeft(uid) )
            {

                var currDep = db.departments.FirstOrDefault(d => d.ID == did);
                if ( currDep != null )
                {
                    var otherDep = db.departments.FirstOrDefault(x => x.manager_id == dep.manager_id);
                    if ( otherDep != null && otherDep.ID != did ) // Has the same manager, not the same department.
                    {

                        employee e = db.employees.Where(x => x.ID == otherDep.manager_id).First();
                        e.DepartmentID = did;
                        currDep.name = dep.name;
                        currDep.manager_id = dep.manager_id;
                        db.SaveChanges();

                    }
                    else
                    {
                        currDep.name = dep.name;
                        currDep.manager_id = dep.manager_id;
                        db.SaveChanges();
                    }

                }
                return currDep;

            }
            else return null;
        }

        public Boolean DeleteDepartment( int uid, int did )
        {
            if ( bl.UserHasActionsLeft(uid) )
            {
                var currDep = db.departments.FirstOrDefault(( x ) => x.ID == did);
                if ( currDep != null )
                {
                    int totalEmployees = db.employees.Where(( x ) => x.DepartmentID == did).Count();
                    if ( totalEmployees == 0 )
                    {
                        db.departments.Remove(currDep);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public department CreateDepartment( int uid, department dep )
        {
            if ( bl.UserHasActionsLeft(uid) )
            {
                {
                    db.departments.Add(dep);
                    db.SaveChanges();
                    return dep;
                }
            }
                
            return null;
            
        }


    }
}


