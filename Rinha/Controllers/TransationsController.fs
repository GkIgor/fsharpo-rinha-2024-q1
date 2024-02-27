namespace Rinha.Controllers

open Database.DatabaseQuerys
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging

type NewTransationRequest = {
  valor: int
  tipo: char
  descricao: string
}

type NewTransationResponse = {
limite: int
saldo: int
}

[<ApiController>]
[<Route("clientes")>]
type TransationsController (logger : ILogger<TransationsController>) =
    inherit ControllerBase()

    [<HttpGet("{id}/transacoes")>]
    member _.Get(id: int) =
      let user = getUserById(id)
      JsonResult(user.[0])
    
    [<HttpPost("{id}/transacoes")>]
    member _.Post(id: int, value: int, type1: char, description) =
      if verifyClient id = 0 then
        NotFoundResult("Cliente não encontrado")

      let patternVerifyTransaction =
        let verify = verifyTransaction id
        let limit = getSaldo id
        match verify with
        | :? int as saldo -> saldo





        
      //let newTransation = newTransaction id value type1 description
      //JsonResult("")

