create database VueStoreDB;
go

use VueStoreDB;

create table Clientes
(
  Id varchar(50) primary key not null,
  Nombre varchar(50) not null,
  Apellido varchar(50) not null
);

create table Productos
(
  Id int identity primary key not null,
  Nombre varchar(50) not null,
  Descripcion text not null,
  UrlImagen varchar(300) not null,
  stock int not null,
  precio decimal(18,2) not null
);
create table Carritos
(
  Id int identity primary key not null,
  IdCliente varchar(50) not null,
  CantProductos int not null default 0,
  TotalAPagar decimal(18,2) not null default 0,
  constraint Fk_CarritoToCliente foreign key(IdCliente) references Clientes(Id)
);
create table DetalleCarritos
(
  Id int identity primary key not null,
  IdCarrito int not null,
  IdProducto int not null,
  constraint Fk_DetalleToCarrito foreign key(IdCarrito) references Carritos(Id),
  constraint Fk_DetalleToProducto foreign key(IdProducto) references Productos(Id)
);

INSERT INTO Clientes(Id, Nombre, Apellido)
VALUES
('delvi2505','Bienvenido','Hernandez'),
('brianna15','Brianna','Severino')

INSERT INTO Carritos(IdCliente)
VALUES
('delvi2505'),
('brianna15')

INSERT INTO Productos(Nombre, Descripcion, UrlImagen, stock, precio)
VALUES
('Pantalones','Los más resistentes geans que podras encontrar','https://media.istockphoto.com/photos/closeup-of-boyfriend-geans-picture-id912931422?s=612x612', 300, 700),
('Sueter','Informal, comodo y económico','https://unitystores.vteximg.com.br/arquivos/ids/163490-1000-1000/sueter-jose-sailor-pm701939580-1.jpg?v=637278266705500000', 500, 300),
('Camisa','Elegante camisa para ocasiones especiales','https://image.dhgate.com/0x0s/f2-albu-g10-M00-9B-51-rBVaVlyktaOADdV8AADPRRhvDzY888.jpg/nuevos-hombres-camisa-para-hombre-de-manga.jpg', 5, 2000),
('Medias','Resistentes y comodas medias','https://cdn01.pisamonas.es/media/catalog/product/cache/3/image/9df78eab33525d08d6e5fb8d27136e95/C/a/Calcetin_Corto_Canale_CONDOR_Gris-106.jpg', 100, 150),
('Gorra','Gorra de estilo unico y original','https://hatstore.imgix.net/302448-1_1.jpg?w=261&auto=compress,format&q=75', 150, 500)