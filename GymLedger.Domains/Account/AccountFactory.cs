using GymLedger.Domains.Account.CommandHandlers;
using GymLedger.Domains.Account.Commands;
using GymLedger.Domains.BaseCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Account
{
    public static partial class AccountFactory
    {
        #region Commands
        public static ICommandHandler<RegisterAccountCommand, DataCommandResponse> RegisterAccountCommandHandler(RegisterAccountCommand command)
        {
            return new RegisterAccountCommandValidator(new RegisterAccountCommandHandler(null, command), command);
        }
        #endregion

        #region Querys

        #endregion
    }
}
