using Prism.Events;
using SQLiteDBConnection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SzafaInterfaces;
using System.Data.Entity.Infrastructure;
using System.IO;
using DatabaseEntities;

namespace SQLiteDatabaseConnectionModule.Services
{
    public class SQLiteDatabaseConnectionService : IDatabaseConnectionService
    {
        private IEventAggregator eventAggregator;
        private SzafaModel dbconn = new SzafaModel();

        public SQLiteDatabaseConnectionService(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public IEnumerable<T> GetEntities<T>() where T : class
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

        public List<clothes> GetClothes()
        {
            if (!CheckDatabaseExistence(dbconn)) throw new DbUpdateException();
            var searchresults = dbconn.clothes.Include(c => c.types).ToList<clothes>();
            return searchresults;
        }

        public List<types> GetTypes()
        {
            if (!CheckDatabaseExistence(dbconn)) throw new DbUpdateException();
            var searchresults = dbconn.types.ToList<types>();
            return searchresults;
        }

        public void UpdateClothes(clothes c)
        {
            if (!CheckDatabaseExistence(dbconn)) throw new DbUpdateException();
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

        public clothes GetPieceOfClothing(long id)
        {
            if (!CheckDatabaseExistence(dbconn)) throw new DbUpdateException();
            return dbconn.clothes.Find(id);
        }

        public void DeletePieceOfClothing(clothes c)
        {
            if (!CheckDatabaseExistence(dbconn)) throw new DbUpdateException();
            clothes cloth = dbconn.clothes.Find(c.id);
            dbconn.clothes.Remove(cloth);
            dbconn.SaveChanges();
        }

        public void AddClothes(clothes c)
        {
            if (!CheckDatabaseExistence(dbconn)) throw new DbUpdateException();
            dbconn.clothes.Add(c);
            dbconn.SaveChanges();
        }

        private string ExctractDataSource(string connectionString)
        {
            List<string> output = connectionString.Split('=').ToList();

            foreach (var t in output)
            {
                if (t.Contains("data source"))
                {
                    return output[output.IndexOf(t) + 1].Trim('"');
                }
            }
            return String.Empty;
        }

        private bool CheckDatabaseExistence(SzafaModel conn)
        {
            conn.Database.Connection.ConnectionString = "data source=" + "\"" + AppDomain.CurrentDomain.BaseDirectory + "main.db\"";
            //MessageBox.Show(conn.Database.Connection.ConnectionString);
            return File.Exists(ExctractDataSource(conn.Database.Connection.ConnectionString));
        }
    }
}
