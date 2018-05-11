using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LearningCenter.Website.Models;

namespace LearningCenter.Website
{
    public interface IIndividualClassRepository
    {
        void Add(int userId, int classID);
        ClassListModel[] ClassList(int userId);
    }
    public class IndividualClassRepository: IIndividualClassRepository
    {
        public void Add(int userId, int classId)
        {
            var user = GetUser(userId);

            var class_info = GetClass(classId);
            
            user.Classes.Add(class_info);

            DatabaseAccessor.Instance.SaveChanges();

        }

        public ClassListModel[] ClassList(int userId)
        {
            var user = GetUser(userId);

            return user.Classes
                .Select(t => new ClassListModel { Id = t.ClassId, Name = t.ClassName, Description = t.ClassDescription, Price = t.ClassPrice })
                .ToArray();
        }

        /*public ClassListModel[] ClassList
        {
            get
            {
                return DatabaseAccessor.Instance.Classes
                    .Select(t => new ClassListModel { Id = t.ClassId, Name = t.ClassName, Description = t.ClassDescription, Price = t.ClassPrice })
                    .ToArray();
            }
        }*/

        private LearningCenter.Database.Class GetClass(int classId)
        {

            var class_info = DatabaseAccessor.Instance.Classes
                .Where(t => t.ClassId == classId)
                .FirstOrDefault();

            return class_info;

        }

        private LearningCenter.Database.User GetUser(int userId)
        {
            var user_info = DatabaseAccessor.Instance.Users
                    .Where(t => t.UserId == userId)
                    .FirstOrDefault();

            return user_info;
        }
    }
}