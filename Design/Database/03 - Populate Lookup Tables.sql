

MERGE 
	GoalStatus AS target  
USING 
	(
		SELECT			'CRE', 'Created'
		UNION SELECT	'ACT', 'Active'
	) 
	AS source (Id, Status)  
ON (target.Id = source.Id)  
WHEN MATCHED THEN   
        UPDATE SET Status = source.Status  
WHEN NOT MATCHED THEN  
    INSERT (Id, Status)  
    VALUES (source.Id, source.Status);