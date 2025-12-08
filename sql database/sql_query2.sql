-- Lisää uusia sisäisiä käyttäjiä (Internal Users)
INSERT INTO `administrator` (`Administrator_ID`, `First_name`, `Last_name`, `Username`, `Password`, `Email`) VALUES
(2, 'Jani', 'Vuorinen', 'Ferrari11', '1234', 'jani.vuorinen@hotmail.com'),
(3, 'Niina', 'Kosonen', 'Ferrari111', '1234', 'niina.kosonen@gmail.com'),
(4, 'Nitesh', 'Sledger', 'Ferrari1111', '1234', 'nitesh.sledger@gmail.com'),
(5, 'Esko', 'Ojala', 'Ferrari11111', '1234', 'esko.ojala@sopimus.fi');

-- Lisää uusia sisäisiä käyttäjiä (Internal Users)
INSERT INTO `internal_user` (`Int_User_ID`, `First_name`, `Last_name`, `Username`, `Password`, `Email`) VALUES
(2, 'Antti', 'Virtanen', 'Ferrari22', '1234', 'antti.virtanen@sopimus.fi'),
(3, 'Sanna', 'Mäkinen', 'Ferrari222', '1234', 'sanna.makinen@sopimus.fi'),
(4, 'Jukka', 'Koskinen', 'Ferrari2222', '1234', 'jukka.koskinen@sopimus.fi'),
(5, 'Laura', 'Nieminen', 'Ferrari22222', '1234', 'laura.nieminen@sopimus.fi');

-- Lisää uusia ulkoisia käyttäjiä (External Users)
INSERT INTO `external_user` (`Ext_User_ID`, `First_name`, `Last_name`, `Company_name`, `Email`, `Username`, `Password`) VALUES
(2, 'Petri', 'Laine', 'TechNova Oy', 'petri.laine@technova.fi', 'Ferrari33', '1234'),
(3, 'Kaisa', 'Järvinen', 'BuildPro Solutions', 'kaisa.jarvinen@buildpro.fi', 'Ferrari333', '1234'),
(4, 'Timo', 'Salo', 'Nordic Trade Oy', 'timo.salo@nordictrade.fi', 'Ferrari3333', '1234'),
(5, 'Heli', 'Rantanen', 'Green Energy Finland', 'heli.rantanen@greenenergy.fi', 'Ferrari33333', '1234');

-- Lisää uusia sopimuksia (Contracts)
INSERT INTO `contract` (`Contract_NR`, `Company_name`, `The_Creator`, `Created_date`, `Approved`, `Sent_to_external`) VALUES
(14, 'TechNova Oy', 2, '2025-12-01 09:15:00', 0, 0),
(15, 'BuildPro Solutions', 3, '2025-12-02 10:30:00', 1, 1),
(16, 'Nordic Trade Oy', 4, '2025-12-03 11:45:00', 0, 0),
(17, 'Green Energy Finland', 5, '2025-12-04 13:20:00', 1, 0),
(18, 'Digital Services Oy', 2, '2025-12-05 14:00:00', 0, 0);

-- Lisää uusia alkuperäisiä sopimuskohdalohkoja (Original Contract Blocks)
INSERT INTO `original_contract_block` (`Org_Cont_ID`, `Category_name`, `Contract_text`, `Created_by`, `Created_date`, `Type`, `MediaContent`) VALUES
(10, 'General Terms', 'Tämä sopimus määrittelee osapuolten välisen yhteistyön yleiset ehdot. Sopimusta sovelletaan kaikkiin osapuolten välisiin liiketoimiin.', 2, '2025-12-01 09:00:00', 0, NULL),
(11, 'Payment Terms', 'Maksu suoritetaan 14 päivän kuluessa laskun päivämäärästä. Viivästyskorko on 8% vuodessa. Maksuviivästyksestä voidaan periä muistutus- ja perintäkulut.', 2, '2025-12-01 09:05:00', 0, NULL),
(12, 'Confidentiality', 'Osapuolet sitoutuvat pitämään kaikki sopimukseen liittyvät tiedot ja liikesalaisuudet ehdottoman luottamuksellisina. Salassapitovelvollisuus jatkuu viisi vuotta sopimuksen päättymisen jälkeen.', 3, '2025-12-02 10:00:00', 0, NULL),
(13, 'Termination', 'Kumpikin osapuoli voi irtisanoa sopimuksen kolmen kuukauden irtisanomisajalla. Irtisanominen on tehtävä kirjallisesti. Välittömään irtisanomiseen on oikeus olennaisessa sopimusrikkomuksessa.', 4, '2025-12-03 11:00:00', 0, NULL),
(14, 'Liability', 'Kummankaan osapuolen korvausvastuu rajoittuu välittömiin vahinkoihin, enintään sopimuksen vuosittaiseen arvoon. Vastuurajoitus ei koske tahallista tai törkeää tuottamuksellista toimintaa.', 5, '2025-12-04 12:00:00', 0, NULL),
(15, 'Dispute Resolution', 'Sopimusta koskevat erimielisyydet pyritään ratkaisemaan ensisijaisesti osapuolten välisin neuvotteluin. Mikäli sovintoa ei saavuteta, riita ratkaistaan välimiesmenettelyssä Keskuskauppakamarin välityslautakunnan sääntöjen mukaisesti.', 2, '2025-12-05 13:00:00', 0, NULL),
(16, 'General Terms', 'Sopimus astuu voimaan allekirjoituspäivänä ja on voimassa toistaiseksi. Sopimukseen tehtävät muutokset on tehtävä kirjallisesti molempien osapuolten hyväksyminä.', 3, '2025-12-06 09:30:00', 0, NULL),
(17, 'Payment Terms', 'Laskutus tapahtuu kuukausittain jälkikäteen. Laskuun liitetään erittely suoritetuista töistä ja palveluista. Hinnat ovat arvonlisäverottomia, ellei toisin mainita.', 4, '2025-12-06 10:00:00', 0, NULL);

-- Lisää uusia sopimuskohdalohkoja (Contract Blocks)
INSERT INTO `contract_block` (`Contract_Block_NR`, `Org_Cont_ID`, `Contract_text`, `New`, `Modified_date`, `Type`, `MediaContent`) VALUES
(9, 10, 'Tämä sopimus määrittelee osapuolten välisen yhteistyön yleiset ehdot. Sopimusta sovelletaan kaikkiin osapuolten välisiin liiketoimiin.', 0, '2025-12-01 09:15:00', 0, NULL),
(10, 11, 'Maksu suoritetaan 14 päivän kuluessa laskun päivämäärästä. Viivästyskorko on 8% vuodessa.', 1, '2025-12-01 09:20:00', 0, NULL),
(11, 12, 'Osapuolet sitoutuvat pitämään kaikki sopimukseen liittyvät tiedot luottamuksellisina.', 0, '2025-12-02 10:30:00', 0, NULL),
(12, 13, 'Kumpikin osapuoli voi irtisanoa sopimuksen kolmen kuukauden irtisanomisajalla kirjallisesti.', 0, '2025-12-03 11:45:00', 0, NULL),
(13, 14, 'Korvausvastuu rajoittuu välittömiin vahinkoihin enintään sopimuksen vuosittaiseen arvoon.', 0, '2025-12-04 13:20:00', 0, NULL),
(14, 15, 'Erimielisyydet ratkaistaan Keskuskauppakamarin välityslautakunnassa Helsingissä.', 0, '2025-12-05 14:00:00', 0, NULL);

-- Yhdistä lohkot sopimuksiin (Contract Blocks relationship)
INSERT INTO `contract_blocks` (`Contract_NR`, `Contract_Block_NR`, `Block_order`) VALUES
(14, 9, 1),
(14, 10, 2),
(15, 11, 1),
(15, 12, 2),
(16, 13, 1),
(17, 14, 1),
(18, 9, 1),
(18, 14, 2);

-- Lisää sidosryhmiä sopimuksiin (Contract Stakeholders)
INSERT INTO `contract_stakeholders` (`Contract_NR`, `Int_User_ID`, `Has_approval_rights`, `Approved`, `Approved_date`) VALUES
(14, 2, 1, 0, NULL),
(14, 3, 1, 0, NULL),
(15, 3, 1, 1, '2025-12-02 15:00:00'),
(15, 4, 0, NULL, NULL),
(16, 4, 1, 0, NULL),
(17, 5, 1, 1, '2025-12-04 16:30:00'),
(18, 2, 1, 0, NULL);

-- Lisää ulkoisia käyttäjiä sopimuksiin (Contract External Users)
INSERT INTO `contract_external_users` (`Contract_NR`, `Ext_User_ID`, `Invited_date`) VALUES
(14, 2, '2025-12-01 10:00:00'),
(15, 3, '2025-12-02 11:00:00'),
(16, 4, '2025-12-03 12:00:00'),
(17, 5, '2025-12-04 14:00:00'),
(18, 2, '2025-12-05 15:00:00');

-- Lisää kommentteja (Comments)
INSERT INTO `comments` (`Comment_ID`, `Contract_NR`, `Contract_Block_NR`, `User_ID`, `User_type`, `Comment_text`, `Comment_date`) VALUES
(14, 14, 9, 2, 'internal', 'Yleiset ehdot näyttävät hyvältä, mutta tarkista vielä irtisanomisehdot.', '2025-12-01 10:30:00'),
(15, 14, 10, 2, 'external', 'Maksuehdot hyväksyttävissä, mutta toivomme 21 päivän maksuaikaa.', '2025-12-01 11:00:00'),
(16, 15, 11, 3, 'internal', 'Salassapitolauseke on liian laaja, täsmennetään vielä.', '2025-12-02 11:30:00'),
(17, 15, NULL, 3, 'external', 'Sopimus on kokonaisuutena hyvä ja voimme hyväksyä sen.', '2025-12-02 14:00:00'),
(18, 16, 13, 4, 'internal', 'Vastuunrajoituslauseke tarvitsee lakimiehen tarkistuksen.', '2025-12-03 12:30:00'),
(19, 17, 14, 5, 'internal', 'Riitojenratkaisulauseke hyväksytty, voidaan lähettää asiakkaalle.', '2025-12-04 15:00:00'),
(20, 18, NULL, 2, 'internal', 'Luonnos valmis, odottaa hyväksyntää.', '2025-12-05 16:00:00');

-- Lisää lohkojen yhteisesiintymistä (Block Co-occurrence)
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

-- Lisää uusi kategoria
INSERT INTO `contract_block_category` (`Category_name`, `Description`) VALUES
('Tietosuoja', 'Määrittelee henkilötietojen käsittelyn periaatteet ja GDPR-vaatimukset.');