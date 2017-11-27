-- ContentBlocks Table ========================
CREATE TABLE [ContentBlocks] (
    [Id] uniqueidentifier PRIMARY KEY NONCLUSTERED,
    [Key] nvarchar(200)  NOT NULL,
    [Content] nvarchar(MAX)  NOT NULL,
    [Myfile_FileName] nvarchar(1500)  NULL
)

