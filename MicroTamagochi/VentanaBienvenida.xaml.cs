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
using System.Windows.Shapes;

namespace MicroTamagochi
{
    /// <summary>
    /// Lógica de interacción para VentanaBienvenida.xaml
    /// </summary>
    public partial class VentanaBienvenida : Window
    {
        MainWindow ventanaPadre;
        public VentanaBienvenida(MainWindow ventanaPadre)
        {
            InitializeComponent();
            this.ventanaPadre = ventanaPadre;
        }

        private void enviarNombre(object sender, RoutedEventArgs e)
        {
            ventanaPadre.setNombre(tbNombre.Text);
            this.Close();
        }

        private void tbNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                enviarNombre(this, new RoutedEventArgs());
            }
        }

        private void tbNombre_GotFocus(object sender, RoutedEventArgs e)
        {
            tbNombre.Clear();
        }
    }
}
