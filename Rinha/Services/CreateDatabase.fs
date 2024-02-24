module CreateDatabase

open Npgsql

let connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=rinha"
let connection = new NpgsqlConnection(connectionString)
// connection.Open()

let createUsersTable () =
    let command = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS usuarios (id INT PRIMARY KEY, saldo INT, limite INT, CONSTRAINT no_negative CHECK (saldo >= (-limite))", connection)
    command.ExecuteNonQuery()
        |> ignore
    
    let limits = [100000; 80000; 1000000; 10000000; 500000]

    for limit in limits do
        for id in 1..5 do 
            let command = new NpgsqlCommand("INSERT INTO usuarios (id, saldo, limite, tipo) VALUES (:id, :saldo, :limite)", connection)
            
            command.Parameters.AddWithValue(":id", id) |> ignore

            command.Parameters.AddWithValue(":saldo", 0)|> ignore

            command.Parameters.AddWithValue(":limite", limit)|> ignore

            command.Parameters.AddWithValue(":tipo", 'C')|> ignore

            command.ExecuteNonQuery() |> ignore

createUsersTable() |> ignore



