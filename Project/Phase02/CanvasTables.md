CREATE TABLE User (
    dob DATE,
    firstName VARCHAR(100)
    lastName VARCHAR(100)
    userID VARCHAR(8)
    PRIMARY KEY (userID)
);

CREATE TABLE Student (
    major INTEGER
) INHERITS (User);

CREATE TABLE Professor (
    employer VARCHAR(4)
) INHERITS (User);

CREATE TABLE Admin (

) INHERITS (User);

CREATE TABLE Department (
    depName VARCHAR(100)
    UNIQUE subject VARCHAR(4)
    PRIMARY KEY (subject, depName)
);

CREATE TABLE Enrollment (
    userID INTEGER
    classID INTEGER
    semester INTEGER
    grade VARCHAR(2)
    PRIMARY KEY (userID, classID, semester)
);

CREATE TABLE Class (
    teacherID INTEGER
    UNIQUE semester DATE
    startTime TIME
    startTIME TIME
    classID INTEGER
    location VARCHAR(100)
    UNIQUE catalogID VARCHAR(5)
    PRIMARY KEY (classID)
);

CREATE TABLE Submissions (
    timestamp DATETIME
    contents VARCHAR(8192)
    score INTEGER
    userID INTEGER
    PRIMARY KEY (timestamp, userID)
);

CREATE TABLE Course (
    courseName VARCHAR(100)
    UNIQUE catalogID VARCHAR(5)
    courseNum VARCHAR(4)
    PRIMARY KEY (catalogID, courseNum)
);

CREATE TABLE Assignment (
    assignmentName VARCHAR(100)
    assignmentID INTEGER
    pointVal INTEGER
    catID INTEGER
    dueDate DATETIME
    PRIMARY KEY (assignmentID)
);

CREATE TABLE Category (
    gradingWeight INTEGER
    catName VARCHAR(100)
    catID INTEGER
    classID INTEGER
    PRIMARY KEY (catID)
);
