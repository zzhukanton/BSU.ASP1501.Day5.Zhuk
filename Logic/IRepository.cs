using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Logic
{
    /// <summary>
    /// Interface for working with binary repository with list of books
    /// </summary>
    public interface IRepository
    {
        List<Book> LoadToList();
        void LoadToFile(List<Book> list);
    }
}
