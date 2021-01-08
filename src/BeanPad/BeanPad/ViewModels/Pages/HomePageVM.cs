using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using AvaloniaEdit.Document;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace BeanPad.ViewModels.Pages {
    public class HomePageVM : ViewModelBase {
        public string Greeting => "Hello, welcome to the world!";
        [Reactive] public TextDocument EditorDocument { get; set; } = new();
        public ICommand MenuExit { get; } = ReactiveCommand.Create(() => { getLifetime().Shutdown(); });
        public ICommand MenuOpenFile { get; }
        public ICommand MenuSave { get; }
        public ICommand MenuSaveAs { get; }

        public HomePageVM() {
            MenuOpenFile = ReactiveCommand.CreateFromTask(openFile);
        }

        private async Task openFile() {
            var dlg = new OpenFileDialog();
            var res = await dlg.ShowAsync(getLifetime().MainWindow);
            var fileName = res.FirstOrDefault();
            if (fileName != null) {
                var fileContents = await File.ReadAllTextAsync(fileName);
                EditorDocument = new TextDocument(fileContents);
            }
        }

        private static IClassicDesktopStyleApplicationLifetime getLifetime() {
            var lifetime = Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
            if (lifetime == null) {
                throw new InvalidOperationException("could not get desktop lifetime for application");
            }

            return lifetime;
        }
    }
}