using BeanPad.ViewModels.Pages;

namespace BeanPad.ViewModels {
    public class MainWindowVM : ViewModelBase {
        public string Greeting => "Hello World!";
        
        public HomePageVM HomePage { get; } = new HomePageVM();
    }
}