using Prism.Modularity;
using Prism.Unity;
using System.Windows;
using System.Collections.ObjectModel;
using ImageServiceModuleLibrary.ModuleDefinitions;

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

            //Adding Types service
            moduleType = typeof(TypesService.ModuleDefinitions.TypesServiceModule);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = "TypesServiceModule",
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
                //depends on clothes service
                DependsOn = new Collection<string>()
                {
                    "ClothesServiceModule",
                    "NavigationViewModule"
                }
            });

            //Adding Clothes detailed view module
            moduleType = typeof(ClothesDetailedViewModule.ModuleDefinitions.ClothesDetailedViewModule);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = "ClothesDetailedViewModule",
                ModuleType = moduleType.AssemblyQualifiedName,
                //depends on clothes service
                DependsOn = new Collection<string>()
                {
                    "ClothesServiceModule"
                }
            });

            //Adding Clothes editing module
            moduleType = typeof(ClothesEditViewModule.ModuleDefinitions.ClothesEditViewModule);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = "ClothesEditViewModule",
                ModuleType = moduleType.AssemblyQualifiedName,
                //depends on clothes service
                DependsOn = new Collection<string>()
                {
                    "ClothesServiceModule",
                    "TypesServiceModule"
                }
            });

            //Adding navigation view module
            moduleType = typeof(NavigationViewModule.ModuleDefinitions.NavigationViewModule);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = "NavigationViewModule",
                ModuleType = moduleType.AssemblyQualifiedName
            });

            moduleType = typeof(ImageServiceModule);
            moduleCatalog.AddModule(new ModuleInfo()
            {
                ModuleName = "ImageServiceModule",
                ModuleType = moduleType.AssemblyQualifiedName
            });

        }
    }
}
