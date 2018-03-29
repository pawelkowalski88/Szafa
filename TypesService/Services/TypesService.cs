using Microsoft.Practices.Unity;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SzafaEntities;
using SzafaInterfaces;
using CustomEvents;

namespace TypesService.Services
{
    public class TypesService : ITypesService
    {
        public TypesService(IUnityContainer container, IDatabaseConnectionService connectionService, IEventAggregator eventAggragator)
        {
            dbConnection = connectionService;
            eventAggragator.GetEvent<DatabaseConnectionRefreshRequestedEvent>().Subscribe(UpdateTypesList);
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
        IDatabaseConnectionService dbConnection;
        Task updateTypesListTask;
        public event EventHandler TypesListUpdated;
    }
}
