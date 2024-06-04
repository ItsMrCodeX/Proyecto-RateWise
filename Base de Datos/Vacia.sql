-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Versión del servidor:         11.3.2-MariaDB - mariadb.org binary distribution
-- SO del servidor:              Win64
-- HeidiSQL Versión:             12.6.0.6765
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Volcando estructura de base de datos para reviewsdb
CREATE DATABASE IF NOT EXISTS `reviewsdb` /*!40100 DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci */;
USE `reviewsdb`;

-- Volcando estructura para tabla reviewsdb.entretenimiento
CREATE TABLE IF NOT EXISTS `entretenimiento` (
  `identreten` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(100) DEFAULT NULL,
  `Descripcion` varchar(300) NOT NULL,
  `Fechadeestreno` date NOT NULL,
  `Poster` longtext NOT NULL,
  `idtipoentreten` int(11) NOT NULL,
  `idgeneroentreten` int(11) NOT NULL,
  `idplataformaentreten` int(11) NOT NULL,
  PRIMARY KEY (`identreten`),
  KEY `idtipoentreten` (`idtipoentreten`),
  KEY `idgeneroentreten` (`idgeneroentreten`),
  KEY `idplataformaentreten` (`idplataformaentreten`),
  CONSTRAINT `entretenimiento_ibfk_1` FOREIGN KEY (`idtipoentreten`) REFERENCES `tipoentreten` (`idtipoEntreten`),
  CONSTRAINT `entretenimiento_ibfk_2` FOREIGN KEY (`idgeneroentreten`) REFERENCES `generoentreten` (`idGeneroEntreten`),
  CONSTRAINT `entretenimiento_ibfk_3` FOREIGN KEY (`idplataformaentreten`) REFERENCES `plataformaentreten` (`idplataformaentreten`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla reviewsdb.generoentreten
CREATE TABLE IF NOT EXISTS `generoentreten` (
  `idGeneroEntreten` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) NOT NULL,
  PRIMARY KEY (`idGeneroEntreten`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla reviewsdb.generovideojuego
CREATE TABLE IF NOT EXISTS `generovideojuego` (
  `idGeneroVid` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) NOT NULL,
  PRIMARY KEY (`idGeneroVid`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla reviewsdb.lugares
CREATE TABLE IF NOT EXISTS `lugares` (
  `idlugares` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(255) NOT NULL,
  `Descripcion` varchar(500) NOT NULL,
  `Direccion` varchar(100) NOT NULL,
  `Contacto` bigint(20) NOT NULL DEFAULT 0,
  `Latitud` double NOT NULL,
  `Longitud` double NOT NULL,
  `Fotografias` longtext NOT NULL,
  `idTipoLugar` int(11) NOT NULL,
  `idRegiones` int(11) NOT NULL,
  PRIMARY KEY (`idlugares`),
  KEY `idTipoLugar` (`idTipoLugar`),
  KEY `idRegiones` (`idRegiones`),
  CONSTRAINT `lugares_ibfk_1` FOREIGN KEY (`idTipoLugar`) REFERENCES `tipolugares` (`idTipoLugar`),
  CONSTRAINT `lugares_ibfk_2` FOREIGN KEY (`idRegiones`) REFERENCES `regiones` (`idRegiones`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla reviewsdb.plataformadescarga
CREATE TABLE IF NOT EXISTS `plataformadescarga` (
  `idPlataformaDescarga` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) NOT NULL,
  PRIMARY KEY (`idPlataformaDescarga`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla reviewsdb.plataformaentreten
CREATE TABLE IF NOT EXISTS `plataformaentreten` (
  `idplataformaentreten` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) NOT NULL,
  PRIMARY KEY (`idplataformaentreten`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla reviewsdb.plataformas
CREATE TABLE IF NOT EXISTS `plataformas` (
  `idPlataforma` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) NOT NULL,
  PRIMARY KEY (`idPlataforma`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla reviewsdb.regiones
CREATE TABLE IF NOT EXISTS `regiones` (
  `idRegiones` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) NOT NULL,
  PRIMARY KEY (`idRegiones`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla reviewsdb.resenasentretenimiento
CREATE TABLE IF NOT EXISTS `resenasentretenimiento` (
  `idresenasentreten` int(11) NOT NULL AUTO_INCREMENT,
  `Calificacion` double NOT NULL,
  `textoresena` varchar(300) DEFAULT NULL,
  `Alias` varchar(15) NOT NULL,
  `identreten` int(11) NOT NULL,
  PRIMARY KEY (`idresenasentreten`),
  KEY `Alias` (`Alias`),
  KEY `identreten` (`identreten`),
  CONSTRAINT `resenasentretenimiento_ibfk_1` FOREIGN KEY (`Alias`) REFERENCES `usuarios` (`Alias`),
  CONSTRAINT `resenasentretenimiento_ibfk_2` FOREIGN KEY (`identreten`) REFERENCES `entretenimiento` (`identreten`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla reviewsdb.resenaslugares
CREATE TABLE IF NOT EXISTS `resenaslugares` (
  `idResenasLugares` int(11) NOT NULL AUTO_INCREMENT,
  `Calificacion` double NOT NULL,
  `textoresena` varchar(300) DEFAULT NULL,
  `Alias` varchar(15) NOT NULL,
  `idLugares` int(11) NOT NULL,
  PRIMARY KEY (`idResenasLugares`),
  KEY `Alias` (`Alias`),
  KEY `idLugares` (`idLugares`),
  CONSTRAINT `resenaslugares_ibfk_1` FOREIGN KEY (`Alias`) REFERENCES `usuarios` (`Alias`),
  CONSTRAINT `resenaslugares_ibfk_2` FOREIGN KEY (`idLugares`) REFERENCES `lugares` (`idlugares`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla reviewsdb.resenasvideogames
CREATE TABLE IF NOT EXISTS `resenasvideogames` (
  `idresenasvideogames` int(11) NOT NULL AUTO_INCREMENT,
  `Calificacion` double NOT NULL,
  `Alias` varchar(15) NOT NULL,
  `idgames` int(11) NOT NULL,
  `textoresena` varchar(300) DEFAULT NULL,
  PRIMARY KEY (`idresenasvideogames`),
  KEY `Alias` (`Alias`),
  KEY `idgames` (`idgames`),
  CONSTRAINT `resenasvideogames_ibfk_1` FOREIGN KEY (`Alias`) REFERENCES `usuarios` (`Alias`),
  CONSTRAINT `resenasvideogames_ibfk_2` FOREIGN KEY (`idgames`) REFERENCES `videogames` (`idgames`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla reviewsdb.tipoentreten
CREATE TABLE IF NOT EXISTS `tipoentreten` (
  `idtipoEntreten` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) NOT NULL,
  PRIMARY KEY (`idtipoEntreten`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla reviewsdb.tipolugares
CREATE TABLE IF NOT EXISTS `tipolugares` (
  `idTipoLugar` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) NOT NULL,
  PRIMARY KEY (`idTipoLugar`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla reviewsdb.usuarios
CREATE TABLE IF NOT EXISTS `usuarios` (
  `Alias` varchar(15) NOT NULL,
  `Nombre` varchar(25) NOT NULL,
  `Amaterno` varchar(30) NOT NULL,
  `Apaterno` varchar(30) NOT NULL,
  `Avatar` longtext NOT NULL,
  `Secreto` varchar(30) NOT NULL,
  `Correo` varchar(100) NOT NULL,
  PRIMARY KEY (`Alias`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- La exportación de datos fue deseleccionada.

-- Volcando estructura para tabla reviewsdb.videogames
CREATE TABLE IF NOT EXISTS `videogames` (
  `idgames` int(11) NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(100) DEFAULT NULL,
  `Descripcion` varchar(300) NOT NULL,
  `Fechadeestreno` date NOT NULL,
  `Poster` longtext NOT NULL,
  `idplataforma` int(11) NOT NULL,
  `idgenerovid` int(11) NOT NULL,
  `idplataformadescarga` int(11) NOT NULL,
  PRIMARY KEY (`idgames`),
  KEY `idplataforma` (`idplataforma`),
  KEY `idgenerovid` (`idgenerovid`),
  KEY `idplataformadescarga` (`idplataformadescarga`),
  CONSTRAINT `videogames_ibfk_1` FOREIGN KEY (`idplataforma`) REFERENCES `plataformas` (`idPlataforma`),
  CONSTRAINT `videogames_ibfk_2` FOREIGN KEY (`idgenerovid`) REFERENCES `generovideojuego` (`idGeneroVid`),
  CONSTRAINT `videogames_ibfk_3` FOREIGN KEY (`idplataformadescarga`) REFERENCES `plataformadescarga` (`idPlataformaDescarga`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

-- La exportación de datos fue deseleccionada.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
