-- AuditEvents Table ========================
CREATE TABLE AuditEvents (
    Id uniqueidentifier PRIMARY KEY NONCLUSTERED,
    UserId nvarchar(200)  NULL,
    [Date] datetime2  NOT NULL,
    Event nvarchar(200)  NOT NULL,
    ItemType nvarchar(200)  NULL,
    ItemId nvarchar(500)  NULL,
    ItemData nvarchar(MAX)  NULL,
    UserIp nvarchar(200)  NULL
);

