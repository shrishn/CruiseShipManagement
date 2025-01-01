
Use CruiseShipDB;
-- Create Roles Table
CREATE TABLE Roles (
    RoleId UNIQUEIDENTIFIER PRIMARY KEY,
    Name VARCHAR(50) NOT NULL UNIQUE,
    Description VARCHAR(255)
);
-- Create Users Table with RoleId
CREATE TABLE Users (
    UserId UNIQUEIDENTIFIER PRIMARY KEY,
    UserName VARCHAR(255) NOT NULL UNIQUE,
    Email VARCHAR(255) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    FullName VARCHAR(255),
    PhoneNumber VARCHAR(15),
    IsVoyager BIT NOT NULL,  -- True for Voyager, False for Admin
    DateCreated DATETIME DEFAULT GETDATE(),
	RoleId UNIQUEIDENTIFIER,
	FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
    
);



-- Create Facility Table
CREATE TABLE Facility (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Description TEXT,
    Fee DECIMAL(18, 2) NOT NULL,
    AvailableSlots INT NOT NULL,
    CreatedBy UNIQUEIDENTIFIER ,  -- FK to Users (Admin)
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserId)
);

-- Create Room Table
CREATE TABLE Room (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RoomNumber VARCHAR(50) NOT NULL,
    Type VARCHAR(100),
    Fee DECIMAL(18, 2) NOT NULL,
    AvailableSlots INT NOT NULL,
    CreatedBy UNIQUEIDENTIFIER ,  -- FK to Users (Admin)
    FOREIGN KEY (CreatedBy) REFERENCES Users(UserId)
);

-- Create Booking Table
CREATE TABLE Booking (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    VoyagerId UNIQUEIDENTIFIER ,  -- FK to Users (Voyager)
    FacilityId INT,  -- FK to Facility
    RoomId INT,  -- FK to Room
    BookingDate DATETIME DEFAULT GETDATE(),
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    Status VARCHAR(50) NOT NULL,  -- e.g., 'Booked', 'Cancelled'
    FOREIGN KEY (VoyagerId) REFERENCES Users(UserId),
    FOREIGN KEY (FacilityId) REFERENCES Facility(Id),
    FOREIGN KEY (RoomId) REFERENCES Room(Id)
);

-- Create Bill Table
CREATE TABLE Bill (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    VoyagerId UNIQUEIDENTIFIER ,  -- FK to Users (Voyager)
    Amount DECIMAL(18, 2) NOT NULL,
    Status VARCHAR(50) NOT NULL,  -- e.g., 'Paid', 'Pending'
    CreatedDate DATETIME DEFAULT GETDATE(),
    BookingDetails TEXT,  -- Details about the bookings (facility/room)
    FOREIGN KEY (VoyagerId) REFERENCES Users(UserId)
);
