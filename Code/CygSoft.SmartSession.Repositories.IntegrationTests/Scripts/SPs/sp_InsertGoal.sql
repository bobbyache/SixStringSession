
CREATE PROCEDURE dbo.sp_InsertGoal
(
	@Title NVARCHAR(150),
	@CreateDate DATETIME = NULL,
	@Status NCHAR(3) = 'CRE', -- Created 
	@TargetCompletionDate DATETIME = NULL

)
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @InsertDate DATETIME = GETDATE()
	IF @CreateDate IS NULL SET @CreateDate = @InsertDate

	INSERT INTO [dbo].[Goal]
			   (
			   [Title]
			   ,[CreateDate]
			   ,[TargetCompletionDate]
			   ,[Status]
			   )
		VALUES
		(
			@Title,
			@CreateDate,
			@TargetCompletionDate,
			@Status
		)
	
END
