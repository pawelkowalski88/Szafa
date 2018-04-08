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
        IFilteringService filteringService;
        IDatabaseConnectionService dbConnection;
        ImageService imageService;
        bool updating = false;
        List<PieceOfClothing> clothesListRaw;

        public List<PieceOfClothing> ClothesList { get; private set; }
        public event EventHandler ClothesListUpdated;

        public ClothesServices(ImageService imageService, IDatabaseConnectionService connectionService, IEventAggregator eventAggregator, IFilteringService filteringService)
        {
            this.eventAggregator = eventAggregator;
            dbConnection = connectionService;
            this.imageService = imageService;
            this.filteringService = filteringService;
            eventAggregator.GetEvent<ClothesListRefreshRequestedEvent>().Subscribe(RefreshClothesListAsync);
            eventAggregator.GetEvent<RefreshClothesFilteringEvent>().Subscribe(RefreshFiltering);
        }

        public void RefreshFiltering()
        {
            ClothesList = filteringService.ProcessAll(clothesListRaw);
            ClothesListUpdated(this, new EventArgs());
        }

        public async void RefreshClothesListAsync()
        {
            if (updating) { return; }

            updating = true;

            //wait for operation to finish
            await Task.Run(() =>
            {
                try
                {
                    RefreshClothesInternal();
                }
                catch (Exception)
                {
                    ClothesList = new List<PieceOfClothing>();
                }
            });

            //when clothes list is updated, fire an event
            ClothesListUpdated(this, new EventArgs());
            updating = false;
        }

        private void RefreshClothesInternal()
        {
            //Retrieve clothes object from DB
            List<clothes> clothesList = dbConnection.GetClothes();
            clothesListRaw = new List<PieceOfClothing>();
            foreach (var c in clothesList)
            {
                //create list of PieceOfClothing elements
                clothesListRaw.Add(new PieceOfClothing(c, imageService.RetrieveImage(c.picture_path)));
            }
            //Apply filtering
            ClothesList = filteringService.ProcessAll(clothesListRaw);
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
        
    }
}
