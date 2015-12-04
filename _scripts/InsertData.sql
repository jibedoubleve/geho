
--------------------------------------------------------------
SET IDENTITY_INSERT Groups ON
--------------------------------------------------------------
insert into Groups(Id, Name) values (2, 'Eveil')
insert into Groups(Id, Name) values (3, 'Secu A')
insert into Groups(Id, Name) values (4, 'Secu B')
insert into Groups(Id, Name) values (5, 'Secu C')
insert into Groups(Id, Name) values (6, 'Sociaux') 
insert into Groups(Id, Name) values (7, 'Estime')
--------------------------------------------------------------
SET IDENTITY_INSERT Groups OFF
SET IDENTITY_INSERT People ON
--------------------------------------------------------------
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(1, 1, 0, 'Stéphanie', '', NULL);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(2, 1, 0, 'Géry', '', NULL);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(3, 1, 0, 'Vincent', '', NULL);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(4, 1, 0, 'Nadine', '', NULL);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(5, 1, 0, 'Pelin', '', NULL);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(7, 1, 0, 'Valérie', '', NULL);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(8, 1, 0, 'Valérie', 'Phi', NULL);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(9, 1, 0, 'Karim', '', NULL);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(10, 1, 0, 'Florie', '', NULL);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(11, 1, 0, 'Tiffany', '', NULL);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(12, 0, 0, 'Alain', '', 2);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(13, 0, 0, 'Younes', 'O', 2);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(14, 0, 0, 'Paolo', '', 2);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(15, 0, 0, 'Sabrina', '', 2);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(16, 0, 0, 'David', '', 2);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(17, 0, 0, 'Stéphane', '', 3);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(18, 0, 0, 'Philippe', '', 3);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(19, 0, 0, 'Franck', '', 3);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(20, 0, 0, 'Mouad', '', 3);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(21, 0, 0, 'Jasmine', '', 3);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(22, 0, 0, 'Fabian', '', 4);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(23, 0, 0, 'Nicolas', '', 4);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(24, 0, 0, 'Eric', '', 4);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(25, 0, 0, 'Marc', '', 4);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(26, 0, 0, 'Younes', 'A', 4);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(27, 0, 0, 'Jean-luc', '', 4);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(28, 0, 0, 'Fabienne', '', 5);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(29, 0, 0, 'Claude', '', 5);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(30, 0, 0, 'Thierry', '', 5);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(31, 0, 0, 'Badr', '', 5);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(32, 0, 0, 'Monique', '', 5);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(33, 0, 0, 'Pascal', '', 6);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(34, 0, 0, 'Sevgi', '', 6);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(35, 0, 0, 'Eddy', '', 6);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(36, 0, 0, 'Michel', '', 6);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(37, 0, 0, 'Carole', '', 6);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(38, 0, 0, 'Tom', '', 7);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(39, 0, 0, 'Christophe', '', 7);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(40, 0, 0, 'Giuseppe', '', 7);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(41, 0, 0, 'Antoine', '', 7);
insert into People (Id, IsEducator, IsTrainee, Name, Surname, Group_id) values(42, 0, 0, 'Ma-Gisabeth', '', 7);