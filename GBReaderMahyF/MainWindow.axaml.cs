using Avalonia.Controls;
using System.Collections.Generic;
using Avalonia.Controls.Notifications;
using GBReaderMahyF.Presentations.Notification;
using GBReaderMahyF.Presentations.Routes;

namespace GBReaderMahyF
{
    public partial class MainWindow : Window, IRouterToView, IShowNotifications
    {
        private readonly WindowNotificationManager _notificationManager;
        private readonly IDictionary<string, UserControl> _pages = new Dictionary<string, UserControl>();

        
        /// <summary>
        /// Constructeur de la MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            _notificationManager = new WindowNotificationManager(this)
            {
                Position = NotificationPosition.BottomRight
            };
        }
        
        
        /// <summary>
        /// Méthode qui permet d'ajouter un UserControl à la MainWindow. La premier
        /// UserControle ajouter sera celui affiché de base. 
        /// </summary>
        /// <param name="pageName">string qui est le nom de la page</param>
        /// <param name="page">UserControl qui est la page que l'on veut ajouter</param>
        internal void RegisterPage(string pageName, UserControl page)
        {
            _pages[pageName] = page;
            if(Content == null)
            {
                Content = page;
            }
        }
        
        /// <summary>
        /// Méthode qui permet de passer d'une page à l'autre 
        /// </summary>
        /// <param name="pageName">string qui est le nom de la page vers laquelle on souhaite se rendre</param>
        public void GoTo(string pageName)
        {
            Content = _pages[pageName];
        }
        
        
        /// <summary>
        /// Méthode qui permet d'afficher une notification à l'utilisateur
        /// </summary>
        /// <param name="severity">NotificationSeverity qui est le type de notification que l'on veut afficher</param>
        /// <param name="title">string qui va être le titre de la notification</param>
        /// <param name="message">string qui va être le message de la notification</param>
        public void Push(NotificationSeverity severity, string title, string message)
        {
            var notification = new Notification(title, message, severity switch
            {
                NotificationSeverity.Info => NotificationType.Information,
                NotificationSeverity.Warning => NotificationType.Warning,
                NotificationSeverity.Success => NotificationType.Success,
                _ => NotificationType.Error,
            });

            if(this.IsVisible)
            {
                _notificationManager.Show(notification);
            }        
        }
    }
}