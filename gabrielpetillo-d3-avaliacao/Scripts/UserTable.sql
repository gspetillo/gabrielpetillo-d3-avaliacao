/*
EM caso de erro na criação do Banco, executar esses comandos:

DROP TABLE Users;
*/



IF NOT EXISTS (SELECT *
				FROM INFORMATION_SCHEMA.TABLES 
				WHERE TABLE_NAME = 'Users')
  CREATE TABLE Users
    (
       IdUser   UNIQUEIDENTIFIER,
       Name     VARCHAR(255),
       Email    VARCHAR(255),
	   Password VARCHAR(255),
    );
	/*IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Users' AND COLUMN_NAME = 'Password')
		ALTER TABLE Users ADD Password varbinary(255);   */
GO  

/*
IF NOT EXISTS (SELECT * 
				FROM sys.symmetric_keys
				WHERE name = 'UsersCertificateKey')
	CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'gabrielpetillo-d3-avaliacao::admin@email.com::admin123';  

IF NOT EXISTS (SELECT * 
				FROM sys.certificates
				WHERE name = 'UsersCertificate')
	CREATE CERTIFICATE UsersCertificate
	WITH SUBJECT = 'Users Password'
	GO

IF NOT EXISTS (SELECT * 
				FROM sys.symmetric_keys
				WHERE name = 'UsersCertificateKey')
	CREATE SYMMETRIC KEY UsersCertificateKey
		WITH ALGORITHM = AES_256  
		ENCRYPTION BY CERTIFICATE UsersCertificate;  
GO  

OPEN SYMMETRIC KEY UsersCertificateKey  
   DECRYPTION BY CERTIFICATE UsersCertificate;  

UPDATE Users  SET Password = EncryptByKey(Key_GUID('UsersCertificateKey')  
    , Password, 1, HASHBYTES('SHA2_256', CONVERT( varbinary  
    , Password)));  
GO  

OPEN SYMMETRIC KEY UsersCertificateKey  
   DECRYPTION BY CERTIFICATE UsersCertificate;  
GO  


SELECT Password  
    AS 'Encrypted Password', CONVERT(nvarchar,  
    DecryptByKey(Password, 1 ,   
    HASHBYTES('SHA2_256', CONVERT(varbinary, IdUser))))  
    AS 'Decrypted card number' FROM Users;  
GO */ 

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
               CONVERT(VARBINARY(255), 'admin123'))

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
               CONVERT(VARBINARY(255), 'abcd1234'))

SELECT *
FROM   Users;
