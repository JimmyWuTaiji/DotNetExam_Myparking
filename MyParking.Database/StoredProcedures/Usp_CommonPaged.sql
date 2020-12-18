CREATE PROCEDURE [dbo].[Usp_CommonPaged]
	@SqlText nvarchar(2000),			--Table name,view name, query sentence 
	@PageSize int=10,				--Page size(Row count)  
	@PageIndex int=1,				--The page which user want to display  
	@RecordsCount int OUTPUT,   
	@Fields varchar(1000) = '*', 
	--Display columns, if query result don't need identifier column,need set this value,and don't contain identifier column  
	@Filter varchar(1000) = '',
	@Sort varchar(200) = ''  --sort by
AS
BEGIN  
	--------------------------   
	Declare @sql nvarchar(4000),
	@Countsql nvarchar(2000)
 
	 DECLARE @FdName nvarchar(250)
		,@Id1 varchar(20), @Id2 varchar(20)
		,@Obj_ID int

	--------------------------
	select @FdName='[ID_'+cast(newid() as varchar(40))+']'
	,@Id1=cast((@PageSize*(@PageIndex-1) + 1) as varchar(20))
	,@Id2=cast(@PageSize*@PageIndex as varchar(20))

	select @Obj_ID=object_id(@SqlText)  
	,@Fields=case isnull(@Fields,'') when '' then ' *' else ' '+ @Fields end  
	,@Filter=case isnull(@Filter,'') when '' then '' else ' where '+@Filter + ' ' end  
	,@SqlText=case when @Obj_ID is not null then ' '+ @SqlText else ' ('+@SqlText+') a ' end  


	-------------------------------
	if @Obj_ID is not null and objectproperty(@Obj_ID,'IsTable')=1
	begin
		-- @SqlText is table
        set @Countsql='set @totalRows=(select count(1) from ' +@SqlText+ ' ' + @Filter + ')'

		set @sql = 'WITH [tempTable] AS (SELECT ' +
			' ROW_NUMBER() OVER (ORDER BY '+@Sort+') AS ' + @FdName + 
			' , ' + @Fields +
			' FROM '+@SqlText+' '+@Filter+') ' +
			' SELECT '+@Fields+' FROM [tempTable] WHERE ' +@FdName+ ' BETWEEN '+@Id1+' AND '+ @Id2
		   
	end
	else
	begin
        set @Countsql='set @totalRows=(select count(1) from (select top 100 percent * from ' +@SqlText+ ') as a ' +@Filter + ')'

		set @sql = 'WITH [tempTable] AS (SELECT ' + 
			' ROW_NUMBER() OVER (ORDER BY '+@Sort+') AS ' + @FdName + 
			' , ' + @Fields +
			' FROM (select top 100 percent * from '+@SqlText+') as a '+@Filter+') ' +
			' SELECT '+@Fields+' FROM [tempTable] WHERE ' +@FdName+ ' BETWEEN '+@Id1+' AND '+ @Id2
	end print @sql
	--------------------------
	--Get RecordsCount
	EXECUTE sp_executesql @Countsql, N'@totalRows INT output', @RecordsCount output	
	Exec (@sql)   
	--------------------------
END