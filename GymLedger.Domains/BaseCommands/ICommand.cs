using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.BaseCommands
{
    public interface ICommand<out TResult> { }
    public interface ICommand { }
}
