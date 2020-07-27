using System;
using DocsRevision.Services;
using DocsRevision.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace DocsRevision.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IPageDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;

        public DelegateCommand LoginCommand { get; set; }

        string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public LoginViewModel(
                        IPageDialogService dialogService,
                        INavigationService navigationService,
                        IApiService apiService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            _apiService = apiService;

            LoginCommand = new DelegateCommand(LoginExecute);
        }

        private async void LoginExecute()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                await _dialogService.DisplayAlertAsync(null, "Campo email vazio", "Ok");
                return;
            }

            var User = await _apiService.GetUser(Email);

            if (User == null)
            {
                await _dialogService.DisplayAlertAsync(null, "Usuario não consta no sistema", "Ok");

                return;
            }

            App.User = User;
            await _navigationService.NavigateAsync("/" + nameof(NavigationPage) + "/" + nameof(TabPage));
        }
    }
}
