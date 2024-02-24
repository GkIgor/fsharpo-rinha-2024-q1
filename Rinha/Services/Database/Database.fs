namespace Rinha.Database

module public Database

open Npgsql

let connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=rinha"
let connection = new NpgsqlConnection(connectionString)
connection.Open()

let updateSaldo (id: int) (saldo: int) =
    let insert = new NpgsqlCommand("UPDATE transactions SET saldo = :saldo WHERE id = :id", connection)
    insert.Parameters.AddWithValue(":id", id)
        |> ignore
    insert.Parameters.AddWithValue(":saldo", saldo)
        |> ignore
    insert.ExecuteNonQuery()

let updateLimite (id: int) (limite: int) =
    let insert = new NpgsqlCommand("UPDATE transactions SET limite = :limite WHERE id = :id", connection)
    insert.Parameters.AddWithValue(":id", id)
        |> ignore
    insert.Parameters.AddWithValue(":limite", limite)
        |> ignore
    insert.ExecuteNonQuery()

let getSaldo (id: int) =
    let select = new NpgsqlCommand("SELECT saldo FROM transactions WHERE id = :id", connection)
    select.Parameters.AddWithValue(":id", id)
        |> ignore
    select.ExecuteScalar()

let getUserById (id: int) =
    let select = new NpgsqlCommand("SELECT * FROM transactions WHERE id = :id", connection)
    select.Parameters.AddWithValue(":id", id)
        |> ignore
    select.ExecuteScalar()

let newTransaction (id: int) (saldo: int) (limite: int) (tipo: char) =
    let insert = new NpgsqlCommand("INSERT INTO transactions (id, saldo, limite, tipo) VALUES (:id, :saldo, :limite, :tipo) WHERE limite >= :saldo", connection)
    insert.Parameters.AddWithValue(":id", id)
        |> ignore
    insert.Parameters.AddWithValue(":saldo", saldo)
        |> ignore
    insert.Parameters.AddWithValue(":limite", limite)
        |> ignore
    insert.Parameters.AddWithValue(":tipo", tipo)
        |> ignore
    insert.ExecuteNonQuery()
