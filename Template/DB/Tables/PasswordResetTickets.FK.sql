ALTER TABLE PasswordResetTickets ADD Constraint
                [FK_PasswordResetTicket.User]
                FOREIGN KEY ([User])
                REFERENCES Users (ID)
                ON DELETE NO ACTION;
GO