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
        bool updating = false;
        public TypesService(IUnityContainer container, IDatabaseConnectionService connectionService, IEventAggregator eventAggragator)
        {
            dbConnection = connectionService;
            eventAggragator.GetEvent<DatabaseConnectionRefreshRequestedEvent>().Subscribe(UpdateTypesList);
            updating = false;
            UpdateTypesList();
        }

        public async void UpdateTypesList()
        {
            if(updating) { return; }

            updating = true;

            //updating types list as an async operation
            await Task.Run(() =>
            {
                TypesList = new List<ClothingType>();
                var typesList = dbConnection.GetTypes();
                foreach (var t in typesList)
                {
                    TypesList.Add(new ClothingType(t));
                }
            });
            //Start the task
            //updateTypesListTask.Start();

            //when types list is updated, fire an event
            TypesListUpdated(this, new EventArgs());
            updating = false;
        }

        public void UpdateType(ClothingType t)
        {
            if (t.Name != "" && t.Name != null)
            {
                dbConnection.UpdateTypes(t.Totypes());
            }
            UpdateTypesList();
        }

        public List<ClothingType> TypesList { get; private set; }
        IDatabaseConnectionService dbConnection;
        Task updateTypesListTask;
        public event EventHandler TypesListUpdated;
    }
}
