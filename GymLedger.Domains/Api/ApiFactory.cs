using GymLedger.Domains.Account.CommandHandlers;
using GymLedger.Domains.Account.Commands;
using GymLedger.Domains.Api.CommandHandlers;
using GymLedger.Domains.Api.Commands;
using GymLedger.Domains.Areas.Ledger.QueryHandlers;
using GymLedger.Domains.Areas.Ledger.Querys;
using GymLedger.Domains.BaseCommands;
using GymLedger.Domains.BaseQuerys;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Api
{
    public static partial class ApiFactory
    {
        #region Commands
        public static ICommandHandler<LoginApiAccountCommand, ApiDataCommandResponse> LoginApiAccountCommandHandler(LoginApiAccountCommand command)
        {
            return new LoginApiAccountCommandValidator(new LoginApiAccountCommandHandler(command),command);
        }
        #endregion

        #region Querys
        public static IQueryHandler<GetExercisesQueryApi, GetExerciseView> GetExercisesQueryHandlerApi(GetExercisesQueryApi query)
        {
            return new GetExercisesQueryValidatorApi(new GetExercisesQueryHandlerApi(query), query);
        }
        #endregion
    }
}
