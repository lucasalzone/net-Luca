using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace GestionePratiche
{
    public interface IGestore{
        void AddAnagrafica(AnagCliente ac);
        void ModificaAnagrafica(int a, AnagCliente b);
        void AddPratica(Pratica p);
        void ModificaPratica(int a, Pratica b);
        void EliminaPratica(Pratica p);
        double CalcolaPreventivo(double stipendio, double rata, int durata);
    }

    public class Gestore : IGestore
    {
        public string qwe;
        public DataB db;
        public Gestore(string mes = "GestionePratiche"){
            qwe = mes;
            db = new DataB(qwe);

        }
        public List<AnagCliente> SearchCliente(string parola){
            return db.CercaClienti(parola);
        }

        public AnagCliente SearchCliente(AnagCliente a){
           //string sql = $"Select * FROM AnagClienti where nome ='{a.Nome}' and cognome='{a.Cognome}' and Indirizzo='{a.Indirizzo}' ";
           //return db.Estrai<List<AnagCliente>>(db.TransformAC, sql)[0];
            //string sql = $"dbo.nomecognome ";
            return db.Estrai<List<AnagCliente>>(db.TransformAC, $"dbo.nomecognome '{a.Nome}', '{a.Cognome}', '{a.Indirizzo}'")[0];
        }
        public AnagCliente SearchCliente(int codCliente) {
            //string sql = $"Select * FROM AnagClienti where codCliente = {codCliente}; ";
            //return db.Estrai<List<AnagCliente>>(db.TransformAC, sql)[0];
            return db.Estrai<List<AnagCliente>>(db.TransformAC, $"dbo.codcliente '{codCliente}'")[0];
        }


        // search che mi restituisca un cliente con criterio di ricerca il codice di un cliente,
        public Pratica SearchPratica(int codPratica)
        {
            string sql = $"select * from Pratiche where NPrat={codPratica};";
            List<Pratica> listaPratiche = db.Estrai<List<Pratica>>(db.TransformPrat, sql);
            return listaPratiche.Count==0?null:listaPratiche[0];
        }
        public List<Pratica> SearchPratiche(int codCliente)
        {
            string sql = $"select * from Pratiche where CodCliente={codCliente};";
            return db.Estrai<List<Pratica>>(db.TransformPrat, sql);
        }

        public void AddAnagrafica(AnagCliente c)
        {
            db.Update($"Insert INTO AnagClienti (Nome,Cognome,DataNascita,Indirizzo) VALUES ('{c.Nome}','{c.Cognome}','{c.DatNascita.ToString("yyyy/MM/dd")}', '{c.Indirizzo}')");
        }
        public void ModificaAnagrafica(int a, AnagCliente b)
        {
            string sql = $"update AnagClienti set Nome='{b.Nome}', Cognome = '{b.Cognome}', dataNascita = '{b.DatNascita.ToString("yyyy/MM/dd")}', indirizzo = '{b.Indirizzo}' where CodCliente = {a}; ";
            db.Update(sql);
        }
        public void AddPratica(Pratica pratica)
        {
            db.Update($"insert into Pratiche( CodCliente, Rata, Durata,MLordo) values ({pratica.CodCliente}, {pratica.Rata}, {pratica.Durata}, {pratica.MLordo});");
        }
        public void ModificaPratica(int a, Pratica b)
        {
            string sql = $"update Pratiche set CodCliente = {b.CodCliente}, Rata = {b.Rata}, Durata = {b.Durata}, MLordo = {b.MLordo} where NPrat = {a}; ";
            db.Update(sql);
        }
        public void EliminaPratica(Pratica pratica)
        {
            string sql = $"delete Pratiche where NPrat = {pratica.NPrat};";
            db.Update(sql);
        }
        public List<AnagCliente> InitList() { 
            string sql = "select * from AnagClienti;";
            return db.Estrai<List<AnagCliente>>(db.TransformAC, sql);
        }

        public double CalcolaPreventivo(double stipendio, double rata, int durata)
        {
            if (ControllaDurata(durata))
            {
                if (ControllaRata(stipendio, rata))
                {
                    return durata * rata;
                }
                else
                {
                    throw new Exception("La rata non rientra nei valori consentiti.");
                }
            }
            else
            {
                throw new Exception("La durata non rientra nei valori consentiti.");
            }
        }
        private bool ControllaDurata(int durata)
        {
            int[] Range = { 24, 36, 48, 60, 72, 84, 96, 108, 120 };
            bool result = false;
            foreach (int value in Range)
            {
                if (durata == value)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        private bool ControllaRata(double stipendio, double rata)
        {
            double value = stipendio / 5;
            bool result = false;
            if (rata <= value)
            {
                result = true;
            }
            return result;
        }
        
    }
    public interface IDaoPratica{
        List<AnagCliente> CercaClienti(string parola);
    }


    public class DataB:IDaoPratica
    {
    //return db.CercaClienti<List<AnagCliente>>(db.TransformAC, sql);
        private string _baseD;
        public string BaseD { get { return _baseD; } set { _baseD = value; } }
        public DataB(string datab = "GestionePratiche")
        {
            this._baseD = datab;
        }
        private string GetConnection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"(localdb)\MSSQLLocalDB";
            builder.InitialCatalog = BaseD;
            return builder.ConnectionString;
        }

        public static void InitTest()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"(localdb)\MSSQLLocalDB";
            SqlConnection con = new SqlConnection(builder.ToString());
            try
            {
                con.Open();
                string sql = "drop database if exists TestPrat;";
                SqlCommand sqlCommand = new SqlCommand(sql, con);
                sqlCommand.ExecuteNonQuery();
                sql = "create database TestPrat;";
                sqlCommand = new SqlCommand(sql, con);
                sqlCommand.ExecuteNonQuery();
                con.Dispose();
                builder.InitialCatalog = "TestPrat";
                con = new SqlConnection(builder.ToString());
                con.Open();
                sql = "CREATE TABLE AnagClienti(CodCliente int IDENTITY(1,1) NOT NULL PRIMARY KEY,Nome varchar(50),Cognome varchar(50),DataNascita Date, Indirizzo nvarchar(200)) CREATE TABLE Pratiche(NPrat int IDENTITY(1, 1) NOT NULL PRIMARY KEY,CodCliente int FOREIGN KEY REFERENCES AnagClienti,Rata decimal,Durata int,MLordo decimal);";
                sqlCommand = new SqlCommand(sql, con);
                sqlCommand.ExecuteNonQuery();
                sqlCommand.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Dispose();
            }
        }
        public static void Drop()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"(localdb)\MSSQLLocalDB";
            builder.InitialCatalog = "TestPrat";
            SqlConnection con = new SqlConnection(builder.ToString());
            try
            {
                con.Open();
                string sql = "delete Pratiche where 0=0;";
                SqlCommand command = new SqlCommand(sql, con);
                command.ExecuteNonQuery();
                command.Dispose();
                string sql1 = "delete AnagClienti where 0=0;";
                SqlCommand command1 = new SqlCommand(sql1, con);
                command1.ExecuteNonQuery();
                command1.Dispose();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Dispose();
            }
        }

        public delegate T Transform<T>(SqlDataReader reader);
        public List<AnagCliente> TransformAC(SqlDataReader reader)
        {
            List<AnagCliente> clienti = new List<AnagCliente>();
            while (reader.Read())
            {
                int codCliente = reader.GetInt32(0);
                string nome = reader.GetString(1);
                string cognome = reader.GetString(2);
                DateTime datNascita = reader.GetDateTime(3);
                string indirizzo = reader.GetString(4);
                //List < Pratica > pratiche= Estrai(TransformPrat,$"select * from Pratiche where CodCliente={codCliente};");
                clienti.Add(new AnagCliente(codCliente, nome, cognome, datNascita, indirizzo));
            }
            reader.Close();
            return clienti;
        }

        public List<Pratica> TransformPrat(SqlDataReader reader)
        {
            List<Pratica> pratiche = new List<Pratica>();
            while (reader.Read())
            {
                int nPrat = reader.GetInt32(0);
                int Codcliente = reader.GetInt32(1);
                double _Rata = (double)reader.GetDecimal(2);
                int _Durata = reader.GetInt32(3);
                double _MLordo = (double)reader.GetDecimal(4);
                pratiche.Add(new Pratica(nPrat, Codcliente, _Rata, _Durata, _MLordo));
            }
            reader.Close();
            return pratiche;
        }
        public T Estrai<T>(Transform<T> metodo, string sql)
        {
            SqlConnection con = new SqlConnection(GetConnection());
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                T ris = metodo(reader);
                reader.Dispose();
                cmd.Dispose();
                return ris;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Dispose();
            }
        }
        public void Update(string sql)
        {
            SqlConnection con = new SqlConnection(GetConnection());
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Dispose();
            }
        }

        public List<AnagCliente> CercaClienti(string parola){
            string sql = $"SELECT * FROM AnagClienti WHERE Nome LIKE '%{parola}%' OR Cognome like '%{parola}%' or Indirizzo like '%{parola}%';";
            return this.Estrai<List<AnagCliente>>(TransformAC,sql); 
        }
    }

    public partial class AnagCliente
    {
        private int _codCliente; public int CodCliente { get { return _codCliente; } }
        private string _nome; public string Nome { get { return _nome; } set { _nome = value; } }
        private string _cognome; public string Cognome { get { return _cognome; } set { _cognome = value; } }
        private DateTime _datNascita; public DateTime DatNascita { get { return _datNascita; } set { _datNascita = value; } }
        private string _indirizzo; public string Indirizzo { get { return _indirizzo; } set { _indirizzo = value; } }
        private List<Pratica> _pratiche; public List<Pratica> Pratiche { get { return _pratiche; } set { _pratiche = value; } }

        public AnagCliente(int codCliente, string nome, string cognome, DateTime dat, string indirizzo)
        {
            this._codCliente = codCliente;
            this._nome = nome;
            this._cognome = cognome;
            this._datNascita = dat;
            this._indirizzo = indirizzo;
            this._pratiche = new List<Pratica>();
        }
        public AnagCliente(string nome, string cognome,string indirizzo)
        {
            this._nome = nome;
            this._cognome = cognome;
            this._indirizzo = indirizzo;
        }
        public AnagCliente(string nome, string cognome, DateTime dat, string indirizzo)
        {
            this._nome = nome;
            this._cognome = cognome;
            this._datNascita = dat;
            this._indirizzo = indirizzo;
            this._pratiche = new List<Pratica>();
        }
        public AnagCliente() { }
        public void AggPratica(Pratica p)
        {
            this._pratiche.Add(p);
        }

    }
    public partial class Pratica
    {
        private int _nPrat; public int NPrat { get { return _nPrat; } set { this._nPrat = value; } }
        private int _codcliente; public int CodCliente { get { return _codcliente; } set { this._codcliente = value; } }
        private double _rata; public double Rata { get { return _rata; } set { this._rata = value; } }
        private int _durata; public int Durata { get { return _durata; } set { this._durata = value; } }
        private double _mLordo; public double MLordo { get { return _mLordo; } set { this._mLordo = value; } }

        public Pratica(int nPrat, int codCliente, double rata, int durata, double mLordo)
        {
            this._nPrat = nPrat;
            this._codcliente = codCliente;
            this._rata = rata;
            this._durata = durata;
            this._mLordo = mLordo;
        }
        public Pratica(int codCliente, double rata, int durata, double mLordo)
        {
            this._codcliente = codCliente;
            this._rata = rata;
            this._durata = durata;
            this._mLordo = mLordo;
        }
    }

}
