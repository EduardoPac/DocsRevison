using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DocsRevision.Models;
using DocsRevision.Services;
using DocsRevision.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace DocsRevision.ViewModel
{
    public class SelectUserViewModel : BaseViewModel, INavigationAware
    {
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;
        private readonly INavigationService _navigationService;

        public ObservableCollection<User> Users { get; set; }
        private Document CurrentDocument { get; set; }

        public ICommand ItemSelectedCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        private User _selected;
        public User Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public ICommand SendRevision { get; set; }

        public SelectUserViewModel(
                        IPageDialogService dialogService,
                        IApiService apiService,
                        INavigationService navigationService)
        {
            _dialogService = dialogService;
            _apiService = apiService;
            _navigationService = navigationService;

            ItemSelectedCommand = new DelegateCommand<User>(ItemSelected);
            RefreshCommand = new DelegateCommand(RefreshSync);
            SendRevision = new DelegateCommand(SendToRevision);
            Users = new ObservableCollection<User>();
        }

        private async void SendToRevision()
        {
            if (Selected == null)
                return;

            await _apiService.NewRevisionSend(CurrentDocument.Id, Selected.Id);

            await _navigationService.GoBackToRootAsync();
        }

        private void RefreshSync()
        {
            LoadData();
        }

        private async void ItemSelected(User obj)
        {
            Selected = obj;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            CurrentDocument = parameters["Document"] as Document;
            LoadData();
        }

        private async void LoadData()
        {
            Users.Clear();

            var users = await _apiService.GetUsers();
            var usersNotMe = users.Where(a => a.Id != App.User.Id);

            Users = new ObservableCollection<User>(usersNotMe);
            RaisePropertyChanged(nameof(Users));
        }
    }
}
