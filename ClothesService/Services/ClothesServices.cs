using CustomEvents;
using DatabaseEntities;
using ImageServiceModuleLibrary.Services;
using Prism.Events;
using SQLiteDBConnection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using SzafaEntities;
using SzafaInterfaces;

namespace ClothesService.Services
{
    public class ClothesServices : IClothesServices
    {
        IEventAggregator eventAggregator;
        bool updating = false;
        public ClothesServices(ImageService imageService, IDatabaseConnectionService connectionService, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            dbConnection = connectionService;
            this.imageService = imageService;
            eventAggregator.GetEvent<DatabaseConnectionRefreshRequestedEvent>().Subscribe(RefreshClothesList);
        }

        public async void RefreshClothesList()
        {
            if (updating) { return; }

            updating = true;

            await Task.Run(() =>
            {
                try
                {
                    List<clothes> clothesList = dbConnection.GetClothes();
                    ClothesList = new List<PieceOfClothing>();
                    foreach (var c in clothesList)
                    {
                        ClothesList.Add(new PieceOfClothing(c, imageService.RetrieveImage(c.picture_path)));
                    }
                }
                catch (Exception e)
                {
                    ClothesList = new List<PieceOfClothing>();
                    ClothesListUpdated(this, new EventArgs());
                    eventAggregator.GetEvent<DatabaseConnectionErrorOccuredEvent>().Publish(e.ToString());
                    //MessageBox.Show(e.ToString());
                }
            });

            //when clothes list is updated, fire an event
            ClothesListUpdated(this, new EventArgs());
            updating = false;
        }

        public void UpdatePieceOfClothing(PieceOfClothing c)
        {
            try
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
            catch (Exception e)
            {
                eventAggregator.GetEvent<DatabaseConnectionErrorOccuredEvent>().Publish(e.ToString());
            }
        }

        public void AddPieceOfClothing(PieceOfClothing c)
        {
            try
            {
                clothes cl = c.Toclothes();
                if (cl.picture_path == null) cl.picture_path = "";
                dbConnection.AddClothes(cl);
                ImageService imageService = new ImageService();
                string generatedName = imageService.SaveImage(c.PicturePath, cl.id);
                if (generatedName != null)
                {
                    cl.picture_path = generatedName;
                }
            }
            catch (Exception e)
            {
                eventAggregator.GetEvent<DatabaseConnectionErrorOccuredEvent>().Publish(e.ToString());
                return;
            }
        }

        public clothes GetPieceOfClothing(long id)
        {
            try
            {
                return dbConnection.GetPieceOfClothing(id);
            }
            catch (Exception e)
            {
                eventAggregator.GetEvent<DatabaseConnectionErrorOccuredEvent>().Publish(e.ToString());
                return null;
            }
        }

        public void DeletePieceOfClothing(PieceOfClothing c)
        {
            try
            {
                clothes cl = c.Toclothes();
                dbConnection.DeletePieceOfClothing(cl);
            }
            catch(Exception e)
            {
                eventAggregator.GetEvent<DatabaseConnectionErrorOccuredEvent>().Publish(e.ToString());
                return;
            }
        }

        public List<PieceOfClothing> ClothesList { get; private set; }
        IDatabaseConnectionService dbConnection;
        Task updateClothesListTask;
        public event EventHandler ClothesListUpdated;
        ImageService imageService;
    }
}
