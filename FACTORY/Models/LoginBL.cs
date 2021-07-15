using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FACTORY.Models
{
    public class LoginBL
    {


        FactoryEntities db = new FactoryEntities();

        public user Login( user u )
        {

            var usr = db.users.Where(( x ) => x.username == u.username && x.password == u.password).First();

            if ( usr != null )
            {
                DateTime last_reset_at = usr.last_reset;
                double hours_since_last_reset = (DateTime.Now - last_reset_at).TotalHours;
                if ( hours_since_last_reset >= 24.0 ) 
                {
                    usr.number_of_actions_left = usr.number_of_actions; // For each user, hold a "number_of_actions" column and reset "number_of_actions_left" accordingly, each 24 hours.
                    usr.last_reset = DateTime.Now;
                    db.SaveChanges();

                    return usr;

                }
                else // Less than 24 hours passed since the last reset, check if eligible for logging in.
                {

                    if ( usr.number_of_actions_left > 0 )
                    {
                        return usr; 
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return null;


        }

        public bool UserHasActionsLeft( int uid )
        {
            var usr = db.users.Where(( x ) => x.ID == uid).FirstOrDefault();
            if ( usr != null )
            {
                if ( usr.number_of_actions_left > 0 )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool ReduceOneActionIfAuthorized( int uid )
        {
            user usr = db.users.Where(( x ) => x.ID == uid).First();


            if ( usr.number_of_actions_left > 0 )
            {
                usr.number_of_actions_left--;
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }



        }
    }
}