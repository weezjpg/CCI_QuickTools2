using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using CCI_QuickTools2.Services;
using CCI_QuickTools2.ViewModels;

namespace CCI_QuickTools2
{
    public partial class App : System.Windows.Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            // Services
            services.AddSingleton<IDiagnosticService, DiagnosticService>();
            services.AddSingleton<ISystemControlService, SystemControlService>();
            services.AddSingleton<IActiveDirectoryService, ActiveDirectoryService>();
            services.AddSingleton<IMaintenanceService, MaintenanceService>();
            services.AddSingleton<INetworkService, NetworkService>();
            services.AddSingleton<IBackupService, BackupService>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddSingleton<IOutlookService, OutlookService>();
            services.AddSingleton<IDriverService, DriverService>();
            services.AddSingleton<IAppFeaturesRegistry, AppFeaturesRegistry>();
            services.AddSingleton<IQuickActionsService, QuickActionsService>();
            services.AddSingleton<IProfileService, ProfileService>();
            services.AddSingleton<IExcelExportService, ExcelExportService>();
            services.AddSingleton<IPluginService, PluginService>();
            services.AddSingleton<IExecutionService, ExecutionService>();

            // ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<DashboardViewModel>();
            services.AddSingleton<AboutViewModel>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<DiagnosticsViewModel>();
            services.AddSingleton<AdToolsViewModel>();
            services.AddSingleton<SystemToolsViewModel>();
            services.AddSingleton<NetworkViewModel>();
            services.AddSingleton<AdExplorerViewModel>();
            services.AddSingleton<UserMigrationViewModel>();
            services.AddSingleton<OutlookViewModel>();
            services.AddSingleton<AdAlertsViewModel>();
            services.AddSingleton<RemotePromptViewModel>();
            services.AddSingleton<PluginsViewModel>();

            ServiceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //CheckElevation();
            ApplyAccessibilitySettings();
            base.OnStartup(e);
            var mainWindow = new MainWindow();
            mainWindow.DataContext = ServiceProvider.GetRequiredService<MainViewModel>();
            mainWindow.Show();
        }

        private void ApplyAccessibilitySettings()
        {
            // If system animations are disabled (Reduce Motion), remove our Animations.xaml
            if (!SystemParameters.ClientAreaAnimation)
            {
                for (int i = 0; i < Resources.MergedDictionaries.Count; i++)
                {
                    if (Resources.MergedDictionaries[i].Source?.OriginalString.Contains("Animations.xaml") == true)
                    {
                        Resources.MergedDictionaries.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void CheckElevation()
        {
            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);

            if (!principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
            {
                var startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    // Use process name to avoid path issues with single-file pub
                    FileName = Environment.ProcessPath ?? System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName,
                    UseShellExecute = true,
                    Verb = "runas"
                };

                try
                {
                    System.Diagnostics.Process.Start(startInfo);
                    Environment.Exit(0);
                }
                catch (System.ComponentModel.Win32Exception)
                {
                    System.Windows.MessageBox.Show("Esta aplicação requer privilégios de administrador para funcionar.",
                                    "Permissões Necessárias", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Environment.Exit(0);
                }
            }
        }
    }
}
