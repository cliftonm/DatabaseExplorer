module UnitTests

open System.Data
open System.Data.SqlClient
open System.Xml
open Xunit
open BackEnd

type BackEndUnitTests() =
    [<Fact>]
    member s.CreateConnection() =
        let conn = openConnection "data source=localhost;initial catalog=AdventureWorks2008;integrated security=SSPI"
        Assert.NotNull(conn);
        conn

    [<Fact>]
    member s.BadConnection() =
        Assert.Throws<SqlException>(fun () ->
            BackEnd.openConnection("data source=localhost;initial catalog=NoDatabase;integrated security=SSPI") |> ignore)

    [<Fact>]
    member s.ReadSchema() =
        use conn = s.CreateConnection()
        let tables = BackEnd.getTables conn
        Assert.True(tables.Length > 0)
        // Verify that some known table exists in the list.
        Assert.True(List.exists(fun (t) -> (t.tableName = "Person.Person")) tables)

    [<Fact>]
    member s.LoadTable() =
        use conn = s.CreateConnection()
        let data = BackEnd.loadData conn "Person.Person"
        Assert.True((fst data).Length > 0)
        // Assert something we know about the schema.
        Assert.True(List.exists(fun (t) -> (t.columnName = "FirstName")) (snd data))
        // Verify the correct order of the schema
        Assert.True((snd data).[0].columnName = "BusinessEntityID")

    [<Fact>]
    member s.ToDataTable() =
        use conn = s.CreateConnection()
        let data = BackEnd.loadData conn "Person.Person"
        let dataTable = BackEnd.toDataTable data
        Assert.IsType<DataTable>(dataTable) |> ignore
        Assert.Equal(dataTable.Columns.Count, (snd data).Length) 
        Assert.True(dataTable.Columns.[0].ColumnName = "BusinessEntityID")
        Assert.Equal(dataTable.Rows.Count, (fst data).Length)

    [<Fact>]
    member s.toGenericList() =
        use conn = s.CreateConnection()
        let tables = BackEnd.getTables conn
        let genericList = BackEnd.tableListToGenericList tables
        Assert.Equal(genericList.Count, tables.Length) 
        Assert.True(genericList.[0] = tables.[0].tableName)
