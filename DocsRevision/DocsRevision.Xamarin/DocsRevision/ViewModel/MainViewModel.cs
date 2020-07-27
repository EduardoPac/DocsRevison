using System;
using DocsRevision.Services;
using DocsRevision.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace DocsRevision.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;
        private readonly INavigationService _navigationService;

        public DelegateCommand LoginCommand { get; set; }

        string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public MainViewModel(
                        IPageDialogService dialogService,
                        IApiService apiService,
                        INavigationService navigationService)
        {
            _dialogService = dialogService;
            _apiService = apiService;
            _navigationService = navigationService;

            LoginCommand = new DelegateCommand(LoginExecute);
        }

        private async void LoginExecute()
        {
            var User = await _apiService.GetUser(Email);

            if(User == null)
            {
                await _dialogService.DisplayAlertAsync(null, "Usuario não consta no sistema", "Ok");
                return;
            }
            
            App.User = User;
            await _navigationService.NavigateAsync(nameof(TabPage));
        }
    }
}
