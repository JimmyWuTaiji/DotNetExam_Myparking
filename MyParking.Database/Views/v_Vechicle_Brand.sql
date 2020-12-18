CREATE VIEW [dbo].[v_Vechicle_Brand]
	AS SELECT [DicID] AS BrandID
      ,[DicName_Key] AS BrandKey
      ,[DicName_Default] AS BrandName
      ,[IsAcitve]
      ,[SeqNo]
  FROM [dbo].[UsedCars_Dictionary]
  WHERE ParentID = 2
