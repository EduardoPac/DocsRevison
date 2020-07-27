using DocsRevision.Models;
using DocsRevision.Services;
using DocsRevision.ViewModel;
using DocsRevision.Views;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Plugin.Popups;
using Xamarin.Forms;

namespace DocsRevision
{
    public partial class App : PrismApplication
    {
        public static User User { get; set; }

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            var startPage = nameof(LoginPage);
            await NavigationService.NavigateAsync(startPage);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry) => RegisterNavigation(containerRegistry);

        private void RegisterNavigation(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IApiService, ApiService>();
            containerRegistry.RegisterPopupNavigationService();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
            containerRegistry.RegisterForNavigation<TabPage, TabViewModel>();
            containerRegistry.RegisterForNavigation<DocsPage, DocsViewModel>();
            containerRegistry.RegisterForNavigation<DetailPage, DetailViewModel>();
            containerRegistry.RegisterForNavigation<PendentPage, PendentViewModel>();
            containerRegistry.RegisterForNavigation<SelectUserPage, SelectUserViewModel>();
        }
    }
}
