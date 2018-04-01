﻿using MainViewModule.Views;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SettingsViewModule.ViewModels
{
    public class SettingsViewModel
    {
        IRegionManager regionManager;
        ICommand cancelCommand;
        
        public SettingsViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        private void CancelSettings()
        {
            IRegion region = regionManager.Regions["MainRegion"];
            var view = region.ActiveViews.ToList().First();
            region.Deactivate(view);
            view = region.Views.ToList().Single(x => x.GetType() == typeof(MainView));
            region.Activate(view);
        }

        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new DelegateCommand(CancelSettings);
                }
                return cancelCommand;
            }
        }
    }
}