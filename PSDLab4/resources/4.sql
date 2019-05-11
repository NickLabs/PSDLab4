BEGIN TRANSACTION;
DROP TABLE IF EXISTS "user";
CREATE TABLE IF NOT EXISTS "user" (
	"id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"name"	TEXT NOT NULL,
	"login"	TEXT NOT NULL,
	"password"	TEXT NOT NULL,
	"idRole"	INTEGER,
	FOREIGN KEY("idRole") REFERENCES "roles_list"("id") ON UPDATE CASCADE ON DELETE CASCADE
);
DROP TABLE IF EXISTS "materials_coefficients";
CREATE TABLE IF NOT EXISTS "materials_coefficients" (
	"idMaterial"	INTEGER NOT NULL,
	"idCoefficient"	INTEGER NOT NULL,
	"value"	REAL NOT NULL,
	FOREIGN KEY("idCoefficient") REFERENCES "coefficient"("id") ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY("idMaterial") REFERENCES "material"("id") ON UPDATE CASCADE ON DELETE CASCADE,
	PRIMARY KEY("idMaterial","idCoefficient")
);
DROP TABLE IF EXISTS "prop_mat";
CREATE TABLE IF NOT EXISTS "prop_mat" (
	"idMat"	INTEGER NOT NULL,
	"idProp"	INTEGER NOT NULL,
	"value"	REAL NOT NULL,
	PRIMARY KEY("idMat","idProp"),
	FOREIGN KEY("idMat") REFERENCES "material"("id") ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY("idProp") REFERENCES "propertie"("id") ON UPDATE CASCADE ON DELETE CASCADE
);
DROP TABLE IF EXISTS "material";
CREATE TABLE IF NOT EXISTS "material" (
	"id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"name"	TEXT NOT NULL
);
DROP TABLE IF EXISTS "canal";
CREATE TABLE IF NOT EXISTS "canal" (
	"id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"length"	REAL NOT NULL,
	"width"	REAL NOT NULL,
	"depth"	REAL NOT NULL
);
DROP TABLE IF EXISTS "roles_list";
CREATE TABLE IF NOT EXISTS "roles_list" (
	"id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"name"	TEXT NOT NULL
);
DROP TABLE IF EXISTS "params_limitations";
CREATE TABLE IF NOT EXISTS "params_limitations" (
	"idVariable"	INTEGER NOT NULL,
	"idMaterial"	INTEGER NOT NULL,
	"minValue"	REAL,
	"maxValue"	REAL,
	FOREIGN KEY("idVariable") REFERENCES "varible_params"("id") ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY("idMaterial") REFERENCES "material"("id") ON UPDATE CASCADE ON DELETE CASCADE,
	PRIMARY KEY("idVariable","idMaterial")
);
DROP TABLE IF EXISTS "propertie";
CREATE TABLE IF NOT EXISTS "propertie" (
	"id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"name"	TEXT NOT NULL,
	"idMeasurementUnit"	INTEGER,
	FOREIGN KEY("idMeasurementUnit") REFERENCES "measurement_unit"("id") ON UPDATE CASCADE ON DELETE CASCADE
);
DROP TABLE IF EXISTS "expers_variables";
CREATE TABLE IF NOT EXISTS "expers_variables" (
	"idExper"	INTEGER NOT NULL,
	"idVar"	INTEGER NOT NULL,
	"value"	INTEGER NOT NULL,
	FOREIGN KEY("idExper") REFERENCES "experiment"("id"),
	PRIMARY KEY("idVar","idExper"),
	FOREIGN KEY("idVar") REFERENCES "varible_params"("id")
);
DROP TABLE IF EXISTS "experiment";
CREATE TABLE IF NOT EXISTS "experiment" (
	"id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"idCanal"	INTEGER NOT NULL,
	"idMaterial"	INTEGER NOT NULL,
	"idEmployee"	INTEGER NOT NULL,
	"date"	TEXT NOT NULL,
	"performance"	REAL NOT NULL,
	"viscosity"	REAL NOT NULL,
	"temperature"	REAL NOT NULL,
	FOREIGN KEY("idMaterial") REFERENCES "material"("id"),
	FOREIGN KEY("idCanal") REFERENCES "canal"("id"),
	FOREIGN KEY("idEmployee") REFERENCES "user"("id")
);
DROP TABLE IF EXISTS "varible_params";
CREATE TABLE IF NOT EXISTS "varible_params" (
	"id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"name"	TEXT NOT NULL,
	"idMeasurementUnit"	INTEGER,
	FOREIGN KEY("idMeasurementUnit") REFERENCES "measurement_unit"("id") ON UPDATE CASCADE ON DELETE CASCADE
);
DROP TABLE IF EXISTS "coefficient";
CREATE TABLE IF NOT EXISTS "coefficient" (
	"id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"name"	TEXT NOT NULL,
	"idMeasurementUnit"	INTEGER,
	FOREIGN KEY("idMeasurementUnit") REFERENCES "measurement_unit"("id") ON UPDATE CASCADE ON DELETE CASCADE
);
DROP TABLE IF EXISTS "measurement_unit";
CREATE TABLE IF NOT EXISTS "measurement_unit" (
	"id"	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
	"units_name"	TEXT NOT NULL
);
INSERT INTO "user" VALUES (1,'Винокуров Никита Александрович','nick123','12345',2);
INSERT INTO "materials_coefficients" VALUES (1,1,12000.0);
INSERT INTO "materials_coefficients" VALUES (1,2,0.05);
INSERT INTO "materials_coefficients" VALUES (1,3,165.0);
INSERT INTO "materials_coefficients" VALUES (1,4,0.28);
INSERT INTO "materials_coefficients" VALUES (1,5,400.0);
INSERT INTO "prop_mat" VALUES (1,1,1380.0);
INSERT INTO "prop_mat" VALUES (1,2,2500.0);
INSERT INTO "prop_mat" VALUES (1,3,145.0);
INSERT INTO "material" VALUES (1,'Поливинилхлорид');
INSERT INTO "canal" VALUES (2,11.0,3.0,'0,1');
INSERT INTO "canal" VALUES (3,'9,5','2,5','0,1');
INSERT INTO "canal" VALUES (4,'9,5','2,5','0,1');
INSERT INTO "canal" VALUES (5,'9,5',3.0,'0,1');
INSERT INTO "canal" VALUES (6,'9,5','2,5','0,1');
INSERT INTO "canal" VALUES (7,3.0,3.0,3.0);
INSERT INTO "canal" VALUES (8,4.0,4.0,4.0);
INSERT INTO "canal" VALUES (9,12.0,3.0,'0,1');
INSERT INTO "canal" VALUES (10,3.0,3.0,'0,2');
INSERT INTO "canal" VALUES (11,3.0,3.0,'0,1');
INSERT INTO "canal" VALUES (12,12.0,2.0,'0,1');
INSERT INTO "canal" VALUES (13,'9,5',3.0,'0,2');
INSERT INTO "canal" VALUES (5001,'9,5',3.0,'0,2');
INSERT INTO "roles_list" VALUES (1,'Администратор');
INSERT INTO "roles_list" VALUES (2,'Исследователь');
INSERT INTO "params_limitations" VALUES (1,1,0.1,100.0);
INSERT INTO "params_limitations" VALUES (2,1,70.0,5000.0);
INSERT INTO "propertie" VALUES (1,'Плотность',1);
INSERT INTO "propertie" VALUES (2,'Удельная теплоёмкость',2);
INSERT INTO "propertie" VALUES (3,'Температура плавления',3);
INSERT INTO "expers_variables" VALUES (0,1,'1,5');
INSERT INTO "expers_variables" VALUES (0,2,149);
INSERT INTO "expers_variables" VALUES (1,1,10);
INSERT INTO "expers_variables" VALUES (1,2,140);
INSERT INTO "expers_variables" VALUES (2,1,'1,5');
INSERT INTO "expers_variables" VALUES (2,2,150);
INSERT INTO "expers_variables" VALUES (3,1,'1,5');
INSERT INTO "expers_variables" VALUES (3,2,200);
INSERT INTO "expers_variables" VALUES (4,1,2);
INSERT INTO "expers_variables" VALUES (4,2,150);
INSERT INTO "expers_variables" VALUES (5,1,3);
INSERT INTO "expers_variables" VALUES (5,2,150);
INSERT INTO "expers_variables" VALUES (6,1,12);
INSERT INTO "expers_variables" VALUES (6,2,180);
INSERT INTO "expers_variables" VALUES (7,1,4);
INSERT INTO "expers_variables" VALUES (7,2,200);
INSERT INTO "expers_variables" VALUES (8,1,3);
INSERT INTO "expers_variables" VALUES (8,2,160);
INSERT INTO "expers_variables" VALUES (9,1,3);
INSERT INTO "expers_variables" VALUES (9,2,160);
INSERT INTO "expers_variables" VALUES (10,1,4);
INSERT INTO "expers_variables" VALUES (10,2,200);
INSERT INTO "expers_variables" VALUES (11,1,4);
INSERT INTO "expers_variables" VALUES (11,2,150);
INSERT INTO "expers_variables" VALUES (12,1,2);
INSERT INTO "expers_variables" VALUES (12,2,150);
INSERT INTO "experiment" VALUES (0,5001,1,1,'06.05.2019',2143692.0,'5,27835214939737E-47','2495,29440378304');
INSERT INTO "experiment" VALUES (1,2,1,1,'06.05.2019',7297785.0,'9,53196106424956E-47','2497,97916954159');
INSERT INTO "experiment" VALUES (2,3,1,1,'06.05.2019','908398,8','7,94268963877925E-47','2491,00326784926');
INSERT INTO "experiment" VALUES (3,4,1,1,'06.05.2019','908398,8','4,17714600026331E-47','2503,85574235032');
INSERT INTO "experiment" VALUES (4,5,1,1,'06.05.2019',1459557.0,'73910,2269840757','145,417209570071');
INSERT INTO "experiment" VALUES (5,6,1,1,'06.05.2019','1816797,6','83292,2661105097','145,297724312045');
INSERT INTO "experiment" VALUES (6,7,1,1,'06.05.2019',134136000.0,'48051,6773397937','145,015840557565');
INSERT INTO "experiment" VALUES (7,8,1,1,'06.05.2019',79488000.0,'32529,8642595559','145,054961641774');
INSERT INTO "experiment" VALUES (8,9,1,1,'06.05.2019','2189335,5','82426,0246559387','145,506813927993');
INSERT INTO "experiment" VALUES (9,10,1,1,'06.05.2019',4287384.0,'69270,4669257985','145,102841068416');
INSERT INTO "experiment" VALUES (10,11,1,1,'06.05.2019',2919114.0,'90666,5693786954','145,212083846834');
INSERT INTO "experiment" VALUES (11,12,1,1,'06.05.2019',1925721.0,'90165,5420253199','145,322911206325');
INSERT INTO "experiment" VALUES (12,13,1,1,'11.05.2019',2858256.0,'61185,3793556049','145,314444285346');
INSERT INTO "varible_params" VALUES (1,'Скорость крышки',4);
INSERT INTO "varible_params" VALUES (2,'Температура крышки',3);
INSERT INTO "coefficient" VALUES (1,'Коэффициент консистенции материала при температуре приведения',5);
INSERT INTO "coefficient" VALUES (2,'Температурный коэффициент вязкости материала',6);
INSERT INTO "coefficient" VALUES (3,'Температура приведения',3);
INSERT INTO "coefficient" VALUES (4,'Индекс течения материала',9);
INSERT INTO "coefficient" VALUES (5,'Коэффициент теплоотдачи от крышки канала материалу',7);
INSERT INTO "measurement_unit" VALUES (1,'кг/м^3');
INSERT INTO "measurement_unit" VALUES (2,'Дж/(кг*С)');
INSERT INTO "measurement_unit" VALUES (3,'С');
INSERT INTO "measurement_unit" VALUES (4,'м/с');
INSERT INTO "measurement_unit" VALUES (5,'Па*с^n');
INSERT INTO "measurement_unit" VALUES (6,'1/С');
INSERT INTO "measurement_unit" VALUES (7,'Вт / (м^2 * С)');
INSERT INTO "measurement_unit" VALUES (8,'м');
INSERT INTO "measurement_unit" VALUES (9,'-');
COMMIT;
