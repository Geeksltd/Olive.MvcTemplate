-- ContentBlockThings Table ========================
CREATE TABLE [ContentBlockThings] (
    [Id] uniqueidentifier PRIMARY KEY NONCLUSTERED,
    [Block] uniqueidentifier  NULL,
    [Name] nvarchar(200)  NOT NULL
)
CREATE INDEX [IX_ContentBlockThings->Block] ON [ContentBlockThings] ([Block])

GO

