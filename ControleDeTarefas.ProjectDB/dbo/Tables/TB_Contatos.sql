CREATE TABLE [dbo].[TB_Contatos]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [nome] VARCHAR(50) NOT NULL, 
    [telefone] VARCHAR(50) NOT NULL, 
    [email] VARCHAR(50) NOT NULL,
    [empresa] VARCHAR(50) NULL,
    [cargo] VARCHAR(50) NULL 


)
