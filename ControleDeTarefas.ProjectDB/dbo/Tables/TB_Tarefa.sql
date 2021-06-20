CREATE TABLE [dbo].[TB_Tarefa] (
    [Id]                    INT           IDENTITY (1, 1) NOT NULL,
    [titulo]                VARCHAR (200) NOT NULL,
    [prioridade]            INT           NOT NULL,
    [dataDeCriacao]         DATE          NOT NULL,
    [dataConclusao]         DATE          NULL,
    [percentualDeConclusao] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

