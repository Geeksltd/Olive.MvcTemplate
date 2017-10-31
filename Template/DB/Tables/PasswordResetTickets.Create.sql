-- PasswordResetTickets Table ========================
CREATE TABLE [PasswordResetTickets] (
    [Id] uniqueidentifier PRIMARY KEY NONCLUSTERED,
    [DateCreated] datetime2  NOT NULL,
    [IsUsed] bit  NOT NULL,
    [User] uniqueidentifier  NOT NULL
)
CREATE INDEX [IX_PasswordResetTickets->User] ON [PasswordResetTickets] ([User])

GO

