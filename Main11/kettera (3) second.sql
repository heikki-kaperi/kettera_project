-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 08, 2025 at 06:59 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `kettera`
--

-- --------------------------------------------------------

--
-- Table structure for table `administrator`
--

CREATE TABLE `administrator` (
  `Administrator_ID` int(6) UNSIGNED NOT NULL,
  `First_name` varchar(255) DEFAULT NULL,
  `Last_name` varchar(255) DEFAULT NULL,
  `Username` varchar(50) DEFAULT NULL,
  `Password` varchar(255) DEFAULT NULL,
  `Email` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `administrator`
--

INSERT INTO `administrator` (`Administrator_ID`, `First_name`, `Last_name`, `Username`, `Password`, `Email`) VALUES
(1, 'Pekka', 'Honkanen', 'Ferrari1', '1234', 'pekka.honkanen@sopimus.fi'),
(2, 'Jani', 'Vuorinen', 'Ferrari11', '1234', 'jani.vuorinen@hotmail.com'),
(3, 'Niina', 'Kosonen', 'Ferrari111', '1234', 'niina.kosonen@gmail.com'),
(4, 'Nitesh', 'Sledger', 'Ferrari1111', '1234', 'nitesh.sledger@gmail.com'),
(5, 'Esko', 'Ojala', 'Ferrari11111', '1234', 'esko.ojala@sopimus.fi');

-- --------------------------------------------------------

--
-- Table structure for table `admin_deletion_votes`
--

CREATE TABLE `admin_deletion_votes` (
  `Vote_ID` int(10) UNSIGNED NOT NULL,
  `Target_Admin_ID` int(6) UNSIGNED NOT NULL,
  `Voting_Admin_ID` int(6) UNSIGNED NOT NULL,
  `Vote_date` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `block_cooccurrence`
--

CREATE TABLE `block_cooccurrence` (
  `Block_A_ID` int(10) UNSIGNED NOT NULL,
  `Block_B_ID` int(10) UNSIGNED NOT NULL,
  `Times_Used_Together` int(11) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `block_cooccurrence`
--

INSERT INTO `block_cooccurrence` (`Block_A_ID`, `Block_B_ID`, `Times_Used_Together`) VALUES
(1, 2, 5),
(1, 3, 4),
(2, 3, 8),
(2, 4, 3),
(3, 4, 6),
(4, 5, 4),
(5, 6, 5),
(10, 11, 3),
(11, 12, 2),
(12, 13, 4),
(13, 14, 2),
(15, 16, 1),
(15, 17, 1),
(15, 18, 1),
(16, 17, 1),
(16, 18, 1),
(17, 18, 1),
(19, 20, 1),
(19, 21, 1),
(19, 22, 1),
(19, 23, 1),
(20, 21, 1),
(20, 22, 1),
(20, 23, 1),
(21, 22, 1),
(21, 23, 1),
(22, 23, 1),
(24, 25, 1),
(24, 26, 1),
(24, 27, 1),
(25, 26, 1),
(25, 27, 1),
(26, 27, 1),
(28, 29, 1),
(28, 30, 1),
(28, 31, 1),
(29, 30, 1),
(29, 31, 1),
(30, 31, 1);

-- --------------------------------------------------------

--
-- Table structure for table `comments`
--

CREATE TABLE `comments` (
  `Comment_ID` int(10) UNSIGNED NOT NULL,
  `Contract_NR` int(10) UNSIGNED DEFAULT NULL,
  `Contract_Block_NR` int(10) UNSIGNED DEFAULT NULL,
  `User_ID` int(10) UNSIGNED DEFAULT NULL,
  `User_type` enum('internal','external') DEFAULT NULL,
  `Comment_text` text DEFAULT NULL,
  `Comment_date` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `comments`
--

INSERT INTO `comments` (`Comment_ID`, `Contract_NR`, `Contract_Block_NR`, `User_ID`, `User_type`, `Comment_text`, `Comment_date`) VALUES
(1, 1, 1, 1, 'internal', 'Please review the general terms section.', '2025-11-10 20:28:43'),
(9, 10, NULL, 1, 'internal', 'Testing comment layer.', '2025-11-10 23:52:42'),
(10, 11, NULL, 1, 'internal', 'Test comment from internal user.', '2025-11-11 00:06:30'),
(11, 1, NULL, 1, '', 'External', '2025-11-20 10:59:37'),
(12, 1, NULL, 1, 'external', 'test', '2025-12-07 20:24:38'),
(13, 1, NULL, 1, 'external', 'teeeest', '2025-12-07 20:25:31'),
(14, 14, 9, 2, 'internal', 'Yleiset ehdot näyttävät hyvältä, mutta tarkista vielä irtisanomisehdot.', '2025-12-01 10:30:00'),
(15, 14, 10, 2, 'external', 'Maksuehdot hyväksyttävissä, mutta toivomme 21 päivän maksuaikaa.', '2025-12-01 11:00:00'),
(16, 15, 11, 3, 'internal', 'Salassapitolauseke on liian laaja, täsmennetään vielä.', '2025-12-02 11:30:00'),
(17, 15, NULL, 3, 'external', 'Sopimus on kokonaisuutena hyvä ja voimme hyväksyä sen.', '2025-12-02 14:00:00'),
(18, 16, 13, 4, 'internal', 'Vastuunrajoituslauseke tarvitsee lakimiehen tarkistuksen.', '2025-12-03 12:30:00'),
(19, 17, 14, 5, 'internal', 'Riitojenratkaisulauseke hyväksytty, voidaan lähettää asiakkaalle.', '2025-12-04 15:00:00'),
(20, 18, NULL, 2, 'internal', 'Luonnos valmis, odottaa hyväksyntää.', '2025-12-05 16:00:00');

-- --------------------------------------------------------

--
-- Table structure for table `contract`
--

CREATE TABLE `contract` (
  `Contract_NR` int(10) UNSIGNED NOT NULL,
  `Company_name` varchar(200) DEFAULT NULL,
  `The_Creator` int(6) UNSIGNED DEFAULT NULL,
  `Created_date` datetime DEFAULT NULL,
  `Approved` tinyint(1) DEFAULT NULL,
  `Sent_to_external` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `contract`
--

INSERT INTO `contract` (`Contract_NR`, `Company_name`, `The_Creator`, `Created_date`, `Approved`, `Sent_to_external`) VALUES
(1, 'Northern Solutions Oy', 1, '2025-11-10 20:26:39', 1, 0),
(2, 'Arttu Oy', 1, '2025-11-15 23:52:41', 0, 0),
(3, 'Olli Oy', 1, '2025-10-11 00:06:30', 0, 0),
(4, 'Sofia Oy', 1, '2025-11-05 23:52:41', 0, 0),
(5, 'Kerkko Oy', 1, '2025-11-03 00:06:30', 0, 0),
(6, 'Heikki Oy', 1, '2025-11-04 23:52:41', 0, 0),
(7, 'Unelmat Company Oy', 1, '2025-11-16 00:06:30', 0, 0),
(8, 'Ranskis Company Oy', 1, '2025-09-10 23:52:41', 0, 0),
(9, 'Toiveet Oy', 1, '2025-02-13 00:06:30', 0, 0),
(10, 'Demo Company Oy', 1, '2025-11-23 23:52:41', 0, 0),
(11, 'Test Company Oy', 1, '2025-11-13 00:06:30', 0, 0),
(12, 'Joo', 1, '2025-11-26 16:03:54', 0, 0),
(14, 'TechNova Oy', 2, '2025-12-01 09:15:00', 0, 0),
(15, 'BuildPro Solutions', 3, '2025-12-02 10:30:00', 1, 1),
(16, 'Nordic Trade Oy', 4, '2025-12-03 11:45:00', 0, 0),
(17, 'Green Energy Finland', 5, '2025-12-04 13:20:00', 1, 0),
(18, 'Digital Services Oy', 2, '2025-12-05 14:00:00', 0, 0);

-- --------------------------------------------------------

--
-- Table structure for table `contract_block`
--

CREATE TABLE `contract_block` (
  `Contract_Block_NR` int(10) UNSIGNED NOT NULL,
  `Org_Cont_ID` int(10) UNSIGNED DEFAULT NULL,
  `Contract_text` text DEFAULT NULL,
  `New` tinyint(1) DEFAULT NULL,
  `Modified_date` datetime DEFAULT NULL,
  `Type` int(11) DEFAULT 0,
  `MediaContent` longblob DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `contract_block`
--

INSERT INTO `contract_block` (`Contract_Block_NR`, `Org_Cont_ID`, `Contract_text`, `New`, `Modified_date`, `Type`, `MediaContent`) VALUES
(1, 1, 'This is the current contract block version of the general terms.', 1, '2025-11-10 20:26:27', 0, NULL),
(2, 1, 'This block defines the general terms of cooperation between the parties.', 0, '2025-11-11 11:13:09', 0, NULL),
(3, 2, 'Payment shall be made within 30 days of invoice receipt.', 0, '2025-11-11 11:13:09', 0, NULL),
(4, 3, 'Both parties agree to keep all shared information strictly confidential.', 0, '2025-11-11 11:13:09', 0, NULL),
(5, 4, 'Either party may terminate this agreement with 30 days’ notice.', 0, '2025-11-11 11:13:09', 0, NULL),
(6, 5, 'Neither party shall be liable for indirect or consequential damages.', 0, '2025-11-11 11:13:09', 0, NULL),
(7, 6, 'Any disputes shall be resolved through arbitration in Helsinki, Finland.', 0, '2025-11-11 11:13:09', 0, NULL),
(8, 5, 'Aenean mattis suscipit ligula, sed tempor mi aliquam id. Etiam sed ligula a justo bibendum tempus.', 1, '2025-11-26 16:32:57', 0, NULL),
(9, 10, 'Tämä sopimus määrittelee osapuolten välisen yhteistyön yleiset ehdot. Sopimusta sovelletaan kaikkiin osapuolten välisiin liiketoimiin.', 0, '2025-12-01 09:15:00', 0, NULL),
(10, 11, 'Maksu suoritetaan 14 päivän kuluessa laskun päivämäärästä. Viivästyskorko on 8% vuodessa.', 1, '2025-12-01 09:20:00', 0, NULL),
(11, 12, 'Osapuolet sitoutuvat pitämään kaikki sopimukseen liittyvät tiedot luottamuksellisina.', 0, '2025-12-02 10:30:00', 0, NULL),
(12, 13, 'Kumpikin osapuoli voi irtisanoa sopimuksen kolmen kuukauden irtisanomisajalla kirjallisesti.', 0, '2025-12-03 11:45:00', 0, NULL),
(13, 14, 'Korvausvastuu rajoittuu välittömiin vahinkoihin enintään sopimuksen vuosittaiseen arvoon.', 0, '2025-12-04 13:20:00', 0, NULL),
(14, 15, 'Erimielisyydet ratkaistaan Keskuskauppakamarin välityslautakunnassa Helsingissä.', 0, '2025-12-05 14:00:00', 0, NULL),
(15, 1, 'This is the original text for the general terms section.', 1, '2025-12-08 19:53:53', 0, NULL),
(16, 11, 'Maksu suoritetaan 14 päivän kuluessa laskun päivämäärästä. Viivästyskorko on 8% vuodessa. Maksuviivästyksestä voidaan periä muistutus- ja perintäkulut.', 1, '2025-12-08 19:53:59', 0, NULL),
(17, 12, 'Osapuolet sitoutuvat pitämään kaikki sopimukseen liittyvät tiedot ja liikesalaisuudet ehdottoman luottamuksellisina. Salassapitovelvollisuus jatkuu viisi vuotta sopimuksen päättymisen jälkeen.', 1, '2025-12-08 19:54:03', 0, NULL),
(18, 13, 'Kumpikin osapuoli voi irtisanoa sopimuksen kolmen kuukauden irtisanomisajalla. Irtisanominen on tehtävä kirjallisesti. Välittömään irtisanomiseen on oikeus olennaisessa sopimusrikkomuksessa.', 1, '2025-12-08 19:54:06', 0, NULL),
(19, 11, 'Maksu suoritetaan 14 päivän kuluessa laskun päivämäärästä. Viivästyskorko on 8% vuodessa. Maksuviivästyksestä voidaan periä muistutus- ja perintäkulut.', 1, '2025-12-08 19:54:16', 0, NULL),
(20, 12, 'Osapuolet sitoutuvat pitämään kaikki sopimukseen liittyvät tiedot ja liikesalaisuudet ehdottoman luottamuksellisina. Salassapitovelvollisuus jatkuu viisi vuotta sopimuksen päättymisen jälkeen.', 1, '2025-12-08 19:54:21', 0, NULL),
(21, 14, 'Kummankaan osapuolen korvausvastuu rajoittuu välittömiin vahinkoihin, enintään sopimuksen vuosittaiseen arvoon. Vastuurajoitus ei koske tahallista tai törkeää tuottamuksellista toimintaa.', 1, '2025-12-08 19:54:25', 0, NULL),
(22, 15, 'Sopimusta koskevat erimielisyydet pyritään ratkaisemaan ensisijaisesti osapuolten välisin neuvotteluin. Mikäli sovintoa ei saavuteta, riita ratkaistaan välimiesmenettelyssä Keskuskauppakamarin välityslautakunnan sääntöjen mukaisesti.', 1, '2025-12-08 19:54:28', 0, NULL),
(23, 16, 'Sopimus astuu voimaan allekirjoituspäivänä ja on voimassa toistaiseksi. Sopimukseen tehtävät muutokset on tehtävä kirjallisesti molempien osapuolten hyväksyminä.', 1, '2025-12-08 19:54:32', 0, NULL),
(24, 17, 'Laskutus tapahtuu kuukausittain jälkikäteen. Laskuun liitetään erittely suoritetuista töistä ja palveluista. Hinnat ovat arvonlisäverottomia, ellei toisin mainita.', 1, '2025-12-08 19:55:02', 0, NULL),
(25, 14, 'Kummankaan osapuolen korvausvastuu rajoittuu välittömiin vahinkoihin, enintään sopimuksen vuosittaiseen arvoon. Vastuurajoitus ei koske tahallista tai törkeää tuottamuksellista toimintaa.', 1, '2025-12-08 19:55:07', 0, NULL),
(26, 15, 'Sopimusta koskevat erimielisyydet pyritään ratkaisemaan ensisijaisesti osapuolten välisin neuvotteluin. Mikäli sovintoa ei saavuteta, riita ratkaistaan välimiesmenettelyssä Keskuskauppakamarin välityslautakunnan sääntöjen mukaisesti.', 1, '2025-12-08 19:55:10', 0, NULL),
(27, 10, 'Tämä sopimus määrittelee osapuolten välisen yhteistyön yleiset ehdot. Sopimusta sovelletaan kaikkiin osapuolten välisiin liiketoimiin.', 1, '2025-12-08 19:55:17', 0, NULL),
(28, 12, 'Osapuolet sitoutuvat pitämään kaikki sopimukseen liittyvät tiedot ja liikesalaisuudet ehdottoman luottamuksellisina. Salassapitovelvollisuus jatkuu viisi vuotta sopimuksen päättymisen jälkeen.', 1, '2025-12-08 19:55:23', 0, NULL),
(29, 13, 'Kumpikin osapuoli voi irtisanoa sopimuksen kolmen kuukauden irtisanomisajalla. Irtisanominen on tehtävä kirjallisesti. Välittömään irtisanomiseen on oikeus olennaisessa sopimusrikkomuksessa.', 1, '2025-12-08 19:55:27', 0, NULL),
(30, 16, 'Sopimus astuu voimaan allekirjoituspäivänä ja on voimassa toistaiseksi. Sopimukseen tehtävät muutokset on tehtävä kirjallisesti molempien osapuolten hyväksyminä.', 1, '2025-12-08 19:55:30', 0, NULL),
(31, 18, '[IMAGE: favicon4.jpg]', 1, '2025-12-08 19:57:19', 0, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `contract_blocks`
--

CREATE TABLE `contract_blocks` (
  `Contract_NR` int(10) UNSIGNED NOT NULL,
  `Contract_Block_NR` int(10) UNSIGNED NOT NULL,
  `Block_order` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `contract_blocks`
--

INSERT INTO `contract_blocks` (`Contract_NR`, `Contract_Block_NR`, `Block_order`) VALUES
(1, 1, 1),
(2, 15, 1),
(2, 16, 2),
(2, 17, 3),
(2, 18, 4),
(3, 24, 1),
(3, 25, 2),
(3, 26, 3),
(3, 27, 4),
(4, 28, 1),
(4, 29, 2),
(4, 30, 3),
(4, 31, 4),
(5, 19, 1),
(5, 20, 2),
(5, 21, 3),
(5, 22, 4),
(5, 23, 5),
(12, 8, 1),
(14, 9, 1),
(14, 10, 2),
(15, 11, 1),
(15, 12, 2),
(16, 13, 1),
(17, 14, 1),
(18, 9, 1),
(18, 14, 2);

-- --------------------------------------------------------

--
-- Table structure for table `contract_block_category`
--

CREATE TABLE `contract_block_category` (
  `Category_name` varchar(100) NOT NULL,
  `Description` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `contract_block_category`
--

INSERT INTO `contract_block_category` (`Category_name`, `Description`) VALUES
('Confidentiality', 'Specifies how both parties handle sensitive information.'),
('Dispute Resolution', 'Explains how conflicts are resolved, e.g., arbitration or mediation.'),
('General Terms', 'Contains general terms and conditions applicable to all contracts'),
('Liability', 'Outlines responsibilities and limitations of liability.'),
('Payment Terms', 'Defines payment conditions, invoicing schedule, and penalties.'),
('Termination', 'Details conditions for terminating the contract.'),
('Tietosuoja', 'Määrittelee henkilötietojen käsittelyn periaatteet ja GDPR-vaatimukset.');

-- --------------------------------------------------------

--
-- Table structure for table `contract_external_users`
--

CREATE TABLE `contract_external_users` (
  `Contract_NR` int(10) UNSIGNED NOT NULL,
  `Ext_User_ID` int(10) UNSIGNED NOT NULL,
  `Invited_date` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `contract_external_users`
--

INSERT INTO `contract_external_users` (`Contract_NR`, `Ext_User_ID`, `Invited_date`) VALUES
(1, 1, '2025-11-10 20:28:30'),
(14, 2, '2025-12-01 10:00:00'),
(15, 3, '2025-12-02 11:00:00'),
(16, 4, '2025-12-03 12:00:00'),
(17, 5, '2025-12-04 14:00:00'),
(18, 2, '2025-12-05 15:00:00');

-- --------------------------------------------------------

--
-- Table structure for table `contract_stakeholders`
--

CREATE TABLE `contract_stakeholders` (
  `Contract_NR` int(10) UNSIGNED NOT NULL,
  `Int_User_ID` int(6) UNSIGNED NOT NULL,
  `Has_approval_rights` tinyint(1) DEFAULT NULL,
  `Approved` tinyint(1) DEFAULT NULL,
  `Approved_date` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `contract_stakeholders`
--

INSERT INTO `contract_stakeholders` (`Contract_NR`, `Int_User_ID`, `Has_approval_rights`, `Approved`, `Approved_date`) VALUES
(1, 1, 1, 1, '2025-11-11 00:06:30'),
(14, 2, 1, 0, NULL),
(14, 3, 1, 0, NULL),
(15, 3, 1, 1, '2025-12-02 15:00:00'),
(15, 4, 0, NULL, NULL),
(16, 4, 1, 0, NULL),
(17, 5, 1, 1, '2025-12-04 16:30:00'),
(18, 2, 1, 0, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `external_user`
--

CREATE TABLE `external_user` (
  `Ext_User_ID` int(6) UNSIGNED NOT NULL,
  `First_name` varchar(255) DEFAULT NULL,
  `Last_name` varchar(255) DEFAULT NULL,
  `Company_name` varchar(200) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `Username` varchar(50) DEFAULT NULL,
  `Password` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `external_user`
--

INSERT INTO `external_user` (`Ext_User_ID`, `First_name`, `Last_name`, `Company_name`, `Email`, `Username`, `Password`) VALUES
(1, 'Mikko', 'Aho', 'Vuokra-apu Oy', 'mikko.aho@vuokrapu.fi', 'Ferrari3', '1234'),
(2, 'Petri', 'Laine', 'TechNova Oy', 'petri.laine@technova.fi', 'Ferrari33', '1234'),
(3, 'Kaisa', 'Järvinen', 'BuildPro Solutions', 'kaisa.jarvinen@buildpro.fi', 'Ferrari333', '1234'),
(4, 'Timo', 'Salo', 'Nordic Trade Oy', 'timo.salo@nordictrade.fi', 'Ferrari3333', '1234'),
(5, 'Heli', 'Rantanen', 'Green Energy Finland', 'heli.rantanen@greenenergy.fi', 'Ferrari33333', '1234');

-- --------------------------------------------------------

--
-- Table structure for table `internal_user`
--

CREATE TABLE `internal_user` (
  `Int_User_ID` int(6) UNSIGNED NOT NULL,
  `First_name` varchar(255) DEFAULT NULL,
  `Last_name` varchar(255) DEFAULT NULL,
  `Username` varchar(50) DEFAULT NULL,
  `Password` varchar(255) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `internal_user`
--

INSERT INTO `internal_user` (`Int_User_ID`, `First_name`, `Last_name`, `Username`, `Password`, `Email`) VALUES
(1, 'Milla', 'Tiihonen', 'Ferrari2', '1234', 'milla.tiihonen@gmail.com'),
(2, 'Antti', 'Virtanen', 'Ferrari22', '1234', 'antti.virtanen@sopimus.fi'),
(3, 'Sanna', 'Mäkinen', 'Ferrari222', '1234', 'sanna.makinen@sopimus.fi'),
(4, 'Jukka', 'Koskinen', 'Ferrari2222', '1234', 'jukka.koskinen@sopimus.fi'),
(5, 'Laura', 'Nieminen', 'Ferrari22222', '1234', 'laura.nieminen@sopimus.fi');

-- --------------------------------------------------------

--
-- Table structure for table `original_contract_block`
--

CREATE TABLE `original_contract_block` (
  `Org_Cont_ID` int(10) UNSIGNED NOT NULL,
  `Category_name` varchar(100) DEFAULT NULL,
  `Contract_text` text DEFAULT NULL,
  `Created_by` int(6) UNSIGNED DEFAULT NULL,
  `Created_date` datetime DEFAULT NULL,
  `Type` int(11) DEFAULT 0,
  `MediaContent` longblob DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `original_contract_block`
--

INSERT INTO `original_contract_block` (`Org_Cont_ID`, `Category_name`, `Contract_text`, `Created_by`, `Created_date`, `Type`, `MediaContent`) VALUES
(1, 'General Terms', 'This is the original text for the general terms section.', 1, '2025-11-10 20:26:15', 0, NULL),
(2, 'General Terms', 'Sed non risus. Suspendisse lectus tortor, dignissim sit amet, adipiscing nec, ultricies sed, dolor.', 1, '2025-11-11 11:12:30', 0, NULL),
(3, 'Payment Terms', 'Curabitur sit amet sem ut sapien varius volutpat. Maecenas egestas sapien sed lacus dignissim, vitae bibendum sapien egestas.', 1, '2025-11-11 11:12:30', 0, NULL),
(4, 'Confidentiality', 'Donec at nisi nec neque luctus ultrices. Curabitur consequat quam sed orci cursus, non tincidunt ex sagittis.', 1, '2025-11-11 11:12:30', 0, NULL),
(5, 'Termination', 'Aenean mattis suscipit ligula, sed tempor mi aliquam id. Etiam sed ligula a justo bibendum tempus.', 1, '2025-11-11 11:12:30', 0, NULL),
(6, 'Liability', 'In dictum ante at justo tempor, nec tincidunt ligula posuere. Mauris varius purus et est efficitur dapibus.', 1, '2025-11-11 11:12:30', 0, NULL),
(7, 'Dispute Resolution', 'Vivamus vehicula ex in magna sagittis, nec dignissim eros facilisis. Sed ac eros eget nulla sagittis sodales.', 1, '2025-11-11 11:12:30', 0, NULL),
(8, 'Liability', '', 1, '2025-11-26 16:05:18', 0, NULL),
(9, 'General Terms', 'This is the original text for the general terms section. (Copy)', 1, '2025-12-07 20:59:11', 0, NULL),
(10, 'General Terms', 'Tämä sopimus määrittelee osapuolten välisen yhteistyön yleiset ehdot. Sopimusta sovelletaan kaikkiin osapuolten välisiin liiketoimiin.', 2, '2025-12-01 09:00:00', 0, NULL),
(11, 'Payment Terms', 'Maksu suoritetaan 14 päivän kuluessa laskun päivämäärästä. Viivästyskorko on 8% vuodessa. Maksuviivästyksestä voidaan periä muistutus- ja perintäkulut.', 2, '2025-12-01 09:05:00', 0, NULL),
(12, 'Confidentiality', 'Osapuolet sitoutuvat pitämään kaikki sopimukseen liittyvät tiedot ja liikesalaisuudet ehdottoman luottamuksellisina. Salassapitovelvollisuus jatkuu viisi vuotta sopimuksen päättymisen jälkeen.', 3, '2025-12-02 10:00:00', 0, NULL),
(13, 'Termination', 'Kumpikin osapuoli voi irtisanoa sopimuksen kolmen kuukauden irtisanomisajalla. Irtisanominen on tehtävä kirjallisesti. Välittömään irtisanomiseen on oikeus olennaisessa sopimusrikkomuksessa.', 4, '2025-12-03 11:00:00', 0, NULL),
(14, 'Liability', 'Kummankaan osapuolen korvausvastuu rajoittuu välittömiin vahinkoihin, enintään sopimuksen vuosittaiseen arvoon. Vastuurajoitus ei koske tahallista tai törkeää tuottamuksellista toimintaa.', 5, '2025-12-04 12:00:00', 0, NULL),
(15, 'Dispute Resolution', 'Sopimusta koskevat erimielisyydet pyritään ratkaisemaan ensisijaisesti osapuolten välisin neuvotteluin. Mikäli sovintoa ei saavuteta, riita ratkaistaan välimiesmenettelyssä Keskuskauppakamarin välityslautakunnan sääntöjen mukaisesti.', 2, '2025-12-05 13:00:00', 0, NULL),
(16, 'General Terms', 'Sopimus astuu voimaan allekirjoituspäivänä ja on voimassa toistaiseksi. Sopimukseen tehtävät muutokset on tehtävä kirjallisesti molempien osapuolten hyväksyminä.', 3, '2025-12-06 09:30:00', 0, NULL),
(17, 'Payment Terms', 'Laskutus tapahtuu kuukausittain jälkikäteen. Laskuun liitetään erittely suoritetuista töistä ja palveluista. Hinnat ovat arvonlisäverottomia, ellei toisin mainita.', 4, '2025-12-06 10:00:00', 0, NULL),
(18, 'Tietosuoja', '[IMAGE: favicon4.jpg]', 4, '2025-12-08 19:56:56', 1, 0xffd8ffe000104a46494600010101006000600000ffe100684578696600004d4d002a000000080004011a0005000000010000003e011b0005000000010000004601280003000000010002000001310002000000110000004e00000000000176f2000003e8000176f2000003e85061696e742e4e455420352e312e31310000ffe202b04943435f50524f46494c45000101000002a06c636d73044000006d6e74725247422058595a2007e90006001c000900010000616373704d5346540000000000000000000000000000000000000000000000000000f6d6000100000000d32d6c636d7300000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000d64657363000001200000004063707274000001600000003677747074000001980000001463686164000001ac0000002c7258595a000001d8000000146258595a000001ec000000146758595a00000200000000147254524300000214000000206754524300000214000000206254524300000214000000206368726d0000023400000024646d6e640000025800000024646d64640000027c000000246d6c756300000000000000010000000c656e5553000000240000001c00470049004d00500020006200750069006c0074002d0069006e002000730052004700426d6c756300000000000000010000000c656e55530000001a0000001c005000750062006c0069006300200044006f006d00610069006e000058595a20000000000000f6d6000100000000d32d736633320000000000010c42000005defffff325000007930000fd90fffffba1fffffda2000003dc0000c06e58595a200000000000006fa0000038f50000039058595a20000000000000249f00000f840000b6c458595a2000000000000062970000b787000018d9706172610000000000030000000266660000f2a700000d59000013d000000a5b6368726d00000000000300000000a3d70000547c00004ccd0000999a0000266700000f5c6d6c756300000000000000010000000c656e5553000000080000001c00470049004d00506d6c756300000000000000010000000c656e5553000000080000001c0073005200470042ffdb0043000201010101010201010102020202020403020202020504040304060506060605060606070908060709070606080b08090a0a0a0a0a06080b0c0b0a0c090a0a0affdb004301020202020202050303050a0706070a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0a0affc00011080090009003011200021101031101ffc4001f0000010501010101010100000000000000000102030405060708090a0bffc400b5100002010303020403050504040000017d01020300041105122131410613516107227114328191a1082342b1c11552d1f02433627282090a161718191a25262728292a3435363738393a434445464748494a535455565758595a636465666768696a737475767778797a838485868788898a92939495969798999aa2a3a4a5a6a7a8a9aab2b3b4b5b6b7b8b9bac2c3c4c5c6c7c8c9cad2d3d4d5d6d7d8d9dae1e2e3e4e5e6e7e8e9eaf1f2f3f4f5f6f7f8f9faffc4001f0100030101010101010101010000000000000102030405060708090a0bffc400b51100020102040403040705040400010277000102031104052131061241510761711322328108144291a1b1c109233352f0156272d10a162434e125f11718191a262728292a35363738393a434445464748494a535455565758595a636465666768696a737475767778797a82838485868788898a92939495969798999aa2a3a4a5a6a7a8a9aab2b3b4b5b6b7b8b9bac2c3c4c5c6c7c8c9cad2d3d4d5d6d7d8d9dae2e3e4e5e6e7e8e9eaf2f3f4f5f6f7f8f9faffda000c03010002110311003f00fdfca2800a2800a2800a2800a2800a2800a2800a2800a2800a2800a2800a2800a2800a2800a2800a2800a2800a2800a2800a8afefec74ab19b54d52f62b6b6b689a5b8b89e4089146a0967663c2a800924f000a00f2dfdb4ff006d4fd9effe09ff00fb3fea9fb4b7ed35e303a3f8634a9e0b766821335c5d5c4ce123820881dd2c8796da3a2a3b1c2a923f971ff838cffe0b177dff000540fdab5bc0bf09f5e94fc1bf87177359f83a18d8aa6b5759d93eacebdfccc6c841e56100e15a5905007f589f09be2cfc35f8edf0db45f8c3f07bc6ba7f88fc31e22b04bdd175bd2ee04b05dc0e3219587e208382a41040208afe4e3fe083dff05eaf8a5ff04a4f8931fc30f8992ea1e25f81fe21bf0de20f0e23f993e8733901b50b00c701c75921c85940ece035007f5d15ccfc1af8cdf0b7f685f85da1fc6af829e39d3fc49e15f11d825e68bad69936f86e616ee0f5560415656019594ab00410003a6a2800a2800a2800a2800a2800a2800a2800a2800af29fdb43f6d4fd9dbf602f805abfed1ffb4d78ee1d0fc3da526d89061eeb52b92098ed2d62c8334ef83b5470002cc555598006e7ed27fb4afc10fd90fe0beb9fb417ed13f106c7c33e13f0f5a99b51d4ef9fbf458a3419696576c2a4680b3b100024d7f20dff000588ff0082cefed13ff0571f8d1fdbfe339a6f0efc3ad0ee9ffe108f87d6b745a0b14395fb4dc11817176ebf7a4230a09540ab9dc01fd83fc09f8dff000c7f694f839e1af8f7f063c5306b5e16f16e910ea5a26a56e789a09172011d51d4e5590e1919594804115fcf1ffc1a41ff000582ff008523f14bfe1d99f1f7c51b3c27e36d45ae3e195fde4df2697ad3f2f6196fbb15d7541d05c0c004ce4800fe9228a0028a0028a00fc71ff83bc7fe0a2bf1e3f673fd99f4bfd903e0af81fc4da7d87c4eb575f1b7c448b4b9d34f8b4edc54e930dd05f2cdc4e54995036e5806dc113e57f5ebc69e09f067c47f0b5f781fe21784b4cd7b45d4e0306a3a46b3611dd5add447aa491480a3a9f4208a00fe05ebfa79ff00828e7fc1a03fb1cfed17f6ff00887fb0f789dfe0ef8ae6dd2ff60488f79e1dbb90f38111266b2c9ef13346a3eec3401fcc357d1bff000502ff0082517edcbff04caf14c5a1fed5df06ae34cd2efaedadf45f1769928bbd1f54700b6d86e5380e5416f2a40928504940050075ff00b00fc7dff829afc37f004fe12fd97ff6c0f1afc36f03cba93dcc96fa5eb73476d2dc90164922801c16f954330da091c92471ecff000c343d3bc35f0e743d0b498d56dedb4a8163dbfc5f2025bdc9249cfbd7f46f0d784b92ac053af9939549c926e29b8c55d5eda7bcfd6ebd0fec0e0df01786e395d1c56732956ab38a938a938c23757b7bb6936afabe649f448e88fed61ff059456223ff0082b67c4a23b16bc97ff8e9a8abe9ff00e217f047fd033ffc1953ff00923ed5f823e1a37fee4fff0006d5ff00e4c987ed6bff0005981ff3969f88ff00f8172fff001ca868ff00885dc11ff40cff00f0654ffe483fe208f86bff00406fff0006d5ff00e4c9cfed73ff00059c5194ff0082b3fc4527dee64ffe2ea0a3fe2177047fd033ff00c0ea7ff2427e08f86bff00406fff0006d5ff00e4c9c7ed7dff0005a21d3fe0ac9f107ff0264ffe2aa0a5ff0010bb827fe819ff00e0753ff9217fc410f0dbfe80dffe0dabff00c99607ed83ff0005a41d3fe0ac7f103f19dfff008aaaf47fc42ee09ffa067ff81d4ffe483fe208786dff00406fff0006d5ff00e4cb43f6c6ff0082d30e9ff0562f1f7fdfc6ff00e2aaad1ff10bb827fe819ffe0753ff00920ff8821e1b7fd01bff00c1b57ff932e0fdb2bfe0b503a7fc1587c79f8b1ff1aa747fc42ee09ffa067ff81d4ffe483fe208786dff00406fff0006d5ff00e4cbe9fb66ff00c169b9dfff000562f1e0e38c2e79fcea851ff10bb827fe819ffe0753ff00921af043c355ff00306fff0006d5ff00e4cf93bfe0a19f1bff006fef8cde20d11ff6d7fda13c49f10adb4c8e58fc377faaea4f35b41bc832044c2ac723617771b8855e48518f51fdb960b297f67cbf96ea156922bfb56b6661cabf980123fe02587e35f9b71ff87394e43954b31c04e5151693849dd3bbb7baf7d3b3be87e39e2bf84190f0b6452ce32ba928a8ca29d393e64f99d972bdd357d536eebaaebf0fd7d3dfb057fc11d3fe0a15ff000521d46193f668f801a8cfe1e79bcbb8f1c6bc0d86896f83863f6a946262bde3844920feed7e2a7f371f34697aa6a5a26a76fad68da84f6979673a4f69756d294921951832ba32e0ab0201047208afe97ffe09efff0006737ec8df03fec3e3cfdbb7e205d7c59f10c5b646f0ce9664d3b4081c73b5b6917177823ab3448c38688d007d49ff0006f67fc156ff00e1e95fb10daeb7e3fb8c7c4df87ef0e89f104797b56fa5f2c9b7d4530318b8442594636ca928002ec27ed1f85bf097e16fc0ff0004597c35f835f0e743f0a787b4e8f658689e1dd2e2b3b5807fb3144aaa33dce327bd0074345001450014500145007cff00ff000541fd82bc09ff000528fd89fc69fb28f8d44105ceb163f69f0beaf347b8e95ac420bda5d0c72007f91c0e5a29244fe2afa02803f8fbf801aa78dfc2b2ebbfb36fc64d126d27c71f0d7549b44d7b4bba189233048d173ebb590a12320ed07a30afd11ff83aabf60abffd9cbe3e785ffe0aebf05fc3cdfd91ae4f0787be3159d9c5c09b684b5bf603fe7a46821663802482dfab4a6bf64e01f12a795f265d9a4af4768cfac3b27de3f8c7cd6dfd11e15f8c95723f6794679272c3e8a153774fb2977876eb1f38e8be36aafa4eaba7eb9a5dbeb3a4dd24f6b7702cd6f346d9574619041f706bfa369d4a75a9a9c1a717aa6b54d774cfec0a35a9622946ad2929464ae9a774d3d9a6b74cb14559a051400514005140051400514005729f1afe29699f07fe1ddff8d2ff006bcb12797616ecd8f3ee1b844fa6793e80135e7e699ae0326c14b178c9a8423d5fe496edbe896a7919e67b9570e65b3c76615553a71eafabe892ddb7d12d49fe19fecbbe32ff0082a17edc5e03ff00827a7c329a786ce6d41757f885aedb0dc347d2a1199a56edb846f840dc34b340bc6eafda9ff83623fe0989a9fec61fb214ff00b51fc6fd1987c58f8dc916b3ac3de4589f4bd21b3259d973ca338737122f0774888c3308afe58e36e3ac671657f6515c9878bbc63d5bdb9a4fbf92d179ee7f0ef893e27e63c79895460bd9e120ef08756f6e69bef6d92d237eaf57fa2df093e157807e05fc2ff0f7c19f857e1c8348f0df85b47b7d2f43d32d97096d6b046238d0773855192792724e4935d0d7c11f96051400514005140051400514005140051401c2fed39fb3afc32fdadff67ef17fecd5f1934617de1af1a6873e99aa4200de8b22fcb346483b658dc2c88dfc2e8addabbaa00fe42f49f855f137f626fda47c79ff0004f0f8f248f10fc3ed5e54d1ef190aa6a5a731124371167f81e278e651d42cb8ea871facdff07637fc13ab58f1a7c2af0eff00c152be03685bbc69f08825b78d62b68be7d47c3cd2122570396fb349236eff00a6371331388857e9bc0be2162b86ea2c262ef3c337eae1e71f2ef1f9ad77fda3c31f16b1dc1b56380c7b75304dedbca9dfac3bc7bc7e71b3ba7f9a358fe01f1ae8df117c1da7f8d340983db6a16e2451904a3746438fe256054fb8afe9ec1e330b8fc34711879a9c24ae9ad99fdb197e6182cd7050c5e0ea2a94e6af192774d7f5badd3d1ea6c515d2760514005140051400579d7ed0de35f15d8e95a67c28f857a55cea7e35f1cea3168de19d2ec137dc4b34ceb10d8a392c59d517fdb75f435f3bc49c4f95f0be05e23172d5fc315f149f64bb777b23e478c78d724e09cb1e2f1f3f79df920be29bec976ef27a2f5b27efff00f0478fd85dff00e0ad9ff052cb69fc65a39bbf82bf03278b55f15f9b1936facea3bcfd9ac0e78712491b161820c36f28c832ad7eef7fc11d7fe09c1e13ff00825dfec2fe16fd9c74f8ada7f134d1ff006b7c40d62dc67fb435a9d17cf21bf8a38c2a411f4fddc2a48c96cff29f13f15e69c558df6d897682f860be18afd5f76f57e4ac8fe15e35e3acef8e732fac63656846fc94d7c305e5ddbeb27abf2564bea3555550aaa00030001d296be64f8b0a2800a2800a2800a2800a2800a2800a2800a2800a28033fc59e15f0df8ebc2da97823c63a25b6a5a46b3a7cd63aae9d7910786eada5431cb13a9e1959199483d4135a1401fc9b7ed43fb24f88ff00e0947ff0511f197ec2de237b97f06eb739d7be14eab7649175a6ce58c71ee3d5d363c0e78ccb6cc40c38cfed4ffc1cc7ff0004cbbefdbb7f61f7f8cbf08b4667f8abf05da6f10f8524b48f3717d64a15ef6c571cb33246b346b824cb6ea831e6357dc706f1be61c2789e5f8e849fbd0ffdba3da5f83d9f46bf4bf0efc4bcd780f19caaf530b37efd3bff00e4d0ed2fc25b3e8d7e31d71ff02be2ad87c62f86f63e30b6645b929e4ea5021ff5370a06f5f607861ecc2bfaab29cdf2fcef031c5e0e6a5097de9f66ba35d51fdd190e7f94f136590c7e5d514e9cbef4fac64b7525d53fc8ec28af48f6428a008353d4ac346d3a7d5f54ba482dad61696e2690e1511464b13e800ae4a7f845f147f6e6fda6bc0bff0004ecf804ec35df1dea91ff006fea0a8cd1e95a626649a7971fc09123cac3392230a326400fc371971ce5fc2987e4d275e4bdd876f39765f8be9d5afcc7c44f13b28e04c2fb356a98a92f769a7b7f7a7da3e5bcba756beedff835c7f603befdac3f696d7bfe0acff1afc38fff0008bf83aea5d13e0f69f7b17cb35f052b3df852304411b9453c8f3a7720868057ee3fecc3fb38fc2efd913f67df08fecd3f05f44161e19f066890e9ba5c381bdd507cf348401ba591cbc8edfc4eecddebf96337ce731cf71d2c5e367cd37f725d92e8976fd4fe1be20e21cdf89f339e3f31aae7525f725d2315b24ba25eaeedb67794579678a1450014500145001450014500145001450014500145001450014500040230451401fcbeff00c162bf61f3ff0004a1ff00829fde4de11d23ec5f073e3bcb2eafe1531a6db7d2751de3ed5623f854472c80a803021b98464f966bf637fe0e55fd9afe0cfed09ff0487f8a3ae7c59db6d79f0f74b3e28f096ac91832daea707c91c63247cb3891add87a4dbb04aad7d370bf15e67c2b8ef6f867783f8a0f692fd1f67d3cd5d3fb4e09e3aceb81b33face0df34256e7a6dfbb35fa35d24b55e6ae9fe1c75e95f16fc2dfdb9be237803c3f1786b5fd1adf5e82d9025acf7370d1ceaa3a2b3807781c01919f735fbf65fe2df0a62a8a9621ca94baa7172fb9c53bfcd2f43faaf2af1f38131b8753c5ca7427d63284a4afe4e0a575e6d27e47d69f133c7da47c2ff0002ea3e39d6ce61b080b2c60f32c84e1231eecc40fc735e7bff0004dcd1f49ff82a87fc14ebe0e7ecb5fb406a10e8fe05d63c4d25c6a3a5596e22f52d6d27bb36cce483ba7f20c1b863689890335e271278bd97d3c2ca964e9cea3fb72568c7cd27ab7d93497577d9fcd7197d20329a381950e1e529d6968aa4a3cb18f9a8bd652ec9a4afabbecff6a7fe0d51ff00826aeb3f05be046adff0523f8ffa1e3e22fc6a8049e1e4ba8b12697e1b2c1e2db9e54dd32a4b8e7f7315b6319615fad9a6699a6e89a6dbe8da3d84369676902436b6b6d1048e18d142aa2a8e15400000380057f3ee271588c6e2255ebc9ca72776deadb3f93b198cc5e638a9e2715373a937794a4eedbeed93d1581cc145001450014500145001450014500145001450014500145001450014500145007e63ff00c1dc5f1a7fe155ff00c11c7c41e0e86efca9be2178db45f0fc615b0cca9336a2e3e856c083ec6bccbfe0edafd933f6e3fdb5fe1efc18f83dfb217ece9e25f1d69fa66b3ab6b3e28974485192d2658ade0b40c59d7e66596ecfd07bd007f3115f607fc3813fe0b29ff48f7f1fff00e02c3ffc76803cb7fe09abf1a7fe19dbfe0a0ff053e364b75e4dbf873e27e8b75a8499c7fa27db225b81f430b483f1af548ffe0811ff0005978a45963ff827cf8fd59482ac2da1c83ebfeb6803fb3aae3ff67bd6fc73e25f807e07f117c4ff000f5ce91e25bff0869b71e22d2af576cd657cf6b1b5c42e01386490ba9e4f20d0076145001450014500145001450014500145001450014500145001450014500145001450014500145001450014500145001450014500145001450014500145007fffd9);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `administrator`
--
ALTER TABLE `administrator`
  ADD PRIMARY KEY (`Administrator_ID`),
  ADD UNIQUE KEY `Username` (`Username`);

--
-- Indexes for table `admin_deletion_votes`
--
ALTER TABLE `admin_deletion_votes`
  ADD PRIMARY KEY (`Vote_ID`),
  ADD UNIQUE KEY `unique_vote` (`Target_Admin_ID`,`Voting_Admin_ID`),
  ADD KEY `Target_Admin_ID` (`Target_Admin_ID`),
  ADD KEY `Voting_Admin_ID` (`Voting_Admin_ID`);

--
-- Indexes for table `block_cooccurrence`
--
ALTER TABLE `block_cooccurrence`
  ADD PRIMARY KEY (`Block_A_ID`,`Block_B_ID`),
  ADD KEY `idx_block_b` (`Block_B_ID`);

--
-- Indexes for table `comments`
--
ALTER TABLE `comments`
  ADD PRIMARY KEY (`Comment_ID`),
  ADD KEY `Contract_NR` (`Contract_NR`),
  ADD KEY `Contract_Block_NR` (`Contract_Block_NR`);

--
-- Indexes for table `contract`
--
ALTER TABLE `contract`
  ADD PRIMARY KEY (`Contract_NR`),
  ADD KEY `The_Creator` (`The_Creator`);

--
-- Indexes for table `contract_block`
--
ALTER TABLE `contract_block`
  ADD PRIMARY KEY (`Contract_Block_NR`),
  ADD KEY `Org_Cont_ID` (`Org_Cont_ID`);

--
-- Indexes for table `contract_blocks`
--
ALTER TABLE `contract_blocks`
  ADD PRIMARY KEY (`Contract_NR`,`Contract_Block_NR`),
  ADD KEY `Contract_Block_NR` (`Contract_Block_NR`);

--
-- Indexes for table `contract_block_category`
--
ALTER TABLE `contract_block_category`
  ADD PRIMARY KEY (`Category_name`);

--
-- Indexes for table `contract_external_users`
--
ALTER TABLE `contract_external_users`
  ADD PRIMARY KEY (`Contract_NR`,`Ext_User_ID`),
  ADD KEY `Ext_User_ID` (`Ext_User_ID`);

--
-- Indexes for table `contract_stakeholders`
--
ALTER TABLE `contract_stakeholders`
  ADD PRIMARY KEY (`Contract_NR`,`Int_User_ID`),
  ADD KEY `Int_User_ID` (`Int_User_ID`);

--
-- Indexes for table `external_user`
--
ALTER TABLE `external_user`
  ADD PRIMARY KEY (`Ext_User_ID`),
  ADD UNIQUE KEY `Username` (`Username`);

--
-- Indexes for table `internal_user`
--
ALTER TABLE `internal_user`
  ADD PRIMARY KEY (`Int_User_ID`),
  ADD UNIQUE KEY `Username` (`Username`);

--
-- Indexes for table `original_contract_block`
--
ALTER TABLE `original_contract_block`
  ADD PRIMARY KEY (`Org_Cont_ID`),
  ADD KEY `Category_name` (`Category_name`),
  ADD KEY `Created_by` (`Created_by`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `administrator`
--
ALTER TABLE `administrator`
  MODIFY `Administrator_ID` int(6) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `admin_deletion_votes`
--
ALTER TABLE `admin_deletion_votes`
  MODIFY `Vote_ID` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `comments`
--
ALTER TABLE `comments`
  MODIFY `Comment_ID` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;

--
-- AUTO_INCREMENT for table `contract`
--
ALTER TABLE `contract`
  MODIFY `Contract_NR` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT for table `contract_block`
--
ALTER TABLE `contract_block`
  MODIFY `Contract_Block_NR` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;

--
-- AUTO_INCREMENT for table `external_user`
--
ALTER TABLE `external_user`
  MODIFY `Ext_User_ID` int(6) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `internal_user`
--
ALTER TABLE `internal_user`
  MODIFY `Int_User_ID` int(6) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `original_contract_block`
--
ALTER TABLE `original_contract_block`
  MODIFY `Org_Cont_ID` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `admin_deletion_votes`
--
ALTER TABLE `admin_deletion_votes`
  ADD CONSTRAINT `admin_deletion_votes_ibfk_1` FOREIGN KEY (`Target_Admin_ID`) REFERENCES `administrator` (`Administrator_ID`) ON DELETE CASCADE,
  ADD CONSTRAINT `admin_deletion_votes_ibfk_2` FOREIGN KEY (`Voting_Admin_ID`) REFERENCES `administrator` (`Administrator_ID`) ON DELETE CASCADE;

--
-- Constraints for table `comments`
--
ALTER TABLE `comments`
  ADD CONSTRAINT `comments_ibfk_1` FOREIGN KEY (`Contract_NR`) REFERENCES `contract` (`Contract_NR`),
  ADD CONSTRAINT `comments_ibfk_2` FOREIGN KEY (`Contract_Block_NR`) REFERENCES `contract_block` (`Contract_Block_NR`);

--
-- Constraints for table `contract`
--
ALTER TABLE `contract`
  ADD CONSTRAINT `contract_ibfk_1` FOREIGN KEY (`The_Creator`) REFERENCES `internal_user` (`Int_User_ID`);

--
-- Constraints for table `contract_block`
--
ALTER TABLE `contract_block`
  ADD CONSTRAINT `contract_block_ibfk_1` FOREIGN KEY (`Org_Cont_ID`) REFERENCES `original_contract_block` (`Org_Cont_ID`);

--
-- Constraints for table `contract_blocks`
--
ALTER TABLE `contract_blocks`
  ADD CONSTRAINT `contract_blocks_ibfk_1` FOREIGN KEY (`Contract_NR`) REFERENCES `contract` (`Contract_NR`),
  ADD CONSTRAINT `contract_blocks_ibfk_2` FOREIGN KEY (`Contract_Block_NR`) REFERENCES `contract_block` (`Contract_Block_NR`);

--
-- Constraints for table `contract_external_users`
--
ALTER TABLE `contract_external_users`
  ADD CONSTRAINT `contract_external_users_ibfk_1` FOREIGN KEY (`Contract_NR`) REFERENCES `contract` (`Contract_NR`),
  ADD CONSTRAINT `contract_external_users_ibfk_2` FOREIGN KEY (`Ext_User_ID`) REFERENCES `external_user` (`Ext_User_ID`);

--
-- Constraints for table `contract_stakeholders`
--
ALTER TABLE `contract_stakeholders`
  ADD CONSTRAINT `contract_stakeholders_ibfk_1` FOREIGN KEY (`Contract_NR`) REFERENCES `contract` (`Contract_NR`),
  ADD CONSTRAINT `contract_stakeholders_ibfk_2` FOREIGN KEY (`Int_User_ID`) REFERENCES `internal_user` (`Int_User_ID`);

--
-- Constraints for table `original_contract_block`
--
ALTER TABLE `original_contract_block`
  ADD CONSTRAINT `original_contract_block_ibfk_1` FOREIGN KEY (`Category_name`) REFERENCES `contract_block_category` (`Category_name`),
  ADD CONSTRAINT `original_contract_block_ibfk_2` FOREIGN KEY (`Created_by`) REFERENCES `internal_user` (`Int_User_ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
