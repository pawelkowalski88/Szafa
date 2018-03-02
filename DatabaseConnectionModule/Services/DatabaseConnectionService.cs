
using SQLiteDatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DatabaseConnectionModule.Services
{
    public class DatabaseConnectionService
    {
        public DatabaseConnectionService()
        {
        }

        public IEnumerable<T> GetEntities<T>() where T : class
        {
            using (var dbconn = new SzafaSQLiteEntities())
            {
                //parse the property name (remove the namespaces with Split method)
                string typeName = typeof(T).ToString().Split('.').Last();
                //get the elements from the right table
                var searchresults = (DbSet<T>)dbconn.GetType().GetProperty(typeName).GetValue(dbconn);
                //yield-return the IEnumerable of the results
                foreach (T el in searchresults)
                {
                    yield return el;
                }
            }
        }

        public List<clothes> GetClothes()
        {
            using (var dbconn = new SzafaSQLiteEntities())
            {
                try
                {
                    var searchresults = dbconn.clothes.Include(c => c.types).ToList<clothes>();
                    return searchresults;
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.ToString());
                    return null;
                }
            }
        }

        public List<types> GetTypes()
        {
            using (var dbconn = new SzafaSQLiteEntities())
            {
                var searchresults = dbconn.types.ToList<types>();
                return searchresults;
            }
        }

        public bool UpdateClothes(clothes c)
        {
            try
            {
                using (var dbconn = new SzafaSQLiteEntities())
                {
                    clothes cloth = dbconn.clothes.Find(c.id);
                    cloth.description = c.description;
                    cloth.name = c.name;
                    cloth.in_use = c.in_use;
                    cloth.in_use_from = c.in_use_from;
                    cloth.last_time_on = c.last_time_on;
                    cloth.picture_path = c.picture_path;
                    cloth.times_on = c.times_on;
                    cloth.type_id = c.type_id;
                    dbconn.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error");
                Console.WriteLine(e.ToString());
                return false;
            }
            return true;
        }

        public clothes GetPieceOfClothing(long id)
        {
            using (var dbconn = new SzafaSQLiteEntities())
            {
                return dbconn.clothes.Find(id);
            }
        }

        public void DeletePieceOfClothing(clothes c)
        {
            using (var dbconn = new SzafaSQLiteEntities())
            {
                clothes cloth = dbconn.clothes.Find(c.id);
                dbconn.clothes.Remove(cloth);
                dbconn.SaveChanges();
            }
        }

        public void AddClothes(clothes c)
        {
            using (var dbconn = new SzafaSQLiteEntities())
            {
                dbconn.clothes.Add(c);
                try
                {
                    dbconn.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
        }
    }
}
