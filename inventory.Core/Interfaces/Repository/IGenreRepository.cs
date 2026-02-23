using Inventory.Core.Entities.DTOs.Genre;
using System.Collections.Generic;

namespace Inventory.Core.Interfaces.Repository
{
    public interface IGenreRepository
    {
        List<GenreDto> GetGenreList();
        GenreDto GetGenreById(int id);
        GenreDto addGenre(CreateGenreDto dto);
        GenreDto updateGenre(updateGenreDto dto);
    }
}
