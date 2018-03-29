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
using GestionePratiche;

namespace GestionePratiche {
	public partial class UiAnagCliente: Window {
		Gestore gestore= new Gestore();
        List<AnagCliente> cli;
        int index = -1;
        public UiAnagCliente(){
            InitializeComponent();
            cli = new List<AnagCliente>();
            RiempiListaClienti();
        }

        private void RiempiListaClienti()
        {
            index = -1;
            cli = gestore.InitList();
            ImpostaCampi();
        }

        private void BtnAvanti_Click (object sender, RoutedEventArgs e) {
            this.index++;
            ImpostaCampi();
            AbilitaBottoni();
        }
        private void BtnIndietro_Click (object sender, RoutedEventArgs e) {
            this.index--;
            ImpostaCampi();
            AbilitaBottoni();
        }
        private void AbilitaBottoni()
        {
            if (index < cli.Count-1) { BtnAvanti.IsEnabled = true; } else {BtnAvanti.IsEnabled = false; }
            if(index>0) { BtnIndietro.IsEnabled = true; } else { BtnIndietro.IsEnabled = false;}
        }


        private void BtnAggiungi_Click (object sender, RoutedEventArgs e) {
			gestore.AddAnagrafica(new AnagCliente(TxtNome.Text, TxtCognome.Text, Convert.ToDateTime(TxtDataNascita.Text), TxtIndirizzo.Text));
			MessageBox.Show("Anagrafica inserita!");
		}
		private void BtnModifica_Click(object sender, RoutedEventArgs e) {
	    	AnagCliente AnagNew = new AnagCliente(int.Parse(TxtCodCliente.Text), TxtNome.Text, TxtCognome.Text, Convert.ToDateTime(TxtDataNascita.Text), TxtIndirizzo.Text);
			gestore.ModificaAnagrafica(int.Parse(TxtCodCliente.Text), AnagNew);
		}
        private void BtnCerca_Click (object sender, RoutedEventArgs e) {
            index = -1;
            try{
                if(TxtRicUniversale.Text.Length>0){ 
                    cli = gestore.SearchCliente(TxtRicUniversale.Text);
                    AbilitaBottoni();
                }
                else if(TxtCodCliente.Text.Length>0) { 
                    ImpostaCampi(gestore.SearchCliente(int.Parse(TxtCodCliente.Text)));
                }
                else if(TxtNome.Text!=null && TxtCognome.Text!=null && TxtIndirizzo.Text!=null){
                    ImpostaCampi(gestore.SearchCliente(new AnagCliente(TxtNome.Text,TxtCognome.Text,TxtIndirizzo.Text)));                    
                }
            }
            catch (Exception) { 
                MessageBox.Show("Contatto non trovato");    
            }
        }
        private void ImpostaCampi(AnagCliente cliente) { 
            TxtCodCliente.Text = cliente.CodCliente.ToString();
            TxtNome.Text = cliente.Nome;
            TxtCognome.Text = cliente.Cognome;
            TxtDataNascita.Text = cliente.DatNascita.ToString("yyyy/MM/dd");
            TxtIndirizzo.Text = cliente.Indirizzo; 
            
        }
        private void ImpostaCampi() {
            TxtCodCliente.Text = cli[index].CodCliente.ToString();
            TxtNome.Text = cli[index].Nome;
            TxtCognome.Text = cli[index].Cognome;
            TxtDataNascita.Text = cli[index].DatNascita.ToString("yyyy/MM/dd");
            TxtIndirizzo.Text = cli[index].Indirizzo;
        }

        private void puliziaCampi()
        {
            TxtCodCliente.Text ="";
            TxtNome.Text = "";
            TxtCognome.Text = "";
            TxtDataNascita.Text = "";
            TxtIndirizzo.Text = "";
        }

        private void BtnClearCampi_Click(object sender, RoutedEventArgs e)
        {
            puliziaCampi();
        }
       
    }
}
