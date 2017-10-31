-- ApplicationEvents Table ========================
CREATE TABLE [ApplicationEvents] (
    [Id] uniqueidentifier PRIMARY KEY NONCLUSTERED,
    [UserId] nvarchar(200)  NULL,
    [Date] datetime2  NOT NULL,
    [Event] nvarchar(200)  NOT NULL,
    [ItemType] nvarchar(200)  NULL,
    [ItemKey] nvarchar(500)  NULL,
    [Data] nvarchar(MAX)  NULL,
    [IP] nvarchar(200)  NULL
)

