using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Osada.menu
{
    class MenuButtons : Window
    {
        private void BtBuildingMenu_Click(object sender, RoutedEventArgs e)
        {
            GridArmy.Visibility = Visibility.Hidden;
            GridDiscovering.Visibility = Visibility.Hidden;
            GridBuild.Visibility = Visibility.Visible;
        }

        private void BtDiscoveringMenu_Click(object sender, RoutedEventArgs e)
        {
            GridArmy.Visibility = Visibility.Hidden;
            GridDiscovering.Visibility = Visibility.Hidden;
            GridBuild.Visibility = Visibility.Visible;
        }

        private void BtArmyMenu_Click(object sender, RoutedEventArgs e)
        {
            GridArmy.Visibility = Visibility.Hidden;
            GridDiscovering.Visibility = Visibility.Hidden;
            GridBuild.Visibility = Visibility.Visible;
        }
    }
}
