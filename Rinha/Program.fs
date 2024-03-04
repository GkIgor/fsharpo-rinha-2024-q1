namespace Rinha
#nowarn "20"

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting

module Program =
    let configureApp (app : IApplicationBuilder) =
      app.UseForwardedHeaders()
           .UseStaticFiles()
           .UseRouting()
           .UseAuthorization()

           .UseEndpoints(fun endpoints -> endpoints.MapControllers() |> ignore)

    let configureServices (services : IServiceCollection) =
        services.AddControllers() |> ignore

    let exitCode = 0

    [<EntryPoint>]
    let main args = 
        let builder = WebApplication.CreateBuilder(args)
        builder.Services |> configureServices
        let app = builder.Build()
        app |> configureApp

        app.Run()
        exitCode
