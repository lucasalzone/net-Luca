using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GestionePratiche;

namespace GestionePratiche
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void BtnPreventivo_Click(object sender, RoutedEventArgs e) {
            Window1 w = new Window1();
            w.Show();
        }

        private void BtnPratiche_Click(object sender, RoutedEventArgs e) {
            UIPratica p = new UIPratica();
            p.Show() ;
        }

        private void BtnAnagrafica_Click(object sender, RoutedEventArgs e) {
            UiAnagCliente c = new UiAnagCliente();
            c.Show();
        }
    }
}