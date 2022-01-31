BEGIN TRANSACTION;
GO

ALTER TABLE [TblProductOptions] DROP CONSTRAINT [FK_TblProductOptions_TblOptions_OptionRefId];
GO

DROP TABLE [TblDeliverableLocations];
GO

DROP TABLE [TblOptions];
GO

DROP TABLE [TblOptionGroups];
GO

DROP INDEX [IX_TblProductOptions_OptionRefId] ON [TblProductOptions];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TblProductOptions]') AND [c].[name] = N'OptionRefId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [TblProductOptions] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [TblProductOptions] DROP COLUMN [OptionRefId];
GO

ALTER TABLE [TblProductOptions] ADD [OptionName] nvarchar(max) NULL;
GO

ALTER TABLE [TblOrderItemDetails] ADD [ProductOptionId] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210304132917_updatetableoption', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[TblProducts]') AND [c].[name] = N'Size');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [TblProducts] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [TblProducts] DROP COLUMN [Size];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210304164421_removeproductsize', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [TblCartItems] ADD [ProductOptionRefId] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210306044840_updatetblcartitem', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [TblRefreshTokens] (
    [Id] bigint NOT NULL IDENTITY,
    [Token] nvarchar(max) NULL,
    [Expires] datetime2 NOT NULL,
    [Created] datetime2 NOT NULL,
    [Revoked] datetime2 NULL,
    [UserRefId] bigint NOT NULL,
    CONSTRAINT [PK_TblRefreshTokens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_TblRefreshTokens_TblUsers_UserRefId] FOREIGN KEY ([UserRefId]) REFERENCES [TblUsers] ([UserId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_TblRefreshTokens_UserRefId] ON [TblRefreshTokens] ([UserRefId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210312135242_refreshToken', N'5.0.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [TblRefreshTokens] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210312143628_UpdaterefreshToken', N'5.0.2');
GO

COMMIT;
GO

