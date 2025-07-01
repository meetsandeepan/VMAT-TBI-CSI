using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using VMATTBICSIAutoPlanningHelpers.Helpers;
using VMATTBICSIAutoPlanningHelpers.Logging;

namespace VMATCSIAutoPlanMT
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var args = desktop.Args?.ToList() ?? new List<string>();
                
                // Handle command line arguments first
                HandleCommandLineArgs(args);
                
                // Create main window
                desktop.MainWindow = new VMAT_CSI.CSIAutoPlanMW(args);
            }

            base.OnFrameworkInitializationCompleted();
        }

        /// <summary>
        /// Handle command line arguments for auto-conversion functionality
        /// </summary>
        /// <param name="args"></param>
        private void HandleCommandLineArgs(List<string> args)
        {
            if (args.Any() && string.Equals(args.First(), "-d"))
            {
                //called from import listener. Need to auto-downsample some important structures
                string logPath = ConfigurationHelper.ReadLogPathFromConfigurationFile(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\configuration\\log_configuration.ini");
                //if the log file path in the configuration file is empty, use the default path
                if (string.IsNullOrEmpty(logPath)) logPath = ConfigurationHelper.GetDefaultLogPath();
                
                Logger.GetInstance().LogPath = logPath;
                Logger.GetInstance().PlanType = VMATTBICSIAutoPlanningHelpers.Enums.PlanType.VMAT_CSI;
                Logger.GetInstance().MRN = args.ElementAt(1);

                Logger.GetInstance().OpType = VMATTBICSIAutoPlanningHelpers.Enums.ScriptOperationType.AutoConvertHighToDefaultRes;
                VMAT_CSI.AutoResConverter ARC = new VMAT_CSI.AutoResConverter(args.ElementAt(1), args.ElementAt(2));
                bool result = ARC.Execute();
                Logger.GetInstance().AppendLogOutput(ARC.GetLogOutput().ToString());
                if (result)
                {
                    Logger.GetInstance().AppendLogOutput(ARC.GetErrorStackTrace());
                    Logger.GetInstance().LogError("Unable to convert high resolution structures to default resolution! Try running the script normally and select the 'Generate Prelim Targets' tab");
                }
                AppClosingHelper.CloseApplication(ARC.GetAriaApplicationInstance(), ARC.GetIsPatientOpenStatus(), ARC.GetAriaIsModifiedStatus(), true);
                if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    desktop.Shutdown();
                }
            }
        }
    }
}
