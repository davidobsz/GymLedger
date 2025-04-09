using GymLedger.Data;
using GymLedger.Domains.Api.Commands;
using GymLedger.Domains.Areas.Profile.Commands;
using GymLedger.Domains.BaseCommands;
using GymLedger.Helpers.PasswordHelper;
using GymLedger.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Domains.Api.CommandHandlers
{
    public class ChangePasswordCommandValidatorApi : ICommandHandler<ChangePasswordCommandApi, ApiDataCommandResponse>
    {
        readonly ICommandHandler<ChangePasswordCommandApi, ApiDataCommandResponse> decorated;
        ChangePasswordCommandApi Command;

        public ChangePasswordCommandValidatorApi(ICommandHandler<ChangePasswordCommandApi, ApiDataCommandResponse> decorated, ChangePasswordCommandApi command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public ApiDataCommandResponse Execute()
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
                    var previousPasswords = db.PreviousPasswords.Where(u => u.UserId == user.Id).OrderByDescending(u => u.DateAdded).Take(5).ToList();

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

                    if (previousPasswords.Any())
                    {
                        foreach (var pass in previousPasswords)
                        {
                            if (PasswordHelper.HashPassword(this.Command.View.NewPassword) == pass.Password)
                            {
                                throw new Exception("New Password cannot be the same as previous passwords");
                            }
                        }
                    }
                }
            }

            return this.decorated.Execute();
        }
    }

    public class ChangePasswordCommandHandlerApi : ICommandHandler<ChangePasswordCommandApi, ApiDataCommandResponse>
    {
        readonly ICommandHandler<ChangePasswordCommandApi, ApiDataCommandResponse> decorated;
        ChangePasswordCommandApi Command;

        public ChangePasswordCommandHandlerApi(ICommandHandler<ChangePasswordCommandApi, ApiDataCommandResponse> decorated, ChangePasswordCommandApi command)
        {
            this.decorated = decorated;
            this.Command = command;
        }

        public ApiDataCommandResponse Execute()
        {
            using (DataContext db = new DataContext())
            {
                // get user
                var user = db.Users.FirstOrDefault(u => u.Username == this.Command.UserIdentity.Username);

                if (user.PreviousPasswords == null)
                {
                    user.PreviousPasswords = new List<PreviousPassword>();
                }

                var previousPasswords = db.PreviousPasswords
                                        .Where(u => u.UserId == user.Id)
                                        .OrderByDescending(u => u.DateAdded)
                                        .ToList();

                // remove any passwords after the 4th most recent one
                db.PreviousPasswords.RemoveRange(previousPasswords.Skip(4));

                PreviousPassword previousPassword = new PreviousPassword
                {
                    Password = user.Password,
                    User = user,
                    UserId = user.Id,
                    DateAdded = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                };

                user.PreviousPasswords.Add(previousPassword);
                user.Password = PasswordHelper.HashPassword(this.Command.View.NewPassword);
                db.SaveChanges();

                var response = new ApiDataCommandResponse();

                response.Success = true;
                response.Message = "Password Changed Successfully";

                return response;
            }
        }
    }
}
