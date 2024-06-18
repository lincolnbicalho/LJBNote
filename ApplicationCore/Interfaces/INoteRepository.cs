using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface INoteRepository : IBaseRepository<Note>
    {
        Task<bool> UpdateText(int Id, string Text);
    }
}
