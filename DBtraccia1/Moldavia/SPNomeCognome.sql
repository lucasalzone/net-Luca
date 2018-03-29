create procedure dbo.nomecognome
	@nome varchar(50),
	@cognome varchar (50),
	@telefono varchar (50)
as 
	Select * FROM AnagClienti
	where nome = @nome and
		cognome = @cognome and
		Indirizzo = @telefono
go
