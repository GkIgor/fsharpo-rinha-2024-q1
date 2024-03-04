namespace Rinha.Controllers

open Database.DatabaseQuerys
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging

open Transations.Operations
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Http.HttpResults
open Microsoft.AspNetCore.Http
open System.Net

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
    member this.Get(id: int) =
      let user = getUserById(id)
      JsonResult(user.[0])

    
    [<HttpPost("{id}/transacoes")>]
    member this.Post(id: int, value: int, type1: char) =
      let operation = type1
      match operation with
      | 'D' -> 
        let response = debit id value
        match response with
        | -1 -> this.BadRequest(response)
        | _ -> this.Ok(response)

      | 'C' -> 
        let response = credit id value
        match response with
        | -1 -> this.BadRequest(response)
        | _ -> this.Ok(response)

      | _ -> this.BadRequest("nada retornado")