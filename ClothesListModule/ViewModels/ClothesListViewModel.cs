using ClothesService.Services;
using DatabaseConnectionSQLite;
using DatabaseConnectionSQLite.Services;
using Microsoft.Practices.Unity;
using PresentationUtility;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ClothesListModule.ViewModels
{
    public class ClothesListViewModel : PropertyChangedImplementation
    {
        public ClothesListViewModel(ClothesServices clothesService)
        {
            this.clothesService = clothesService;
            //Might be useful to use event aggregation in the future
            clothesService.ClothesListUpdated += ClothesService_ClothesListUpdated;
            UpdateClothesList();
        }

        private void ClothesService_ClothesListUpdated(object sender, EventArgs e)
        {
            InvokePropertyChanged("ClothesList");
            Updating = false;
        }

        private void UpdateClothesList()
        {
            clothesService.UpdateClothesList();
            Updating = true;
        }

        private void OnElementSelected(object obj)
        {
            clothes c = obj as clothes;

            if (c != null)
            {
                MessageBox.Show(c.name);
            }
        }

        public List<clothes> ClothesList
        {
            get
            {
                return clothesService.ClothesList;
            }
        }

        public bool Updating
        {
            get
            {
                return updating;
            }
            set
            {
                updating = value;
                InvokePropertyChanged("Updating");
            }
        }

        public ICommand SelectElementCommand
        {
            get
            {
                if(selectElementCommand == null)
                {
                    selectElementCommand = new DelegateCommand<object>(OnElementSelected);
                }
                return selectElementCommand;
            }
        }

        ClothesServices clothesService;
        bool updating;
        ICommand selectElementCommand;

    }
}
