using Inventory.Core.Entities.DAO;
using Inventory.Core.Entities.DTOs.Login;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Core.Interfaces.Repository
{
    public interface ILoginRepository 
    {
        Task<Administrator?> getUsernameAsync(string username);
    }
}
