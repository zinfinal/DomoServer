/*
Navicat MySQL Data Transfer

Source Server         : MySQL
Source Server Version : 100113
Source Host           : localhost:3306
Source Database       : upthink

Target Server Type    : MYSQL
Target Server Version : 100113
File Encoding         : 65001

Date: 2016-09-24 17:35:34
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for auditorias
-- ----------------------------
DROP TABLE IF EXISTS `auditorias`;
CREATE TABLE `auditorias` (
  `id_auditoria` int(11) NOT NULL AUTO_INCREMENT,
  `ip` varchar(80) NOT NULL,
  `host` varchar(50) NOT NULL,
  `tabla` varchar(80) NOT NULL,
  `accion` tinyint(1) NOT NULL,
  `descripcion` text NOT NULL,
  `fecha_creado` datetime NOT NULL,
  PRIMARY KEY (`id_auditoria`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of auditorias
-- ----------------------------
INSERT INTO `auditorias` VALUES ('1', 'localhost', 'HP', 'aniolectivos', '2', 'Los registros: ID: 2 Nombre: 2014 Fecha inico: 2014-01-19 Fecha fin: 2014-12-18 Estado: 1 Se editaron por: ID: 2 Nombre: 2222 Fecha inico: 2014-01-19 Fecha fin: 2014-12-18 Estado: 1', '2015-02-01 21:57:11');

-- ----------------------------
-- Table structure for uptACCtAccesos
-- ----------------------------
DROP TABLE IF EXISTS `uptACCtAccesos`;
CREATE TABLE `uptACCtAccesos` (
  `ACCid` int(11) NOT NULL AUTO_INCREMENT,
  `USUid` int(11) NOT NULL,
  `ACCnombre_pc` varchar(80) NOT NULL,
  `ACCip` varchar(20) NOT NULL,
  `ACCnumero_intento` tinyint(1) NOT NULL,
  `ACCestado` tinyint(1) NOT NULL,
  `ACCfecha_creado` datetime NOT NULL,
  PRIMARY KEY (`ACCid`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of uptACCtAccesos
-- ----------------------------
INSERT INTO `uptACCtAccesos` VALUES ('1', '1', 'Harold-PC', '192.168.1.34', '0', '1', '2015-04-08 23:07:57');

-- ----------------------------
-- Table structure for uptDIStDispositivo
-- ----------------------------
DROP TABLE IF EXISTS `uptDIStDispositivo`;
CREATE TABLE `uptDIStDispositivo` (
  `DISid` int(11) NOT NULL AUTO_INCREMENT,
  `ESPid` int(11) NOT NULL,
  `TDIid` int(11) NOT NULL,
  `DISnombre` varchar(80) NOT NULL,
  `DISmodo` tinyint(7) NOT NULL,
  `DISpin` smallint(255) DEFAULT NULL,
  `DISestado` tinyint(1) NOT NULL,
  PRIMARY KEY (`DISid`),
  KEY `ESPid` (`ESPid`),
  KEY `TDIid` (`TDIid`),
  CONSTRAINT `uptdistdispositivo_ibfk_1` FOREIGN KEY (`ESPid`) REFERENCES `uptESPpEspacio` (`ESPid`),
  CONSTRAINT `uptdistdispositivo_ibfk_2` FOREIGN KEY (`TDIid`) REFERENCES `uptTDIpTipoDispositivo` (`TDIid`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of uptDIStDispositivo
-- ----------------------------
INSERT INTO `uptDIStDispositivo` VALUES ('1', '1', '1', 'Foco 1', '0', '1', '1');
INSERT INTO `uptDIStDispositivo` VALUES ('2', '1', '1', 'Foco 2', '0', '2', '1');
INSERT INTO `uptDIStDispositivo` VALUES ('4', '1', '1', 'Foco 3', '0', '3', '1');
INSERT INTO `uptDIStDispositivo` VALUES ('5', '1', '1', 'Foco 4', '0', '4', '1');
INSERT INTO `uptDIStDispositivo` VALUES ('7', '1', '4', 'Puerta Principal', '0', '6', '1');
INSERT INTO `uptDIStDispositivo` VALUES ('9', '1', '4', 'Puerta Secundaria', '0', '7', '1');

-- ----------------------------
-- Table structure for uptESPpEspacio
-- ----------------------------
DROP TABLE IF EXISTS `uptESPpEspacio`;
CREATE TABLE `uptESPpEspacio` (
  `ESPid` int(11) NOT NULL AUTO_INCREMENT,
  `TESid` int(11) NOT NULL,
  `ESPnombre` varchar(80) NOT NULL,
  `ESPdecripcion` varchar(255) NOT NULL,
  `ESPip` varchar(15) NOT NULL,
  `ESPpuerto` smallint(6) NOT NULL,
  `ESPestado` tinyint(1) NOT NULL,
  PRIMARY KEY (`ESPid`),
  KEY `TESid` (`TESid`),
  CONSTRAINT `uptesppespacio_ibfk_1` FOREIGN KEY (`TESid`) REFERENCES `uptTESpTipoEspacio` (`TESid`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of uptESPpEspacio
-- ----------------------------
INSERT INTO `uptESPpEspacio` VALUES ('1', '1', 'C-101', 'Aula de clases de epis.', '192.168.0.75', '7070', '1');
INSERT INTO `uptESPpEspacio` VALUES ('2', '1', 'C-102', 'Aula de clases de epis.', '192.168.0.76', '7070', '1');
INSERT INTO `uptESPpEspacio` VALUES ('3', '1', 'C-103DSFDSF', 'SDFSDF', '', '0', '0');

-- ----------------------------
-- Table structure for uptNIVpNivel
-- ----------------------------
DROP TABLE IF EXISTS `uptNIVpNivel`;
CREATE TABLE `uptNIVpNivel` (
  `NIVid` int(11) NOT NULL AUTO_INCREMENT,
  `NIVnombre` varchar(80) NOT NULL,
  PRIMARY KEY (`NIVid`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of uptNIVpNivel
-- ----------------------------
INSERT INTO `uptNIVpNivel` VALUES ('1', 'Administrador');
INSERT INTO `uptNIVpNivel` VALUES ('2', 'Docente');

-- ----------------------------
-- Table structure for uptPACtPermisoAcciones
-- ----------------------------
DROP TABLE IF EXISTS `uptPACtPermisoAcciones`;
CREATE TABLE `uptPACtPermisoAcciones` (
  `PACid` int(11) NOT NULL AUTO_INCREMENT,
  `TDIid` int(11) NOT NULL,
  `PACaccion` varchar(80) NOT NULL,
  PRIMARY KEY (`PACid`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of uptPACtPermisoAcciones
-- ----------------------------
INSERT INTO `uptPACtPermisoAcciones` VALUES ('1', '1', 'encender');
INSERT INTO `uptPACtPermisoAcciones` VALUES ('2', '1', 'apagar');
INSERT INTO `uptPACtPermisoAcciones` VALUES ('3', '1', 'mostrar');
INSERT INTO `uptPACtPermisoAcciones` VALUES ('4', '2', 'encender');
INSERT INTO `uptPACtPermisoAcciones` VALUES ('5', '2', 'apagar');
INSERT INTO `uptPACtPermisoAcciones` VALUES ('6', '2', 'mostrar');
INSERT INTO `uptPACtPermisoAcciones` VALUES ('7', '3', 'abrir');
INSERT INTO `uptPACtPermisoAcciones` VALUES ('8', '3', 'cerrar');
INSERT INTO `uptPACtPermisoAcciones` VALUES ('9', '3', 'mostrar');
INSERT INTO `uptPACtPermisoAcciones` VALUES ('10', '4', 'abrir');
INSERT INTO `uptPACtPermisoAcciones` VALUES ('11', '4', 'ver_estado');
INSERT INTO `uptPACtPermisoAcciones` VALUES ('12', '4', 'mostrar');
INSERT INTO `uptPACtPermisoAcciones` VALUES ('13', '5', 'encender');
INSERT INTO `uptPACtPermisoAcciones` VALUES ('14', '5', 'apagar');
INSERT INTO `uptPACtPermisoAcciones` VALUES ('15', '5', 'mostrar');
INSERT INTO `uptPACtPermisoAcciones` VALUES ('16', '6', 'encender');
INSERT INTO `uptPACtPermisoAcciones` VALUES ('17', '6', 'apagar');
INSERT INTO `uptPACtPermisoAcciones` VALUES ('18', '6', 'transmitir');
INSERT INTO `uptPACtPermisoAcciones` VALUES ('19', '6', 'mostrar');
INSERT INTO `uptPACtPermisoAcciones` VALUES ('20', '7', 'ver_temperatura');

-- ----------------------------
-- Table structure for uptPERtPermiso
-- ----------------------------
DROP TABLE IF EXISTS `uptPERtPermiso`;
CREATE TABLE `uptPERtPermiso` (
  `PERid` int(11) NOT NULL AUTO_INCREMENT,
  `NIVid` int(11) NOT NULL,
  `DISid` int(11) DEFAULT NULL,
  `PACid` int(11) NOT NULL,
  `PERvalor` tinyint(1) NOT NULL,
  PRIMARY KEY (`PERid`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of uptPERtPermiso
-- ----------------------------
INSERT INTO `uptPERtPermiso` VALUES ('1', '1', '1', '1', '1');
INSERT INTO `uptPERtPermiso` VALUES ('2', '1', '1', '2', '0');
INSERT INTO `uptPERtPermiso` VALUES ('3', '1', '1', '3', '0');
INSERT INTO `uptPERtPermiso` VALUES ('4', '1', '2', '1', '1');
INSERT INTO `uptPERtPermiso` VALUES ('5', '1', '2', '2', '0');
INSERT INTO `uptPERtPermiso` VALUES ('6', '1', '2', '3', '0');
INSERT INTO `uptPERtPermiso` VALUES ('7', '1', '3', '1', '0');
INSERT INTO `uptPERtPermiso` VALUES ('8', '1', '3', '2', '1');
INSERT INTO `uptPERtPermiso` VALUES ('9', '1', '3', '3', '1');
INSERT INTO `uptPERtPermiso` VALUES ('10', '1', '4', '1', '1');
INSERT INTO `uptPERtPermiso` VALUES ('11', '1', '4', '2', '1');
INSERT INTO `uptPERtPermiso` VALUES ('12', '1', '4', '3', '0');
INSERT INTO `uptPERtPermiso` VALUES ('13', '2', '1', '1', '0');
INSERT INTO `uptPERtPermiso` VALUES ('14', '2', '1', '2', '0');
INSERT INTO `uptPERtPermiso` VALUES ('15', '2', '1', '3', '1');
INSERT INTO `uptPERtPermiso` VALUES ('16', '2', '2', '1', '1');
INSERT INTO `uptPERtPermiso` VALUES ('17', '2', '2', '2', '0');
INSERT INTO `uptPERtPermiso` VALUES ('18', '2', '2', '3', '1');
INSERT INTO `uptPERtPermiso` VALUES ('19', '2', '3', '1', '1');
INSERT INTO `uptPERtPermiso` VALUES ('20', '2', '3', '2', '1');
INSERT INTO `uptPERtPermiso` VALUES ('21', '2', '3', '3', '1');

-- ----------------------------
-- Table structure for uptSEStSessiones
-- ----------------------------
DROP TABLE IF EXISTS `uptSEStSessiones`;
CREATE TABLE `uptSEStSessiones` (
  `SESid` int(11) NOT NULL,
  `SESlast_update` int(11) NOT NULL,
  `SESdata` text NOT NULL,
  PRIMARY KEY (`SESid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of uptSEStSessiones
-- ----------------------------

-- ----------------------------
-- Table structure for uptTARpTarea
-- ----------------------------
DROP TABLE IF EXISTS `uptTARpTarea`;
CREATE TABLE `uptTARpTarea` (
  `TARid` int(11) NOT NULL AUTO_INCREMENT,
  `USUid` int(11) NOT NULL,
  `TARnombre` varchar(50) NOT NULL,
  `TARfecha_inicio` datetime NOT NULL DEFAULT '0000-00-00 00:00:00' ON UPDATE CURRENT_TIMESTAMP,
  `TARfecha_final` datetime NOT NULL DEFAULT '0000-00-00 00:00:00' ON UPDATE CURRENT_TIMESTAMP,
  `TARestado` tinyint(1) NOT NULL,
  `TARfecha_creado` datetime NOT NULL DEFAULT '0000-00-00 00:00:00' ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`TARid`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of uptTARpTarea
-- ----------------------------

-- ----------------------------
-- Table structure for uptTDIpTipoDispositivo
-- ----------------------------
DROP TABLE IF EXISTS `uptTDIpTipoDispositivo`;
CREATE TABLE `uptTDIpTipoDispositivo` (
  `TDIid` int(11) NOT NULL AUTO_INCREMENT,
  `TDInombre` varchar(80) NOT NULL,
  `TDIimagen` varchar(80) NOT NULL,
  PRIMARY KEY (`TDIid`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of uptTDIpTipoDispositivo
-- ----------------------------
INSERT INTO `uptTDIpTipoDispositivo` VALUES ('1', 'Foco', 'foco.png');
INSERT INTO `uptTDIpTipoDispositivo` VALUES ('2', 'Ventilador', 'ventilador');
INSERT INTO `uptTDIpTipoDispositivo` VALUES ('3', 'Cortina', 'cortina.png');
INSERT INTO `uptTDIpTipoDispositivo` VALUES ('4', 'Puerta', 'puerta.png');
INSERT INTO `uptTDIpTipoDispositivo` VALUES ('5', 'Proyector', 'proyecto.png');
INSERT INTO `uptTDIpTipoDispositivo` VALUES ('6', 'Camara de Video', 'camara-de-video.png');
INSERT INTO `uptTDIpTipoDispositivo` VALUES ('7', 'Temperatura', 'temperatura.png');
INSERT INTO `uptTDIpTipoDispositivo` VALUES ('8', 'Sensor de Movimiento', 'sensor-de-movimiento.png');

-- ----------------------------
-- Table structure for uptTESpTipoEspacio
-- ----------------------------
DROP TABLE IF EXISTS `uptTESpTipoEspacio`;
CREATE TABLE `uptTESpTipoEspacio` (
  `TESid` int(11) NOT NULL AUTO_INCREMENT,
  `TESnombre` varchar(80) NOT NULL,
  `TESimagen` varchar(80) NOT NULL,
  PRIMARY KEY (`TESid`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of uptTESpTipoEspacio
-- ----------------------------
INSERT INTO `uptTESpTipoEspacio` VALUES ('1', 'Aula', 'aula.png');
INSERT INTO `uptTESpTipoEspacio` VALUES ('2', 'Laboratorio', 'laboratorio.png');

-- ----------------------------
-- Table structure for uptUSUpUsuario
-- ----------------------------
DROP TABLE IF EXISTS `uptUSUpUsuario`;
CREATE TABLE `uptUSUpUsuario` (
  `USUid` int(11) NOT NULL AUTO_INCREMENT,
  `NIVid` int(11) NOT NULL,
  `USUusuario` varchar(25) NOT NULL,
  `USUpassword` varchar(80) NOT NULL,
  `USUimagen` varchar(255) NOT NULL,
  `USUemail` varchar(80) NOT NULL,
  `USUubicacion` int(11) NOT NULL,
  `USUonline` tinyint(1) NOT NULL,
  `USUestado` tinyint(1) NOT NULL,
  `USUfecha_creado` datetime NOT NULL DEFAULT '0000-00-00 00:00:00' ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`USUid`),
  KEY `NIVid` (`NIVid`)
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of uptUSUpUsuario
-- ----------------------------
INSERT INTO `uptUSUpUsuario` VALUES ('1', '1', 'admin', '123', '', 'admin@localhost', '754975871', '0', '1', '2016-09-24 15:51:53');
INSERT INTO `uptUSUpUsuario` VALUES ('2', '1', 'cesar', '123', '', 'acero@hotmail.com', '0', '0', '1', '2016-06-02 09:32:01');
INSERT INTO `uptUSUpUsuario` VALUES ('3', '2', 'elanchipa', '123456', '', 'elanchipa@hotmail.com', '0', '0', '1', '2016-08-03 21:33:23');
INSERT INTO `uptUSUpUsuario` VALUES ('4', '1', 'zinfinal', '123', '', 'zinfinal@hotmail.com', '0', '0', '1', '2016-06-09 14:26:48');
INSERT INTO `uptUSUpUsuario` VALUES ('5', '1', '2010036339', '123456', '', 'hpacha@hotmail.com', '0', '0', '1', '2016-09-24 12:34:31');
INSERT INTO `uptUSUpUsuario` VALUES ('6', '1', 'pc1', '123', '', '', '0', '0', '1', '2016-08-27 18:03:52');
INSERT INTO `uptUSUpUsuario` VALUES ('7', '1', 'pc2', '123', '', '', '0', '0', '1', '2016-08-27 15:57:05');
INSERT INTO `uptUSUpUsuario` VALUES ('8', '1', 'pc3', '123', '', '', '0', '0', '1', '2016-08-27 18:03:52');
INSERT INTO `uptUSUpUsuario` VALUES ('9', '1', 'pc4', '123', '', '', '0', '0', '1', '2016-08-27 18:03:52');
INSERT INTO `uptUSUpUsuario` VALUES ('10', '1', 'pc5', '123', '', '', '0', '0', '1', '2016-08-27 18:03:52');
INSERT INTO `uptUSUpUsuario` VALUES ('11', '1', 'pc6', '123', '', '', '0', '0', '1', '2016-08-27 15:44:22');
INSERT INTO `uptUSUpUsuario` VALUES ('12', '1', 'pc7', '123', '', '', '0', '0', '1', '2016-08-27 15:44:32');
INSERT INTO `uptUSUpUsuario` VALUES ('13', '1', 'pc8', '123', '', '', '0', '0', '1', '2016-08-27 15:44:36');
INSERT INTO `uptUSUpUsuario` VALUES ('14', '1', 'pc9', '123', '', '', '0', '0', '1', '2016-08-27 14:02:39');
INSERT INTO `uptUSUpUsuario` VALUES ('15', '1', 'pc10', '123', '', '', '0', '0', '1', '2016-08-27 14:02:40');
INSERT INTO `uptUSUpUsuario` VALUES ('16', '1', 'pc11', '123', '', '', '0', '0', '1', '2016-08-27 14:02:40');
INSERT INTO `uptUSUpUsuario` VALUES ('17', '1', 'pc12', '123', '', '', '0', '0', '1', '2016-08-27 14:02:41');
INSERT INTO `uptUSUpUsuario` VALUES ('18', '1', 'pc13', '123', '', '', '0', '0', '1', '2016-08-27 14:02:41');
INSERT INTO `uptUSUpUsuario` VALUES ('19', '1', 'pc14', '123', '', '', '0', '0', '1', '2016-08-27 14:02:41');
INSERT INTO `uptUSUpUsuario` VALUES ('20', '1', 'pc15', '123', '', '', '0', '0', '1', '2016-08-27 15:46:30');
INSERT INTO `uptUSUpUsuario` VALUES ('21', '1', 'pc16', '123', '', '', '0', '0', '1', '2016-08-27 15:46:36');
INSERT INTO `uptUSUpUsuario` VALUES ('22', '1', 'pc17', '123', '', '', '0', '0', '1', '2016-08-27 15:57:43');
INSERT INTO `uptUSUpUsuario` VALUES ('23', '1', 'pc18', '123', '', '', '0', '0', '1', '2016-08-27 15:45:50');
INSERT INTO `uptUSUpUsuario` VALUES ('24', '1', 'pc19', '123', '', '', '0', '0', '1', '2016-08-27 15:46:01');
INSERT INTO `uptUSUpUsuario` VALUES ('25', '1', 'pc20', '123', '', '', '0', '0', '1', '2016-08-27 15:46:13');
INSERT INTO `uptUSUpUsuario` VALUES ('26', '1', 'pc21', '123', '', '', '0', '0', '1', '2016-08-27 15:45:08');
INSERT INTO `uptUSUpUsuario` VALUES ('27', '1', 'pc22', '123', '', '', '0', '0', '1', '2016-08-27 15:45:19');
INSERT INTO `uptUSUpUsuario` VALUES ('28', '1', 'pc23', '123', '', '', '0', '0', '1', '2016-08-27 15:45:27');
INSERT INTO `uptUSUpUsuario` VALUES ('29', '1', 'pc24', '123', '', '', '0', '0', '1', '2016-08-27 15:42:33');
INSERT INTO `uptUSUpUsuario` VALUES ('30', '1', 'pc25', '123', '', '', '0', '0', '1', '2016-08-27 15:42:45');
INSERT INTO `uptUSUpUsuario` VALUES ('31', '1', 'pc26', '123', '', '', '0', '0', '1', '2016-08-27 15:42:52');
INSERT INTO `uptUSUpUsuario` VALUES ('32', '1', 'pc27', '123', '', '', '0', '0', '1', '2016-08-27 15:42:06');
INSERT INTO `uptUSUpUsuario` VALUES ('33', '1', 'pc28', '123', '', '', '0', '0', '1', '2016-08-27 15:42:14');
INSERT INTO `uptUSUpUsuario` VALUES ('34', '1', 'pc29', '123', '', '', '0', '0', '1', '2016-08-27 15:41:57');
INSERT INTO `uptUSUpUsuario` VALUES ('35', '1', 'pc30', '123', '', '', '0', '0', '1', '2016-08-27 14:04:09');
