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
        // [Reactive] public TextDocument EditorDocument { get; set; } = new();
        private TextDocument editorDocument = new();

        public TextDocument EditorDocument {
            get => editorDocument;
            set => this.RaiseAndSetIfChanged(ref editorDocument, value);
        }

        public ICommand MenuNew { get; }
        public ICommand MenuExit { get; } = ReactiveCommand.Create(() => { getLifetime().Shutdown(); });
        public ICommand MenuOpenFile { get; }
        public ICommand MenuSave { get; }
        public ICommand MenuSaveAs { get; }

        [ObservableAsProperty] public string CurrentFileShortname { get; set; }

        public HomePageVM() {
            MenuNew = ReactiveCommand.Create(newFile);
            MenuOpenFile = ReactiveCommand.CreateFromTask(openFile);
            MenuSave = ReactiveCommand.Create(saveFile);
            MenuSaveAs = ReactiveCommand.Create(saveFileAs);

            this
                .WhenAnyValue(x => x.EditorDocument, change => Path.GetFileName(change.FileName) ?? "*")
                .ToPropertyEx(this, x => x.CurrentFileShortname);
        }

        private void newFile() {
            EditorDocument = new TextDocument();
        }

        private async Task openFile() {
            var dlg = new OpenFileDialog();
            var res = await dlg.ShowAsync(getLifetime().MainWindow);
            var fileName = res.FirstOrDefault();
            if (fileName != null) {
                var fileContents = await File.ReadAllTextAsync(fileName);
                EditorDocument = new TextDocument(fileContents) {FileName = fileName};
            }
        }

        private async Task saveFile() {
            if (EditorDocument.FileName != null) {
                // save to current file
                await File.WriteAllTextAsync(EditorDocument.FileName, EditorDocument.Text);
            } else {
                // run save as
                await saveFileAs();
            }
        }

        private async Task saveFileAs() {
            var dlg = new SaveFileDialog();
            var fileName = await dlg.ShowAsync(getLifetime().MainWindow);
            if (fileName != null) {
                EditorDocument = new TextDocument(EditorDocument.Text) {FileName = fileName};
                await saveFile();
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