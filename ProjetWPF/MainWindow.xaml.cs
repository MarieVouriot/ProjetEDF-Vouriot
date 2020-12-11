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

namespace ProjetWPF
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        edfEntities gst;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gst = new edfEntities();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(txtLogin.Text == "")
            {
                txtMessageErreur.Text = "Veuillez saisir un login";
            }
            else
            {
                if(txtMdp.Text == "")
                {
                    txtMessageErreur.Text = "Veuillez saisir un mot de passe";
                }
                else
                {
                    controleur unCtrl = gst.controleur.ToList().Find(contr => contr.login == txtLogin.Text && contr.mdp == txtMdp.Text);
                    if(unCtrl == null)
                    {
                        txtMessageErreur.Text = "Vos identifiants sont incorrects";
                    }
                    else
                    {

                        if(unCtrl.statut == "ctrl") 
                        {
                            frmController frm = new frmController(unCtrl);
                            frm.Show();
                        }
                        else
                        {
                            frmAdmin frm = new frmAdmin();
                            frm.Show();
                            
                        }
                    }


                }
            }

        }

        
    }
}
