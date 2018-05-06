using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LearningCenter.Database;

namespace LearningCenter.Website
{
    public class DatabaseAccessor
    {
        private static readonly LearningCenterDbEntities entities;

        static DatabaseAccessor()
        {
            entities = new LearningCenterDbEntities();
            entities.Database.Connection.Open();
        }

        public static LearningCenterDbEntities Instance
        {
            get
            {
                return entities;
            }
        }
    }
}