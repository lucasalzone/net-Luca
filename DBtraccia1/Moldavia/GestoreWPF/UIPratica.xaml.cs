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
	/// <summary>
	/// Logica di interazione per Pratica.xaml
	/// </summary>
	public partial class UIPratica : Window {
		Gestore gestore = new Gestore("GestionePraticheDB");
        private List<Pratica> pratiche = new List<Pratica>();
        int indice = -1;
		public UIPratica() {
			InitializeComponent();
            AbilitaBottoni();
			
		}

		private void BtnAdd_Click(object sender, RoutedEventArgs e) {
			int codcliente= int.Parse(txtCodcliente.Text);
			int nPrat = int.Parse(txtnPrat.Text);
			int durata = int.Parse(txt_Durata.Text);
			double rata = double.Parse(txt_Rata.Text);
			double stipendio = double.Parse(txtStipendio.Text);
			double preventivo = gestore.CalcolaPreventivo(stipendio,rata,durata);
			gestore.AddPratica(new Pratica(codcliente,rata,durata,preventivo));
			Clear();
			MessageBox.Show("Pratica Inserita!");
		}

		private void BtnMod_Click(object sender, RoutedEventArgs e) {
			gestore.ModificaPratica(int.Parse(txtnPrat.Text),new Pratica(int.Parse(txtCodcliente.Text),double.Parse(txt_Rata.Text),int.Parse(txt_Durata.Text),double.Parse(txt_MLordo.Text)));
		}

		private void BtnDel_Click(object sender, RoutedEventArgs e) {
			Pratica trovate = gestore.SearchPratica(int.Parse(txtCodcliente.Text));
			gestore.EliminaPratica(trovate);
			Clear();
		}
		private void BtnCerca_Click(object sender, RoutedEventArgs e) {
			List<Pratica> trovate = gestore.SearchPratiche(int.Parse(txtCodcliente.Text));
			if (trovate.Count!=0){ 
			txtCodcliente.Text =$"{trovate[0].CodCliente}";
			txtnPrat.Text = $"{trovate[0].NPrat}";
			txt_Rata.Text = $"{trovate[0].Rata}";
			txtStipendio.Text = "";
			txt_Durata.Text=$"{trovate[0].Durata}";
			txt_MLordo.Text=$"{trovate[0].MLordo}";
            pratiche = trovate;
            this.indice=0;
			}else{
			MessageBox.Show("Non è stato trovato nessun elemento");
			}
		}
		public  void Clear(){
			txtCodcliente.Text = "";
			txtnPrat.Text="";
			txt_Durata.Text="";
			txt_MLordo.Text="";
			txt_Rata.Text="";


		}

		private void ButtonAvanti_Click(object sender, RoutedEventArgs e) {
            this.indice ++;
            ImpostaCampi();
            AbilitaBottoni();

		}
        private void AbilitaBottoni(){
            if (this.indice < pratiche.Count - 1) {
                BtnAvanti.IsEnabled = true;

            } else {
                BtnAvanti.IsEnabled = false;
            }
            if (this.indice > 0) {
                BtnIndietro.IsEnabled = true;
            } else {
                BtnIndietro.IsEnabled = false;
            }
        }
        private void ImpostaCampi() {
            txtnPrat.Text = $"{pratiche[indice].NPrat}";
            txt_Rata.Text = $"{pratiche[indice].Rata}";
            txt_Durata.Text = $"{pratiche[indice].Durata}";
            txt_MLordo.Text = $"{pratiche[indice].MLordo}";
        }

        private void BtnIndietro_Click(object sender, RoutedEventArgs e) {
            this.indice -- ;
            ImpostaCampi();
            AbilitaBottoni();
		}
	}
}
