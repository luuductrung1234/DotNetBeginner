CREATE DATABASE MyFirstEfCoreAppDb
GO

USE MyFirstEfCoreAppDb
GO

CREATE TABLE [Authors]
(
    [AuthorId] INTEGER IDENTITY,
    [Name] VARCHAR(255) NOT NULL,
    [WebUrl] VARCHAR(500),
    PRIMARY KEY (AuthorId)
)
GO

CREATE TABLE [Books]
(
    [BookId] INTEGER IDENTITY,
    [Title] VARCHAR(255) NOT NULL,
    [Description] VARCHAR(500),
    [PublishedOn] DATETIME,
    [AuthorId] INTEGER NOT NULL,
    PRIMARY KEY (BookId),
    FOREIGN KEY (AuthorId) REFERENCES Authors(AuthorId)
)
GO

INSERT INTO [Authors]
    ([Name], [WebUrl])
VALUES
    ('Martin Fowler', null),
    ('Eric Evans', null),
    ('Future Person', null)
GO

INSERT INTO [Books]
    ([Title], [Description], [PublishedOn], [AuthorId])
VALUES
    ('Refactoring', '', '08-Jul-1999', 1),
    ('Patterns of Enterprise Application', '', '15-Nov-2002', 1),
    ('Domain-Driven Design', '', '30-Aug-2003', 2),
    ('Quantum Networking', '', '01-Jan-2021', 3)
GO