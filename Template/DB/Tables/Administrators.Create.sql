-- Administrators Table ========================
CREATE TABLE Administrators (
    Id uniqueidentifier PRIMARY KEY NONCLUSTERED,
    ImpersonationToken nvarchar(40)  NULL
);

