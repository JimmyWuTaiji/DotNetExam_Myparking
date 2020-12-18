CREATE VIEW V_AllVechicles
AS 
SELECT b.BrandName, A.VehicleModel, D.SettingName, D.ProductionYear ,c.StyleName,d.Price FROM dbo.UsedCars_Vehicle A INNER JOIN dbo.v_Vechicle_Brand B ON A.BrandID = b.BrandID
	INNER JOIN dbo.v_Vechicle_Style C ON A.StyleID = c.StyleID
	INNER JOIN dbo.UsedCars_Vechicle_Setting D ON A.VehicleID = D.VehicleID