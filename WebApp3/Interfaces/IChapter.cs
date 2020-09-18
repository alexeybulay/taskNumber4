using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp3.Models;

namespace WebApp3.Interfaces
{
    public interface IChapter
    {
        IEnumerable<Chapter> GetChapters(Fanfic fanfic);
       Chapter GetChapterById(int id);
        void AddChapter(Chapter chapter);
        void Update(Chapter chapter);
        void Delete(Chapter chapter);
    }
}
