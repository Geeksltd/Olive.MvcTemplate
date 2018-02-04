-- LogonFailures Table ========================
CREATE TABLE LogonFailures (
    Id uniqueidentifier PRIMARY KEY NONCLUSTERED,
    Email nvarchar(200)  NOT NULL,
    IP nvarchar(200)  NOT NULL,
    Attempts int  NOT NULL,
    [Date] datetime2  NOT NULL
);

