create database ClinicDb;
go

use ClinicDb;
go

create table ContactInformation
(
    id          uniqueidentifier primary key,
    phoneNumber nvarchar(20),
    email       nvarchar(30),
    social      nvarchar(30)
)
    go

create table Qualifications
(
    id        uniqueidentifier primary key,
    name      nvarchar(200),
    issueDate DATETIME2
)
    go

create table Doctors
(
    id                   uniqueidentifier primary key,
    firstName            nvarchar(30),
    lastName             nvarchar(30),
    parentsName          nvarchar(30),
    contactInformationId uniqueidentifier references ContactInformation (id),
    qualificationId      uniqueidentifier references Qualifications (id),
    photoUrl             nvarchar(300)
)
    go

create table Patients
(
    id             uniqueidentifier primary key,
    firstName      nvarchar(30),
    lastName       nvarchar(30),
    contactNumber  nvarchar(20),
    passportNumber nvarchar(14),
    gender         nvarchar(1) CHECK (gender = 'M' or gender = 'F' or gender = 'U'),
    photoUrl       nvarchar(300),
    email          nvarchar(30)
)
    go

create table Cabinets
(
    id                uniqueidentifier primary key,
    number            nvarchar(10),
    location          nvarchar(300),
    departmentName    nvarchar(50),
    contactNumber     nvarchar(20),
    workingHoursFrom  TIME,
    workingHoursUntil TIME
)
    go

create table Tickets
(
    id         uniqueidentifier primary key,
    issuedAt   DATETIME2,
    issuedById uniqueidentifier REFERENCES Patients (id),
    expiresAt  DATETIME2,
    cabinetId  uniqueidentifier references Cabinets (Id)
)
    go

create TABLE Schedule
(
    id         uniqueidentifier primary key,
    doctorId   uniqueidentifier references Doctors (id),
    cabinetId  uniqueidentifier references Cabinets (id),
    dateOfWork DATETIME2
)
    go

create procedure GetAllDoctors
    as
SELECT d.id,
       firstName,
       lastName,
       parentsName,
       phoneNumber,
       Email,
       social,
       Q.name as qualificationName,
       photoUrl
FROM Doctors d
         join Qualifications Q on Q.id = d.qualificationId
         join ContactInformation CI on d.contactInformationId = CI.id
    go

create procedure GetDoctorById @doctorId uniqueidentifier
as
SELECT TOP 1 d.id,
        firstName,
       lastName,
       parentsName,
       phoneNumber,
       Email,
       social,
       Q.name as qualificationName,
       photoUrl
FROM Doctors d
         join Qualifications Q on Q.id = d.qualificationId
         join ContactInformation CI on d.contactInformationId = CI.id
WHERE d.id = @doctorId
    go

create procedure GetDoctorsByCriteria @firstName nvarchar(100), @lastName nvarchar(100), @parentsName nvarchar(100),
                                      @qualification nvarchar(100)
as
SELECT d.id,
       firstName,
       lastName,
       parentsName,
       phoneNumber,
       Email,
       social,
       Q.name as qualificationName,
       photoUrl
FROM Doctors d
         join Qualifications Q on Q.id = d.qualificationId
         join ContactInformation CI on d.contactInformationId = CI.id
where firstName LIKE '^.*' + @firstName + '.*$'
   or lastName LIKE '^.*' + ISNULL(@lastName, '') + '.*$'
   or parentsName LIKE '^.*' + ISNULL(@parentsName, '') + '.*$'
   or Q.name LIKE '^.*' + ISNULL(@qualification, '') + '.*$'
    go

create procedure CreatePatient @patientId uniqueidentifier, @firstName nvarchar(100), @lastName nvarchar(100),
                               @contactNumber nvarchar(100),
                               @email nvarchar(100), @passportNumber nvarchar(100), @gender nvarchar(1),
                               @photoUrl nvarchar(300)
as
insert into Patients(id, firstName, lastName, contactNumber, passportNumber, gender, photoUrl, email)
values (@patientId,
        @firstName,
        @lastName,
        @contactNumber,
        @passportNumber,
        @gender,
        @photoUrl,
        @email)
go

create procedure GetPatientById @patientId uniqueidentifier
as
select TOP 1 *
from Patients
where id = @patientId
    go

create procedure GetPatientsByCriteria @firstName nvarchar(100), @lastName nvarchar(100), @passportNumber nvarchar(100),
                                       @email nvarchar(100), @gender nvarchar(1)
as
SELECT *
FROM Patients
where firstName LIKE '^.*' + @firstName + '.*$'
   or lastName LIKE '^.*' + ISNULL(@lastName, '') + '.*$'
   or passportNumber = @passportNumber
   or email = @email
   or gender = @gender
    go

create procedure GetAllPatients
    as
select *
from Patients
order by firstName
    go

create procedure GetTicketById @ticketId uniqueidentifier
as
select TOP 1 *
FROM Tickets
where id = @ticketId
    go

create procedure CreateTicket @patientId uniqueidentifier, @cabinetNumber nvarchar(10), @requestDateTime DATETIME2
as
begin
    declare @generatedId uniqueidentifier
select @generatedId = NEWID()

select TOP 1 @generatedId     as id,
        SYSDATETIME()    as issuedAt,
       @patientId       as issuedById,
       @requestDateTime as expiresAt,
       c.number         as cabinetId
into Tickets
from Cabinets c
where @cabinetNumber = c.number

select @generatedId
end
go

create procedure RevokeTicket @ticketId uniqueidentifier
as
delete
from Tickets
where id = @ticketId
    go


-- Заполнение случайными данными

-- declare variables for random values
declare @floor int;
declare @number nvarchar(10);
declare @location nvarchar(300);
declare @departmentName nvarchar(50);
declare @contactNumber nvarchar(20);

-- declare table variable for department names
declare @departments table
                     (
                         name nvarchar(50)
                     );
insert into @departments
values ('Cardiology'),
       ('Dermatology'),
       ('Endocrinology'),
       ('Gastroenterology'),
       ('Hematology'),
       ('Immunology'),
       ('Nephrology'),
       ('Neurology'),
       ('Oncology'),
       ('Ophthalmology'),
       ('Orthopedics'),
       ('Pediatrics'),
       ('Psychiatry'),
       ('Radiology'),
       ('Surgery');

-- loop 100 times to insert rows
declare @i int = 0;
while @i < 100
begin
        -- generate random floor number between 1 and 9
        set @floor = ceiling(rand() * 9);

        -- generate random cabinet number with the same first digit as floor number
        set @number = cast(@floor as nvarchar(1)) + right(cast(ceiling(rand() * 1000) as nvarchar(3)), 2);

        -- generate location as floor number
        set @location = 'Floor ' + cast(@floor as nvarchar(1));

        -- select random department name from table variable
        set @departmentName = (select top 1 name from @departments order by newid());

        -- generate random contact number with 10 digits
        set @contactNumber = '8800555353';

        -- insert row into table
insert into Cabinets
values (newid(), @number, @location, @departmentName, @contactNumber, '09:00:00', '18:00:00');

-- increment loop counter
set @i = @i + 1;
end

-- select all rows from table
select *
from Cabinets;

create database AuthDb;
go;

use AuthDb;
go

create table PatientCredentials(
                                   id uniqueidentifier primary key,
                                   email nvarchar(30),
                                   passwordHash nvarchar(max),
                                   isEmailConfirmed bit default 0,
                                   emailConfirmationCode nvarchar(6)
)
    go
create table Tokens(
                       id uniqueidentifier primary key,
                       refreshToken nvarchar(500),
                       patientCredentialId uniqueidentifier references PatientCredentials(id),
                       issueDateTime DATETIME2
)

create procedure GetPatientCredential @login nvarchar(100)
as
select TOP 1 *
FROM PatientCredentials
where email = @login
    go

create procedure CreateNewPatient @email nvarchar(100), @passwordHash nvarchar(1000),
                                  @emailConfirmationCode nvarchar(6)
as
begin
    declare @generatedId uniqueidentifier
    set @generatedId = NEWID()

    insert into PatientCredentials(id, email, passwordHash, emailConfirmationCode)
    values (
            @generatedId,
            @email,
            @passwordHash,
            @emailConfirmationCode
           )

select @generatedId
end
go











