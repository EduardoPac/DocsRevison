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
    public class PendentViewModel : BaseViewModel, IInitialize, INavigationAware
    {
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;
        private readonly INavigationService _navigationService;

        public ObservableCollection<Document> DocumentsPend { get; set; }
        private List<User> Users { get; set; }

        public ICommand ItemSelectedCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        private Document _selected;
        public Document Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public PendentViewModel(
                        IPageDialogService dialogService,
                        IApiService apiService,
                        INavigationService navigationService)
        {
            _dialogService = dialogService;
            _apiService = apiService;
            _navigationService = navigationService;
            DocumentsPend = new ObservableCollection<Document>();

            ItemSelectedCommand = new DelegateCommand<Document>(ItemSelected);
            RefreshCommand = new DelegateCommand(RefreshSync);
        }

        private void RefreshSync()
        {
            LoadData();
            IsBusy = false;
        }

        private async void ItemSelected(Document obj)
        {
            if(obj.CurrentRevision.Status != Enum.EStatus.Pending)
            {
                string[] options = new string[]
                    {
                        "Aprovar",
                        "Ver Detalhes"
                    };

                var optionSelected = await _dialogService.DisplayActionSheetAsync(obj.NameFull, "Cancelar", "Reprovar", options);

                switch (optionSelected)
                {
                    case "Aprovar":
                        AproveRevision(obj);
                        break;
                    case "Ver Detalhes":
                        OpenDocument(obj);
                        break;
                    case "Reprovar":
                        ReproveDocument(obj);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                OpenDocument(obj);
            }

            Selected = null;
        }

        private async void ReproveDocument(Document obj)
        {
            await _apiService.ReproveDocument(obj.Id);
            LoadData();
        }

        private async void OpenDocument(Document obj)
        {
            var navParams = new NavigationParameters();
            navParams.Add("Document", obj);
            await _navigationService.NavigateAsync(nameof(DetailPage), navParams);
        }

        private async void AproveRevision(Document obj)
        {
            await _apiService.AproveDocument(obj.Id);
            LoadData();
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadData();
        }

        private async void LoadData()
        {
            IsBusy = true;
            DocumentsPend.Clear();
            
            var users = await _apiService.GetUsers();
            Users = users.Where(a => a.Id != App.User.Id).ToList();
            var documents = await _apiService.GetDocuments();

            if (!documents.Any())
            {
                DocumentsPend.Clear();
            }
            else
            {
                var documentsPend = documents.Where(d => d.CurrentRevision.Status != Enum.EStatus.Ok).ToList();
                documentsPend.ForEach(d =>
                {
                    d.CreatorName = users.FirstOrDefault(u => u.Id == d.CreatorId).Name;
                    d.RevisorName = users.FirstOrDefault(u => u.Id == d.CurrentRevision.RevisorId).Name;
                });

                DocumentsPend = new ObservableCollection<Document>(documentsPend);
                RaisePropertyChanged(nameof(DocumentsPend));
            }
            IsBusy = false;
        }

        public void Initialize(INavigationParameters parameters)
        {
            LoadData();
        }
    }
}
