module BackEnd

open System
open System.Data
open System.Data.SqlClient
open System.Xml;

// A simple record for holding table names
type TableName = {tableName : string}

// A simple record for holding column names
// Could be extended to include things like column types.
type ColumnName = {columnName : string}

// A discriminated union for the types of queries we're going to use
type Queries =
    | LoadUserTables
    | LoadTableData
    | LoadTableSchema

// Opens a SqlConnection instance given the full connection string.
let openConnection connectionString =
    let connection = new SqlConnection()
    connection.ConnectionString <- connectionString
    connection.Open()
    connection

// Returns a SQL statement for the desired query
let getSqlQuery query (tableName : string option) =
    match query with
    | LoadUserTables -> "select s.name + '.' + o.name as table_name from sys.objects o left join sys.schemas s on s.schema_id = o.schema_id where type_desc = 'USER_TABLE' order by s.name + '.' + o.name"
    | LoadTableData -> 
        match tableName with
        | Some name -> "select * from " + name
        | None -> failwith "table name is required."
    | LoadTableSchema -> 
        match tableName with
        | Some name ->
            let schemaAndName = name.Split('.')
            "select COLUMN_NAME from information_schema.columns where table_name = '" + schemaAndName.[1] + "' AND table_schema='" + schemaAndName.[0] + "' order by ORDINAL_POSITION"
        | None -> failwith "table name is required." 

// Returns a SqlCommand instance
let getCommand (conn : SqlConnection) query (tableName : string option) =
    let cmd = conn.CreateCommand()
    cmd.CommandText <- getSqlQuery query tableName
    cmd

// Returns all the fields for a record.
let getFieldValues (reader : SqlDataReader) =
    let objects = Array.create reader.FieldCount (new Object())
    reader.GetValues(objects) |> ignore
    Array.toList objects

// Reads all the records and parses them as specified by the rowParser parameter.
let readData rowParser (cmd : SqlCommand) =
    let rec read (reader : SqlDataReader) list = 
        match reader.Read() with
        | true -> read reader (rowParser reader :: list)
        | false -> list
    use reader = cmd.ExecuteReader()
    read reader []

// Returns a table name from the current reader position.
let getTableNameRecord (reader : SqlDataReader) =
    {tableName = (reader.[0]).ToString()}

// Returns the list of tables in the database specified by the connection.
let getTables (conn : SqlConnection) =
    getCommand conn LoadUserTables None |> readData getTableNameRecord |> List.rev

// Returns a list of ColumnName records representing the field names of a table.
let readTableSchema (cmd : SqlCommand) =
    let schema = readData getFieldValues cmd
    List.map(fun (c) -> {columnName = (c : List<Object>).[0].ToString()}) schema |> List.rev

// Returns a tuple of table data and the table schema.
let loadData (conn : SqlConnection) tableName =
    let data = getCommand conn LoadTableData (Some tableName) |> readData getFieldValues
    let schema = (getCommand conn LoadTableSchema (Some tableName) |> readTableSchema)
    (data, schema)

// Populates a DataTable given a ColumnName List, returning
// the DataTable instance.
let setupColumns (dataTable : DataTable) schema =
    let rec addColumn colList =
        match colList with
        | hd::tl -> 
            let newColumn = new DataColumn()
            newColumn.ColumnName <- hd.columnName
            dataTable.Columns.Add(newColumn)
            addColumn tl
        | [] -> dataTable
    addColumn schema

// Populates the rows of a DataTable from a data List.
let setupRows data (dataTable : DataTable) =
    let rec addRow dataList =
        match dataList with
        | hd::tl ->
            let dataRow = dataTable.NewRow()
            let rec addFieldValue (index : int) fieldList =
                match fieldList with
                | fhd::ftl ->
                    dataRow.[index] <- fhd
                    addFieldValue (index + 1) ftl
                | [] -> ()
            addFieldValue 0 hd
            dataTable.Rows.InsertAt(dataRow, 0)
            addRow tl
        | [] -> dataTable
    addRow data

// Return a DataTable populated from our (data, schema) tuple
let toDataTable (data, schema) =
    let dataTable = new DataTable()
    setupColumns dataTable schema |> setupRows data

// Convert a TableName : list to a System.Collection.Generic.List
let tableListToGenericList list = 
    let genericList = new System.Collections.Generic.List<string>()
    List.iter(fun (e) -> genericList.Add(e.tableName)) list
    genericList

let getTablesAsGenericList conn =
    getTables conn |> tableListToGenericList









