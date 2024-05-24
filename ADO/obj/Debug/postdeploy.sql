INSERT INTO [dbo].[Type] VALUES
('Normal'),
('Feu'),
('Plante'),
('Eau'),
('Eletrik'),
('Poison'),
('Vol');

INSERT INTO [dbo].[Pokemon]([Name], [Height], [Weight], [Type1Id], [Type2Id])
VALUES 
('Bulbizarre', 70, 7, 3, 6),
('Nosferalto', 160, 55, 6, 7),
('Pikachu', 40, 6, 5, null);
GO
