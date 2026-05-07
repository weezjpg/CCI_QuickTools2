using SourceChord.FluentWPF;
using System.Windows;
using System.Windows.Input;

namespace CCI_QuickTools2
{
    public partial class MainWindow : AcrylicWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.PreviewKeyDown += (s, e) => {
                if (e.Key == Key.Space && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                {
                    SearchBox.Focus();
                    e.Handled = true;
                }
            };

            // Single click execution for search results
            ResultsList.PreviewMouseLeftButtonDown += (s, e) => {
                var item = (e.OriginalSource as FrameworkElement)?.DataContext as ViewModels.SearchResultItem;
                if (item != null)
                {
                    (DataContext as ViewModels.MainViewModel)?.SelectSearchResultCommand.Execute(item);
                    e.Handled = true;
                }
            };
        }

        // Permite arrastar a janela clicando no Header
        private void AppTopbar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                AdjustWindowSize();
            }
            else
            {
                this.DragMove();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            AdjustWindowSize();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void AdjustWindowSize()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                BtnMaximize.Content = "\uE922"; // Ícone de Maximizar
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                BtnMaximize.Content = "\uE923"; // Ícone de Restaurar
            }
        }

        private void SearchBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                if (ResultsList.SelectedIndex < ResultsList.Items.Count - 1)
                    ResultsList.SelectedIndex++;
                ResultsList.ScrollIntoView(ResultsList.SelectedItem);
                e.Handled = true;
            }
            else if (e.Key == Key.Up)
            {
                if (ResultsList.SelectedIndex > 0)
                    ResultsList.SelectedIndex--;
                ResultsList.ScrollIntoView(ResultsList.SelectedItem);
                e.Handled = true;
            }
            else if (e.Key == Key.Enter)
            {
                if (ResultsList.SelectedItem != null)
                {
                    var vm = DataContext as ViewModels.MainViewModel;
                    var item = ResultsList.SelectedItem as ViewModels.SearchResultItem;
                    
                    if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control && item?.Data is Services.AdObject adObj)
                    {
                        vm?.OpenInExplorerCommand.Execute(adObj);
                    }
                    else
                    {
                        vm?.SelectSearchResultCommand.Execute(item);
                    }
                    e.Handled = true;
                }
            }
        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (DataContext as ViewModels.MainViewModel)?.ShowSuggestionsCommand.Execute(null);
        }
    }
}
