using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows;
using System.IO;

namespace Produkterkenner
{
    public partial class MainWindow : Window
    {

        ObservableCollection<Kategorie> obs = new ObservableCollection<Kategorie>();
        Dictionary<string, Kategorie> dic = new Dictionary<string, Kategorie>();

        public void DateiLesen()
        {
            StreamReader dateiname = new StreamReader("Produktdetails.csv");
            string zeile;
            //Solange die Zeile(dateiname) nicht leer ist, wird sie ausgeführt
            while ((zeile = dateiname.ReadLine()) != null)
            {
                if (zeile.Split(';')[1] == "Handy")
                    dic.Add(zeile.Split(';')[0],
                        new Handy(Convert.ToInt32(zeile.Split(';')[0]), zeile.Split(';')[1], zeile.Split(';')[2], zeile.Split(';')[3],
                        Convert.ToDouble(zeile.Split(';')[4]), Convert.ToInt32(zeile.Split(';')[5]), Convert.ToDouble(zeile.Split(';')[6].Substring(0, zeile.Split(';')[6].Length - 1)))
                    );
                else
                    dic.Add(zeile.Split(';')[0],
                        new Tasche(Convert.ToInt32(zeile.Split(';')[0]), zeile.Split(';')[1], zeile.Split(';')[2], zeile.Split(';')[3],
                        Convert.ToDouble(zeile.Split(';')[4]), zeile.Split(';')[5], Convert.ToDouble(zeile.Split(';')[6].Substring(0, zeile.Split(';')[6].Length - 1)))
                    );
            }

            dateiname.Close();
        }

        public MainWindow()
        {
            InitializeComponent();
            //ObserveableCollection wird an die ListBox gebunden
            lb_produkte.ItemsSource = obs;
            DateiLesen();
        }

        private void Artikel_Suchen()
        {
            try
            {

                obs.Add(dic[tb_artikelnummer.Text]);
                lb_preis.Content = String.Format("{0:0,0.00}", Convert.ToDouble(lb_preis.Content.ToString().Substring(0, lb_preis.Content.ToString().Length - 1)) + dic[tb_artikelnummer.Text].Preis) + "€";
            }
            catch
            {
                throw new Exception("KEINE gültige Artikelnummer!");
            }
        }


        private void Hinzufuegen_Click(object sender, RoutedEventArgs e)
        {
            Artikel_Suchen();
        }

        private void Stornieren_Click(object sender, RoutedEventArgs e)
        {
            //Ausgewähltes Produkt
            int index = lb_produkte.SelectedIndex;
            //wenn keines ausgewählt wurde -> Exception
            if (index < 0)
                throw new Exception("Kein Element zum Entfernen ausgewählt!!!");
            else
            {
                //Das ausgewählte Element wird vom Preis subtrahiert
                lb_preis.Content = String.Format("{0:0,0.00}", Convert.ToDouble(lb_preis.Content.ToString().Substring(0, lb_preis.Content.ToString().Length - 1)) - obs[index].Preis) + "€";
                //Löscht das Element
                obs.RemoveAt(index);
                MessageBox.Show("ERFOLGREICH ENTFERNT !!!");

            }
        }

        private void Beenden_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("AUF WIEDERSEHEN !!!");
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter writer = new StreamWriter("Produktdetails.csv", true);
            writer.WriteLine(tb1.Text);
            MessageBox.Show("ERFOLGREICH !!!");
            MessageBox.Show("WICHTIG!!!\nStarten Sie das Programm erneut, damit Sie Ihr Produkt scannen können!");

            writer.Close();
        }
        
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("WICHTIG!!!\nAchten Sie bitte auf die richtige Schreibweise !!!");
            MessageBox.Show("WICHTIG!!!\nSehen Sie sich bitte JETZT in 'Produktdetails.csv' an, was die LETZTE SERIENNUMMER ist !!!");

        }
    }
}
