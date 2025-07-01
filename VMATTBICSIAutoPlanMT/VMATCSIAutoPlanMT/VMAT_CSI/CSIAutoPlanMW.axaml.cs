using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace VMATCSIAutoPlanMT.VMAT_CSI
{
    public partial class CSIAutoPlanMW : Window
    {
        public CSIAutoPlanMW(List<string> args)
        {
            InitializeComponent();
            Console.WriteLine($"CSIAutoPlanMW initialized with {args.Count} arguments");
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            var messageBox = new Window
            {
                Title = "Help",
                Width = 400,
                Height = 300,
                Content = new TextBlock
                {
                    Text = "VMAT CSI Auto Planning Help\n\nThis application helps automate radiation therapy planning for craniospinal irradiation (CSI).\n\nUse the tabs to navigate through the planning workflow:\n1. Export/Import - Get patient data\n2. Specify Targets - Define treatment areas\n3. Structure Tuning - Create protective structures\n4. Beam Placement - Position radiation beams\n5. Optimization - Fine-tune the treatment plan",
                    TextWrapping = Avalonia.Media.TextWrapping.Wrap,
                    Margin = new Thickness(20)
                }
            };
            messageBox.Show();
        }

        private void QuickStart_Click(object sender, RoutedEventArgs e)
        {
            var messageBox = new Window
            {
                Title = "Quick Start",
                Width = 400,
                Height = 200,
                Content = new TextBlock
                {
                    Text = "Quick Start Guide\n\n1. Enter patient MRN\n2. Select structure set\n3. Choose plan template\n4. Set prescription parameters\n5. Use tabs to complete planning workflow",
                    TextWrapping = Avalonia.Media.TextWrapping.Wrap,
                    Margin = new Thickness(20)
                }
            };
            messageBox.Show();
        }
    }
} 