IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Cities] (
    [Id] int NOT NULL IDENTITY,
    [Population] int NOT NULL,
    [CityName] nvarchar(max) NULL,
    CONSTRAINT [PK_Cities] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [CaseDatas] (
    [Id] int NOT NULL IDENTITY,
    [Date] datetime2 NOT NULL,
    [Cases] int NOT NULL,
    [Deaths] int NOT NULL,
    [Tested] int NOT NULL,
    [CityId] int NULL,
    CONSTRAINT [PK_CaseDatas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CaseDatas_Cities_CityId] FOREIGN KEY ([CityId]) REFERENCES [Cities] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_CaseDatas_CityId] ON [CaseDatas] ([CityId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200730025934_initialschema', N'3.1.6');

GO

