use Portofolio;  
go  
alter table dbo.Package drop column DeliveryDate;

alter table dbo.BackerBuyPackage add DeliveryDate datetime;

alter table dbo.Project add Category nvarchar(100) not null default('innovation');