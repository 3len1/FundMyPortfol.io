go 
set identity_insert Portofolio.dbo.CreatorDetails on
go
insert into Portofolio.dbo.CreatorDetails( Id, LastName, FirstName, Country, Town, Street, PostalCode, PhoneNumber) values
(1, 'Papadopoulou', 'Afroditi', 'Greece', 'Agia Varvara', 'Plati P 21', '12351', '6928356589'),
(2, 'Antoniou', 'Victoras', 'Greece', 'Agia Varvara', 'Parodos Virona 15', '12351', '6928235656'),
(3, 'Apostolopoulou', 'Ermioni', 'Greece', 'Peristeri', 'Palea Kabalas 21', '12141', '2105753088');
go
set identity_insert Portofolio.dbo.CreatorDetails off
set identity_insert Portofolio.dbo.ProjectCreator on
go
insert into Portofolio.dbo.ProjectCreator( Id, BrandName, Link, BirthDay, About, CreatorDetailsId) values
(1, 'On my Own', 'https://github.com', cast(N'1995-08-25' as Date), 'About info',1),
(2, 'LikeIt', 'https://github.com', cast(N'1988-06-15' as Date), 'About info',2),
(3, 'Madalomania', 'https://github.com', cast(N'1991-08-22' as Date), 'About info',3);
go
set identity_insert Portofolio.dbo.ProjectCreator off
set identity_insert Portofolio.dbo.BackerDetails on
go
insert into Portofolio.dbo.BackerDetails( Id, LastName, FirstName, Country, Town, Street, PostalCode, PhoneNumber) values
(1, 'Xristoforou', 'Dimitris', 'Greece', 'Aigaleo', 'Street', '12351', '2105753089'),
(2, 'Aggelousis', 'Emanouil', 'Greece', 'Agia Varvara', 'Street', '12351', '6928234656'),
(3, 'Malupa', 'Ivan', 'Greece', 'Koridalos', 'Street', '23451', '2105753188'),
(4, 'Papantoniou', 'Eleonora', 'Greece', 'Marousi', 'Street', '23451', '6928225656'),
(5, 'Sveltana', 'Tamila', 'Greece', 'Peireas', 'Street', '23451', '2106753088');
go
set identity_insert Portofolio.dbo.BackerDetails off
set identity_insert Portofolio.dbo.Backer on
go
insert into Portofolio.dbo.Backer( Id, BackerDetailsId) values
(1, 1),(2, 2),(3, 3),(4, 4),(5, 5);
go
set identity_insert Portofolio.dbo.Backer off
set identity_insert Portofolio.dbo.Project on
go
insert into Portofolio.dbo.Project(Id, Title, Link, PablishDate, [ExpireDate], MoneyGoal, [Description], CreatorId) values
(1,'On my Own', 'https://github.com', cast(N'2018-06-01' as Date), cast(N'2019-06-01' as Date), 2500.00, 'Description info', 1 ),
(2,'LikeIt', 'https://github.com', cast(N'2018-06-05' as Date), cast(N'2019-05-01' as Date), 5000.00, 'Description info', 2 ),
(3,'Madalomania','https://github.com', cast(N'2018-06-10' as Date), cast(N'2018-12-01' as Date), 1000.00, 'Description info', 3 );
go
set identity_insert Portofolio.dbo.Project off
set identity_insert Portofolio.dbo.Package on
go
insert into Portofolio.dbo.Package(Id, PackageName, MoneyCost, [Description], ProjectId) values
(1,'Starter', 5.00, 'Description info', 1 ), (2,'Demi', 10.00, 'Description info', 1 ),
(3,'Medi', 15.00, 'Description info', 2 ), (4,'Large', 30.00, 'Description info', 2 ),
(5,'Thx', 2.00, 'Description info', 3 ), (6,'Suport', 7.00, 'Description info', 3 );
go
set identity_insert Portofolio.dbo.Package off