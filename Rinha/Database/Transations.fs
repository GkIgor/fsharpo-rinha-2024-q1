namespace Transations

open System.Net
open Database

module Operations = 
  let debit (id: int) (value: int) =
    let saldo = DatabaseQuerys.getSaldo id
    if saldo - value < 0 then
      -1
    else
      let transation = DatabaseQuerys.newTransaction id value 'D' "Debito"
      printf "Transacao realizada com sucesso %A" transation
      transation

  let credit (id: int) (value: int) =
    let limite = DatabaseQuerys.getLimit id
    let saldo = DatabaseQuerys.getSaldo id
    if limite - saldo - value < 0 then
      -1
    else
      let transation = DatabaseQuerys.newTransaction id value 'C' "Credito"
      printf "Transacao realizada com sucesso %A" transation
      transation
