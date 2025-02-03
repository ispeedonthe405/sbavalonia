using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace DemoTest.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        public string symName = "add";

        [RelayCommand]
        public void ToggleSymbol()
        {
            SymName = "book";
        }


        public MainWindowViewModel()
        {
            
        }
    }
}
