-- ContentBlocks Table ========================
CREATE TABLE ContentBlocks (
    Id uniqueidentifier PRIMARY KEY NONCLUSTERED,
    [Key] nvarchar(200)  NOT NULL,
    Content nvarchar(MAX)  NOT NULL,
    Template uniqueidentifier  NULL
)
CREATE INDEX [IX_ContentBlocks->Template] ON ContentBlocks (Template)

GO

