using System;
using System.Windows.Input;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;

namespace BeanPad.ViewModels.Pages {
    public class HomePageVM : ViewModelBase {
        public string Greeting => "Hello, welcome to the world!";

        public ICommand MenuExit { get; } = ReactiveCommand.Create(() => {
            var lifetime = App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
            if (lifetime == null) {
                throw new InvalidOperationException("could not get desktop lifetime for application");
            }

            lifetime.Shutdown();
        });
    }
}