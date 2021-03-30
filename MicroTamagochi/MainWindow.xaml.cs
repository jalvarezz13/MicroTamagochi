using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        double decremento1 = 1.5;
        double decremento2 = 3.0;
        double decremento3 = 0.5;
        string nombre;


        public MainWindow()
        {
            InitializeComponent();
            VentanaBienvenida pantallaInicio = new VentanaBienvenida(this);
            pantallaInicio.ShowDialog();

            crearRanking();

            MessageBoxResult result = MessageBox.Show("¡¡Bienvenido " + this.nombre + "!!\nTe presento el Super Tamagochi donde el tiempo pasa volando...\nMario Bros es el protagonitas de nuestra historia, en su rostro tiene escondido algunos coleccionables y pulsando sobre éste conseguiras encontrarlos. Además conforme avances en el juego recibirás logros y premios que podrás canjear en futuro para salvar a la princesa Peach.\n¿Te apuntas a ayudar a Mario Bros en esta aventura? Let it go!", "Welcome to Super Tamagochi", MessageBoxButton.OK);

            t1 = new DispatcherTimer();
            t1.Interval = TimeSpan.FromSeconds(1);
            t1.Tick += new EventHandler(reloj);
            t1.Start();
        }

        private void crearRanking()
        {
            string[] lines = File.ReadAllLines(@"..\..\data.txt");
            int[] ranking = new int[100];
            for (int i = 0; i < lines.Length; i++)
            {
                ranking[i] = int.Parse(lines[i]);
            }
            Array.Sort(ranking);
            Array.Reverse(ranking);

            tb1.Text = "1) " + ranking[0].ToString();
            tb2.Text = "2) " + ranking[1].ToString();
            tb3.Text = "3) " + ranking[2].ToString();
            tb4.Text = "4) " + ranking[3].ToString();
            tb5.Text = "5) " + ranking[4].ToString();

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
                this.lblMensajes.Text = "TU PUNTUACIÓN ES DE " + this.timer.ToString() + " PUNTOS";
                this.lblMensajes.Visibility = Visibility.Visible;
                btnAlimentar.IsEnabled = false;
                btnDescansar.IsEnabled = false;
                btnJugar.IsEnabled = false;
                wpColeccionables.IsEnabled = false;
                RoutedEventArgs rea = new RoutedEventArgs();
                Button_Click(sender, rea);
                File.AppendAllText(@"..\..\data.txt", "\n" + this.timer.ToString());
            }
            else if (this.pbComida.Value <= 10)
            {
                t1.Stop();
                apagarEncenderBotones(false);
                Storyboard sbComidaBaja = (Storyboard)this.Resources["comidaBajaAnimation"];
                sbComidaBaja.Begin();
                apagarEncenderBotones(true);
                t1.Start();
            }
            else if (this.pbEnergia.Value <= 10)
            {
                t1.Stop();
                apagarEncenderBotones(false);
                Storyboard sbEnergiaBaja = (Storyboard)this.Resources["energiaBajaAnimation"];
                sbEnergiaBaja.Begin();
                apagarEncenderBotones(true);
                t1.Start();
            }
            else if (this.pbDiversion.Value <= 10)
            {
                t1.Stop();
                apagarEncenderBotones(false);
                Storyboard sbDiversionBaja = (Storyboard)this.Resources["diversionBajaAnimation"];
                sbDiversionBaja.Begin();
                apagarEncenderBotones(true);
                t1.Start();
            }

            if (timer == 10 && premio1.Visibility == Visibility.Hidden)
            {
                premio1.Visibility = Visibility.Visible;
                t1.Stop();
                MessageBoxResult result2 = MessageBox.Show("¡¡ENHORABUENA!!\nHas conseguido un premio por conseguir una duración de 10 segundos.", "Super Tamagochi Rewards", MessageBoxButton.OK);
                t1.Start();
            }

            if (timer == 20 && premio2.Visibility == Visibility.Hidden)
            {
                premio2.Visibility = Visibility.Visible;
                t1.Stop();
                MessageBoxResult result2 = MessageBox.Show("¡¡ENHORABUENA!!\nHas conseguido un premio por conseguir una duración de 20 segundos.", "Super Tamagochi Rewards", MessageBoxButton.OK);
                t1.Start();
            }

            if (timer == 30 && premio3.Visibility == Visibility.Hidden)
            {
                premio3.Visibility = Visibility.Visible;
                t1.Stop();
                MessageBoxResult result2 = MessageBox.Show("¡¡ENHORABUENA!!\nHas conseguido un premio por conseguir una duración de 30 segundos.", "Super Tamagochi Rewards", MessageBoxButton.OK);
                t1.Start();
            }
        }

        private void btnDescansar_Click(object sender, RoutedEventArgs e)
        {
            this.pbEnergia.Value += 9;
            decremento1 += 0.05;
            apagarEncenderBotones(false);
            t1.Stop();

            Storyboard sbDormir = (Storyboard)this.Resources["dormirAnimation"];
            sbDormir.Begin();

            apagarEncenderBotones(true);
            t1.Start();
        }

        private void btnJugar_Click(object sender, RoutedEventArgs e)
        {
            this.pbDiversion.Value += 12;
            decremento2 += 0.1;
            apagarEncenderBotones(false);
            t1.Stop();

            Storyboard sbJugar = (Storyboard)this.Resources["jugarAnimation"];
            sbJugar.Begin();

            apagarEncenderBotones(true);
            t1.Start();

        }

        private void btnAlimentar_Click(object sender, RoutedEventArgs e)
        {
            this.pbComida.Value += 10;
            decremento3 += 0.2;
            apagarEncenderBotones(false);
            t1.Stop();

            Storyboard sbComer = (Storyboard)this.Resources["comerAnimation"];
            sbComer.Begin();

            apagarEncenderBotones(true);
            t1.Start();
        }

        private void apagarEncenderBotones(Boolean propiedad)
        {
            btnAlimentar.IsEnabled = propiedad;
            btnDescansar.IsEnabled = propiedad;
            btnJugar.IsEnabled = propiedad;
        }


        private void cambiarFondo(object sender, MouseButtonEventArgs e)
        {
            this.imgFondo.Source = ((Image)sender).Source;
            if (logro3.Visibility == Visibility.Hidden)
            {
                logro3.Visibility = Visibility.Visible;
                t1.Stop();
                MessageBoxResult result2 = MessageBox.Show("¡¡ENHORABUENA!!\nHas conseguido un logro por cambiar el fondo del Tamagochi", "Super Tamagochi Rewards", MessageBoxButton.OK);
                t1.Start();
            }
        }

        private void acercaDe(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Programa realizado por:\nJavier Álvarez Páramo\n\n¿Desea salir?", "Acerca de Super Tamagochi", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.Close();
                    break;
            }
        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre.ToUpper();
            lblMensajes.Text = lblMensajes.Text + " " + this.nombre;
        }

        private void colocarColeccionable(object sender, DragEventArgs e)
        {
            Image aux = (Image)e.Data.GetData(typeof(Image));
            switch (aux.Name)
            {
                case "imgGorroMini":
                    imgGorro.Visibility = Visibility.Visible;
                    imgChef.Visibility = Visibility.Hidden;
                    btnLimpiar.Visibility = Visibility.Visible;
                    break;
                case "imgGafasMini":
                    imgGafas.Visibility = Visibility.Visible;
                    btnLimpiar.Visibility = Visibility.Visible;
                    break;
                case "imgChefMini":
                    imgChef.Visibility = Visibility.Visible;
                    imgGorro.Visibility = Visibility.Hidden;
                    btnLimpiar.Visibility = Visibility.Visible;
                    break;
                case "imgBigoteMini":
                    imgBigote.Visibility = Visibility.Visible;
                    bigote.Visibility = Visibility.Hidden;
                    btnLimpiar.Visibility = Visibility.Visible;
                    break;
            }
            if (logro2.Visibility == Visibility.Hidden)
            {
                logro2.Visibility = Visibility.Visible;
                t1.Stop();
                MessageBoxResult result2 = MessageBox.Show("¡¡ENHORABUENA!!\nHas conseguido un logro por poner un coleccionable a Mario", "Super Tamagochi Rewards", MessageBoxButton.OK);
                t1.Start();
            }

        }

        private void inicioArrastrarCol(object sender, MouseButtonEventArgs e)
        {
            DataObject data = new DataObject(((Image)sender));
            DragDrop.DoDragDrop((Image)sender, data, DragDropEffects.Move);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            imgGorro.Visibility = Visibility.Hidden;
            imgChef.Visibility = Visibility.Hidden;
            imgGafas.Visibility = Visibility.Hidden;
            imgBigote.Visibility = Visibility.Hidden;
            bigote.Visibility = Visibility.Visible;
            btnLimpiar.Visibility = Visibility.Hidden;
        }

        private void unlockChef(object sender, MouseButtonEventArgs e)
        {
            imgChefMini.Visibility = Visibility.Visible;
            MessageBoxResult result = MessageBox.Show("Has desbloqueado el coleccionable 'Gorro de Chef'", "Super Tamagochi Rewards", MessageBoxButton.OK);
            if (logro1.Visibility == Visibility.Hidden)
            {
                logro1.Visibility = Visibility.Visible;
                t1.Stop();
                MessageBoxResult result2 = MessageBox.Show("¡¡ENHORABUENA!!\nHas conseguido un logro por desbloquear un coleccionable", "Super Tamagochi Rewards", MessageBoxButton.OK);
                t1.Start();
            }
        }

        private void unlockGafas(object sender, MouseButtonEventArgs e)
        {
            imgGafasMini.Visibility = Visibility.Visible;
            MessageBoxResult result = MessageBox.Show("Has desbloqueado el coleccionable 'Gafas Rosas'", "Super Tamagochi Rewards", MessageBoxButton.OK);
            if (logro1.Visibility == Visibility.Hidden)
            {
                logro1.Visibility = Visibility.Visible;
                t1.Stop();
                MessageBoxResult result2 = MessageBox.Show("¡¡ENHORABUENA!!\nHas conseguido un logro por desbloquear un coleccionable", "Super Tamagochi Rewards", MessageBoxButton.OK);
                t1.Start();
            }
        }

        private void unlockBigote(object sender, MouseButtonEventArgs e)
        {
            imgBigoteMini.Visibility = Visibility.Visible;
            MessageBoxResult result = MessageBox.Show("Has desbloqueado el coleccionable 'Bigote'", "Super Tamagochi Rewards", MessageBoxButton.OK);

            if (logro1.Visibility == Visibility.Hidden)
            {
                logro1.Visibility = Visibility.Visible;
                t1.Stop();
                MessageBoxResult result2 = MessageBox.Show("¡¡ENHORABUENA!!\nHas conseguido un logro por desbloquear un coleccionable", "Super Tamagochi Rewards", MessageBoxButton.OK);
                t1.Start();
            }
        }

        private void unlockGorro(object sender, MouseButtonEventArgs e)
        {
            imgGorroMini.Visibility = Visibility.Visible;
            MessageBoxResult result = MessageBox.Show("Has desbloqueado el coleccionable 'Gorro de Navidad'", "Super Tamagochi Rewards", MessageBoxButton.OK);
            if (logro1.Visibility == Visibility.Hidden)
            {
                logro1.Visibility = Visibility.Visible;
                t1.Stop();
                MessageBoxResult result2 = MessageBox.Show("¡¡ENHORABUENA!!\nHas conseguido un logro por desbloquear un coleccionable", "Super Tamagochi Rewards", MessageBoxButton.OK);
                t1.Start();
            }
        }
    }
}