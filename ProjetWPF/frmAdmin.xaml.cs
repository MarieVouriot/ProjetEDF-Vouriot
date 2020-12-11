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
    /// Logique d'interaction pour frmAdmin.xaml
    /// </summary>
    public partial class frmAdmin : Window
    {
        edfEntities gst;
        public frmAdmin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gst = new edfEntities();
            lstControleurs.ItemsSource = gst.controleur.ToList();
        }

        private void lstControleurs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstControleurs.SelectedItem != null)
            {
                int numControleur = (lstControleurs.SelectedItem as controleur).id;
                var lesClients = from cli in gst.client
                                 where cli.idcontroleur == numControleur 
                                 select new ClientPerso()
                                 {
                                     identifiant = cli.identifiant,
                                     nom = cli.nom,
                                     prenom = cli.prenom,
                                     ancienReleve = cli.ancienReleve,
                                     dernierReleve = cli.dernierReleve,
                                     total = cli.dernierReleve - cli.ancienReleve
                                 };
                lstClients.ItemsSource = lesClients.ToList();

            }
        }

        private void btnInsererControleur_Click(object sender, RoutedEventArgs e)
        {
            if(txtNomControleur.Text == "")
            {
                MessageBox.Show("Saisir le nom du controleur", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if(txtPrenomControleur.Text == "")
                {
                    MessageBox.Show("Saisir le prenom du controleur", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {                    

                    controleur newControleur = new controleur()
                    {
                        id = gst.controleur.ToList().Max(ctrl => ctrl.id),
                        nom = txtNomControleur.Text,
                        prenom = txtPrenomControleur.Text,
                        login = txtNomControleur.Text.Substring(0,1).ToLower() + txtPrenomControleur.Text.Substring(0,1).ToLower(),
                        mdp = txtNomControleur.Text.Substring(0, 1).ToLower() + txtPrenomControleur.Text.Substring(0, 1).ToLower() + "123",
                        statut = "ctrl"
                    };

                    gst.controleur.Add(newControleur);
                    gst.SaveChanges();
                    lstControleurs.ItemsSource = null;
                    lstControleurs.ItemsSource = gst.controleur.ToList();

                }
            }
        }

        private void btnInsererClient_Click(object sender, RoutedEventArgs e)
        {
            if (txtNomClient.Text == "")
            {
                MessageBox.Show("Saisir le nom du client", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (txtPrenomClient.Text == "")
                {
                    MessageBox.Show("Saisir le prenom du client", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if(lstControleurs.SelectedItem == null)
                    {
                        MessageBox.Show("Saisir un controleur", "Erreur de saisie", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {

                        client newClient = new client()
                        {
                            identifiant = gst.client.ToList().Max(cli => cli.identifiant),
                            nom = txtNomClient.Text,
                            prenom = txtPrenomClient.Text,
                            ancienReleve = 0,
                            dernierReleve = 0, 
                            idcontroleur = (lstControleurs.SelectedItem as controleur).id
                        };
                        gst.client.Add(newClient);
                        gst.SaveChanges();

                        lstClients.ItemsSource = null;

                        int numControleur = (lstControleurs.SelectedItem as controleur).id;
                        var lesClients = from cli in gst.client
                                         where cli.idcontroleur == numControleur
                                         select new ClientPerso()
                                         {
                                             identifiant = cli.identifiant,
                                             nom = cli.nom,
                                             prenom = cli.prenom,
                                             ancienReleve = cli.ancienReleve,
                                             dernierReleve = cli.dernierReleve,
                                             total = cli.dernierReleve - cli.ancienReleve
                                         };
                        lstClients.ItemsSource = lesClients.ToList();

                    }
                }
            }
        }
    }
}
