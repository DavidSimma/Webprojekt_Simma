create database swp_fitnessstudio collate utf8mb4_general_ci;

use swp_fitnessstudio;

create table users(
	username varchar(32) not null,
	password varchar(32) not null,
	firstname varchar(32) not null,
	lastname varchar(32) not null,
	age Date not null,
	gender int null,

	constraint username_PK primary key(username)
)engine=InnoDB;

insert into articles values("admin", "admin", "first", "last", "2020-2-02", 0);

select * from users;