-- EmailQueueItems Table ========================
CREATE TABLE [EmailQueueItems] (
    [.Deleted] bit NOT NULL,
    Id uniqueidentifier PRIMARY KEY NONCLUSTERED,
    [Body] nvarchar(MAX)  NULL,
    [Date] datetime2  NOT NULL,
    [EnableSsl] bit  NOT NULL,
    [Html] bit  NOT NULL,
    [SenderAddress] nvarchar(200)  NULL,
    [SenderName] nvarchar(200)  NULL,
    [Subject] nvarchar(200)  NOT NULL,
    [To] nvarchar(200)  NULL,
    [Attachments] nvarchar(200)  NULL,
    [Bcc] nvarchar(200)  NULL,
    [Cc] nvarchar(200)  NULL,
    [Retries] int  NOT NULL,
    [VCalendarView] nvarchar(200)  NULL,
    [Username] nvarchar(200)  NULL,
    [Password] nvarchar(200)  NULL,
    [SmtpHost] nvarchar(200)  NULL,
    [SmtpPort] int  NULL,
    [Category] nvarchar(200)  NULL
)

