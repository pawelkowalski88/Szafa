using ClothesService.Services;
using DatabaseConnectionSQLite;
using DatabaseConnectionSQLite.Services;
using Microsoft.Practices.Unity;
using PresentationUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothesListModule.ViewModels
{
    public class ClothesListViewModel : PropertyChangedImplementation
    {
        public ClothesListViewModel(ClothesServices clothesService)
        {
            this.clothesService = clothesService;
            clothesService.ClothesListUpdated += ClothesService_ClothesListUpdated;
            clothesService.UpdateClothesList();
        }

        private void ClothesService_ClothesListUpdated(object sender, EventArgs e)
        {
            InvokePropertyChanged("ClothesList");
        }

        public List<clothes> ClothesList
        {
            get
            {
                return clothesService.ClothesList;
            }
        }
        ClothesServices clothesService;
    }
}
