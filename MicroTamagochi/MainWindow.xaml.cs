using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MicroTamagochi
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer t1;
        int timer = 0;
        double decremento1 = 5.0;
        double decremento2 = 5.0;
        double decremento3 = 5.0;


        public MainWindow()
        {
            InitializeComponent();
            t1 = new DispatcherTimer();
            t1.Interval = TimeSpan.FromSeconds(1);
            t1.Tick += new EventHandler(reloj);
            t1.Start();
        }

        private void reloj(object sender, EventArgs e)
        {
            this.pbEnergia.Value -= decremento1;
            this.pbDiversion.Value -= decremento2;
            this.pbComida.Value -= decremento3;
            timer += 1;

            if (this.pbEnergia.Value <= 0 || this.pbDiversion.Value <= 0 || this.pbComida.Value <= 0)
            {
                t1.Stop();
                this.lblPuntuacion.Text = "TU PUNTUACIÓN ES DE " + this.timer.ToString() + " PUNTOS";
                this.lblPuntuacion.Visibility = Visibility.Visible;
                btnAlimentar.IsEnabled = false;
                btnDescansar.IsEnabled = false;
                btnJugar.IsEnabled = false;
            }
        }

        private void btnDescansar_Click(object sender, RoutedEventArgs e)
        {
            this.pbEnergia.Value += 6;
            decremento1 += 0.2;
            btnDescansar.IsEnabled = false;
            t1.Stop();

            Storyboard sbDormir = (Storyboard)this.Resources["dormirAnimation"];
            sbDormir.Begin();

            Thread.Sleep(1000);

            btnDescansar.IsEnabled = true;
            t1.Start();

            /*
                 DoubleAnimation cerrarOjoDerecho = new DoubleAnimation();
                 cerrarOjoDerecho.To = 5;
                 cerrarOjoDerecho.Duration = new Duration(TimeSpan.FromSeconds(1));
                 cerrarOjoDerecho.AutoReverse = true;
                 cerrarOjoDerecho.Completed += new EventHandler(finCerrarOjoDer);



                 DoubleAnimation cerrarOjoIzquierdo = new DoubleAnimation();
                 cerrarOjoIzquierdo.To = 5;
                 cerrarOjoIzquierdo.Duration = new Duration(TimeSpan.FromSeconds(1));
                 cerrarOjoIzquierdo.AutoReverse = true;

                 ojoDer.BeginAnimation(Ellipse.HeightProperty, cerrarOjoDerecho);
                 irisDer.BeginAnimation(Ellipse.HeightProperty, cerrarOjoDerecho);
                 irisDerNeg.BeginAnimation(Ellipse.HeightProperty, cerrarOjoDerecho);
                 ojoIzq.BeginAnimation(Ellipse.HeightProperty, cerrarOjoDerecho); 
             */
        }

        private void finAnimacionDormir(object sender, EventArgs e)
        {
            btnDescansar.IsEnabled = true;
            t1.Start();
        }

        private void btnJugar_Click(object sender, RoutedEventArgs e)
        {
            this.pbDiversion.Value += 5;
            decremento2 += 0.3;

            Storyboard sbJugar = (Storyboard)this.Resources["jugarAnimation"];
            sbJugar.Begin();

        }

        private void btnAlimentar_Click(object sender, RoutedEventArgs e)
        {
            this.pbComida.Value += 4;
            decremento3 += 0.6;

            Storyboard sbaux = (Storyboard)this.Resources["dormir"]; 
        }

        private void cambiarFondo(object sender, MouseButtonEventArgs e)
        {
            this.imgFondo.Source = ((Image)sender).Source;
        }
    }
}
