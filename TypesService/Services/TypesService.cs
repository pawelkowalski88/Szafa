using DatabaseConnectionSQLite;
using DatabaseConnectionSQLite.Services;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TypesService.Services
{
    public class TypesService
    {
        public TypesService(IUnityContainer container)
        {
            dbConnection = container.Resolve<DatabaseConnectionService>();
            UpdateTypesList();
        }

        public void UpdateTypesList()
        {
            //updating types list as an async operation
            updateTypesListTask = new Task(() =>
            {

                TypesList = dbConnection.GetTypes();
                //when types list is updated, fire an event
                TypesListUpdated(this, new EventArgs());
            });
            //Start the task
            updateTypesListTask.Start();
        }

        public List<types> TypesList { get; private set; }
        DatabaseConnectionService dbConnection;
        Task updateTypesListTask;
        public event EventHandler TypesListUpdated;
    }
}
