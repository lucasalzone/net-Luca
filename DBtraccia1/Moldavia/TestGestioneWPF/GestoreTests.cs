using Microsoft.VisualStudio.TestTools.UnitTesting;
using GestionePratiche;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionePratiche.Tests{
    [TestClass()]
    public class GestoreTests{
       
        Gestore g = new Gestore("GestionePratiche");
        //private DataB cont = new DataB("TestPrat");
        [TestMethod()]
        public void ModificaAnagraficaTest(){
            AnagCliente primo = new AnagCliente("Drag", "Brinzila", new DateTime(1995,01,17), "Cacciocavallo");
            AnagCliente secondo = new AnagCliente("Dragos", "Brinzila", new DateTime(1995,01,17), "Mortadella");
            g.AddAnagrafica(primo);
            primo = g.SearchCliente(primo);
            g.ModificaAnagrafica(primo.CodCliente, secondo);
            Assert.IsTrue(g.SearchCliente(secondo)!=null);
        }
        [TestMethod()]
        public void ModificaPraticaTest(){
            Assert.ThrowsException<Exception>(delegate{g.CalcolaPreventivo(800,200,48);});
            int durata = 48;
            double rata = 160;
            double stipendio = 800;
            AnagCliente alessio = new AnagCliente("Alesssio", "TutteCose", new DateTime(1997, 11, 24), "PanePanelle");
            g.AddAnagrafica(alessio);
            //Assert.IsNotNull(alessio);    ook
            alessio = g.SearchCliente(alessio);
            //Assert.IsNotNull(alessio);    ook
            Pratica uno = new Pratica(alessio.CodCliente, rata, durata, g.CalcolaPreventivo(stipendio, rata, durata));
            Pratica due = new Pratica(alessio.CodCliente, rata, 60, g.CalcolaPreventivo(stipendio, rata, durata));
            g.AddPratica(uno);
            //Assert.IsNotNull(uno);    ook
            uno = g.SearchPratica(alessio.CodCliente);            
            Assert.IsNotNull(uno);
            g.ModificaPratica(uno.NPrat,due);
            alessio.Pratiche = g.SearchPratiche(alessio.CodCliente);
            Assert.IsTrue(g.SearchPratiche(alessio.CodCliente)[0].Durata == 60);
        }
        [TestMethod()]
        public void EliminaPraticaTest(){
            int durata = 48;
            double rata = 100;
            double stipendio = 800;
            AnagCliente edo = new AnagCliente("Edo", "Farina", new DateTime(1993,03,11), "HarryPOtter");
            g.AddAnagrafica(edo);
            edo = g.SearchCliente(edo);
            Assert.IsNotNull(edo);
            Pratica uno = new Pratica(edo.CodCliente, rata, durata, g.CalcolaPreventivo(stipendio, rata, durata));
            Pratica due = new Pratica(edo.CodCliente, rata, 60, g.CalcolaPreventivo(stipendio, rata, durata));
            g.AddPratica(uno);
            uno = g.SearchPratiche(edo.CodCliente)[0];
            g.AddPratica(due);
            due = g.SearchPratiche(edo.CodCliente)[1];
            g.EliminaPratica(uno);
            Assert.IsTrue(g.SearchPratiche(edo.CodCliente).Count>=1);
        }

            

        [TestMethod()]
        public void AddAnagraficaTest(){
                AnagCliente Pippo = new AnagCliente();
                Pippo.Nome = "Pippo";
                Pippo.Cognome = "Paperino";
                Pippo.DatNascita = DateTime.Today;
                Pippo.Indirizzo = "Via dei pioppi 31";
                g.AddAnagrafica(Pippo);
                Assert.IsTrue(g.SearchCliente(Pippo) != null);
        }

        [TestMethod()]
        public void AddPraticaTest()
        {
            int durata = 24;
            double rata = 100;
            double stipendio = 600;
            AnagCliente carmen = new AnagCliente("Carmen", "Capobianco", new DateTime(1983, 07, 19), "Poroproporporporporop");
            g.AddAnagrafica(carmen);
            carmen = g.SearchCliente(carmen);
            g.AddPratica(new Pratica(carmen.CodCliente, rata, durata, g.CalcolaPreventivo(stipendio, rata, durata)));
            bool esiste = false;
            if (g.SearchPratiche(carmen.CodCliente).Count>0) {
                esiste = true;
            }
            Assert.IsTrue(esiste);
        }
        [ClassInitialize]
        public static void InitClass(TestContext e)
        {
            DataB.InitTest();
        }
        [TestInitialize]
        public void InitMethod()
        {
            DataB.Drop();
        }
    }
}
