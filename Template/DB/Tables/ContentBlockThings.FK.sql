ALTER TABLE [ContentBlockThings] ADD Constraint
                [FK_ContentBlockThing.Block->ContentBlock]
                FOREIGN KEY ([Block])
                REFERENCES [ContentBlocks] ([ID])
                ON DELETE NO ACTION 
GO