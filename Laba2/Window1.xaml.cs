using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Laba2
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1(List<ThreatModel> updatedThreats, List<ThreatModel> newThreats)
        {
            InitializeComponent();
            if (updatedThreats.Any()) dgUpdatedThreats.ItemsSource = updatedThreats;
            else
            {
                dgUpdatedThreats.Visibility = System.Windows.Visibility.Hidden;
                UpdatesMessage.Visibility = System.Windows.Visibility.Visible;
            }
            if (newThreats.Any()) dgNewThreats.ItemsSource = newThreats;
            else
            {
                dgNewThreats.Visibility = System.Windows.Visibility.Hidden;
                NewThreatsMessage.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
