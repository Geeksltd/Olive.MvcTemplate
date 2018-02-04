-- Settings Table ========================
CREATE TABLE Settings (
    Id uniqueidentifier PRIMARY KEY NONCLUSTERED,
    Name nvarchar(200)  NOT NULL,
    PasswordResetTicketExpiryMinutes int  NOT NULL,
    CacheVersion int  NOT NULL
);

