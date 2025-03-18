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

        public static ICommandHandler<DeleteExerciseCommand, DataCommandResponse> DeleteExerciseCommandHandler(DeleteExerciseCommand command)
        {
            return new DeleteExerciseCommandValidator(new DeleteExerciseCommandHandler(null, command), command);
        }

        public static ICommandHandler<EditSessionCommand, DataCommandResponse> EditSessionCommandHandler(EditSessionCommand command)
        {
            return new EditSessionCommandValidator(new EditSessionCommandHandler(null, command), command);
        }

        public static ICommandHandler<DeleteSessionCommand, DataCommandResponse> DeleteSessionCommandHandler(DeleteSessionCommand command)
        {
            return new DeleteSessionCommandValidator(new DeleteSessionCommandHandler(null, command), command);
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

        public static IQueryHandler<GetSessionDetailQuery, SessionDetailView> GetSessionDetailsQueryHandler(GetSessionDetailQuery query)
        {
            return new GetSessionDetailQueryValidator(new GetSessionDetailQueryHandler(query), query);
        }

        public static IQueryHandler<GetExercisesPiechartDataQuery, GetExercisesPiechartDataView> GetExercisesPiechartDataDetailsQueryHandler(GetExercisesPiechartDataQuery query)
        {
            return new GetExercisesPiechartDataQueryValidator(new GetExercisesPiechartDataQueryHandler(query), query);
        }

        public static IQueryHandler<GetSessionsLinechartDataQuery, GetSessionsLinechartDataView> GetSessionsLinechartDataDetailsQueryHandler(GetSessionsLinechartDataQuery query)
        {
            return new GetSessionsLinechartDataQueryValidator(new GetSessionsLinechartDataQueryHandler(query), query);
        }

        public static IQueryHandler<GetSetsBarchartDataQuery, GetSetsBarchartDataView> GetSetsBarchartDataDetailsQueryHandler(GetSetsBarchartDataQuery query)
        {
            return new GetSetsBarchartDataQueryValidator(new GetSetsBarchartDataQueryHandler(query), query);
        }

        public static IQueryHandler<GetExercisesSetsBarchartQuery, GetExercisesSetsBarchartView> GetExercisesSetsBarchartQueryHandler(GetExercisesSetsBarchartQuery query)
        {
            return new GetExercisesSetsBarchartQueryValidator(new GetExercisesSetsBarchartQueryHandler(query), query);
        }

        public static IQueryHandler<GetOneRepMaxLineChartQuery, GetOneRepMaxLineChartView> GetOneRepMaxLineChartQueryHandler(GetOneRepMaxLineChartQuery query)
        {
            return new GetOneRepMaxLineChartQueryValidator(new GetOneRepMaxLineChartQueryHandler(query), query);
        }

        public static IQueryHandler<GetUniqueExerciseQuery, ExerciseDetailView> GetUniqueExerciseQueryHandler(GetUniqueExerciseQuery query)
        {
            return new GetUniqueExerciseQueryValidator(new GetUniqueExerciseQueryHandler(query), query);
        }
        #endregion
    }
}
