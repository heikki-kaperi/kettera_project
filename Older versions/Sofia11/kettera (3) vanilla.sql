-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 08, 2025 at 06:52 PM
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
(13, 14, 2);

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
(14, 15, 'Erimielisyydet ratkaistaan Keskuskauppakamarin välityslautakunnassa Helsingissä.', 0, '2025-12-05 14:00:00', 0, NULL);

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
(17, 'Payment Terms', 'Laskutus tapahtuu kuukausittain jälkikäteen. Laskuun liitetään erittely suoritetuista töistä ja palveluista. Hinnat ovat arvonlisäverottomia, ellei toisin mainita.', 4, '2025-12-06 10:00:00', 0, NULL);

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
  MODIFY `Contract_Block_NR` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

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
  MODIFY `Org_Cont_ID` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

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
