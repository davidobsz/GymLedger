using GymLedger.Domains.Account.CommandHandlers;
using GymLedger.Domains.Account.Commands;
using GymLedger.Domains.Areas.Ledger.CommandHandlers;
using GymLedger.Domains.Areas.Ledger.Commands;
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

namespace GymLedger.Domains.Areas.Ledger
{
    public static partial class LedgerFactory
    {
        #region Commands
        public static ICommandHandler<AddExerciseCommand, DataCommandResponse> AddExerciseCommandHandler(AddExerciseCommand command)
        {
            return new AddExerciseCommandValidator(new AddExerciseCommandHandler(null, command), command);
        }

        public static ICommandHandler<AddSessionCommand, DataCommandResponse> AddSessionCommandHandler(AddSessionCommand command)
        {
            return new AddSessionCommandValidator(new AddSessionCommandHandler(null, command), command);
        }

        public static ICommandHandler<EditExerciseCommand, DataCommandResponse> EditExerciseCommandHandler(EditExerciseCommand command)
        {
            return new EditExerciseCommandValidator(new EditExerciseCommandHandler(null, command), command);
        }
        #endregion

        #region Querys
        public static IQueryHandler<GetExercisesQuery, GetExerciseView> GetExercisesQueryHandler(GetExercisesQuery query)
        {
            return new GetExercisesQueryValidator(new GetExercisesQueryHandler(query), query);
        }

        public static IQueryHandler<GetSessionsQuery, GetSessionView> GetSessionsQueryHandler(GetSessionsQuery query)
        {
            return new GetSessionsQueryValidator(new GetSessionsQueryHandler(query), query);
        }

        public static IQueryHandler<GetExerciseDetailQuery, ExerciseDetailView> GetExerciseDetailsQueryHandler(GetExerciseDetailQuery query)
        {
            return new GetExerciseDetailQueryValidator(new GetExerciseDetailQueryHandler(query), query);
        }

        #endregion
    }
}
