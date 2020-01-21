using System;
using Prism.Mvvm;

namespace DocsRevision.ViewModel
{
    public class BaseViewModel : BindableBase
    {
        string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
    }
}
