CREATE TABLE [dbo].[Articles]
(
	[Id] INT NOT NULL PRIMARY Key Identity(1, 1),  -- 일련번호
	[Title] NVarChar(255) Not Null,                -- 제목
    [Content] NVARCHAR(Max)  NULL,              -- 내용    =>   TODO: Not Null

    [IsPinned] Bit Null Default(0),                -- 공지글로 올리기

    -- AuditableBase.cs 참조
    [CreatedBy] NVARCHAR(255) NULL,                -- 등록자
    [Created] DateTime Default(GetDate()),         -- 생성일
    [ModifiedBy] NVARCHAR(255) NULL,               -- 수정자
    [Modified] NCHAR(10) NULL,                     -- 수정일

)
Go
