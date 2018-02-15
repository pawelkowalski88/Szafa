using DatabaseConnectionSQLite;
using DatabaseConnectionSQLite.Services;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClothesService.Services
{
    public class ClothesServices
    {
        public ClothesServices(IUnityContainer container)
        {
            dbConnection = container.Resolve<DatabaseConnectionService>();
        }

        public void RefreshClothesList()
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

        public void UpdatePieceOfClothing(clothes c)
        {
            dbConnection.UpdateClothes(c);
        }

        public clothes GetPieceOfClothing(long id)
        {
            return dbConnection.GetPieceOfClothing(id);
        }

        public void AddPieceOfClothing(clothes c)
        {
            if (c.picture_path == null)
            {
                c.picture_path = "";
            }
            dbConnection.AddClothes(c);
        }

        public List<clothes> ClothesList { get; private set; }
        DatabaseConnectionService dbConnection;
        Task updateClothesListTask;
        public event EventHandler ClothesListUpdated;
    }
}
