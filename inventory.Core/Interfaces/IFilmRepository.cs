using Inventory.Core.DAO;
using Inventory.Core.DTOs.Film;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Interfaces
{
    public interface IFilmRepository
    {
        List<FilmDto> GetFilmList();
    }
}
