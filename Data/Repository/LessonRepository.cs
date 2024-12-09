using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.IRepository;
using Models;

namespace Data.Repository
{
    public class LessonRepository:Repository<Lesson>,ILessonRepository
    {
        public LessonRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
