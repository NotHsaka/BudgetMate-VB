-- BudgetMate MariaDB schema v2
-- Designed to fit the current VB.NET models in BudgetMate-VB-main.
-- IDs are VARCHAR(64): the app uses GUID strings, default wallet IDs
-- ("cash", "gcash", "maya", "bank"), and recurring IDs prefixed with "repeat-".
-- Safe starting point: creates a NEW database named budgetmate_app.
-- Does not drop or overwrite the user's existing `budgetmate` database.

CREATE DATABASE IF NOT EXISTS budgetmate_app
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE budgetmate_app;

CREATE TABLE IF NOT EXISTS users (
    user_id VARCHAR(64) NOT NULL,
    username VARCHAR(50) NOT NULL,
    full_name VARCHAR(100) NOT NULL,
    email VARCHAR(150) NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (user_id),
    UNIQUE KEY uq_users_username (username),
    UNIQUE KEY uq_users_email (email)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS wallets (
    wallet_id VARCHAR(64) NOT NULL,
    user_id VARCHAR(64) NOT NULL,
    wallet_name VARCHAR(100) NOT NULL,
    provider VARCHAR(50) NOT NULL DEFAULT 'Cash',
    wallet_type VARCHAR(20) NOT NULL DEFAULT 'cash',
    opening_balance DECIMAL(14,2) NOT NULL DEFAULT 0.00,
    color CHAR(7) NOT NULL DEFAULT '#16bd8b',
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (wallet_id),
    UNIQUE KEY uq_wallet_user_name (user_id, wallet_name),
    KEY ix_wallets_user (user_id),
    CONSTRAINT fk_wallets_user FOREIGN KEY (user_id)
      REFERENCES users(user_id) ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS categories (
    category_id VARCHAR(64) NOT NULL,
    user_id VARCHAR(64) NULL,
    category_name VARCHAR(100) NOT NULL,
    category_type ENUM('income','expense') NOT NULL,
    color CHAR(7) NULL,
    default_monthly_limit DECIMAL(14,2) NULL,
    is_default BOOLEAN NOT NULL DEFAULT FALSE,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (category_id),
    UNIQUE KEY uq_category_owner_name_type (user_id, category_name, category_type),
    KEY ix_categories_user_type (user_id, category_type),
    CONSTRAINT fk_categories_user FOREIGN KEY (user_id)
      REFERENCES users(user_id) ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS recurring_rules (
    rule_id VARCHAR(64) NOT NULL,
    user_id VARCHAR(64) NOT NULL,
    title VARCHAR(150) NOT NULL,
    transaction_type ENUM('income','expense') NOT NULL,
    amount DECIMAL(14,2) NOT NULL,
    category_id VARCHAR(64) NULL,
    wallet_id VARCHAR(64) NOT NULL,
    payment_method VARCHAR(50) NOT NULL DEFAULT 'Cash',
    notes TEXT NULL,
    frequency ENUM('monthly') NOT NULL DEFAULT 'monthly',
    day_of_month TINYINT UNSIGNED NOT NULL DEFAULT 1,
    start_date DATE NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (rule_id),
    KEY ix_recurring_user_active (user_id, is_active),
    CONSTRAINT fk_recurring_user FOREIGN KEY (user_id)
      REFERENCES users(user_id) ON DELETE CASCADE,
    CONSTRAINT fk_recurring_category FOREIGN KEY (category_id)
      REFERENCES categories(category_id) ON DELETE SET NULL,
    CONSTRAINT fk_recurring_wallet FOREIGN KEY (wallet_id)
      REFERENCES wallets(wallet_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS transactions (
    transaction_id VARCHAR(64) NOT NULL,
    user_id VARCHAR(64) NOT NULL,
    wallet_id VARCHAR(64) NOT NULL,
    category_id VARCHAR(64) NULL,
    recurring_rule_id VARCHAR(64) NULL,
    transaction_type ENUM('income','expense') NOT NULL,
    title VARCHAR(150) NOT NULL,
    amount DECIMAL(14,2) NOT NULL,
    transaction_date DATE NOT NULL,
    payment_method VARCHAR(50) NOT NULL DEFAULT 'Cash',
    notes TEXT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (transaction_id),
    KEY ix_transactions_user_date (user_id, transaction_date),
    KEY ix_transactions_user_type_date (user_id, transaction_type, transaction_date),
    KEY ix_transactions_wallet (wallet_id),
    KEY ix_transactions_category (category_id),
    CONSTRAINT fk_transactions_user FOREIGN KEY (user_id)
      REFERENCES users(user_id) ON DELETE CASCADE,
    CONSTRAINT fk_transactions_wallet FOREIGN KEY (wallet_id)
      REFERENCES wallets(wallet_id) ON DELETE RESTRICT,
    CONSTRAINT fk_transactions_category FOREIGN KEY (category_id)
      REFERENCES categories(category_id) ON DELETE SET NULL,
    CONSTRAINT fk_transactions_recurring FOREIGN KEY (recurring_rule_id)
      REFERENCES recurring_rules(rule_id) ON DELETE SET NULL
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS budgets (
    budget_id VARCHAR(64) NOT NULL,
    user_id VARCHAR(64) NOT NULL,
    category_id VARCHAR(64) NOT NULL,
    budget_month DATE NOT NULL COMMENT 'First day of the budget month',
    spending_limit DECIMAL(14,2) NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (budget_id),
    UNIQUE KEY uq_budget_user_category_month (user_id, category_id, budget_month),
    KEY ix_budgets_user_month (user_id, budget_month),
    CONSTRAINT fk_budgets_user FOREIGN KEY (user_id)
      REFERENCES users(user_id) ON DELETE CASCADE,
    CONSTRAINT fk_budgets_category FOREIGN KEY (category_id)
      REFERENCES categories(category_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS savings_goals (
    goal_id VARCHAR(64) NOT NULL,
    user_id VARCHAR(64) NOT NULL,
    goal_name VARCHAR(150) NOT NULL,
    target_amount DECIMAL(14,2) NOT NULL,
    current_amount DECIMAL(14,2) NOT NULL DEFAULT 0.00,
    deadline DATE NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (goal_id),
    KEY ix_goals_user (user_id),
    CONSTRAINT fk_goals_user FOREIGN KEY (user_id)
      REFERENCES users(user_id) ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS goal_contributions (
    contribution_id VARCHAR(64) NOT NULL,
    goal_id VARCHAR(64) NOT NULL,
    user_id VARCHAR(64) NOT NULL,
    amount DECIMAL(14,2) NOT NULL,
    contribution_date DATE NOT NULL,
    note VARCHAR(255) NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (contribution_id),
    KEY ix_contributions_goal_date (goal_id, contribution_date),
    CONSTRAINT fk_contributions_goal FOREIGN KEY (goal_id)
      REFERENCES savings_goals(goal_id) ON DELETE CASCADE,
    CONSTRAINT fk_contributions_user FOREIGN KEY (user_id)
      REFERENCES users(user_id) ON DELETE CASCADE
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS transfers (
    transfer_id VARCHAR(64) NOT NULL,
    user_id VARCHAR(64) NOT NULL,
    from_wallet_id VARCHAR(64) NOT NULL,
    to_wallet_id VARCHAR(64) NOT NULL,
    amount DECIMAL(14,2) NOT NULL,
    transfer_date DATE NOT NULL,
    note TEXT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (transfer_id),
    KEY ix_transfers_user_date (user_id, transfer_date),
    CONSTRAINT fk_transfers_user FOREIGN KEY (user_id)
      REFERENCES users(user_id) ON DELETE CASCADE,
    CONSTRAINT fk_transfers_from_wallet FOREIGN KEY (from_wallet_id)
      REFERENCES wallets(wallet_id) ON DELETE RESTRICT,
    CONSTRAINT fk_transfers_to_wallet FOREIGN KEY (to_wallet_id)
      REFERENCES wallets(wallet_id) ON DELETE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS recurring_occurrences (
    occurrence_id VARCHAR(64) NOT NULL,
    rule_id VARCHAR(64) NOT NULL,
    occurrence_month CHAR(7) NOT NULL COMMENT 'YYYY-MM; one generated transaction per rule per month',
    occurrence_date DATE NOT NULL,
    transaction_id VARCHAR(64) NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (occurrence_id),
    UNIQUE KEY uq_recurring_rule_month (rule_id, occurrence_month),
    UNIQUE KEY uq_occurrence_transaction (transaction_id),
    CONSTRAINT fk_occurrences_rule FOREIGN KEY (rule_id)
      REFERENCES recurring_rules(rule_id) ON DELETE CASCADE,
    CONSTRAINT fk_occurrences_transaction FOREIGN KEY (transaction_id)
      REFERENCES transactions(transaction_id) ON DELETE SET NULL
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS user_preferences (
    user_id VARCHAR(64) NOT NULL,
    dark_theme BOOLEAN NOT NULL DEFAULT TRUE,
    updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (user_id),
    CONSTRAINT fk_preferences_user FOREIGN KEY (user_id)
      REFERENCES users(user_id) ON DELETE CASCADE
) ENGINE=InnoDB;

-- Default categories match the current VB.NET Catalog.
INSERT INTO categories
    (category_id, user_id, category_name, category_type, color, default_monthly_limit, is_default)
VALUES
    ('cat_exp_housing', NULL, 'Housing', 'expense', '#4d91ff', 12000.00, TRUE),
    ('cat_exp_food', NULL, 'Food', 'expense', '#ff5b75', 5000.00, TRUE),
    ('cat_exp_transport', NULL, 'Transportation', 'expense', '#ffb11b', 2500.00, TRUE),
    ('cat_exp_utilities', NULL, 'Utilities', 'expense', '#15b8ce', 3000.00, TRUE),
    ('cat_exp_entertainment', NULL, 'Entertainment', 'expense', '#9365ff', 2000.00, TRUE),
    ('cat_exp_health', NULL, 'Health', 'expense', '#20bd8b', 1500.00, TRUE),
    ('cat_exp_shopping', NULL, 'Shopping', 'expense', '#f07845', 2000.00, TRUE),
    ('cat_exp_misc', NULL, 'Miscellaneous', 'expense', '#8492a6', 1500.00, TRUE),
    ('cat_inc_salary', NULL, 'Salary', 'income', NULL, NULL, TRUE),
    ('cat_inc_freelance', NULL, 'Freelance', 'income', NULL, NULL, TRUE),
    ('cat_inc_investment', NULL, 'Investment', 'income', NULL, NULL, TRUE),
    ('cat_inc_allowance', NULL, 'Allowance', 'income', NULL, NULL, TRUE),
    ('cat_inc_gift', NULL, 'Gift', 'income', NULL, NULL, TRUE),
    ('cat_inc_other', NULL, 'Other income', 'income', NULL, NULL, TRUE)
ON DUPLICATE KEY UPDATE
    category_name = VALUES(category_name),
    category_type = VALUES(category_type),
    color = VALUES(color),
    default_monthly_limit = VALUES(default_monthly_limit),
    is_default = VALUES(is_default);
