create database Portofolio;
go

create table Portofolio.dbo.UserDetails(
	Id bigint primary key identity(1, 1) not null,
	CreatedDate date not null default (getdate()),
	LastName nvarchar(50) not null,
	FirstName nvarchar(50) not null,
	Country nvarchar(20),
	Town nvarchar(20),
	Street nvarchar(100),
	PostalCode varchar(5),
	PhoneNumber varchar(10),
	ProfileImage image
);

create table Portofolio.dbo.[User](
	Id bigint primary key identity(1, 1) not null,
	CreatedDate date not null default (getdate()),
	Email nvarchar(100) not null unique,
	[Password] nvarchar(100) not null ,
	SeasonId uniqueidentifier null,
	ProjectCounter int not null default 0,
	Followers int not null default 0,
	UserDetails bigint unique foreign key references Portofolio.dbo.UserDetails(Id) not null
);


create table Portofolio.dbo.Project(
	Id bigint primary key identity(1, 1) not null,
	CreatedDate date not null default (getdate()),
	Title nvarchar(100) not null unique,
	ProjectImage image,
	Likes int not null default 0,
	PablishDate date not null default (getdate()),
	[ExpireDate] date not null,
	MoneyGoal money not null,
	MoneyReach money not null default 0.00,
	[Description] ntext,
	ProjectCtrator
	 bigint foreign key references Portofolio.dbo.[User](Id) not null
);

create table Portofolio.dbo.Package(
	Id bigint primary key identity(1, 1) not null,
	CreatedDate date not null default (getdate()),
	PackageName nvarchar(100) not null,
	PledgeAmount money not null default 0.00,
	TimesSelected int not null default 0,
	PackageLeft int,
	DeliveryDate datetime,
	[Description] ntext not null,
	Project bigint foreign key references Portofolio.dbo.Project(Id) not null
);

create table Portofolio.dbo.BackerBuyPackage(
	Backer bigint foreign key references Portofolio.dbo.[User](Id) not null,
	Package bigint foreign key references Portofolio.dbo.Package(Id) not null,
	constraint PK_BackerBuyPackage primary key clustered (Backer,  Package)
);

create table Portofolio.dbo.BackerFollowCreator(
	Backer bigint foreign key references Portofolio.dbo.[User](Id) not null,
	ProjectCreator bigint foreign key references Portofolio.dbo.[User](Id) not null,
	constraint PK_BackerFollowCreator primary key clustered (Backer,  ProjectCreator)
);