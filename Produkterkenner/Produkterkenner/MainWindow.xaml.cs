using System;
using System.Collections.ObjectModel;
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
using System.IO;

namespace Produkterkenner
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Kategorie> obs = new ObservableCollection<Kategorie>();

        public void DateiLesen()
        {

            StreamReader dateiname = new StreamReader("Produktdetails.csv");
            string zeile;
            bool ja = false;
            string artikelnummer = tb_artikelnummer.Text;
            string artikel = "";

            //Solange die Zeile nicht leer ist, wird sie ausgeführt
            while ((zeile = dateiname.ReadLine()) != null)
            {
                //Wenn die Artikelnummer an der Stelle [0] ist
                if (artikelnummer == zeile.Split(';')[0])
                {
                    //ja, es kommt vor.. der gefundene artikel hat den wert der zeile
                    ja = true;
                    artikel = zeile;
                    break;
                }

            }
            dateiname.Close();

            //Wir kontrollieren die Kategorie
            if (ja == true)
            {
                //Array wird gesplittet, und dann wird es kontrolliert was an der Stelle [1] steht
                string[] array = zeile.Split(';');
                if (array[1] == "Handy")
                {
                    //Substring -> beginnt bei 0 und endet bei array[4].Length -1 weil am Ende ein "€" steht
                    Handy h = new Handy(Convert.ToInt32(array[0]), array[1], array[2], array[3], Convert.ToDouble(array[4].Substring(0, array[4].Length)), Convert.ToInt32(array[5]), Convert.ToDouble(array[6].Substring(0, array[6].Length - 1)));
                    //wird an obs hinzugefügt
                    obs.Add(h);
                    //Preise werden zusammengerechnet
                    lb_preis.Content = String.Format("{0:0,0.00}", Convert.ToDouble(lb_preis.Content.ToString().Substring(0, lb_preis.Content.ToString().Length - 1)) + h.Preis) + "€";
                }
                else
                {
                    Tasche t = new Tasche(Convert.ToInt32(array[0]), array[1], array[2], array[3], Convert.ToDouble(array[4].Substring(0, array[4].Length)), array[5], Convert.ToDouble(array[6].Substring(0, array[6].Length - 1)));
                    obs.Add(t);
                    lb_preis.Content = String.Format("{0:0,0.00}", Convert.ToDouble(lb_preis.Content.ToString().Substring(0, lb_preis.Content.ToString().Length - 1)) + t.Preis) + "€";
                }
            }

            else
            {
                throw new Exception("Artikelnummer nicht vorhanden!");
            }
        }


        public MainWindow()
        {
            InitializeComponent();
            //ObserveableCollection wird an die ListBox gebunden
            lb1.ItemsSource = obs;
        }

        private void Hinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            DateiLesen();
        }
        

        private void Entfernen_Click(object sender, RoutedEventArgs e)
        {
            //Ausgewähltes Produkt
            int index = lb1.SelectedIndex;
            //wenn keines ausgewählt wurde -> Exception
            if (index < 0)
                throw new Exception("Kein Element ausgewählt");
            else
            {
                //Der ausgewählte Element wird vom Preis subtrahiert
                lb_preis.Content = String.Format("{0:0,0.00}", Convert.ToDouble(lb_preis.Content.ToString().Substring(0, lb_preis.Content.ToString().Length - 1)) - obs[index].Preis) + "€";
                //Löscht das Element
                obs.RemoveAt(index);
            }
        }

        private void Beenden_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
