module Middlewares

open Microsoft.AspNetCore.Http

type Middlewares() = 
    member _.Invoke (context: HttpContext, next: RequestDelegate) = 
      let param = context.Request.RouteValues.["id"]
      let mutable id =
        match param with
        | :? int as id -> id
        | _ -> 0
      let user = Database.DatabaseQuerys.getUserById(id)
      if user.Length > 0 then
        next.Invoke(context)
      else
          context.Response.StatusCode <- StatusCodes.Status404NotFound
          context.Response.WriteAsync("User not found")
