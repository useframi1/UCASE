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
    Logo BYTEA,
    FOREIGN KEY (Gov_Name, Area) REFERENCES governorates(gov_name, area)
    ON UPDATE CASCADE
    ON DELETE SET NULL
);

CREATE TABLE IF NOT EXISTS Majors
(
	Major_Name VARCHAR(200) PRIMARY KEY,
    Major_Requirements TEXT
);

CREATE TABLE IF NOT EXISTS University_Majors
(
	Uni_Name VARCHAR(200),
    Major_Name VARCHAR(200),
    PRIMARY KEY (Uni_Name, Major_Name), 
    FOREIGN KEY (Uni_Name) REFERENCES Universities(Uni_Name)
    ON UPDATE CASCADE
    ON DELETE CASCADE,
    FOREIGN KEY (Major_Name) REFERENCES Majors(Major_Name)
    ON UPDATE CASCADE
    ON DELETE CASCADE
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
    ON DELETE CASCADE
);

-- NOT DONE
CREATE TABLE IF NOT EXISTS Application
(
	Email VARCHAR(100),
    Application_Date DATETIME,
    Semester ENUM("Fall", "Spring"),
    App_Year INT,
    Education_System VARCHAR(100),
    GPA DECIMAL(3,2),
    School VARCHAR(100),
    PRIMARY KEY (Email, Application_Date), 
    FOREIGN KEY (Email) REFERENCES Applicant (Email)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Application_References
(
	Email VARCHAR(100),
    Application_Date DATETIME,
    Reference_Email VARCHAR(100),
	PRIMARY KEY (Email, Application_Date,Reference_Email),
    FOREIGN KEY (Email, Application_Date) REFERENCES Application (Email, Application_Date)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Application_ExtraCurricular
(
	Email VARCHAR(100),
    Application_Date DATETIME,
    Extra_Curricular VARCHAR(100),
	PRIMARY KEY (Email, Application_Date,Extra_Curricular),
    FOREIGN KEY (Email, Application_Date) REFERENCES Application (Email, Application_Date)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Application_Major
(
	Email VARCHAR(100),
    Application_Date DATETIME,
    Major_Name VARCHAR(100),
	PRIMARY KEY (Email, Application_Date,Major_Name),
    FOREIGN KEY (Email, Application_Date) REFERENCES Application (Email, Application_Date)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

-- ????
CREATE TABLE IF NOT EXISTS University_Application
(
	Uni_Name VARCHAR(200),
	Email VARCHAR(100),
    Application_Date DATETIME,
    Major VARCHAR(100),
	PRIMARY KEY (Uni_Name, Email, Application_Date,Major),
    FOREIGN KEY (Uni_Name) REFERENCES University (Uni_Name)
    ON UPDATE CASCADE
    ON DELETE CASCADE,
    FOREIGN KEY (Email, Application_Date) REFERENCES Application (Email, Application_Date)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Certificates
(
	Email VARCHAR(100),
    Application_Date DATETIME,
    Certificate_Name VARCHAR(100),
    Test_Date DATE,
    Score VARCHAR(100),
    Certificate_Photo BYTEA,
	PRIMARY KEY (Email, Application_Date,Test_Name,Test_Date),
    FOREIGN KEY (Email, Application_Date) REFERENCES Application (Email, Application_Date)
    ON UPDATE CASCADE
    ON DELETE CASCADE
);

