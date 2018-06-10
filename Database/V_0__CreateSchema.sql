create database Portofolio;
go

create table Portofolio.dbo.CreatorDetails(
	Id bigint primary key identity(1, 1) not null,
	CreatedDate date not null default (getdate()),
	LastName nvarchar(50) not null,
	FirstName nvarchar(50) not null,
	Country nvarchar(20),
	Town nvarchar(20),
	Street nvarchar(100),
	PostalCode varchar(5),
	PhoneNumber varchar(10)
);

create table Portofolio.dbo.ProjectCreator(
	Id bigint primary key identity(1, 1) not null,
	CreatedDate date not null default (getdate()),
	BrandName nvarchar(100) not null unique,
	Link nvarchar(100) not null,
	ProfileImage image,
	ProjectCounter int not null default 0,
	Followers int not null default 0,
	BirthDay date,
	About ntext,
	CreatorDetailsId bigint unique foreign key references Portofolio.dbo.CreatorDetails(Id) not null
);

create table Portofolio.dbo.BackerDetails(
	Id bigint primary key identity(1, 1) not null,
	CreatedDate date not null default (getdate()),
	LastName nvarchar(50) not null,
	FirstName nvarchar(50) not null,
	Country nvarchar(20),
	Town nvarchar(20),
	Street nvarchar(100),
	PostalCode varchar(5),
	PhoneNumber varchar(10)
);

create table Portofolio.dbo.Backer(
	Id bigint primary key identity(1, 1) not null,
	CreatedDate date not null default (getdate()),
	BackerDetailsId bigint unique foreign key references Portofolio.dbo.BackerDetails(Id) not null
);

create table Portofolio.dbo.Project(
	Id bigint primary key identity(1, 1) not null,
	CreatedDate date not null default (getdate()),
	Title nvarchar(100) not null unique,
	Link nvarchar(100) not null,
	ProjectImage image,
	Likes int not null default 0,
	PablishDate date not null,
	[ExpireDate] date not null,
	MoneyGoal money not null,
	MoneyReach money not null default 0.00,
	[Description] ntext,
	CreatorId bigint foreign key references Portofolio.dbo.ProjectCreator(Id) not null
);

create table Portofolio.dbo.Package(
	Id bigint primary key identity(1, 1) not null,
	CreatedDate date not null default (getdate()),
	PackageName nvarchar(100) not null,
	MoneyCost money not null default 0.00,
	SoldCounter int not null default 0,
	[Description] ntext not null,
	ProjectId bigint foreign key references Portofolio.dbo.Project(Id) not null
);

create table Portofolio.dbo.BackerBuyPackage(
	BackerId bigint foreign key references Portofolio.dbo.Backer(Id) not null,
	PackageId bigint foreign key references Portofolio.dbo.Package(Id) not null,
	constraint PK_BackerBuyPackage primary key clustered (BackerId,  PackageId)
);

create table Portofolio.dbo.BackerFollowCreator(
	BackerId bigint foreign key references Portofolio.dbo.Backer(Id) not null,
	ProjectCreatorId bigint foreign key references Portofolio.dbo.ProjectCreator(Id) not null,
	constraint PK_BackerFollowCreator primary key clustered (BackerId,  ProjectCreatorId)
);