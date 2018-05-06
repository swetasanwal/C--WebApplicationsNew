using System;
using System.Linq;
using LearningCenter.Website.Models;

namespace LearningCenter.Website
{

    public interface IClassListRepository
    {
        ClassListModel[] ClassList { get; }
    }

    
    public class ClassListRepository : IClassListRepository
    {
        public ClassListModel[] ClassList
        {
            get
            {
                return DatabaseAccessor.Instance.Classes
                    .Select(t => new ClassListModel { Id = t.ClassId, Name = t.ClassName, Description = t.ClassDescription, Price = t.ClassPrice })
                    .ToArray();
            }
        }
    }
}