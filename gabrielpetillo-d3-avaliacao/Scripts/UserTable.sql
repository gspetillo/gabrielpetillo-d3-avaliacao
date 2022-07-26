IF NOT EXISTS (SELECT *
               FROM   Users)
  CREATE TABLE Users
    (
       IdUser   UNIQUEIDENTIFIER,
       Name     VARCHAR(255),
       Email    VARCHAR(255),
       Password VARCHAR(255),
    );

DECLARE @userUUID UNIQUEIDENTIFIER = Newid();

IF NOT EXISTS (SELECT *
               FROM   Users
               WHERE  email = 'admin@email.com')
  INSERT INTO Users
              (iduser,
               NAME,
               email,
               password)
  VALUES      (@userUUID,
               'Admin',
               'admin@email.com',
               'admin123')

IF NOT EXISTS (SELECT *
               FROM   Users
               WHERE  email = 'gspetillo@gmail.com')
  INSERT INTO Users
              (iduser,
               NAME,
               email,
               password)
  VALUES      (@userUUID,
               'Gabriel',
               'gspetillo@gmail.com',
               'abcd1234')

SELECT *
FROM   Users;
/*delete from Users where Email = 'gspetillo@gmail.com';




declare @Email varchar(255) = 'admin@email.com'

select * from Users where Email = @Email
*/