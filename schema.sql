-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 15, 2026 at 07:23 AM
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
-- Database: `budgetmate_app`
--
CREATE DATABASE IF NOT EXISTS `budgetmate_app` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE `budgetmate_app`;

-- --------------------------------------------------------

--
-- Table structure for table `budgets`
--

CREATE TABLE IF NOT EXISTS `budgets` (
  `budget_id` varchar(64) NOT NULL,
  `user_id` varchar(64) NOT NULL,
  `category_id` varchar(64) NOT NULL,
  `budget_month` date NOT NULL COMMENT 'First day of the budget month',
  `spending_limit` decimal(14,2) NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`budget_id`),
  UNIQUE KEY `uq_budget_user_category_month` (`user_id`,`category_id`,`budget_month`),
  KEY `ix_budgets_user_month` (`user_id`,`budget_month`),
  KEY `fk_budgets_category` (`category_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `categories`
--

CREATE TABLE IF NOT EXISTS `categories` (
  `category_id` varchar(64) NOT NULL,
  `user_id` varchar(64) DEFAULT NULL,
  `category_name` varchar(100) NOT NULL,
  `category_type` enum('income','expense') NOT NULL,
  `color` char(7) DEFAULT NULL,
  `default_monthly_limit` decimal(14,2) DEFAULT NULL,
  `is_default` tinyint(1) NOT NULL DEFAULT 0,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`category_id`),
  UNIQUE KEY `uq_category_owner_name_type` (`user_id`,`category_name`,`category_type`),
  KEY `ix_categories_user_type` (`user_id`,`category_type`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `goal_contributions`
--

CREATE TABLE IF NOT EXISTS `goal_contributions` (
  `contribution_id` varchar(64) NOT NULL,
  `goal_id` varchar(64) NOT NULL,
  `user_id` varchar(64) NOT NULL,
  `amount` decimal(14,2) NOT NULL,
  `contribution_date` date NOT NULL,
  `note` varchar(255) DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`contribution_id`),
  KEY `ix_contributions_goal_date` (`goal_id`,`contribution_date`),
  KEY `fk_contributions_user` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `recurring_occurrences`
--

CREATE TABLE IF NOT EXISTS `recurring_occurrences` (
  `occurrence_id` varchar(64) NOT NULL,
  `rule_id` varchar(64) NOT NULL,
  `occurrence_month` char(7) NOT NULL COMMENT 'YYYY-MM; one generated transaction per rule per month',
  `occurrence_date` date NOT NULL,
  `transaction_id` varchar(64) DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`occurrence_id`),
  UNIQUE KEY `uq_recurring_rule_month` (`rule_id`,`occurrence_month`),
  UNIQUE KEY `uq_occurrence_transaction` (`transaction_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `recurring_rules`
--

CREATE TABLE IF NOT EXISTS `recurring_rules` (
  `rule_id` varchar(64) NOT NULL,
  `user_id` varchar(64) NOT NULL,
  `title` varchar(150) NOT NULL,
  `transaction_type` enum('income','expense') NOT NULL,
  `amount` decimal(14,2) NOT NULL,
  `category_id` varchar(64) DEFAULT NULL,
  `wallet_id` varchar(64) NOT NULL,
  `payment_method` varchar(50) NOT NULL DEFAULT 'Cash',
  `notes` text DEFAULT NULL,
  `frequency` enum('monthly') NOT NULL DEFAULT 'monthly',
  `day_of_month` tinyint(3) UNSIGNED NOT NULL DEFAULT 1,
  `start_date` date NOT NULL,
  `is_active` tinyint(1) NOT NULL DEFAULT 1,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`rule_id`),
  KEY `ix_recurring_user_active` (`user_id`,`is_active`),
  KEY `fk_recurring_category` (`category_id`),
  KEY `fk_recurring_wallet` (`wallet_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `savings_goals`
--

CREATE TABLE IF NOT EXISTS `savings_goals` (
  `goal_id` varchar(64) NOT NULL,
  `user_id` varchar(64) NOT NULL,
  `goal_name` varchar(150) NOT NULL,
  `target_amount` decimal(14,2) NOT NULL,
  `current_amount` decimal(14,2) NOT NULL DEFAULT 0.00,
  `deadline` date DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`goal_id`),
  KEY `ix_goals_user` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `transactions`
--

CREATE TABLE IF NOT EXISTS `transactions` (
  `transaction_id` varchar(64) NOT NULL,
  `user_id` varchar(64) NOT NULL,
  `wallet_id` varchar(64) NOT NULL,
  `category_id` varchar(64) DEFAULT NULL,
  `recurring_rule_id` varchar(64) DEFAULT NULL,
  `transaction_type` enum('income','expense') NOT NULL,
  `title` varchar(150) NOT NULL,
  `amount` decimal(14,2) NOT NULL,
  `transaction_date` date NOT NULL,
  `payment_method` varchar(50) NOT NULL DEFAULT 'Cash',
  `notes` text DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`transaction_id`),
  KEY `ix_transactions_user_date` (`user_id`,`transaction_date`),
  KEY `ix_transactions_user_type_date` (`user_id`,`transaction_type`,`transaction_date`),
  KEY `ix_transactions_wallet` (`wallet_id`),
  KEY `ix_transactions_category` (`category_id`),
  KEY `fk_transactions_recurring` (`recurring_rule_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `transfers`
--

CREATE TABLE IF NOT EXISTS `transfers` (
  `transfer_id` varchar(64) NOT NULL,
  `user_id` varchar(64) NOT NULL,
  `from_wallet_id` varchar(64) NOT NULL,
  `to_wallet_id` varchar(64) NOT NULL,
  `amount` decimal(14,2) NOT NULL,
  `transfer_date` date NOT NULL,
  `note` text DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`transfer_id`),
  KEY `ix_transfers_user_date` (`user_id`,`transfer_date`),
  KEY `fk_transfers_from_wallet` (`from_wallet_id`),
  KEY `fk_transfers_to_wallet` (`to_wallet_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `user_id` varchar(64) NOT NULL,
  `username` varchar(50) NOT NULL,
  `full_name` varchar(100) NOT NULL,
  `email` varchar(150) NOT NULL,
  `password_hash` varchar(255) NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `uq_users_username` (`username`),
  UNIQUE KEY `uq_users_email` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `user_preferences`
--

CREATE TABLE IF NOT EXISTS `user_preferences` (
  `user_id` varchar(64) NOT NULL,
  `dark_theme` tinyint(1) NOT NULL DEFAULT 1,
  `updated_at` datetime NOT NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Table structure for table `wallets`
--

CREATE TABLE IF NOT EXISTS `wallets` (
  `wallet_id` varchar(64) NOT NULL,
  `user_id` varchar(64) NOT NULL,
  `wallet_name` varchar(100) NOT NULL,
  `provider` varchar(50) NOT NULL DEFAULT 'Cash',
  `wallet_type` varchar(20) NOT NULL DEFAULT 'cash',
  `opening_balance` decimal(14,2) NOT NULL DEFAULT 0.00,
  `color` char(7) NOT NULL DEFAULT '#16bd8b',
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `is_default` tinyint(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`wallet_id`),
  UNIQUE KEY `uq_wallet_user_name` (`user_id`,`wallet_name`),
  KEY `ix_wallets_user` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `budgets`
--
ALTER TABLE `budgets`
  ADD CONSTRAINT `fk_budgets_category` FOREIGN KEY (`category_id`) REFERENCES `categories` (`category_id`),
  ADD CONSTRAINT `fk_budgets_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE;

--
-- Constraints for table `categories`
--
ALTER TABLE `categories`
  ADD CONSTRAINT `fk_categories_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE;

--
-- Constraints for table `goal_contributions`
--
ALTER TABLE `goal_contributions`
  ADD CONSTRAINT `fk_contributions_goal` FOREIGN KEY (`goal_id`) REFERENCES `savings_goals` (`goal_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `fk_contributions_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE;

--
-- Constraints for table `recurring_occurrences`
--
ALTER TABLE `recurring_occurrences`
  ADD CONSTRAINT `fk_occurrences_rule` FOREIGN KEY (`rule_id`) REFERENCES `recurring_rules` (`rule_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `fk_occurrences_transaction` FOREIGN KEY (`transaction_id`) REFERENCES `transactions` (`transaction_id`) ON DELETE SET NULL;

--
-- Constraints for table `recurring_rules`
--
ALTER TABLE `recurring_rules`
  ADD CONSTRAINT `fk_recurring_category` FOREIGN KEY (`category_id`) REFERENCES `categories` (`category_id`) ON DELETE SET NULL,
  ADD CONSTRAINT `fk_recurring_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `fk_recurring_wallet` FOREIGN KEY (`wallet_id`) REFERENCES `wallets` (`wallet_id`);

--
-- Constraints for table `savings_goals`
--
ALTER TABLE `savings_goals`
  ADD CONSTRAINT `fk_goals_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE;

--
-- Constraints for table `transactions`
--
ALTER TABLE `transactions`
  ADD CONSTRAINT `fk_transactions_category` FOREIGN KEY (`category_id`) REFERENCES `categories` (`category_id`) ON DELETE SET NULL,
  ADD CONSTRAINT `fk_transactions_recurring` FOREIGN KEY (`recurring_rule_id`) REFERENCES `recurring_rules` (`rule_id`) ON DELETE SET NULL,
  ADD CONSTRAINT `fk_transactions_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `fk_transactions_wallet` FOREIGN KEY (`wallet_id`) REFERENCES `wallets` (`wallet_id`);

--
-- Constraints for table `transfers`
--
ALTER TABLE `transfers`
  ADD CONSTRAINT `fk_transfers_from_wallet` FOREIGN KEY (`from_wallet_id`) REFERENCES `wallets` (`wallet_id`),
  ADD CONSTRAINT `fk_transfers_to_wallet` FOREIGN KEY (`to_wallet_id`) REFERENCES `wallets` (`wallet_id`),
  ADD CONSTRAINT `fk_transfers_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE;

--
-- Constraints for table `user_preferences`
--
ALTER TABLE `user_preferences`
  ADD CONSTRAINT `fk_preferences_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE;

--
-- Constraints for table `wallets`
--
ALTER TABLE `wallets`
  ADD CONSTRAINT `fk_wallets_user` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
