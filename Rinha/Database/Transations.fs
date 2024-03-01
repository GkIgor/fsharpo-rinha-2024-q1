namespace Transations

open System.Net
open Database

module Operations = 
  let debit (id: int) (value: int) =
    let saldo = DatabaseQuerys.getSaldo id
    let limit = DatabaseQuerys.getLimit id
    printfn "%A" saldo
    if limit < value then
      HttpStatusCode.BadRequest
    else
      let newSaldo = saldo - value
      DatabaseQuerys.updateSaldo id newSaldo
      HttpStatusCode.OK

