module Database

open Npgsql

let connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=rinha"
let connection = new NpgsqlConnection(connectionString)
// connection.Open()

let updateSaldo (id: int) (saldo: int) =
    let command = new NpgsqlCommand("UPDATE transactions SET saldo = :saldo WHERE id = :id", connection)
    command.Parameters.AddWithValue(":id", id)
        |> ignore
    command.Parameters.AddWithValue(":saldo", saldo)
        |> ignore
    command.ExecuteNonQuery()

let updateLimite (id: int) (limite: int) =
    let command = new NpgsqlCommand("UPDATE transactions SET limite = :limite WHERE id = :id", connection)
    command.Parameters.AddWithValue(":id", id)
        |> ignore
    command.Parameters.AddWithValue(":limite", limite)
        |> ignore
    command.ExecuteNonQuery()
