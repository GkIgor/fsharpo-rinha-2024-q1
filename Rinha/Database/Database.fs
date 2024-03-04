namespace Database

module public DatabaseQuerys =
  open Npgsql

  let [<Literal>] connectionString = "Host=localhost; Port=5432; Username=postgres; Password=postgres; Database=postgres"
  let connection = new NpgsqlConnection(connectionString)
  connection.Open()

  let getUserById (id: int) = 
    let select = new NpgsqlCommand("SELECT * FROM clientes WHERE id = :id", connection)
    select.Parameters.AddWithValue(":id", id) |> ignore
    let reader = select.ExecuteReader()
    let mutable result = []

    while reader.Read() do
      let userData = {|
        id = reader.GetInt32(0)
        nome = reader.GetString(1)
        limite = reader.GetInt32(2)
      |}
      result <- userData :: result

    reader.Close()
    result 
  
  let getSaldo (id: int) =
    let select = new NpgsqlCommand("SELECT limite, SUM(valor) AS saldo_total FROM clientes JOIN transacoes ON clientes.id = transacoes.cliente_id WHERE clientes.id = :id GROUP BY clientes.id
", connection)

    select.Parameters.AddWithValue(":id", id) |> ignore
    let reader = select.ExecuteReader()
    let mutable result = 0

    while reader.Read() do
      result <- reader.GetInt32(0) - reader.GetInt32(1)
    reader.Close()
    result


  let getLimit (id: int) =
    let select = new NpgsqlCommand("SELECT limite FROM clientes WHERE id = :id", connection)

    select.Parameters.AddWithValue(":id", id) |> ignore
    let reader = select.ExecuteReader()
    let mutable result = 0

    while reader.Read() do
      result <- reader.GetInt32(0)
    reader.Close()
    result

  let updateSaldo (id: int) (saldo: int) =
    let update = new NpgsqlCommand("UPDATE clientes SET limite = :saldo WHERE id = :id", connection)
    update.Parameters.AddWithValue(":id", id) |> ignore
    update.Parameters.AddWithValue(":saldo", saldo) |> ignore

    let reader = update.ExecuteReader()
    let mutable result = 0
    while reader.Read() do
      result <- reader.GetInt32(0)

    reader.Close()
      

  let newTransaction (id: int) (valor: int) (tipo: char) (descricao: string) =
    let insert = new NpgsqlCommand("INSERT INTO transacoes (cliente_id, valor, tipo, descricao) VALUES (:id, :valor, :tipo, :descricao);
", connection)

    insert.Parameters.AddWithValue(":id", id)|> ignore
    insert.Parameters.AddWithValue(":valor", valor)|> ignore
    insert.Parameters.AddWithValue(":tipo", tipo)|> ignore
    insert.Parameters.AddWithValue(":descricao", descricao)|> ignore

    insert.ExecuteNonQuery()


  let verifyClient (id: int) =
    let select = new NpgsqlCommand("SELECT COUNT(*) FROM clientes WHERE id = :id", connection)

    select.Parameters.AddWithValue(":id", id) |> ignore
    let reader = select.ExecuteReader()
    let mutable result = 0

    while reader.Read() do
      result <- reader.GetInt32(0)
    reader.Close()
    result


  let getStatement (id: int) =
    let select = new NpgsqlCommand("SELECT (SELECT limite FROM clientes WHERE id = :id) AS limite, SUM(valor) AS total, NOW() AS data_extrato FROM transacoes WHERE cliente_id = :id;
SELECT valor, tipo, descricao, realizada_em FROM transacoes WHERE cliente_id = :id ORDER BY realizada_em DESC LIMIT 10;", connection)

    select.Parameters.AddWithValue(":id", id) |> ignore

    let reader = select.ExecuteReader()

    let mutable result = []

    while reader.Read() do
      let userData = {|
        limite = reader.GetInt32(0)
        total = reader.GetInt32(1)
        data_extrato = reader.GetDateTime(2)
      |}

      result <- userData :: result
