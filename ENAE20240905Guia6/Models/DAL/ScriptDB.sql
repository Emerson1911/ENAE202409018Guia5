create database dbENAE20240905;

use dbENAE20240905;

create table ProductENAE(
Id int identity(1,1) primary key,
NombreENAE varchar(50)NOT NULL,
DescripcionENAE varchar(50),
PrecioENAE Decimal(10,2)NOT NULL
);