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
    public class EditOneRepMaxCommandValidator : ICommandHandler<EditOneRepMaxCommand, DataCommandResponse>
    {
        readonly ICommandHandler<EditOneRepMaxCommand, DataCommandResponse> decorated;
        EditOneRepMaxCommand Command;

        public EditOneRepMaxCommandValidator(ICommandHandler<EditOneRepMaxCommand, DataCommandResponse> decorated, EditOneRepMaxCommand command)
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

    public class EditOneRepMaxCommandHandler : ICommandHandler<EditOneRepMaxCommand, DataCommandResponse>
    {
        readonly ICommandHandler<EditOneRepMaxCommand, DataCommandResponse> decorated;
        EditOneRepMaxCommand Command;

        public EditOneRepMaxCommandHandler(ICommandHandler<EditOneRepMaxCommand, DataCommandResponse> decorated, EditOneRepMaxCommand command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public DataCommandResponse Execute()
        {
            using (DataContext db = new DataContext())
            {

                // get user
                var user = db.Users.FirstOrDefault(u => u.Username == this.Command.UserIdentity.Username);

                var orm = db.OneRepMaxes.SingleOrDefault(r => r.UniqueId == this.Command.View.UniqueId);

                // update one rep max value
                orm.Weight = this.Command.View.Weight;
                orm.Date = this.Command.View.Date;

                db.SaveChanges();

                var response = new DataCommandResponse();

                response.Success = true;
                response.Message = "One rep max edited successfully";

                return response;
            }
        }
    }
}
