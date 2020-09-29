create database DB_PRC_Project
use DB_PRC_Project

create table Users (
	Username varchar(100) primary key,
	PasswordHash varbinary(max) not null,
	PasswordSalt varbinary(max) not null,
	Email varchar(450) not null,
	FullName nvarchar(100) not null,
	Phonenumber varchar(11),
	Address nvarchar(450),
	Photo varchar(max),
	RoleId varchar(10) not null,
	DelFlg bit not null default 0,
	InsBy varchar(50) not null,
	InsDatetime datetime not null default getdate(),
	UpdBy varchar(50) not null,
	UpdDatetime datetime not null default getdate()
)

create table Role (
	RoleId varchar(10) primary key,
	RoleNm varchar(50) not null,
	DelFlg bit not null default 0,
	InsBy varchar(50) not null,
	InsDatetime datetime not null default getdate(),
	UpdBy varchar(50) not null,
	UpdDatetime datetime not null default getdate()
)

create table Category (
	CategoryId varchar(50) primary key,
	CategoryNm nvarchar(50) not null,
	DelFlg bit not null default 0,
	InsBy varchar(50) not null,
	InsDatetime datetime not null default getdate(),
	UpdBy varchar(50) not null,
	UpdDatetime datetime not null default getdate()
)

create table Product (
	ProductId varchar(50) primary key,
	ProductNm nvarchar(200) not null,
	Description nvarchar(500) not null,
	Price float not null,
	Photo varchar(max),
	CategoryId varchar(50) not null,
	DelFlg bit not null default 0,
	InsBy varchar(50) not null,
	InsDatetime datetime not null default getdate(),
	UpdBy varchar(50) not null,
	UpdDatetime datetime not null default getdate()
)

create table Orders (
	OrderId varchar(50) primary key,
	Username varchar(100) not null,
	DelFlg bit not null default 0,
	InsBy varchar(50) not null,
	InsDatetime datetime not null default getdate(),
	UpdBy varchar(50) not null,
	UpdDatetime datetime not null default getdate()
)

create table OrderDetail  (
	Id int identity(1,1) primary key,
	OrderId int not null,
	ProductId varchar(50) not null,
	Quantity int not null,
	Price float not null,
)

alter table Users
add constraint FK_User_Role foreign key (RoleId) references Role(RoleId)

alter table Product
add constraint FK_Product_Category foreign key (CategoryId) references Category(CategoryId)

alter table Orders
add constraint FK_Order_User foreign key (Username) references Users(Username)

alter table OrderDetail
add constraint FK_OrderDetail_Order foreign key (OrderId) references Orders(OrderId)

alter table OrderDetail
add constraint FK_OrderDetail_Product foreign key (ProductId) references Product(ProductId)
