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
using System.ComponentModel;

namespace XAMLValidace
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Zamestnanec z;
        public MainWindow()
        {
            InitializeComponent();
            z = new Zamestnanec ("Jméno", "Příjmení", "Rok", "Vzdělání", "Pozice", "Plat" );
            this.DataContext = z;
            ErrorLabelJmeno.DataContext = this;
            ErrorLabelPrijmeni.DataContext = this;
            ErrorLabelRok.DataContext = this;
            ErrorLabelPozice.DataContext = this;
            ErrorLabelPlat.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Zamestnanec zam = new Zamestnanec(Name.Text, Surname.Text, Age.Text, Education.Text, Position.Text, Pay.Text);

            Info.Text = $@"Jméno: {zam.Jmeno}
Příjmení: {zam.Prijmeni}
Rok narození: {zam.Rok}
Vzdělání: {zam.Vzdelani}
Pozice: {zam.Pozice}
Plat: {zam.Plat}";
        }

        private string _JmenoError = "Jméno je povinná položka.";
        public string JmenoError { get { return _JmenoError; } }

        private bool CheckNameOK()
        {
            if (z.Jmeno.Length > 0)
            {
                JmenoErrorVisible = Visibility.Hidden;
                return true;
            }

            else
            {
                JmenoErrorVisible = Visibility.Visible;
                return false;
            }
        }

        private bool CheckSurnameOK()
        {
            if (z.Prijmeni.Length > 1 && z.Prijmeni.Length < 20)
            {
                PrijmeniErrorVisible = Visibility.Hidden;
                return true;
            }

            else
            {
                PrijmeniErrorVisible = Visibility.Visible;
                PrijmeniError = "Cokoliv";
                return false;
            }
        }

        private Visibility _JmenoErrorVisible;
        public Visibility JmenoErrorVisible
        {
            get { return _JmenoErrorVisible; }
            set
            {
                _JmenoErrorVisible = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("JmenoErrorVisible"));
            }
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckNameOK();
        }

        private Visibility _PrijmeniErrorVisible;
        public Visibility PrijmeniErrorVisible
        {
            get { return _PrijmeniErrorVisible; }
            set
            {
                _PrijmeniErrorVisible = value;
                PropertyChanged(this, new PropertyChangedEventArgs("PrijmeniErrorVisible"));
            }
        }

        private string _PrijmeniError;
        public string PrijmeniError
        {
            get { return _PrijmeniError; }
            set
            {
                if (z.Prijmeni.Length < 2)
                    _PrijmeniError = "Jméno nemůže být kratší než 2 znaky.";
                else if (z.Prijmeni.Length > 20)
                    _PrijmeniError = "Jméno nemůže být delší než 20 znaků.";
                else
                    _PrijmeniError = "";
                PropertyChanged(this, new PropertyChangedEventArgs("PrijmeniError"));
            }
        }

        private void Surname_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckSurnameOK();
        }

        private Visibility _VekErrorVisible;
        public Visibility VekErrorVisible
        {
            get { return _VekErrorVisible; }
            set
            {
                _VekErrorVisible = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("VekErrorVisible"));
            }
        }

        private void Age_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Convert.ToInt32(z.Rok);
                VekErrorVisible = Visibility.Hidden;
            }
            catch
            {
                VekErrorVisible = Visibility.Visible;
            }
        }

        private Visibility _PoziceErrorVisible;
        public Visibility PoziceErrorVisible
        {
            get { return _PoziceErrorVisible; }
            set
            {
                _PoziceErrorVisible = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("PoziceErrorVisible"));
            }
        }

        private void Position_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (z.Pozice.Length > 0)
            {
                PoziceErrorVisible = Visibility.Hidden;
            }

            else
            {
                PoziceErrorVisible = Visibility.Visible;
            }
        }

        private Visibility _PlatErrorVisible;
        public Visibility PlatErrorVisible
        {
            get { return _PlatErrorVisible; }
            set
            {
                _PlatErrorVisible = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("PlatErrorVisible"));
            }
        }

        private void Pay_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Convert.ToInt32(z.Plat);
                PlatErrorVisible = Visibility.Hidden;
            }
            catch
            {
                PlatErrorVisible = Visibility.Visible;
            }
        }
    }

    public class Osoba
    {
        public Osoba(string jmeno, string prijmeni, string rok)
        {
            Jmeno = jmeno;
            Prijmeni = prijmeni;
            Rok = rok;
        }

        public string Jmeno
        {
            get;
            set;
        }

        public string Prijmeni
        {
            get;
            set;
        }

        public string Rok
        {
            get;
            set;
        }
    }

    public class Zamestnanec : Osoba
    {
        public Zamestnanec(string jmeno, string prijmeni, string rok, string vzdelani, string pozice, string plat) : base(jmeno, prijmeni, rok)
        {
            Vzdelani = vzdelani;
            Pozice = pozice;
            Plat = plat;
        }

        public string Vzdelani
        {
            get;
            set;
        }

        public string Pozice
        {
            get;
            set;
        }

        public string Plat
        {
            get;
            set;
        }
    }
}
