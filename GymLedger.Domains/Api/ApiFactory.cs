using GymLedger.Domains.Account.CommandHandlers;
using GymLedger.Domains.Account.Commands;
using GymLedger.Domains.Api.CommandHandlers;
using GymLedger.Domains.Api.Commands;
using GymLedger.Domains.Api.QueryHandler;
using GymLedger.Domains.Api.Querys;
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

        public static ICommandHandler<AddExerciseCommand, ApiDataCommandResponse> AddExerciseCommandHandlerApi(AddExerciseCommand command)
        {
            return new AddExerciseCommandValidatorApi(new AddExerciseCommandHandlerApi(null,command), command);
        }

        public static ICommandHandler<EditExerciseCommandApi, ApiDataCommandResponse> EditExerciseCommandHandlerApi(EditExerciseCommandApi command)
        {
            return new EditExerciseCommandValidatorApi(new EditExerciseCommandApiHandler(null, command), command);
        }

        public static ICommandHandler<DeleteExerciseCommandApi, ApiDataCommandResponse> DeleteExerciseCommandHandlerApi(DeleteExerciseCommandApi command)
        {
            return new DeleteExerciseCommandValidatorApi(new DeleteExerciseCommandHandlerApi(null, command), command);
        }
        #endregion

        #region Querys
        public static IQueryHandler<GetExercisesQueryApi, GetExerciseView> GetExercisesQueryHandlerApi(GetExercisesQueryApi query)
        {
            return new GetExercisesQueryValidatorApi(new GetExercisesQueryHandlerApi(query), query);
        }

        public static IQueryHandler<GetExerciseQueryApi, ExerciseDetailView> GetExerciseQueryHandlerApi(GetExerciseQueryApi query)
        {
            return new GetExerciseQueryValidatorApi(new GetExerciseQueryHandlerApi(query), query);
        }

        public static IQueryHandler<GetSessionsQueryApi, GetSessionView> GetSessionsQueryHandlerApi(GetSessionsQueryApi query)
        {
            return new GetSessionsQueryValidatorApi(new GetSessionsQueryHandlerApi(query), query);
        }

        public static IQueryHandler<GetSessionQueryApi, SessionDetailView> GetSessionQueryHandlerApi(GetSessionQueryApi query)
        {
            return new GetSessionQueryValidatorApi(new GetSessionQueryHandlerApi(query), query);
        }
        #endregion
    }
}
