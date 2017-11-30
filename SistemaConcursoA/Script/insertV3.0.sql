insert into Semestre values ('2015-II','2015','2015-08-07','2016-12-07')
insert into Semestre values ('2016-I','2016','2016-03-10','2016-07-07')
insert into Semestre values ('2016-II','2016','2016-08-07','2016-12-07')
insert into Semestre values ('2017-I','2017','2017-03-10','2017-07-07')
insert into Semestre values ('2017-II','2017','2017-08-07','2017-12-07')

insert into Ciclo VALUES ('Primer ciclo','Primer ciclo')
insert into Ciclo VALUES ('Segundo ciclo','Segundo ciclo')
insert into Ciclo VALUES ('Tercer ciclo','Tercer ciclo')
insert into Ciclo VALUES ('Cuarto ciclo','Cuarto ciclo')
insert into Ciclo VALUES ('Quinto ciclo','Quinto ciclo')
insert into Ciclo VALUES ('Sexto ciclo','Sexto ciclo')
insert into Ciclo VALUES ('Septimo ciclo','Septimo ciclo')
insert into Ciclo VALUES ('Octavo ciclo','Octavo ciclo')
insert into Ciclo VALUES ('Noveno ciclo','Noveno ciclo')
insert into Ciclo VALUES ('Decimo ciclo','Decimo ciclo')

insert into PlanEstudio VALUES('1','Plan de Estudio 2015-II','Activo');
insert into PlanEstudio VALUES('2','Plan de Estudio 2016-I','Activo');
insert into PlanEstudio VALUES('3','Plan de Estudio 2016-II','Activo');
insert into PlanEstudio VALUES('4','Plan de Estudio 2017-I','Activo');
insert into PlanEstudio VALUES('5','Plan de Estudio 2017-II','Activo');

insert into CURSO VALUES ('ING-101','4','1','MATEMATICA 1','5','4','2','6','No tiene','Activo');
insert into CURSO VALUES ('ING-102','4','1','MATEMATICA BASICA 1','5','4','2','6','No tiene','Activo');
insert into CURSO VALUES ('ING-103','4','1','DISEÑO EN INGENIERIA ','4','4','2','6','No tiene','Activo');
insert into CURSO VALUES ('ING-104','4','1','COMUNICACION ORAL Y ESCRITA','3','2','2','4','No tiene','Activo');
insert into CURSO VALUES ('ING-105','4','1','METODOLOGIA DEL TRABAJO UNIVERSITARIO','3','2','2','4','No tiene','Activo');
insert into CURSO VALUES ('SI-171','4','1','INTRODUCCION A LA INGENIERIA DE SISTEMAS','2','0','0','0','No tiene','Activo');
insert into CURSO VALUES ('ING-201','4','2','MATEMATICA II','5','4','2','6','ING-101','Activo');
insert into CURSO VALUES ('ING-202','4','2','FISICA I','5','4','2','6','14 creditos','Activo');
insert into CURSO VALUES ('ING-203','4','2','TECNICAS DE PROGRAMACION ','4','4','2','6','No tiene','Activo');
insert into CURSO VALUES ('ING-204','4','2','ECONOMIA I','2','2','2','4','12 creditos','Activo');
insert into CURSO VALUES ('ING-205','4','2','ESTADISTICA I','3','2','2','4','12 creditos','Activo');
insert into CURSO VALUES ('ING-206','4','2','QUIMICA I','4','0','0','5','No tiene','Activo');
insert into CURSO VALUES ('SI-371','4','3','MATEMATICA DISCRETA','5','4','2','6','ING-201','Activo');
insert into CURSO VALUES ('SI-372','4','3','SISTEMAS ELECTRONICOS DIGITALES','5','4','2','6','ING-202','Activo');
insert into CURSO VALUES ('SI-373','4','3','ALGORITMOS Y ESTRUCTURA DE DATOS ','4','4','2','6','ING-203','Activo');
insert into CURSO VALUES ('SI-374','4','3','DISEÑO Y MODELAMIENTO VIRTUAL','2','2','2','4','ING-103','Activo');
insert into CURSO VALUES ('SI-375','4','3','MOEDELO DE PROCESOS DE NEGOCIOS','3','2','2','4','12 creditos','Activo');
insert into CURSO VALUES ('SI-376','4','3','SISTEMAS DE INFORMACION','4','0','0','5','36 creditos','Activo');
insert into CURSO VALUES ('SI-471','4','4','INTRODUCCION AL DESARROLLO WEB','3','4','2','4','36 creditos','Activo');
insert into CURSO VALUES ('SI-472','4','4','ARQUITECTURA DEL COMPUTADOR','4','4','2','6','SI-372','Activo');
insert into CURSO VALUES ('SI-473','4','4','PROGRAMACION I','4','4','2','6','SI-373','Activo');
insert into CURSO VALUES ('SI-474','4','4','INGENIERIA ECONOMICA Y FINANCIERA','3','2','2','4','SI-371','Activo');
insert into CURSO VALUES ('SI-475','4','4','INGENIERIA DE REQUERIMIENTOS','4','2','2','6','SI-375','Activo');
insert into CURSO VALUES ('SI-476','4','4','PROGRAMACION ORIENTADA A OBJETOS','4','2','2','6','60 creditos','Activo');
insert into CURSO VALUES ('SI-571','4','5','DISEÑO DE BASE DE DATOS','4','4','2','6','SI-475','Activo');
insert into CURSO VALUES ('SI-572','4','5','SISTEMAS OPERATIVOS I','4','4','2','6','SI-472','Activo');
insert into CURSO VALUES ('SI-573','4','5','PROGRAMACION II','4','4','2','6','SI-473','Activo');
insert into CURSO VALUES ('SI-574','4','5','INVESTIGACION DE OPERACIONES','3','2','2','4','SI-474','Activo');
insert into CURSO VALUES ('SI-575','4','5','DISEÑO Y ARQUITECTURA DE SPFTWARE','4','2','2','6','SI-475','Activo');
insert into CURSO VALUES ('SI-576','4','5','INTERACCION Y DISEÑO DE INTERFACES','3','2','2','4','80 creditos','Activo');
insert into CURSO VALUES ('SI-671','4','6','BASE DE DATOS I','4','4','2','6','SI-575','Activo');
insert into CURSO VALUES ('SI-672','4','6','SISTEMAS OPERATIVOS II','4','4','2','6','SI-572','Activo');
insert into CURSO VALUES ('SI-673','4','6','PROGRAMACION III','4','4','2','6','SI-573','Activo');
insert into CURSO VALUES ('SI-674','4','6','DESARRROLLO DE APLICACIONES WEB I','3','2','2','4','SI-571','Activo');
insert into CURSO VALUES ('SI-675','4','6','INGENIERIA DE SOFTWARE','3','2','2','6','SI-575','Activo');
insert into CURSO VALUES ('SI-676','4','6','ETICA PROFESIONAL','3','0','0','0','100 creditos','Activo');
insert into CURSO VALUES ('SI-771','4','7','BASE DE DATOS II','4','4','2','6','SI-671','Activo');
insert into CURSO VALUES ('SI-772','4','7','REDES Y COMUNICACIONES DE DATOS I','3','4','2','4','SI-672','Activo');
insert into CURSO VALUES ('SI-773','4','7','SOLUCIONES MOVILES I','4','4','2','6','SI-673','Activo');
insert into CURSO VALUES ('SI-774','4','7','GESTION DE PROYECTOS DE TI','4','2','2','6','SI-675','Activo');
insert into CURSO VALUES ('SI-775','4','7','CALIDAD Y PRUEBAS DE SOFTWARE','4','2','2','6','120 creditos','Activo');
insert into CURSO VALUES ('SI-871','4','8','INTELIGENCIA DE NEGOCIOS','4','4','2','6','SI-771','Activo');
insert into CURSO VALUES ('SI-872','4','8','REDES Y COMUNICACIONES DE DATOS II','4','4','2','6','SI-772','Activo');
insert into CURSO VALUES ('SI-873','4','8','INGLES TECNICO','4','4','2','6','inguno','Activo');
insert into CURSO VALUES ('SI-874','4','8','DESARRROLLO DE APLICACIONES WEB II','4','2','2','6','SI-674','Activo');
insert into CURSO VALUES ('SI-875','4','8','SEGURIDAD INFORMATICA','4','2','2','5','140 creditos','Activo');
insert into CURSO VALUES ('SI-971','4','9','AUDITORIA DE SISTEMAS','3','4','2','4','SI-875','Activo');
insert into CURSO VALUES ('SI-972','4','9','REDES Y COMUNICACIONES DE DATOS III','4','4','2','6','SI-872','Activo');
insert into CURSO VALUES ('SI-973','4','9','METODOLOGIA DE LA INVESTIGACION APLICADA','3','4','2','4','SI-873','Activo');
insert into CURSO VALUES ('SI-974','4','9','CONSTRUCCION DE SOFTWARE I','4','2','2','6','SI-874','Activo');
insert into CURSO VALUES ('SI-975','4','9','PLANEAMIENTO ESTRATEGICO DE TI','4','2','2','5','160 creditos','Activo');
insert into CURSO VALUES ('SI-976','4','9','GESTION DE LA CONFIGURACION Y ADMINISTRACION DE SOFTWARE','3','2','2','4','SI-775','Activo');
insert into CURSO VALUES ('SI-071','4','10','TALLER DE LIDERAZGO Y EMPRENDIMIENTO','3','4','2','4','180 creditos','Activo');
insert into CURSO VALUES ('SI-072','4','10','TALLER REDES Y COMUNICACION DE DATOS ','4','4','2','6','SI-972','Activo');
insert into CURSO VALUES ('SI-073','4','10','PROYECTO DE TESIS','4','4','2','6','SI-973','Activo');
insert into CURSO VALUES ('SI-074','4','10','CONSTRUCCION DE SOFTWARE II','4','2','2','6','SI-974','Activo');
insert into CURSO VALUES ('SI-075','4','10','GERENCIA DE TI','3','2','2','4','SI-975','Activo');
insert into CURSO VALUES ('Libre','4','10','Curso Libre','0','0','0','0','No tiene','Activo');

insert into TipoPersona VALUES('Docente','Activo');
insert into TipoPersona VALUES('Alumno','Activo');
insert into TipoPersona VALUES('Externo','Activo');

insert into Persona values(1,'00568978','00635487','Alberto','Flor Rodriguez','alflor@egx.com','986656532','Activo')
insert into Persona values(1,'00568979','00635488','Henry','Chaina','hchaina@hotmail.com','986656539','Activo')
insert into Persona values(1,'00235650','00369898','Elard','Rodriguez','erodriguez@upt.pe','965876879','Activo')
insert into Persona values(1,'00235645','00369874','Enrique','Lanchipa','elanchipa@upt.pe','965875421','Activo')
insert into Persona values(2,'72463957','2014047663','Luis Eduardo','Mamani Bedregal','bedregale@gmail.com','952020236','Activo')
insert into Persona values(2,'72463960','2011239244','Eduardo','Ayca Mamani','emamania@hotmail.com','952020240','Activo')
insert into PERSONA VALUES(2,'70748140','2013000209','lucero','gonzales','lucerogoga63@hotmail.com','990079772','activada');
insert into PERSONA VALUES(2,'70788545','2014049092','andrea','faucheux','andrea.ale543@hotmail.com','952969595','Activo');
insert into PERSONA VALUES(2,'72859645','2013000280','pedro','perez','ppepez@hotmail.com','952969595','Activo');
insert into PERSONA VALUES(2,'00854596','2013000290','angelica','milla','amilla@hotmail.com','952969595','Activo');
insert into PERSONA VALUES(2,'70748523','2013000215','sergio','perez','sperez@hotmail.com','952969595','Activo');

--insert into CursoDocente values('SI-874',4)
--insert into CursoDocente values('SI-875',1)

insert into Concurso values(4,4,'Concurso de Proyectos 2017-I','','2017-07-04','17:00','2017-07-01','2017-07-03','8:00','20:00','15 minutos','Activo')
insert into Concurso values(3,3,'Concurso de Proyectos 2016-II','','2016-06-04','17:00','2016-06-01','2016-06-03','8:00','20:00','15 minutos','Inactivo')
insert into Concurso values(2,2,'Concurso de Proyectos 2016-I','','2016-04-04','17:00:00','2016-04-01','2016-04-04','08:00:00','20:00:00','15 minutos','Inactivo')

insert into CATEGORIA VALUES(1,'CATEGORIA A','1-3 ciclo');
insert into CATEGORIA VALUES(1,'CATEGORIA B','4-7 ciclo');
insert into CATEGORIA VALUES(1,'CATEGORIA C','8-10 ciclo');
insert into CATEGORIA VALUES(1,'CATEGORIA D','Categoria Libre');
insert into Categoria values(2,'CATEGORIA A','CATEGORIA A')
insert into Categoria values(2,'CATEGORIA B','CATEGORIA B')
insert into Categoria values(3,'CATEGORIA A','CATEGORIA A')
insert into Categoria values(3,'CATEGORIA B','CATEGORIA B')
insert into Categoria values(3,'CATEGORIA C','CATEGORIA C')

--insert into CategoriaJurado values (2,1)
--insert into CategoriaJurado values (2,2)

insert into Criterio values (1,'Creatividad','5','Activo')
insert into Criterio values (1,'Diseño','5','Activo')
insert into Criterio values (1,'Aplicacion Practica','5','Activo')
insert into Criterio values (1,'Complejidad tecnica','5','Activo')
insert into Criterio values (1,'Uso de herramientas tecnologicas','5','Activo')
insert into Criterio values (1,'Grado de explicacion del participante','5','Activo')

insert into RangoEvaluacion VALUES (1,'Nulo','1');
insert into RangoEvaluacion VALUES (1,'Bajo','2');
insert into RangoEvaluacion VALUES (1,'Medio','3');
insert into RangoEvaluacion VALUES (1,'Alto','4');
insert into RangoEvaluacion VALUES (1,'Muy Alto','5');

--insert into PROYECTO  VALUES (1,'SI-874',3,'Proyecto 1','2017-07-1','20:15:00','Activo');
--insert into PROYECTO  VALUES (1,'SI-874',3,'Proyecto 2','2017-07-1','20:15:00','Activo');
--insert into PROYECTO  VALUES (1,'SI-874',3,'Proyecto 3','2017-07-1','20:15:00','Activo');
--insert into PROYECTO  VALUES (1,'SI-874',3,'Proyecto 4','2017-07-1','20:15:00','Activo');

insert into PROYECTO  VALUES (1,'SI-874',3,'Proyecto 1','2016-06-1','20:15:00','Activo');
insert into PROYECTO  VALUES (2,'SI-874',3,'Proyecto 2','2016-06-1','20:15:00','Inactivo');


insert into TIPOUSUARIO VALUES('Administrador','Ingresa a todos los modulos');
insert into TIPOUSUARIO VALUES('Jurado','Ingresa a algunos modulos');

--clave: 123456
insert into USUARIO VALUES(4,1,'elanchipa','e10adc3949ba59abbe56e057f20f883e','user_default.png','Activo');
insert into USUARIO VALUES(1,2,'aflor','e10adc3949ba59abbe56e057f20f883e','user_default.png','Activo');

insert into PROYECTOESTUDIANTE  VALUES (1,5);
insert into PROYECTOESTUDIANTE  VALUES (1,6);
insert into PROYECTOESTUDIANTE  VALUES (2,7);
insert into PROYECTOESTUDIANTE  VALUES (2,8);

insert into DOCUMENTO VALUES('1','DOCUMENTO CONCURSO 1 : 2016-II','documento 1','doc','60','Activo');
insert into DOCUMENTO VALUES('2','DOCUMENTO CONCURSO 1 : 2016-II','documento 1','doc','50','Activo');

--insert into OrdenProyecto values(1,1,'16:30','Activo')
--insert into OrdenProyecto values(2,2,'16:50','Activo')
--insert into OrdenProyecto values(3,3,'17:10','Activo')
--insert into OrdenProyecto values(4,4,'17:30','Activo')

insert into OrdenProyecto values(1,3,'17:10','Activo')
insert into OrdenProyecto values(2,4,'17:30','Activo')

insert into PREMIO VALUES(1,1,'PREMIO CATEGORIA A','200 soles','Activo');
insert into PREMIO VALUES(2,1,'PREMIO CATEGORIA B','300 soles','Activo');
insert into PREMIO VALUES(3,1,'PREMIO CATEGORIA C','500 soles','Activo');
insert into PREMIO VALUES(4,1,'PREMIO CATEGORIA D','350 soles','Activo');

insert into ResultadoGanadorCategoria values(7,1,3,32,'Activo')
insert into ResultadoGanadorCategoria values(8,1,3,30,'Activo')
insert into ResultadoGanadorCategoria values(9,1,3,35,'Activo')