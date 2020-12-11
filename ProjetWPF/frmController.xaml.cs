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

namespace ProjetWPF
{
    /// <summary>
    /// Logique d'interaction pour frmController.xaml
    /// </summary>
    public partial class frmController : Window
    {
        edfEntities gst;
        controleur leCtrl;
        public frmController(controleur unCtrl)
        {
            InitializeComponent();
            leCtrl = unCtrl;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gst = new edfEntities();

            var lesClient = from cli in gst.client
                            where cli.idcontroleur == leCtrl.id
                            select new ClientPerso()
                            {
                                identifiant = cli.identifiant,
                                nom = cli.nom,
                                prenom = cli.prenom,
                                ancienReleve = cli.ancienReleve,
                                dernierReleve = cli.dernierReleve,
                                total = cli.dernierReleve - cli.ancienReleve
                            };
            lstClients.ItemsSource = lesClient.ToList();
        }
        private void btnInsererReleve_Click(object sender, RoutedEventArgs e)
        {
            if(lstClients.SelectedItem == null)
            {
                MessageBox.Show("Saisir un client", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if(txtNbReleve.Text == "")
                {
                    MessageBox.Show("Saisir un un relevé", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var leClient = gst.client.ToList().Find(cli => cli.dernierReleve > Convert.ToInt32(txtNbReleve.Text));
                    if (leClient == null)
                    {
                        MessageBox.Show("Impossible de consommer moins que le dernier relevé", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {

                        var lesClient = from cli in gst.client
                                        where cli.idcontroleur == leCtrl.id
                                        select new ClientPerso()
                                        {
                                            identifiant = cli.identifiant,
                                            nom = (lstClients.SelectedItem as client).nom,
                                            prenom = (lstClients.SelectedItem as client).prenom,
                                            ancienReleve = (lstClients.SelectedItem as client).dernierReleve,
                                            dernierReleve = leClient.dernierReleve,
                                            total = leClient.dernierReleve - (lstClients.SelectedItem as client).dernierReleve
                                        };
                        lstClients.ItemsSource = lesClient.ToList();
                    }
                }
            }
        }

    }
}
