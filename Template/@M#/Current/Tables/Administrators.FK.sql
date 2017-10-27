ALTER TABLE [Administrators] ADD CONSTRAINT 
[FK_Administrator.Id->User] FOREIGN KEY([Id]) 
REFERENCES [Users] ([Id])
 ON DELETE CASCADE


GO