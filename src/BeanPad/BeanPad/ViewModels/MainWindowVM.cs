using BeanPad.ViewModels.Pages;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace BeanPad.ViewModels {
    public class MainWindowVM : ViewModelBase {
        public HomePageVM HomePage { get; } = new HomePageVM();

        [ObservableAsProperty] public string TitleBar { get; set; }

        public MainWindowVM() {
            HomePage.WhenAnyValue(x => x.CurrentFileShortname, (string change) => $"BeanPad - {change}")
                .ToPropertyEx(this, x => x.TitleBar);
        }
    }
}