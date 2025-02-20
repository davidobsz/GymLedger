using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.BaseCommands
{
    public interface ICommandHandler<in TCommand, out TResult> where TCommand: ICommand<TResult>
    {
        TResult Execute();
    }
}
