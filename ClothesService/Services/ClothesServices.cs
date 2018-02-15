using DatabaseConnectionSQLite;
using DatabaseConnectionSQLite.Services;
using ImageServiceModuleLibrary.Services;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SzafaEntities;

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
                ImageService imageService = new ImageService();
                var clothesList = dbConnection.GetClothes();
                ClothesList = new List<PieceOfClothing>();
                foreach (var c in clothesList)
                {
                    ClothesList.Add(new PieceOfClothing(c, imageService.RetrieveImage(c.picture_path)));
                }
                //when clothes list is updated, fire an event
                ClothesListUpdated(this, new EventArgs());
            });
            //Start the task
            updateClothesListTask.Start();
        }

        public void UpdatePieceOfClothing(PieceOfClothing c)
        {
            clothes cl = c.Toclothes();
            ImageService imageService = new ImageService();
            string generatedName = imageService.SaveImage(c.PicturePath, c.Id);
            if (generatedName != null)
            {
                cl.picture_path = generatedName;
            }
            dbConnection.UpdateClothes(cl);

        }

        public clothes GetPieceOfClothing(long id)
        {
            return dbConnection.GetPieceOfClothing(id);
        }

        public void AddPieceOfClothing(PieceOfClothing c)
        {
            clothes cl = c.Toclothes();
            if (cl.picture_path == null)
            {
                cl.picture_path = "";
            }
            dbConnection.AddClothes(cl);
        }

        public List<PieceOfClothing> ClothesList { get; private set; }
        DatabaseConnectionService dbConnection;
        Task updateClothesListTask;
        public event EventHandler ClothesListUpdated;
    }
}
