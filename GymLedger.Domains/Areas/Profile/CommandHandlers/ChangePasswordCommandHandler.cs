using GymLedger.Data;
using GymLedger.Domains.Areas.Ledger.Commands;
using GymLedger.Domains.Areas.Profile.Commands;
using GymLedger.Domains.BaseCommands;
using GymLedger.Helpers.PasswordHelper;
using GymLedger.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Areas.Profile.CommandHandlers
{

    public class ChangePasswordCommandValidator : ICommandHandler<ChangePasswordCommand, DataCommandResponse>
    {
        readonly ICommandHandler<ChangePasswordCommand, DataCommandResponse> decorated;
        ChangePasswordCommand Command;

        public ChangePasswordCommandValidator(ICommandHandler<ChangePasswordCommand, DataCommandResponse> decorated, ChangePasswordCommand command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public DataCommandResponse Execute()
        {
            Command.ValidateMe();

            using (DataContext db = new DataContext()) 
            {
                bool exists = db.Users.Any(u => u.Username == this.Command.UserIdentity.Username);

                if (!exists)
                {
                    throw new Exception("Could not find user");
                }

                if (exists)
                {
                    var user = db.Users.Where(u => u.Username == this.Command.UserIdentity.Username).FirstOrDefault();

                    if (user.IsLockedOut)
                    {
                        if (user.LockedOutDate.Value.AddMinutes(15) > DateTime.UtcNow)
                        {
                            throw new Exception("You have been locked out of your account for multiple failed login attempts");
                        }

                        if (user.LockedOutDate.Value.AddMinutes(15) < DateTime.UtcNow)
                        {
                            user.LockedOutDate = null;
                            user.IsLockedOut = false;
                            user.FailedLoginAttempts = 0;

                            db.SaveChanges();
                        }

                    }

                    if (user.Password != PasswordHelper.HashPassword(this.Command.View.CurrentPassword))
                    {
                        user.FailedLoginAttempts += 1;
                        db.SaveChanges();

                        if (user.FailedLoginAttempts > 4)
                        {
                            user.LockedOutDate = DateTime.UtcNow;
                            user.IsLockedOut = true;

                            db.SaveChanges();

                            throw new Exception("You have been locked out of your account for multiple failed login attempts");
                        }

                        throw new Exception("Username or password is incorrect");
                    }
                }
            }

            return this.decorated.Execute();
        }
    }

    public class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand, DataCommandResponse>
    {
        readonly ICommandHandler<ChangePasswordCommand, DataCommandResponse> decorated;
        ChangePasswordCommand Command;

        public ChangePasswordCommandHandler(ICommandHandler<ChangePasswordCommand, DataCommandResponse> decorated, ChangePasswordCommand command)
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

                user.Password = PasswordHelper.HashPassword(this.Command.View.NewPassword);
                db.SaveChanges();

                var response = new DataCommandResponse();

                response.Success = true;
                response.Message = "Password Changed Successfully";

                Helpers.CookieAuth.AuthCookieHelper.Logout();
                return response;
            }
        }
    }
}
