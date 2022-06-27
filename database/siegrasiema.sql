-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: mysql:3306
-- Generation Time: Jun 26, 2022 at 02:50 AM
-- Server version: 8.0.29
-- PHP Version: 8.0.19
USE SieGraSieMa;
SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `SieGraSieMa`
--

-- --------------------------------------------------------

--
-- Table structure for table `album`
--

CREATE TABLE `album` (
  `id` int NOT NULL,
  `name` varchar(256) COLLATE utf8mb4_polish_ci NOT NULL,
  `create_date` datetime NOT NULL,
  `tournament_id` int NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `AspNetRoleClaims`
--

CREATE TABLE `AspNetRoleClaims` (
  `Id` int NOT NULL,
  `RoleId` int NOT NULL,
  `ClaimType` text COLLATE utf8mb4_polish_ci,
  `ClaimValue` text COLLATE utf8mb4_polish_ci
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `AspNetRoles`
--

CREATE TABLE `AspNetRoles` (
  `Id` int NOT NULL,
  `Name` varchar(256) COLLATE utf8mb4_polish_ci DEFAULT NULL,
  `NormalizedName` varchar(256) COLLATE utf8mb4_polish_ci DEFAULT NULL,
  `ConcurrencyStamp` text COLLATE utf8mb4_polish_ci
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `AspNetUserClaims`
--

CREATE TABLE `AspNetUserClaims` (
  `Id` int NOT NULL,
  `UserId` int NOT NULL,
  `ClaimType` text COLLATE utf8mb4_polish_ci,
  `ClaimValue` text COLLATE utf8mb4_polish_ci
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `AspNetUserLogins`
--

CREATE TABLE `AspNetUserLogins` (
  `LoginProvider` varchar(85) COLLATE utf8mb4_polish_ci NOT NULL,
  `ProviderKey` varchar(85) COLLATE utf8mb4_polish_ci NOT NULL,
  `ProviderDisplayName` text COLLATE utf8mb4_polish_ci,
  `UserId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `AspNetUserRoles`
--

CREATE TABLE `AspNetUserRoles` (
  `UserId` int NOT NULL,
  `RoleId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `AspNetUserTokens`
--

CREATE TABLE `AspNetUserTokens` (
  `UserId` int NOT NULL,
  `LoginProvider` varchar(85) COLLATE utf8mb4_polish_ci NOT NULL,
  `Name` varchar(85) COLLATE utf8mb4_polish_ci NOT NULL,
  `Value` text COLLATE utf8mb4_polish_ci
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `contest`
--

CREATE TABLE `contest` (
  `id` int NOT NULL,
  `name` varchar(64) COLLATE utf8mb4_polish_ci NOT NULL,
  `tournament_id` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `contestants`
--

CREATE TABLE `contestants` (
  `contest_id` int NOT NULL,
  `user_id` int NOT NULL,
  `points` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `group`
--

CREATE TABLE `group` (
  `id` int NOT NULL,
  `name` varchar(2) COLLATE utf8mb4_polish_ci NOT NULL,
  `tournament_id` int NOT NULL,
  `ladder` tinyint(1) NOT NULL,
  `phase` int NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `logs`
--

CREATE TABLE `logs` (
  `id` int NOT NULL,
  `user_id` int NOT NULL,
  `action` varchar(256) COLLATE utf8mb4_polish_ci NOT NULL,
  `time` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `match`
--

CREATE TABLE `match` (
  `match_id` int NOT NULL,
  `team_home_score` int DEFAULT NULL,
  `team_away_score` int DEFAULT NULL,
  `team_home_id` int DEFAULT NULL,
  `team_away_id` int DEFAULT NULL,
  `tournament_Id` int NOT NULL DEFAULT '0',
  `phase` int NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `media`
--

CREATE TABLE `media` (
  `id` int NOT NULL,
  `url` varchar(256) COLLATE utf8mb4_polish_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `medium_in_album`
--

CREATE TABLE `medium_in_album` (
  `album_id` int NOT NULL,
  `medium_id` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `newsletter`
--

CREATE TABLE `newsletter` (
  `id` int NOT NULL,
  `user_id` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `player`
--

CREATE TABLE `player` (
  `team_id` int NOT NULL,
  `user_id` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `refresh_token`
--

CREATE TABLE `refresh_token` (
  `id` int NOT NULL,
  `token` text COLLATE utf8mb4_polish_ci NOT NULL,
  `expires` datetime NOT NULL,
  `created` datetime NOT NULL,
  `createdByIp` varchar(45) COLLATE utf8mb4_polish_ci NOT NULL,
  `revoked` datetime DEFAULT NULL,
  `revokedByIp` varchar(45) COLLATE utf8mb4_polish_ci DEFAULT NULL,
  `replacedByToken` text COLLATE utf8mb4_polish_ci,
  `UserId` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `team`
--

CREATE TABLE `team` (
  `id` int NOT NULL,
  `name` varchar(64) COLLATE utf8mb4_polish_ci NOT NULL,
  `captain_id` int DEFAULT NULL,
  `code` char(5) COLLATE utf8mb4_polish_ci NOT NULL,
  `MediumId` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `team_in_group`
--

CREATE TABLE `team_in_group` (
  `id` int NOT NULL,
  `group_id` int NOT NULL,
  `team_id` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `team_in_tournament`
--

CREATE TABLE `team_in_tournament` (
  `team_id` int NOT NULL,
  `tournament_id` int NOT NULL,
  `paid` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `tournament`
--

CREATE TABLE `tournament` (
  `id` int NOT NULL,
  `name` varchar(128) COLLATE utf8mb4_polish_ci NOT NULL,
  `start_date` datetime NOT NULL,
  `end_date` datetime NOT NULL,
  `description` text COLLATE utf8mb4_polish_ci,
  `address` varchar(256) COLLATE utf8mb4_polish_ci NOT NULL,
  `MediumId` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `id` int NOT NULL,
  `name` varchar(32) COLLATE utf8mb4_polish_ci NOT NULL,
  `surname` varchar(64) COLLATE utf8mb4_polish_ci NOT NULL,
  `email` varchar(320) COLLATE utf8mb4_polish_ci NOT NULL,
  `AccessFailedCount` int NOT NULL DEFAULT '0',
  `ConcurrencyStamp` text COLLATE utf8mb4_polish_ci,
  `EmailConfirmed` tinyint(1) NOT NULL DEFAULT '0',
  `LockoutEnabled` tinyint(1) NOT NULL DEFAULT '0',
  `LockoutEnd` timestamp NULL DEFAULT NULL,
  `NormalizedEmail` varchar(256) COLLATE utf8mb4_polish_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) COLLATE utf8mb4_polish_ci DEFAULT NULL,
  `PasswordHash` text COLLATE utf8mb4_polish_ci,
  `PhoneNumber` text COLLATE utf8mb4_polish_ci,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL DEFAULT '0',
  `SecurityStamp` text COLLATE utf8mb4_polish_ci,
  `TwoFactorEnabled` tinyint(1) NOT NULL DEFAULT '0',
  `UserName` varchar(256) COLLATE utf8mb4_polish_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

-- --------------------------------------------------------

--
-- Table structure for table `__EFMigrationsHistory`
--

CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) COLLATE utf8mb4_polish_ci NOT NULL,
  `ProductVersion` varchar(32) COLLATE utf8mb4_polish_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_polish_ci;

--
-- Dumping data for table `__EFMigrationsHistory`
--

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`) VALUES
('20210726202335_AddAutoGeneratedId', '5.0.16'),
('20211023203421_AddRefreshTokens', '5.0.16'),
('20211023211311_RepairRefreshTokensByNullable', '5.0.16'),
('20211112223036_ChangeMatchAndGroupTableForMatchmaking', '5.0.16'),
('20211220184133_identity v2', '5.0.16'),
('20211220185253_identity v3', '5.0.16'),
('20211220185622_identity v4', '5.0.16'),
('20211220194533_new seed', '5.0.16'),
('20211227185050_correct seed', '5.0.16'),
('20220103120930_identity-fixes', '5.0.16'),
('20220109210738_mergeFix', '5.0.16'),
('20220424213550_AddPhaseToGroups', '5.0.16'),
('20220425191540_AddTwoMoreAttributesInMatchToBeKey', '5.0.16'),
('20220425232932_AddNullsForTeamsInMatches', '5.0.16'),
('20220503182217_AddNullsForTeamsInTeamInGroups', '5.0.16'),
('20220524190739_MediaAlbumsChange_OtherLittleChanges', '5.0.16'),
('20220524202519_IndexChanging', '5.0.16'),
('20220605190548_medium-team-tournament', '5.0.16'),
('20220605230029_deleted-summary', '5.0.16'),
('20220607212340_media-cascade-delete', '5.0.16'),
('20220607213116_cascade-medium', '5.0.16'),
('20220608144207_mediumInAlbumCascade_CaptainCanBeNull_PlayerCascade', '5.0.16'),
('20220612151321_ChangeEmpToEmployee', '5.0.16'),
('20220626021824_removeRoleFromContext', '5.0.16');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `album`
--
ALTER TABLE `album`
  ADD PRIMARY KEY (`id`),
  ADD KEY `album_tournament` (`tournament_id`);

--
-- Indexes for table `AspNetRoleClaims`
--
ALTER TABLE `AspNetRoleClaims`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`);

--
-- Indexes for table `AspNetRoles`
--
ALTER TABLE `AspNetRoles`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `RoleNameIndex` (`NormalizedName`);

--
-- Indexes for table `AspNetUserClaims`
--
ALTER TABLE `AspNetUserClaims`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_AspNetUserClaims_UserId` (`UserId`);

--
-- Indexes for table `AspNetUserLogins`
--
ALTER TABLE `AspNetUserLogins`
  ADD PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  ADD KEY `IX_AspNetUserLogins_UserId` (`UserId`);

--
-- Indexes for table `AspNetUserRoles`
--
ALTER TABLE `AspNetUserRoles`
  ADD PRIMARY KEY (`UserId`,`RoleId`),
  ADD KEY `IX_AspNetUserRoles_RoleId` (`RoleId`);

--
-- Indexes for table `AspNetUserTokens`
--
ALTER TABLE `AspNetUserTokens`
  ADD PRIMARY KEY (`UserId`,`LoginProvider`,`Name`);

--
-- Indexes for table `contest`
--
ALTER TABLE `contest`
  ADD PRIMARY KEY (`id`),
  ADD KEY `contest_tournament` (`tournament_id`);

--
-- Indexes for table `contestants`
--
ALTER TABLE `contestants`
  ADD PRIMARY KEY (`contest_id`,`user_id`),
  ADD KEY `IX_contestants_user_id` (`user_id`),
  ADD KEY `contestants_contest` (`contest_id`);

--
-- Indexes for table `group`
--
ALTER TABLE `group`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `name_in_tournament` (`name`,`tournament_id`),
  ADD KEY `group_tournament` (`tournament_id`);

--
-- Indexes for table `logs`
--
ALTER TABLE `logs`
  ADD PRIMARY KEY (`id`),
  ADD KEY `logs_user` (`user_id`);

--
-- Indexes for table `match`
--
ALTER TABLE `match`
  ADD PRIMARY KEY (`match_id`,`tournament_Id`,`phase`),
  ADD UNIQUE KEY `meet` (`team_home_id`,`team_away_id`),
  ADD KEY `IX_match_team_away_id` (`team_away_id`),
  ADD KEY `tournament` (`tournament_Id`);

--
-- Indexes for table `media`
--
ALTER TABLE `media`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `medium_in_album`
--
ALTER TABLE `medium_in_album`
  ADD PRIMARY KEY (`album_id`,`medium_id`),
  ADD KEY `medium_in_album_media` (`medium_id`);

--
-- Indexes for table `newsletter`
--
ALTER TABLE `newsletter`
  ADD PRIMARY KEY (`id`),
  ADD KEY `newsletter_user` (`user_id`);

--
-- Indexes for table `player`
--
ALTER TABLE `player`
  ADD PRIMARY KEY (`team_id`,`user_id`),
  ADD KEY `player_user` (`user_id`);

--
-- Indexes for table `refresh_token`
--
ALTER TABLE `refresh_token`
  ADD PRIMARY KEY (`id`),
  ADD KEY `IX_refresh_token_UserId` (`UserId`);

--
-- Indexes for table `team`
--
ALTER TABLE `team`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `code` (`code`),
  ADD KEY `captain` (`captain_id`),
  ADD KEY `IX_team_MediumId` (`MediumId`);

--
-- Indexes for table `team_in_group`
--
ALTER TABLE `team_in_group`
  ADD PRIMARY KEY (`id`),
  ADD KEY `IX_team_in_group_team_id` (`team_id`),
  ADD KEY `team_in_group_group` (`group_id`);

--
-- Indexes for table `team_in_tournament`
--
ALTER TABLE `team_in_tournament`
  ADD PRIMARY KEY (`team_id`,`tournament_id`),
  ADD KEY `team_in_tournament_tournament` (`tournament_id`),
  ADD KEY `IX_team_in_tournament_team_id` (`team_id`);

--
-- Indexes for table `tournament`
--
ALTER TABLE `tournament`
  ADD PRIMARY KEY (`id`),
  ADD KEY `IX_tournament_MediumId` (`MediumId`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `email` (`email`),
  ADD UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  ADD KEY `EmailIndex` (`NormalizedEmail`);

--
-- Indexes for table `__EFMigrationsHistory`
--
ALTER TABLE `__EFMigrationsHistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `album`
--
ALTER TABLE `album`
  MODIFY `id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `AspNetRoleClaims`
--
ALTER TABLE `AspNetRoleClaims`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `AspNetRoles`
--
ALTER TABLE `AspNetRoles`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `AspNetUserClaims`
--
ALTER TABLE `AspNetUserClaims`
  MODIFY `Id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `contest`
--
ALTER TABLE `contest`
  MODIFY `id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `group`
--
ALTER TABLE `group`
  MODIFY `id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `logs`
--
ALTER TABLE `logs`
  MODIFY `id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `media`
--
ALTER TABLE `media`
  MODIFY `id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `newsletter`
--
ALTER TABLE `newsletter`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `refresh_token`
--
ALTER TABLE `refresh_token`
  MODIFY `id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `team`
--
ALTER TABLE `team`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `team_in_group`
--
ALTER TABLE `team_in_group`
  MODIFY `id` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `tournament`
--
ALTER TABLE `tournament`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `album`
--
ALTER TABLE `album`
  ADD CONSTRAINT `album_tournament` FOREIGN KEY (`tournament_id`) REFERENCES `tournament` (`id`) ON DELETE RESTRICT;

--
-- Constraints for table `AspNetRoleClaims`
--
ALTER TABLE `AspNetRoleClaims`
  ADD CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE;

--
-- Constraints for table `AspNetUserClaims`
--
ALTER TABLE `AspNetUserClaims`
  ADD CONSTRAINT `FK_AspNetUserClaims_user_UserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `AspNetUserLogins`
--
ALTER TABLE `AspNetUserLogins`
  ADD CONSTRAINT `FK_AspNetUserLogins_user_UserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `AspNetUserRoles`
--
ALTER TABLE `AspNetUserRoles`
  ADD CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_AspNetUserRoles_user_UserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `AspNetUserTokens`
--
ALTER TABLE `AspNetUserTokens`
  ADD CONSTRAINT `FK_AspNetUserTokens_user_UserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `contest`
--
ALTER TABLE `contest`
  ADD CONSTRAINT `contest_tournament` FOREIGN KEY (`tournament_id`) REFERENCES `tournament` (`id`) ON DELETE RESTRICT;

--
-- Constraints for table `contestants`
--
ALTER TABLE `contestants`
  ADD CONSTRAINT `contestants_contest` FOREIGN KEY (`contest_id`) REFERENCES `contest` (`id`) ON DELETE RESTRICT,
  ADD CONSTRAINT `contestants_user` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `group`
--
ALTER TABLE `group`
  ADD CONSTRAINT `group_tournament` FOREIGN KEY (`tournament_id`) REFERENCES `tournament` (`id`) ON DELETE RESTRICT;

--
-- Constraints for table `logs`
--
ALTER TABLE `logs`
  ADD CONSTRAINT `logs_user` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `match`
--
ALTER TABLE `match`
  ADD CONSTRAINT `match_away` FOREIGN KEY (`team_away_id`) REFERENCES `team_in_group` (`id`) ON DELETE RESTRICT,
  ADD CONSTRAINT `match_home` FOREIGN KEY (`team_home_id`) REFERENCES `team_in_group` (`id`) ON DELETE RESTRICT,
  ADD CONSTRAINT `tournament` FOREIGN KEY (`tournament_Id`) REFERENCES `tournament` (`id`) ON DELETE RESTRICT;

--
-- Constraints for table `medium_in_album`
--
ALTER TABLE `medium_in_album`
  ADD CONSTRAINT `medium_in_album_album` FOREIGN KEY (`album_id`) REFERENCES `album` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `medium_in_album_media` FOREIGN KEY (`medium_id`) REFERENCES `media` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `newsletter`
--
ALTER TABLE `newsletter`
  ADD CONSTRAINT `newsletter_user` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `player`
--
ALTER TABLE `player`
  ADD CONSTRAINT `player_team` FOREIGN KEY (`team_id`) REFERENCES `team` (`id`) ON DELETE RESTRICT,
  ADD CONSTRAINT `player_user` FOREIGN KEY (`user_id`) REFERENCES `user` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `refresh_token`
--
ALTER TABLE `refresh_token`
  ADD CONSTRAINT `refresh_token_user` FOREIGN KEY (`UserId`) REFERENCES `user` (`id`) ON DELETE CASCADE;

--
-- Constraints for table `team`
--
ALTER TABLE `team`
  ADD CONSTRAINT `captain` FOREIGN KEY (`captain_id`) REFERENCES `user` (`id`) ON DELETE RESTRICT,
  ADD CONSTRAINT `team_medium` FOREIGN KEY (`MediumId`) REFERENCES `media` (`id`) ON DELETE RESTRICT;

--
-- Constraints for table `team_in_group`
--
ALTER TABLE `team_in_group`
  ADD CONSTRAINT `team_in_group_group` FOREIGN KEY (`group_id`) REFERENCES `group` (`id`) ON DELETE RESTRICT,
  ADD CONSTRAINT `team_in_group_team` FOREIGN KEY (`team_id`) REFERENCES `team` (`id`) ON DELETE RESTRICT;

--
-- Constraints for table `team_in_tournament`
--
ALTER TABLE `team_in_tournament`
  ADD CONSTRAINT `team_in_tournament_team` FOREIGN KEY (`team_id`) REFERENCES `team` (`id`) ON DELETE RESTRICT,
  ADD CONSTRAINT `team_in_tournament_tournament` FOREIGN KEY (`tournament_id`) REFERENCES `tournament` (`id`) ON DELETE RESTRICT;

--
-- Constraints for table `tournament`
--
ALTER TABLE `tournament`
  ADD CONSTRAINT `tournament_medium` FOREIGN KEY (`MediumId`) REFERENCES `media` (`id`) ON DELETE RESTRICT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
