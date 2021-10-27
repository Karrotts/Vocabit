DROP DATABASE IF EXISTS Vocabit;
CREATE DATABASE Vocabit;

USING Vocabit;

CREATE TABLE words
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	term VARCHAR(100)
);

CREATE TABLE definitions
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	word_id INT REFERENCES words (id),
	description NVARCHAR(MAX),
	type VARCHAR(20)
);

SELECT * FROM words;
SELECT * FROM definitions;

SELECT * FROM words as w
INNER JOIN definitions AS d
ON w.id = d.word_id;

CREATE TABLE words
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	term VARCHAR(100)
);

CREATE TABLE definitions
(
	id INT IDENTITY(1,1) PRIMARY KEY,
	word_id INT REFERENCES words (id),
	description NVARCHAR(MAX),
	type VARCHAR(20)
);

drop table definitions;
drop table words;