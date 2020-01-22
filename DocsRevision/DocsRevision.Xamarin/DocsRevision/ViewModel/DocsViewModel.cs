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
    public class DocsViewModel : BaseViewModel, INavigatedAware
    {
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;
        private readonly INavigationService _navigationService;

        public ObservableCollection<Document> Documents { get; set; }

        public ICommand ItemSelectedCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        private Document _selected;
        public Document Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }


        public DocsViewModel(
                        IPageDialogService dialogService,
                        IApiService apiService,
                        INavigationService navigationService)
        {
            _dialogService = dialogService;
            _apiService = apiService;
            _navigationService = navigationService;
            Documents = new ObservableCollection<Document>();

            ItemSelectedCommand = new DelegateCommand<Document>(ItemSelected);
            RefreshCommand = new DelegateCommand(RefreshSync);
        }

        private void RefreshSync()
        {
            LoadData();
        }

        private async void ItemSelected(Document obj)
        {
            string[] options = new string[]
                {
                    "Solicitar Revisão",
                    "Ver Detalhes"
                };

            var optionSelected = await _dialogService.DisplayActionSheetAsync(obj.NameFull, "Cancelar", "Deletar", options);

            switch (optionSelected)
            {
                case "Solicitar Revisão":
                    SendRevision(obj);
                    break;
                case "Ver Detalhes":
                    OpenDocument(obj);
                    break;
                case "Deletar":
                    RemoveDocument(obj);
                    break;
                default:
                    break;
            }

            Selected = null;
        }

        private async void RemoveDocument(Document obj)
        {
            await _apiService.RemoveDocument(obj.Id);
            LoadData();
        }

        private async void OpenDocument(Document obj)
        {
            var navParams = new NavigationParameters();
            navParams.Add("Document", obj);
            await _navigationService.NavigateAsync(nameof(DetailPage), navParams);
        }

        private async void SendRevision(Document obj)
        {
            var navParams = new NavigationParameters();
            navParams.Add("Document", obj);
            await _navigationService.NavigateAsync(nameof(SelectUserPage), navParams);
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

            var users = await _apiService.GetUsers();
            var documents = await _apiService.GetDocuments();

            var documentsOk = documents.Where(d => d.CurrentRevision.Status == Enum.EStatus.Ok).ToList();
            documentsOk.ForEach(d =>
            {
                d.CreatorName = users.FirstOrDefault(u => u.Id == d.CreatorId).Name;
            });

            Documents = new ObservableCollection<Document>(documentsOk);
            RaisePropertyChanged(nameof(Documents));
            IsBusy = false;
        }
    }
}
