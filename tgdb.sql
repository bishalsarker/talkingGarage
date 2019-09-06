create table parking_lots(
	lot_id int identity(1,1) primary key,
	lot_name varchar(max) not null,
	latitude varchar(max) not null,
	longitude varchar(max) not null,
	max_vehicle varchar(max) not null
);

create table parking_logs(
	log_id int identity(1,1) primary key,
	lot_id varchar(max) not null,
	user_id varchar(max) not null,
	day varchar(max) not null,
	in_time varchar(max) not null,
	out_time varchar(max) not null,
	pay_status varchar(max) not null
);

create table parking_bookings(
	book_id int identity(1,1) primary key,
	lot_id varchar(max) not null,
	user_id varchar(max) not null,
	day varchar(max) not null,
	tm varchar(max) not null,
	status varchar(max) not null,
);

create table parking_users(
	user_id int identity(1,1) primary key,
	user_name varchar(max) not null,
	email varchar(max) not null,
	password varchar(max) not null,
	card_number varchar(max) not null,
);