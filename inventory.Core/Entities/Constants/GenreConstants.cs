using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Core.Entities.Constants
{
    public static class genresConstants
    {
        // We use 'readonly' so the dictionary instance itself cannot be replaced.
        private static readonly Dictionary<int, string> _genres = new Dictionary<int, string>
    {
        { 1, "Action" },
        { 2, "Sci-Fi" },
        { 3, "Comedy" },
            {  4, "Drama"  },
            { 22, "Crime" },
            { 29, "Mystery" },
    };

        // Public property to access the data
        public static Dictionary<int, string> Genres => _genres;

        // Optional: A helper method to get a name by ID safely
        public static string GetName(int id)
        {
            return _genres.TryGetValue(id, out var name) ? name : "Unknown";
        }
    }
}
