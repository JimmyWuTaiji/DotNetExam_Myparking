CREATE VIEW [dbo].[v_Vechicle_Style]
	AS SELECT [DicID] AS StyleID
      ,[DicName_Key] AS StyleKey
      ,[DicName_Default] AS StyleName
      ,[IsAcitve]
      ,[SeqNo]
  FROM [dbo].[UsedCars_Dictionary]
  WHERE ParentID = 1
