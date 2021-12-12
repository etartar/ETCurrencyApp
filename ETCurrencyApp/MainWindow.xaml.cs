using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Xml;

namespace ETCurrencyApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string _tcmbTodayPath = "https://www.tcmb.gov.tr/kurlar/today.xml";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument document = new XmlDocument();
            document.Load(_tcmbTodayPath);

            DateTime dt = Convert.ToDateTime(document.SelectSingleNode("//Tarih_Date").Attributes["Tarih"].Value);
            lblDate.Content = dt.ToString();

            var usdSales = document.SelectSingleNode("Tarih_Date/Currency [@Kod = 'USD']/BanknoteSelling").InnerXml;
            var usdBuying = document.SelectSingleNode("Tarih_Date/Currency [@Kod = 'USD']/BanknoteBuying").InnerXml;

            lblUsdSales.Content = $"{Math.Round(decimal.Parse(usdSales, new CultureInfo("en-US")), 2)}";
            lblUsdBuying.Content = $"{Math.Round(decimal.Parse(usdBuying, new CultureInfo("en-US")), 2)}";

            string eurSales = document.SelectSingleNode("Tarih_Date/Currency [@Kod = 'EUR']/BanknoteSelling").InnerXml;
            string eurBuying = document.SelectSingleNode("Tarih_Date/Currency [@Kod = 'EUR']/BanknoteBuying").InnerXml;

            lblEurSales.Content = $"{Math.Round(decimal.Parse(eurSales, new CultureInfo("en-US")), 2)}";
            lblEurBuying.Content = $"{Math.Round(decimal.Parse(eurBuying, new CultureInfo("en-US")), 2)}";
        }
    }
}
