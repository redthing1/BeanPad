using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace BeanPad.Views.Pages
{
    public class HomePage : UserControl
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}