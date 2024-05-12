CREATE DATABASE UCASE;

CREATE TABLE IF NOT EXISTS Governorate 
(
	Gov_Name VARCHAR(200),
    Area VARCHAR(200),
    PRIMARY KEY(Gov_Name, Area)
);

CREATE TABLE IF NOT EXISTS University
(
	Uni_Name VARCHAR(200) PRIMARY KEY,
    Description TEXT,
    Ranking INT,
    Acceptance_Rate FLOAT,
    Link VARCHAR(200),
    Gov_Name VARCHAR(200),
    Area VARCHAR(200),
    General_Requirements TEXT,
    Logo BYTEA,
    FOREIGN KEY (Gov_Name, Area) REFERENCES governorate(gov_name, area)
    ON UPDATE CASCADE
    ON DELETE SET NULL
);

CREATE TABLE IF NOT EXISTS Majors
(
	Major_Name VARCHAR(200) PRIMARY KEY,
    Major_Requirements TEXT
);

CREATE TABLE IF NOT EXISTS UniversityMajors
(
	Uni_Name VARCHAR(200),
    Major_Name VARCHAR(200),
    PRIMARY KEY (Uni_Name, Major), 
    FOREIGN KEY (Uni_Name) REFERENCES University (Uni_Name)
    ON UPDATE CASCADE
    ON DELETE CASCADE,
    FOREIGN KEY (Major_Name) REFERENCES Majors(Major_Name)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Users
(
	Email VARCHAR(100) PRIMARY KEY,
    PasswordHash BYTEA,
    PasswordSalt BYTEA,
    First_Name VARCHAR(100),
    Last_Name VARCHAR(100),
    DOB DATE,
    PhoneNo CHAR(11),
    AddressLine1 VARCHAR(200),
	AddressLine2 VARCHAR(200),
    Nationality VARCHAR(100),
    Gender CHAR(1),
    Gov_Name VARCHAR(200),
    Area VARCHAR(200),
	FOREIGN KEY (Gov_Name, Area) REFERENCES Governorate(Gov_Name, Area)
    ON UPDATE CASCADE
    ON DELETE SET NULL
);

CREATE TABLE IF NOT EXISTS User_Subjects
(
	Email VARCHAR(100),
    Subject VARCHAR(200),
    PRIMARY KEY (Email, Subject), 
    FOREIGN KEY (Email) REFERENCES Users(Email)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS User_Industry
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
    ON DELETE CASCADE
);

-- NOT DONE
CREATE TABLE IF NOT EXISTS Application
(
	Email VARCHAR(100),
    ApplicationDate DATETIME,
    Semester ENUM("Fall", "Spring"),
    App_Year INT,
    Education_System VARCHAR(100),
    GPA DECIMAL(3,2),
    School VARCHAR(100),
    PRIMARY KEY (Email, ApplicationDate), 
    FOREIGN KEY (Email) REFERENCES Applicant (Email)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Application_References
(
	Email VARCHAR(100),
    ApplicationDate DATETIME,
    Reference_Email VARCHAR(100),
	PRIMARY KEY (Email, ApplicationDate,Reference_Email),
    FOREIGN KEY (Email, ApplicationDate) REFERENCES Application (Email, ApplicationDate)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Application_ExtraCurricular
(
	Email VARCHAR(100),
    ApplicationDate DATETIME,
    ExtraCurricular VARCHAR(100),
	PRIMARY KEY (Email, ApplicationDate,ExtraCurricular),
    FOREIGN KEY (Email, ApplicationDate) REFERENCES Application (Email, ApplicationDate)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Application_Major
(
	Email VARCHAR(100),
    ApplicationDate DATETIME,
    Major_Name VARCHAR(100),
	PRIMARY KEY (Email, ApplicationDate,Major_Name),
    FOREIGN KEY (Email, ApplicationDate) REFERENCES Application (Email, ApplicationDate)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

-- ????
CREATE TABLE IF NOT EXISTS University_Application
(
	Uni_Name VARCHAR(200),
	Email VARCHAR(100),
    ApplicationDate DATETIME,
    Major VARCHAR(100),
	PRIMARY KEY (Uni_Name, Email, ApplicationDate,Major),
    FOREIGN KEY (Uni_Name) REFERENCES University (Uni_Name)
    ON UPDATE CASCADE
    ON DELETE CASCADE,
    FOREIGN KEY (Email, ApplicationDate) REFERENCES Application (Email, ApplicationDate)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Certificates
(
	Email VARCHAR(100),
    ApplicationDate DATETIME,
    Certificate_Name VARCHAR(100),
    Test_Date DATE,
    Score VARCHAR(100),
    Certificate_Photo BYTEA,
	PRIMARY KEY (Email, ApplicationDate,Test_Name,Test_Date),
    FOREIGN KEY (Email, ApplicationDate) REFERENCES Application (Email, ApplicationDate)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

