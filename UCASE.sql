CREATE TABLE IF NOT EXISTS Governorates
(
	Gov_Name VARCHAR(200),
    Area VARCHAR(200),
    PRIMARY KEY(Gov_Name, Area)
);

CREATE TABLE IF NOT EXISTS Universities
(
	Uni_Name VARCHAR(200) PRIMARY KEY,
    Description TEXT,
    Ranking INT,
    Acceptance_Rate FLOAT,
    Link VARCHAR(200),
    Gov_Name VARCHAR(200),
    Area VARCHAR(200),
    General_Requirements TEXT,
    Logo BYTEA
);

CREATE TABLE IF NOT EXISTS Users
(
	Email VARCHAR(100) PRIMARY KEY,
    Password_Hash BYTEA,
    Password_Salt BYTEA,
    First_Name VARCHAR(100),
    Last_Name VARCHAR(100),
    DOB DATE,
    Phone_No CHAR(11),
    Address_Line1 VARCHAR(200),
	Address_Line2 VARCHAR(200),
    Nationality VARCHAR(100),
    Gender CHAR(1),
    Gov_Name VARCHAR(200),
    Area VARCHAR(200),
    Start_Uni INT
);

CREATE TABLE IF NOT EXISTS Preferred_Subjects
(
	Email VARCHAR(100),
    Subject VARCHAR(200),
    PRIMARY KEY (Email, Subject), 
    FOREIGN KEY (Email) REFERENCES Users(Email)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Preferred_Industries
(
	Email VARCHAR(100),
    Industry VARCHAR(200),
    PRIMARY KEY (Email, Industry), 
    FOREIGN KEY (Email) REFERENCES Users(Email)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Favorite_Universities
(
	Email VARCHAR(100),
    University VARCHAR(200),
    PRIMARY KEY (Email, University), 
    FOREIGN KEY (Email) REFERENCES Users (Email)
    ON UPDATE CASCADE
    ON DELETE CASCADE,
    FOREIGN KEY (University) REFERENCES Universities (Uni_Name)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Subjects
(
    Subject_Name VARCHAR(200) PRIMARY KEY
);

CREATE TABLE IF NOT EXISTS Industries
(
    Industry_Name VARCHAR(200) PRIMARY KEY
);

CREATE TABLE IF NOT EXISTS Applications
(
	Email VARCHAR(100),
    National_ID BYTEA,
    Passport BYTEA,
    Personal_Statement BYTEA,
    Guardian_Name varchar(100),
    Guardian_Profession varchar(100),
    Guardian_Company varchar(100),
	Guardian_Number char(11),
    Guardian_Email varchar(100),
	School_Country varchar(100),
	School_City varchar(100),
	School_Name varchar(100),
	Year_Of_Graduation INT,
    Birth_Certificate BYTEA,
	Transcript BYTEA,
	Recommendation_Letter BYTEA, 
	Personal_Photo BYTEA,
	Military_Form_2 BYTEA,
    Military_Form_6 BYTEA,
    Residency_Copy BYTEA,
    PRIMARY KEY (Email),
    FOREIGN KEY (Email) REFERENCES Users(Email)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS University_Choices
(
	Uni_Name VARCHAR(200),
	Email VARCHAR(100),
	PRIMARY KEY (Uni_Name, Email),
    FOREIGN KEY (Uni_Name) REFERENCES Universities(Uni_Name)
    ON UPDATE CASCADE
    ON DELETE CASCADE,
    FOREIGN KEY (Email) REFERENCES Applications(Email)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Certificates
(
	Email VARCHAR(100),
    Certificate_Name VARCHAR(100),
    Certificate_Photo BYTEA,
	PRIMARY KEY (Email, Certificate_Name),
    FOREIGN KEY (Email) REFERENCES Applications(Email)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);