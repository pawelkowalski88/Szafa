
using DatabaseConnectionModule.Services;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SzafaEntities;
using SzafaInterfaces;

namespace TypesService.Services
{
    public class TypesService : ITypesService
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
                TypesList = new List<ClothingType>();
                var typesList = dbConnection.GetTypes();
                foreach (var t in typesList)
                {
                    TypesList.Add(new ClothingType(t));
                }
                //when types list is updated, fire an event
                TypesListUpdated(this, new EventArgs());
            });
            //Start the task
            updateTypesListTask.Start();
        }

        public List<ClothingType> TypesList { get; private set; }
        DatabaseConnectionService dbConnection;
        Task updateTypesListTask;
        public event EventHandler TypesListUpdated;
    }
}
