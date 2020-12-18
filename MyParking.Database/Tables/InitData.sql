DELETE FROM dbo.UsedCars_Dictionary

INSERT INTO dbo.UsedCars_Dictionary
(
    DicID,
    DicName_Key,
    DicName_Default,
    ParentID,
    IsAcitve,
    SeqNo
)
SELECT   1, 'VechicleStyle', N'车辆类别', 0, 1, 1 
UNION SELECT   2, 'VechicleBrand', N'车辆品牌', 0, 1, 2 
UNION SELECT   3, 'BENZ', N'奔驰', 2, 1, 1 
UNION SELECT   4, 'BMW', N'宝马', 2, 1, 2
UNION SELECT   5, 'AUDI', N'奥迪', 2, 1, 3
UNION SELECT   6, 'LandRover', N'路虎', 2, 1, 4
UNION SELECT   7, 'Porsche', N'宝时捷', 2, 1, 5
UNION SELECT   8, 'MiniCar', N'微型车',1, 1, 1  
UNION SELECT   9, 'SmallCar', N'小型车', 1, 1, 2 
UNION SELECT   10, 'CompactCar', N'紧凑型车', 1, 1, 3 
UNION SELECT   11, 'MediumCar', N'中型车', 1, 1, 4 
UNION SELECT   12, 'MediumLargeCar', N'中大型车', 1, 1, 5 
UNION SELECT   13, 'LargeCar', N'大型车', 1, 1, 6 
UNION SELECT   14, 'SUV', N'越野车', 1, 1, 7 
UNION SELECT   15, 'SportsCar', N'跑车', 1, 1, 8 
UNION SELECT   16, 'MPV', N'商务车', 1, 1, 9 
UNION SELECT   17, 'SmallSUV', N'小型越野车', 14, 1, 1 
UNION SELECT   18, 'CompactSUV', N'紧凑型越野车', 14, 1, 2 
UNION SELECT   19, 'MediumSUV', N'中型越野车', 14, 1, 3 
UNION SELECT   20, 'MediumLargSUV', N'中大型越野车', 14, 1, 4 
UNION SELECT   21, 'LargeSUV', N'大型越野车', 14, 1, 5 
UNION SELECT   22, 'CompactMPV', N'紧凑型商务车', 16, 1,1 
UNION SELECT   23, 'MediumMPV', N'中型商务车', 16, 1, 2 
UNION SELECT   24, 'MediumLargMPV', N'中大型商务车', 16, 1, 3 
UNION SELECT   25, 'LargeMPV', N'大型商务车', 16, 1, 4


UNION SELECT   26, 'Honda', N'本田', 2, 1, 6
UNION SELECT   27, 'Toyota', N'丰田', 2, 1, 7
UNION SELECT   28, 'Volkswagen', N'大众', 2, 1, 8

DELETE dbo.UsedCars_Vehicle
INSERT INTO dbo.UsedCars_Vehicle
(
    VehicleID,
    BrandID,
    StyleID,
    VehicleModel
)
SELECT  1,    4,      9,      N'宝马i3'
UNION SELECT  2,    5,      9,      N'奥迪A1'
UNION SELECT  3,    26,      9,      N'飞度'
UNION SELECT  4,    27,      9,      N'威驰'
UNION SELECT  5,    28,      9,      N'POLO'
UNION SELECT  6,    4,      10,      N'宝马1系'
UNION SELECT  7,    4,      10,      N'宝马2系'
UNION SELECT  8,    3,      10,      N'奔驰A级'
UNION SELECT  9,    3,      10,      N'奔驰A级AMG'
UNION SELECT  10,   5,      10,      N'奥迪A3'
UNION SELECT  11,   5,      10,      N'奥迪S3'
UNION SELECT  12,   3,      11,      N'奔驰C级'
UNION SELECT  13,   4,      11,      N'宝马3系'
UNION SELECT  14,   4,      11,      N'宝马M3'
UNION SELECT  15,   5,      11,      N'奥迪A4L'
UNION SELECT  16,   5,      11,      N'奥迪A5'

UNION SELECT  17,   3,      12,      N'奔驰E级'
UNION SELECT  18,   4,      12,      N'宝马5系'
UNION SELECT  19,   4,      12,      N'宝马M5'
UNION SELECT  20,   5,      12,      N'奥迪A6L'
UNION SELECT  21,   5,      12,      N'奥迪A7'

INSERT INTO dbo.UsedCars_Vechicle_Setting
(
    VechicleSettingID,
    VehicleID,
    SettingName,
    ProductionYear,
    Price
)
SELECT  1, 1, N'快充畅行款', 2020, 305800
UNION  SELECT  2, 2, N'1.4 TFSI Ego', 2012, 271200
UNION  SELECT  3, 3, N'1.5L CVT 潮启版', 2021, 81800
UNION  SELECT  4, 3, N'1.5L CVT 潮享版', 2021, 86800
UNION  SELECT  5, 3, N'1.5L CVT 潮跑版', 2021, 95800
UNION  SELECT  6, 3, N'1.5L CVT 潮跑Pro版', 2021, 104800
UNION  SELECT  7, 3, N'1.5L CVT 潮越版', 2021, 99800
UNION  SELECT  8, 3, N'1.5L CVT 潮越Max版', 2021, 108800

UNION  SELECT  9, 4, N'1.5L 手动前行版', 2021, 73800
UNION  SELECT  10, 4, N'1.5L 手动创行版', 2021, 77800
UNION  SELECT  11, 4, N'1.5L CVT创行版', 2021, 87800
UNION  SELECT  12, 4, N'1.5L CVT智行版', 2021, 94800
UNION  SELECT  13, 4, N'1.5L CVT舒行版', 2021, 94800

UNION  SELECT  14, 5, N'Plus 1.5 L手动全景乐享版', 2019, 99900
UNION  SELECT  15, 5, N'Plus 1.5 L自动全景乐享版', 2019, 109900
UNION  SELECT  16, 5, N'Plus 1.5 L自动全炫彩科技版', 2019, 115900
UNION  SELECT  17, 5, N'Plus 1.5 L自动Beats潮酷版', 2019, 123900

UNION  SELECT  18, 6, N'120i M运动版', 2021, 203800
UNION  SELECT  19, 6, N'120i M运动曜夜版', 2021, 226800
UNION  SELECT  20, 6, N'125i M运动曜夜版', 2021, 246800
UNION  SELECT  21, 6, N'120i 时尚型', 2020, 198800
UNION  SELECT  22, 6, N'120i M运动套装', 2020, 223800
UNION  SELECT  23, 6, N'125i 领先型M运动套装', 2020, 243800
UNION  SELECT  24, 6, N'125i M运动套装', 2020, 263800

UNION  SELECT  25, 7, N'225i 运动设计套装', 2019, 263800
UNION  SELECT  26, 7, N'225i 尊享型M运动套装', 2019, 293800
UNION  SELECT  27, 7, N'225i 敞篷图轿跑车运动设计套装', 2019, 309800
UNION  SELECT  28, 7, N'225i 敞篷图轿跑车尊享型M运动套装', 2019, 339800

DELETE FROM dbo.UsedCars_Vechicle_Pictures

INSERT INTO dbo.UsedCars_Vechicle_Pictures
(
    VechicleSettingID,
    SeqNo,
    PictureURL
)
SELECT 1, 1, N'https://car.autohome.com.cn/photo/series/43861/1/6100795.html#pvareaid=2042264'
UNION SELECT 1, 2, N'https://car.autohome.com.cn/photo/series/43861/1/6100793.html'
UNION SELECT 1, 3, N'https://car.autohome.com.cn/photo/series/43861/1/6100788.html'
UNION SELECT 1, 4, N'https://car.autohome.com.cn/photo/series/43861/1/6100787.html'
UNION SELECT 1, 5, N'https://car.autohome.com.cn/photo/series/43861/1/6100619.html'
UNION SELECT 1, 6, N'https://car.autohome.com.cn/photo/series/43861/1/5512109.html'
UNION SELECT 1, 7, N'https://car.autohome.com.cn/photo/series/43861/10/5697366.html#pvareaid=2042264'
UNION SELECT 1, 8, N'https://car.autohome.com.cn/photo/series/43861/3/5697332.html#pvareaid=2042264'
UNION SELECT 1, 9, N'https://car.autohome.com.cn/photo/series/43861/3/5697329.html#pvareaid=2042264'
UNION SELECT 1, 10, N'https://car.autohome.com.cn/photo/series/43861/3/5697325.html#pvareaid=2042264'

SELECT 2, 1, N'https://www.che168.com/dealer/350125/38425533.html?pvareaid=105562'

SELECT 3, 1, N'https://car.autohome.com.cn/photo/series/45328/1/5807546.html#pvareaid=2042264'
UNION SELECT 3, 2, N'https://car.autohome.com.cn/photo/series/color/45328/864/#pvareaid=2042292'
UNION SELECT 3, 3, N'https://car.autohome.com.cn/photo/series-45322-7883-1-5902476.html'
UNION SELECT 4, 1, N'https://car.autohome.com.cn/photo/series-45322-7883-1-5902475.html'
UNION SELECT 5, 1, N'https://car.autohome.com.cn/photo/series/45328/10/5807501.html'
UNION SELECT 6, 1, N'https://car.autohome.com.cn/photo/series/45328/10/5807498.html'

UNION SELECT 9, 1, N'https://car.autohome.com.cn/photo/series/45328/10/5807498.html'
UNION SELECT 9, 2, N'https://car.autohome.com.cn/photo/series/47196/1/5928869.html#pvareaid=2042264'
UNION SELECT 9, 3, N'https://car.autohome.com.cn/photo/series/44220/1/5813895.html#pvareaid=2042264'
UNION SELECT 10, 1, N'https://car.autohome.com.cn/photo/series/47196/10/5928865.html#pvareaid=2042264'
UNION SELECT 10, 2, N'https://car.autohome.com.cn/photo/series/47196/3/5928818.html#pvareaid=2042264'
UNION SELECT 10, 3, N'https://car.autohome.com.cn/photo/series/47196/12/5928780.html#pvareaid=2042264'

