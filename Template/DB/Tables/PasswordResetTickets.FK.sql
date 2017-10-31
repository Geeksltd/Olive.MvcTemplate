ALTER TABLE [PasswordResetTickets] ADD Constraint
                []
                FOREIGN KEY ([User])
                REFERENCES [Users] ([ID])
                ON DELETE NO ACTION 
GO