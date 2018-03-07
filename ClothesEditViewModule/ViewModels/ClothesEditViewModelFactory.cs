using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzafaEntities;
using SzafaInterfaces;

namespace ClothesEditViewModule.ViewModels
{
    public class ClothesEditViewModelFactory
    {
        IEventAggregator eventAggregator;
        IRegionManager regionManager;
        IClothesServices clothesService;
        ITypesService typesService;

        public ClothesEditViewModelFactory(IEventAggregator eventAggregator, IRegionManager regionManager, IClothesServices clothesService, ITypesService typesService)
        {
            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
            this.clothesService = clothesService;
            this.typesService = typesService;
        }

        public IClothesEditViewModel GenerateViewModel()
        {
            return new ClothesCreateViewModel(eventAggregator, regionManager, clothesService, typesService);
        }

        public IClothesEditViewModel GenerateViewModel(PieceOfClothing p)
        {
            return new ClothesEditViewModel(eventAggregator, regionManager, clothesService, typesService, p);
        }
    }
}
