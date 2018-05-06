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
        ClassListModel[] ClassList { get; }
    }
    public class IndividualClassRepository: IIndividualClassRepository
    {
        public void Add(int userId, int classId)
        {
            var class_info = GetClass(classId);

            DatabaseAccessor.Instance.Users.FirstOrDefault(t => t.UserId == userId)
                                                   .Classes
                                                  .Add(new LearningCenter.Database.Class
                                                  {
                                                      ClassId = class_info.Id,
                                                      ClassName = class_info.Name,
                                                      ClassDescription = class_info.Description,
                                                      ClassPrice = class_info.Price
                                                  });
            DatabaseAccessor.Instance.SaveChanges(); 

        }

        public ClassListModel[] ClassList
        {
            get
            {
                return DatabaseAccessor.Instance.Classes
                    .Select(t => new ClassListModel { Id = t.ClassId, Name = t.ClassName, Description = t.ClassDescription, Price = t.ClassPrice })
                    .ToArray();
            }
        }

        private ClassListModel GetClass(int classId)
        {

            var class_info = DatabaseAccessor.Instance.Classes
                .FirstOrDefault(t => t.ClassId == classId);

            return new ClassListModel { Id = class_info.ClassId, Name = class_info.ClassName, Description = class_info.ClassDescription, Price = class_info.ClassPrice };
        }
    }
}