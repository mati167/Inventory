using Inventory.Core.Entities.DTOs.Login;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Core.Interfaces.Services
{
    public interface ILoginService 
    {
        Task<bool> LoginAsync(loginDTO login);
    }
}
