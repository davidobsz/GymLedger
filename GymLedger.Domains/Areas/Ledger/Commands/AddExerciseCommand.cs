using GymLedger.Domains.BaseCommands;
using GymLedger.Views.Account.Login;
using GymLedger.Views.Areas.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Areas.Ledger.Commands
{
    public class AddExerciseCommand : ICommand<DataCommandResponse>
    {
        public AddExerciseView View { get; set; }

        public AddExerciseCommand(AddExerciseView view)
        {
            this.View = view;
        }
        public void ValidateMe()
        {
            if (View.Name == null)
            {
                throw new Exception("Exercise name is empty");
            }

            
        }
    }
}
