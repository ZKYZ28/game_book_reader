using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GBReaderMahyF.Domains;
using GBReaderMahyF.Presentations;
using GBReaderMahyF.Infrastructures.BD;
using GBReaderMahyF.Infrastructures.JSON;
using GBReaderMahyF.Page;
using GBReaderMahyF.Respositories;

namespace GBReaderMahyF
{
    //Aucune Dispence
    public partial class App : Application
    {
        private MainWindow _mainWindow = null!;
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                //MAIN
                _mainWindow = new MainWindow();
                desktop.MainWindow = _mainWindow;
                
                CreateAll();
                base.OnFrameworkInitializationCompleted();
            }
        }

        /// <summary>
        /// Méthode qui permet de créer les composants dont on aura besoin durant
        /// l'utilisation de l'application
        /// </summary>
        private void CreateAll()
        {
            try
            {
                ManagerReader manager = new ManagerReader();
                
                //DATABASE
                string myConnectionString = @"server=192.168.128.13;uid=in21b20208;pwd=0208;database=in21b20208";
                BookStorageFactory factory= new BookStorageFactory("MySql.Data.MySqlClient", myConnectionString);

                JsonSessionStorage storageJson = new JsonSessionStorage(manager);
                    
                //INIT
                
                //STATISTICS
                StatisticsView statisticsView = new StatisticsView();
                StatisticsPresenter statisticsPresenter = new StatisticsPresenter(statisticsView, manager, _mainWindow);
                
                //PAGE
                PageView pageView = new PageView();
                PagePresenter pagePresenter = new PagePresenter(pageView, manager,  _mainWindow, storageJson);
                
                //MENU
                MenuView menuView = new MenuView();
                MenuPresenter menuPresenter = new MenuPresenter(menuView,manager, _mainWindow, factory);

                MainPresenter mainPresenter = new MainPresenter(menuPresenter, pagePresenter, statisticsPresenter, _mainWindow); 
                
                _mainWindow.RegisterPage("menuView", menuView);
                _mainWindow.RegisterPage("pageView", pageView);
                _mainWindow.RegisterPage("statisticsView", statisticsView);
            }
            catch (Exception e)
            {
                MenuView menuView = new MenuView();
                _mainWindow.RegisterPage("menuView", menuView);
                menuView.DisplayError(e.Message);
            }
        }
    }
}
