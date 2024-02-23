module Transations

open Npgsql

let connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=rinha"
let connection = new NpgsqlConnection(connectionString)

let newTransaction (id: int) (saldo: int) (limite: int) (tipo: char) =
    let command = new NpgsqlCommand("INSERT INTO transactions (id, saldo, limite, tipo) VALUES (:id, :saldo, :limite, :tipo) WHERE limite >= :saldo", connection)
    command.Parameters.AddWithValue(":id", id)
        |> ignore
    command.Parameters.AddWithValue(":saldo", saldo)
        |> ignore
    command.Parameters.AddWithValue(":limite", limite)
        |> ignore
    command.Parameters.AddWithValue(":tipo", tipo)
        |> ignore
    command.ExecuteNonQuery()
