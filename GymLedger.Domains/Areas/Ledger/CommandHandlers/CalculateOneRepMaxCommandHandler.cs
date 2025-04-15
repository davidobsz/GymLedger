using GymLedger.Data;
using GymLedger.Domains.Areas.Ledger.Commands;
using GymLedger.Domains.BaseCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Areas.Ledger.CommandHandlers
{
    public class CalculateOneRepMaxCommandValidator : ICommandHandler<CalculateOneRepMaxCommand, DataCommandResponse>
    {
        readonly ICommandHandler<CalculateOneRepMaxCommand, DataCommandResponse> decorated;
        CalculateOneRepMaxCommand Command;

        public CalculateOneRepMaxCommandValidator(ICommandHandler<CalculateOneRepMaxCommand, DataCommandResponse> decorated, CalculateOneRepMaxCommand command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public DataCommandResponse Execute()
        {
            Command.ValidateMe();

            return this.decorated.Execute();
        }
    }

    public class CalculateOneRepMaxCommandHandler : ICommandHandler<CalculateOneRepMaxCommand, DataCommandResponse>
    {
        readonly ICommandHandler<CalculateOneRepMaxCommand, DataCommandResponse> decorated;
        CalculateOneRepMaxCommand Command;

        public CalculateOneRepMaxCommandHandler(ICommandHandler<CalculateOneRepMaxCommand, DataCommandResponse> decorated, CalculateOneRepMaxCommand command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public DataCommandResponse Execute()
        {
            using (DataContext db = new DataContext())
            {

                // get user
                var oneRepMax = Math.Round(this.Command.View.Weight * (1 + this.Command.View.Reps / 30.0), 2);

                var response = new DataCommandResponse();

                response.Success = true;
                response.Message = "One rep max calculated successfully";
                response.Data = new {OneRepMax = oneRepMax};
                return response;
            }
        }
    }
}
