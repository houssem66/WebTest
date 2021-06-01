using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourMe.Data.Entities.Enum;

namespace TourMe.Data.Entities
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SubcategoryOf : Attribute
    {
        public SubcategoryOf(TypeExperience cat)
        {
            TypeExperience = cat;
        }
        public TypeExperience TypeExperience { get; private set; }
    }
}
