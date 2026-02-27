using Inventory.Core.Entities.DTOs.OMDb;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.Interfaces.Gateway
{
    public interface IimdbGateway
    {
        Task<movieResponse> getMovie(string imdbId);
    }
}
