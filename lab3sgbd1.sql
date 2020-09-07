use Steam
go
create function validare_joc (@numej varchar(50), @descriere varchar(5000),@pret float) 
RETURNS INT AS
	BEGIN
		DECLARE @return INT
		SET @return = 0
		IF(@numej <>'' and @descriere <>'' and @pret>0)
			SET @return = 1
		RETURN @return
END
go

CREATE FUNCTION validare_comenzi (@suma int) 
RETURNS INT AS
	BEGIN
		DECLARE @return1 INT
		SET @return1 = 0
		IF(@suma>0)
			SET @return1 = 1
		RETURN @return1
END

go

use Steam
go
create procedure Adaugare11 @numej varchar(50), @descriere varchar(5000),@pret float,@datac date,@suma int AS
	begin
		begin tran
			begin try
				if(dbo.validare_joc(@numej,@descriere,@pret) <>1) or(dbo.validare_comenzi(@suma)<>1)
					begin
						raiserror('Incearca din nou! :(',14,1)
						end
						declare @jid int
						set @jid=(SELECT MAX(Jid) FROM Jocuri) + 1
						declare @cid int
						set @cid=(SELECT MAX(Cid) FROM ComenziPtRollback1 ) + 1
						insert into Jocuri values (@jid,@numej,@descriere,@pret)
						insert into ComenziPtRollback1 values(@cid,@datac,@suma)
						insert into Subcomenzi11 values(@jid,@cid)
						commit tran
						select 'Succes! :D'
					end try
					begin catch
						rollback tran
						select 'S-a facut roll-back! '
					end catch
	end
go

--functioneaza pt adaugarea fara erori insa daca o data de intrare nu este corecta se face rollback la tot
create procedure AdaugareSuperioara11 @numej varchar(50), @descriere varchar(5000),@pret float,@datac date,@suma int AS
	begin
		begin tran
			begin try
				if(dbo.validare_joc(@numej,@descriere,@pret) <>1) or(dbo.validare_comenzi(@suma)<>1)
					begin
						raiserror('Incearca din nou! :(',14,1)
						end
						declare @jid int
						set @jid=(SELECT MAX(Jid) FROM Jocuri) + 1
						declare @cid int
						set @cid=(SELECT MAX(Cid) FROM ComenziPtRollback1 ) + 1
						begin tran
						insert into Jocuri values (@jid,@numej,@descriere,@pret)
						save tran savepoint
						insert into ComenziPtRollback1 values(@cid,@datac,@suma)
						save tran savepoint
						insert into Subcomenzi11 values(@jid,@cid)						
						rollback tran savepoint
						commit tran
						select 'Succes! :D'
					end try
					begin catch
						select 'S-a facut roll-back! '
					end catch
	end
go


--am incercat in mai multe moduri pt varianta cu salvare intermediara dar nu am prea inteles cum se face
go

create procedure AdaugareSuperioara2 @numej varchar(50), @descriere varchar(5000),@pret float,@datac date,@suma int AS
	begin
		begin tran
			begin try
				if(dbo.validare_joc(@numej,@descriere,@pret) <>1) 
					begin
						raiserror('Incearca din nou! :(',14,1)
						end
						declare @jid int
						set @jid=(SELECT MAX(Jid) FROM Jocuri) + 1
						declare @cid int
						set @cid=(SELECT MAX(Cid) FROM ComenziPtRollback1 ) + 1
						begin tran
						insert into Jocuri values (@jid,@numej,@descriere,@pret)
						save tran savepoint
						commit tran
						select 'Succes! :D'
					if(dbo.validare_comenzi(@suma)<>1)
					begin
						raiserror('Incearca din nou! :(',14,1)
						end
						begin tran
						insert into ComenziPtRollback1 values(@cid,@datac,@suma)
						save tran savepoint
						insert into Subcomenzi11 values(@jid,@cid)					
						commit tran
						select 'Succes! :D'
	
					end try
					begin catch
						rollback tran savepoint
						select 'S-a facut roll-back! '
					end catch
	end
go

Select * from Jocuri
Select * from ComenziPtRollback1
Select * from Subcomenzi11
EXEC Adaugare1 'joc1111','desc1111',56,'2020-05-07',458
EXEC Adaugare1 'joc111221','',56,'2020-05-07',-458
Select * from Jocuri
Select * from ComenziPtRollback1
Select * from Subcomenzi11

Select * from Jocuri
Select * from ComenziPtRollback1
Select * from Subcomenzi11
EXEC Adaugaresuperioara1 'joc1','desc1',56,'2020-05-07',458
EXEC Adaugaresuperioara1 'j1111111','desc1',56,'2020-05-07',-4444
Select * from Jocuri
Select * from ComenziPtRollback1
Select * from Subcomenzi11

Select * from Jocuri
Select * from ComenziPtRollback1
Select * from Subcomenzi11
EXEC Adaugaresuperioara2 'j1111111','desc1',-56,'2020-05-07',100
EXEC Adaugaresuperioara2 'j1111111','desc1',56,'2020-05-07',-4444
Select * from Jocuri
Select * from ComenziPtRollback1
Select * from Subcomenzi11