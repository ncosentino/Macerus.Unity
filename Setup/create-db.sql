-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: localhost    Database: macerus
-- ------------------------------------------------------
-- Server version	8.0.19

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `affix_types`
--

DROP TABLE IF EXISTS `affix_types`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `affix_types` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` text NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `affix_types`
--

LOCK TABLES `affix_types` WRITE;
/*!40000 ALTER TABLE `affix_types` DISABLE KEYS */;
INSERT INTO `affix_types` VALUES (1,'normal'),(2,'magic'),(3,'rare'),(4,'imbued'),(5,'unique'),(6,'legendary'),(7,'relic');
/*!40000 ALTER TABLE `affix_types` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `enchantment_definitions`
--

DROP TABLE IF EXISTS `enchantment_definitions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `enchantment_definitions` (
  `id` int NOT NULL AUTO_INCREMENT,
  `serialized` text NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `enchantment_definitions`
--

LOCK TABLES `enchantment_definitions` WRITE;
/*!40000 ALTER TABLE `enchantment_definitions` DISABLE KEYS */;
/*!40000 ALTER TABLE `enchantment_definitions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `magic_affixes`
--

DROP TABLE IF EXISTS `magic_affixes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `magic_affixes` (
  `id` int NOT NULL AUTO_INCREMENT,
  `value` text NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `magic_affixes`
--

LOCK TABLES `magic_affixes` WRITE;
/*!40000 ALTER TABLE `magic_affixes` DISABLE KEYS */;
INSERT INTO `magic_affixes` VALUES (1,'Lively'),(2,'Hearty'),(3,'Magic'),(4,'of Life'),(5,'of Heartiness'),(6,'of Magic');
/*!40000 ALTER TABLE `magic_affixes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stat_definition_bounds`
--

DROP TABLE IF EXISTS `stat_definition_bounds`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stat_definition_bounds` (
  `id` int NOT NULL AUTO_INCREMENT,
  `stat_definition_id` int NOT NULL,
  `minimum_expression` text,
  `maximum_expression` text,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stat_definition_bounds`
--

LOCK TABLES `stat_definition_bounds` WRITE;
/*!40000 ALTER TABLE `stat_definition_bounds` DISABLE KEYS */;
INSERT INTO `stat_definition_bounds` VALUES (1,1,'0',NULL),(2,2,'0','LIFE_MAXIMUM'),(4,3,'0',NULL),(5,4,'0','MANA_MAXIMUM');
/*!40000 ALTER TABLE `stat_definition_bounds` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stat_definitions`
--

DROP TABLE IF EXISTS `stat_definitions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `stat_definitions` (
  `id` int NOT NULL AUTO_INCREMENT,
  `term` text NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stat_definitions`
--

LOCK TABLES `stat_definitions` WRITE;
/*!40000 ALTER TABLE `stat_definitions` DISABLE KEYS */;
INSERT INTO `stat_definitions` VALUES (1,'LIFE_MAXIMUM'),(2,'LIFE_CURRENT'),(3,'MANA_MAXIMUM'),(4,'MANA_CURRENT'),(5,'LIGHT_RADIUS_RADIUS'),(6,'LIGHT_RADIUS_INTENSITY'),(7,'LIGHT_RADIUS_RED'),(8,'LIGHT_RADIUS_GREEN'),(9,'LIGHT_RADIUS_BLUE');
/*!40000 ALTER TABLE `stat_definitions` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-04-11  9:24:19
