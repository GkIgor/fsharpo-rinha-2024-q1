namespace Rinha.Controllers

open Database.DatabaseQuerys
open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging


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
      let mutable returnRequest = []
      let newTransation = newTransaction id value type1 description
      JsonResult(returnRequest)

