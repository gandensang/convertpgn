using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ConvertPNG
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

        private void prosesBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var ambilText = textAsli.Text;

                var ambilEvent = GetText(ambilText, @"Event \"".*?\""");
                ambilEvent = ambilEvent.Replace("Event \"", "").Replace("\"", "");
                var pemainPutih = GetText(ambilText, @"White \"".*?\""");
                pemainPutih = pemainPutih.Replace("White \"", "").Replace("\"", "");
                var pemainHitam = GetText(ambilText, @"Black \"".*?\""");
                pemainHitam = pemainHitam.Replace("Black \"", "").Replace("\"", "");
                var waktu = GetText(ambilText, @"Date \"".*?\""");
                waktu = waktu.Replace("Date \"", "").Replace("\"", "");

                var getNotasi = GetText(ambilText, @"1\..*?\-\d");

                var hasilNotasi = getNotasi + "\n" +
                                  $"{pemainPutih} - {pemainHitam} \n" +
                                  $"{ambilEvent} - {waktu}";
                hasil.Text = hasilNotasi;

                Clipboard.SetDataObject(hasil.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // buatkan fungsi regext untuk mengambil text yang diinginkan 
        string GetText(string isiText, string patern)
        {
            try
            {
                var rg = new Regex(patern);
                var getText = rg.Match(isiText);
                if (getText != null)
                {
                    return getText.Value;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return string.Empty;
        }
    }
}
