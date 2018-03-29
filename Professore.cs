namespace GeCo{
	public class Professore: Utente{
		private Corso materia;
		public _materia{
			get{return materia;}
		}
		public Professore(string _nome, string _cognome, int id, string materia):base(_nome, _cognome, id){
			this._materia = materia;
		}
		public Aggiungi(Lezione lez){
			lez = new Lezione();
			materia.add(lez);
		}
		public override ToSting(){
			return $"Professore:  Nome: {this.Nome}  Cognome: {this.Cognome}  Id: {this.Id}";
		}
	}
}