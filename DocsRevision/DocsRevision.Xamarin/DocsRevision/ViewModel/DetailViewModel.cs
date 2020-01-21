using System;
using System.Collections.Generic;
using DocsRevision.Models;
using DocsRevision.Services;
using DocsRevision.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace DocsRevision.ViewModel
{
    public class DetailViewModel : BaseViewModel, INavigationAware
    {
        private readonly IPageDialogService _dialogService;
        private readonly IApiService _apiService;
        private readonly INavigationService _navigationService;

        public DelegateCommand RevisionSendCommand { get; set; }

        Document _documentDetail;
        public Document DocumentDetail
        {
            get => _documentDetail;
            set => SetProperty(ref _documentDetail, value);
        }

        string _createIN;
        public string CreateIn
        {
            get => _createIN;
            set => SetProperty(ref _createIN, value);
        }



        public DetailViewModel(
                        IPageDialogService dialogService,
                        IApiService apiService,
                        INavigationService navigationService)
        {
            _dialogService = dialogService;
            _apiService = apiService;
            _navigationService = navigationService;

            RevisionSendCommand = new DelegateCommand(SendRevision);
        }

        private async void SendRevision()
        {
            var navParams = new NavigationParameters();
            navParams.Add("Document", DocumentDetail);
            await _navigationService.NavigateAsync(nameof(SelectUserPage), navParams);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            DocumentDetail = parameters["Document"] as Document;
        }
    }
}
