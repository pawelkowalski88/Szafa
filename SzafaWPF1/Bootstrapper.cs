using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ClothesListModule;
using System.Collections.ObjectModel;
using ClothesService.ModuleDefinitions;

namespace SzafaWPF1
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return new Shell();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;

            //Adding Database connection module
            var moduleType = typeof(DatabaseConnectionModule.ModuleDefinitions.DatabaseConnectionModule);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = "DatabaseConnectionModule",
                ModuleType = moduleType.AssemblyQualifiedName
            });

            //Adding Clothes service
            moduleType = typeof(ClothesService.ModuleDefinitions.ClothesServiceModule);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = "ClothesServiceModule",
                ModuleType = moduleType.AssemblyQualifiedName,
                //depends on database connection
                DependsOn = new Collection<string>()
                {
                    "DatabaseConnectionModule"
                }
            });

            //Adding Clothes view module
            moduleType = typeof(ClothesListModule.ModuleDefinitions.ClothesListModule);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = "ClothesViewModule",
                ModuleType = moduleType.AssemblyQualifiedName,
                //depends on database connection and clothes service
                DependsOn = new Collection<string>()
                {
                    "ClothesServiceModule"
                }
            });

            //Adding Clothes view module
            moduleType = typeof(ClothesDetailedViewModule.ModuleDefinitions.ClothesDetailedViewModule);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = "ClothesDetailedViewModule",
                ModuleType = moduleType.AssemblyQualifiedName,
                //depends on database connection and clothes service
                DependsOn = new Collection<string>()
                {
                    "ClothesServiceModule"
                }
            });
        }
    }
}
