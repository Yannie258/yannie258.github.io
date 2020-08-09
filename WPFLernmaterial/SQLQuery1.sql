alter table TBLData add Datum date;
alter table dbo.TBLData drop Datum; 
drop table TBLData;
CREATE TABLE [dbo].[TBLData]
(
	[Id] INT NOT NULL PRIMARY KEY,
	Fachname varchar(100),
	Dozentvorname varchar(50),
	Dozentnachname varchar(50),
	Seminar varchar (20),
	Datum varchar(100),
	Lernmaterial varchar(max)
)
