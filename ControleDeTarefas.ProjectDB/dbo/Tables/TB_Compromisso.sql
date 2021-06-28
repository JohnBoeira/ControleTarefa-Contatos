CREATE TABLE [dbo].[TB_Compromisso] (
    [id]          INT          IDENTITY (1, 1) NOT NULL,
    [assunto]     VARCHAR (50) NOT NULL,
    [local]       VARCHAR (50) NULL,
    [dataInicio]  DATETIME     NOT NULL,
    [dataTermino] DATETIME     NOT NULL,
    [contato_id]  INT          NULL,
    [link]        VARCHAR (50) NULL,
    CONSTRAINT [PK_TB_Compromisso] PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([contato_id]) REFERENCES [dbo].[TB_Contatos] ([Id])
);

