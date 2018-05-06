using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LearningCenter.Website.Models;

namespace LearningCenter.Website
{
    public interface IUserRepository
    {
        UserModel LogIn(string email, string password);
        UserModel Register(string email, string password);
    }

    
    public class UserRepository: IUserRepository
    {
        public UserModel LogIn(string email, string password)
        {
            var user = DatabaseAccessor.Instance.Users
                .FirstOrDefault(t => t.UserEmail.ToLower() == email.ToLower()
                                      && t.UserPassword == password);

            if (user == null)
            {
                return null;
            }

            return new UserModel { Id = user.UserId, Name = user.UserEmail };
        }

        public UserModel Register(string email, string password)
        {
            var user = DatabaseAccessor.Instance.Users
                    .Add(new LearningCenter.Database.User { UserEmail = email, UserPassword = password });

            DatabaseAccessor.Instance.SaveChanges();

            return new UserModel { Id = user.UserId, Name = user.UserEmail };
        }
    }
}