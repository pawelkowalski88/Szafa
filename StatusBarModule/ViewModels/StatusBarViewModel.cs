using MainViewModule.Views;
using Prism.Commands;
using Prism.Regions;
using SettingsViewModule.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StatusBarModule.ViewModels
{
    public class StatusBarViewModel
    {
        IRegionManager regionManager;

        public StatusBarViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        ICommand openSettingsCommand;

        public ICommand OpenSettingsCommand
        {
            get
            {
                if (openSettingsCommand == null)
                {
                    openSettingsCommand = new DelegateCommand(OpenSettings);
                }
                return openSettingsCommand;
            }
        }

        private void OpenSettings()
        {
            IRegion region = regionManager.Regions["MainRegion"];
            if (!(region.ActiveViews.First().GetType() == typeof(SettingsView)))
            {
                var view = region.Views.ToList().Single(x => x.GetType() == typeof(SettingsView));
                region.Activate(view);
                return;
            }
            CancelSettings(region);
        }

        private void CancelSettings(IRegion region)
        {
            var view = region.ActiveViews.ToList().First();
            region.Deactivate(view);
            view = region.Views.ToList().Single(x => x.GetType() == typeof(MainView));
            region.Activate(view);
        }
    }
}
