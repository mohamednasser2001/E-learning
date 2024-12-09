using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.IRepository;
using Models;

namespace Data.Repository
{
    public class VideoRepository:Repository<Video>,IVideoRepository
    {
        public VideoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
