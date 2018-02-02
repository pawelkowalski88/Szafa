using DatabaseConnectionSQLite;
using DatabaseConnectionSQLite.Services;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesService.Services
{
    public class ClothesServices
    {
        public ClothesServices(IUnityContainer container)
        {
            dbConnection = container.Resolve<DatabaseConnectionService>();
        }

        public void UpdateClothesList()
        {
            //updating clothes list as an async operation
            updateClothesListTask = new Task(() =>
            {

                ClothesList = dbConnection.GetClothes();
                //when clothes list is updated, fire an event
                ClothesListUpdated(this, new EventArgs());
            });
            //Start the task
            updateClothesListTask.Start();
        }

        public List<clothes> ClothesList { get; private set; }
        DatabaseConnectionService dbConnection;
        Task updateClothesListTask;
        public event EventHandler ClothesListUpdated;
    }
}
