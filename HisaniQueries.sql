CREATE DATABASE HisaniDB;

USE HisaniDB;

-- Create the Users table
CREATE TABLE Users (
    Email VARCHAR(255) PRIMARY KEY,
    Password VARCHAR(255) NOT NULL,
    TypeOfUser VARCHAR(10) NOT NULL CHECK (TypeOfUser IN ('Trainer', 'Vet')) -- Using CHECK constraint for 'Trainer' or 'Vet'
);

-- Create the Stable table
CREATE TABLE Stable (
    StableID INT IDENTITY(1,1) PRIMARY KEY, -- Using IDENTITY for auto-increment
    StableName VARCHAR(255) NOT NULL,
    Location VARCHAR(255) NOT NULL,
    Capacity INT NOT NULL,
    TrainerEmail VARCHAR(255),
    VetEmail VARCHAR(255),
    FOREIGN KEY (TrainerEmail) REFERENCES Users(Email),
    FOREIGN KEY (VetEmail) REFERENCES Users(Email)
);

-- Create the Vet table
CREATE TABLE Vet (
    Email VARCHAR(255),
    VetName VARCHAR(255) NOT NULL,
    Speciality VARCHAR(255),
    Contact VARCHAR(50),
    StableID INT,
    PRIMARY KEY (Email),
    FOREIGN KEY (Email) REFERENCES Users(Email),
    FOREIGN KEY (StableID) REFERENCES Stable(StableID)
);

-- Create the Records table
CREATE TABLE Records (
    RecordID INT IDENTITY(1,1) PRIMARY KEY, -- Using IDENTITY for auto-increment
    StableID INT,
    PartName VARCHAR(255),
    Comment TEXT, -- SQL Server uses TEXT for large text data
    RecordDate DATE NOT NULL,
    FOREIGN KEY (StableID) REFERENCES Stable(StableID)
);

-- Create the Horse table
CREATE TABLE Horse (
    HorseID INT IDENTITY(1,1) PRIMARY KEY, -- Auto-incrementing HorseID
    StableID INT, -- Foreign key referencing Stable table
    HorseName VARCHAR(255) NOT NULL,
    HorseBreed VARCHAR(255),
    Sex VARCHAR(10), -- Can be 'Male', 'Female', or other possible values
    DateOfBirth DATE NOT NULL,
    FOREIGN KEY (StableID) REFERENCES Stable(StableID) -- Foreign key constraint
);
