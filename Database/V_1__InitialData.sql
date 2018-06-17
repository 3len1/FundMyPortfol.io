go 
set identity_insert Portofolio.dbo.UserDetails on
go
insert into Portofolio.dbo.UserDetails( Id, LastName, FirstName, Country, Town, Street, PostalCode, PhoneNumber) values
(1, 'Papadopoulou', 'Afroditi', 'Greece', 'Agia Varvara', 'Plati P 21', '12351', '6928356589'),
(2, 'Antoniou', 'Victoras', 'Greece', 'Agia Varvara', 'Parodos Virona 15', '12351', '6928235656'),
(3, 'Apostolopoulou', 'Ermioni', 'Greece', 'Peristeri', 'Palea Kabalas 21', '12141', '2105753088'),
(4, 'Xristoforou', 'Dimitris', 'Greece', 'Aigaleo', 'Street', '12351', '2105753089'),
(5, 'Aggelousis', 'Emanouil', 'Greece', 'Agia Varvara', 'Street', '12351', '6928234656'),
(6, 'Malupa', 'Ivan', 'Greece', 'Koridalos', 'Street', '23451', '2105753188'),
(7, 'Papantoniou', 'Eleonora', 'Greece', 'Marousi', 'Street', '23451', '6928225656'),
(8, 'Sveltana', 'Tamila', 'Greece', 'Peireas', 'Street', '23451', '2106753088');
go
set identity_insert Portofolio.dbo.UserDetails off
set identity_insert Portofolio.dbo.[User] on
go
insert into Portofolio.dbo.[User]( Id, Email, [Password], UserDetails, ProjectCounter) values
(1, 'papado@in.gr', 'root01', 1, 1), (2, 'antoni@in.gr', 'root02', 2, 1),
(3, 'aposto@in.gr', 'root03', 3, 1), (4, 'xristo@in.gr', 'root04', 4, 0),
(5, 'aggelo@in.gr', 'root05', 5, 0), (6, 'malupa@in.gr', 'root06', 6, 0),
(7, 'papant@in.gr', 'root07', 7, 0), (8, 'svalta@in.gr', 'root08', 8, 0);
go
set identity_insert Portofolio.dbo.[User] off
set identity_insert Portofolio.dbo.Project on
go
insert into Portofolio.dbo.Project(Id, Title, PablishDate, [ExpireDate], MoneyGoal, [Description], ProjectCtrator) values
(1,'On my Own', cast(N'2018-06-01' as Date), cast(N'2019-06-01' as Date), 2500.00, 'Description info', 1 ),
(2,'LikeIt', cast(N'2018-06-05' as Date), cast(N'2019-05-01' as Date), 5000.00, 'Description info', 2 ),
(3,'Madalomania', cast(N'2018-06-10' as Date), cast(N'2018-12-01' as Date), 1000.00, 'Description info', 3 );
go
set identity_insert Portofolio.dbo.Project off
set identity_insert Portofolio.dbo.Package on
go
insert into Portofolio.dbo.Package(Id, PackageName, PledgeAmount, PackageLeft, [Description], Project) values
(1,'Starter', 5.00, 15, 'Description info', 1 ), (2,'Demi', 10.00, 20,  'Description info', 1 ),
(3,'Medi', 15.00, 5,  'Description info', 2 ), (4,'Large', 30.00, 12, 'Description info', 2 ),
(5,'Thx', 2.00, 34, 'Description info', 3 ), (6,'Suport', 7.00, 12, 'Description info', 3 );
go
set identity_insert Portofolio.dbo.Package off