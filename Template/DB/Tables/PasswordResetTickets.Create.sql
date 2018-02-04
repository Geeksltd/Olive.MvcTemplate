-- PasswordResetTickets Table ========================
CREATE TABLE PasswordResetTickets (
    Id uniqueidentifier PRIMARY KEY NONCLUSTERED,
    [User] uniqueidentifier  NOT NULL,
    DateCreated datetime2  NOT NULL,
    IsUsed bit  NOT NULL
);
CREATE INDEX [IX_PasswordResetTickets->User] ON PasswordResetTickets ([User]);

GO

