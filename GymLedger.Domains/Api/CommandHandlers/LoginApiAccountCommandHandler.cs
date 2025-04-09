using GymLedger.Data;
using GymLedger.Domains.Api.Commands;
using GymLedger.Domains.BaseCommands;
using GymLedger.Helpers.JwtAuth;
using GymLedger.Helpers.PasswordHelper;
using GymLedger.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GymLedger.Domains.Api.CommandHandlers
{
    public class LoginApiAccountCommandValidator : ICommandHandler<LoginApiAccountCommand, ApiDataCommandResponse>
    {
        private readonly ICommandHandler<LoginApiAccountCommand, ApiDataCommandResponse> decorated;
        private readonly LoginApiAccountCommand command;

        public LoginApiAccountCommandValidator(ICommandHandler<LoginApiAccountCommand, ApiDataCommandResponse> decorated, LoginApiAccountCommand command)
        {
            this.decorated = decorated ?? throw new ArgumentNullException(nameof(decorated));
            this.command = command ?? throw new ArgumentNullException(nameof(command));
        }

        public ApiDataCommandResponse Execute()
        {
            command.ValidateMe();

            using (var db = new DataContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Username == command.View.Username);

                if (user == null)
                {
                    throw new Exception("Username or password incorrect.");
                }

                if (user.IsLockedOut && user.LockedOutDate.HasValue && user.LockedOutDate.Value.AddMinutes(15) > DateTime.UtcNow)
                {
                    throw new Exception("Your account is locked. Please try again later.");
                }

                if (user.Password != PasswordHelper.HashPassword(command.View.Password))
                {
                    user.FailedLoginAttempts++;

                    if (user.FailedLoginAttempts > 4)
                    {
                        user.LockedOutDate = DateTime.UtcNow;
                        user.IsLockedOut = true;
                    }

                    db.SaveChanges();
                    throw new Exception("Username or password incorrect.");
                }

                user.FailedLoginAttempts = 0;
                user.IsLockedOut = false;
                user.LockedOutDate = null;

                db.SaveChanges();
            }

            return decorated.Execute();
        }
    }

    public class LoginApiAccountCommandHandler : ICommandHandler<LoginApiAccountCommand, ApiDataCommandResponse>
    {
        private readonly LoginApiAccountCommand command;

        public LoginApiAccountCommandHandler(LoginApiAccountCommand command)
        {
            this.command = command ?? throw new ArgumentNullException(nameof(command));
        }

        public ApiDataCommandResponse Execute()
        {
            using (var db = new DataContext())
            {
                var user = db.Users.SingleOrDefault(u => u.Username == command.View.Username);

                var login = new PreviousLogin
                {
                    UniqueId = Guid.NewGuid().ToString("N"),
                    LoginDate = DateTime.UtcNow,
                    DateAdded = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    UserId = user.Id,
                    User = user
                };

                if (user.PreviousLogins == null)
                {
                    user.PreviousLogins = new List<PreviousLogin>();
                }

                user.PreviousLogins.Add(login);
                db.SaveChanges();

                string token = JwtTokenHelper.GenerateToken(user);

                return new ApiDataCommandResponse
                {
                    Success = true,
                    Token = token,
                    Message = "Login successful."
                };
            }
        }
    }
}
