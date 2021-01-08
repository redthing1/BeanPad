using BeanPad.ViewModels.Pages;

namespace BeanPad.ViewModels {
    public class MainWindowVM : ViewModelBase {
        public HomePageVM HomePage { get; } = new HomePageVM();

        public string TitleBar => $"BeanPad - {HomePage.CurrentFileShortname}";
    }
}