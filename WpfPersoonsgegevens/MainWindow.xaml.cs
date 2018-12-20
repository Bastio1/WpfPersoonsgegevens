using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Microsoft.Win32;

namespace WpfPersoonsgegevens
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        ObservableCollection<Persoon> AllePersonen = new ObservableCollection<Persoon>();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var p = new Persoon();
            p.Voornaam = txtVoornaam.Text;
            p.Achternaam = txtAchternaam.Text;
            p.Geslacht = (GeslachtEnum)Enum.Parse(typeof(GeslachtEnum), lbxGeslacht.SelectedItem.ToString());
            p.Land = cmbLand.Text;
            p.Geboortedatum = dtpGeboortedatum.SelectedDate.Value;
            // if (lbxGeslacht.SelectedItem.ToString() == "Man")
            // if (lbxGeslacht.SelectedItem.ToString() == "Vrouw")
            // if (lbxGeslacht.SelectedItem.ToString() == "Onbekend")

            lbl.Content = $"Hallo {p.Voornaam} {p.Achternaam}." + p.Geboortedatum.ToString("dd-MM-yyyy");

            AllePersonen.Add(p);
            dg.ItemsSource = null;
            dg.ItemsSource = AllePersonen;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // var geslachten = new[] { "Man", "Vrouw", "Onbekend" };
            lbxGeslacht.ItemsSource = Enum.GetValues(typeof(GeslachtEnum));           
            cmbLand.ItemsSource = Enum.GetValues(typeof(Landenum));

            dtpGeboortedatum.SelectedDate = new DateTime(1980, 1, 1);
        }
                
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialoog = new SaveFileDialog();

            dialoog.ShowDialog();

            string csvData = "";

            foreach (Persoon p in AllePersonen)
            {
                //csdata += p.Voornaam + "," + p.Achternaam + Environment.NewLine;
                csvData += string.Join(",", p.Voornaam, p.Achternaam, p.Land, p.Geslacht, p.Geboortedatum, p.Leeftijd) + Environment.NewLine;
            }

            //File.WriteAllText("c:\\temp\\personen.csv", csvData);
            File.WriteAllText(dialoog.FileName, csvData);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialoog = new OpenFileDialog();
            dialoog.ShowDialog();

            string[] data = File.ReadAllLines(dialoog.FileName);
            
            foreach(string regel in data)
            {
                string[] velden = regel.Split(',');
                Persoon p = new Persoon();
                p.Voornaam = velden[0];
                p.Achternaam = velden[1];
                p.Land = velden[2];
                p.Geslacht = (GeslachtEnum)Enum.Parse(typeof(GeslachtEnum), velden[3]);
                p.Geboortedatum = DateTime.Parse(velden[4]);                

                AllePersonen.Add(p);
            }
        }
    }
}
