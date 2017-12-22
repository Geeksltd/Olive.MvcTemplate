ALTER TABLE ContentBlocks ADD Constraint
                [FK_ContentBlock.Template->EmailTemplate]
                FOREIGN KEY (Template)
                REFERENCES EmailTemplates (ID)
                ON DELETE NO ACTION 
GO