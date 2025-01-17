CREATE TABLE [dbo].[Pokemon]
(
  [Id] INT PRIMARY KEY IDENTITY,
  [Name] VARCHAR(50) NOT NULL UNIQUE,
  [Height] INT NOT NULL,
  [Weight] INT NOT NULL,
  [Description] VARCHAR(MAX) NULL,
  [Type1Id] INT NOT NULL,
  [Type2Id] INT NULL,
  [Image] VARCHAR(250) NULL,
  CONSTRAINT [FK_Pokemon_Type1] FOREIGN KEY ([Type1Id]) REFERENCES [Type]([Id]),
  CONSTRAINT [FK_Pokemon_Type2] FOREIGN KEY ([Type2Id]) REFERENCES [Type]([Id])
)
