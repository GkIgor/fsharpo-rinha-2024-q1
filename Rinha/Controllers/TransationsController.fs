namespace Rinha.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Rinha

[<ApiController>]
[<Route("[clientes]")>]
type TransationsController (logger : ILogger<TransationsController>) =
    inherit ControllerBase()
    
    [<HttpGet("{id}/transacoes")>]
    member _.Get() =
        
        
    
