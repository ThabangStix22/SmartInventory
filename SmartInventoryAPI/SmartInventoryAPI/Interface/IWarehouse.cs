using SmartInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartInventoryAPI.Interface
{
    public interface IWarehouse
    {
        //CRUD Operations

        int CreateWarehouse(Warehouse newWarehouse);

        Warehouse GetWareHouse(string WareID);
        IEnumerable<Warehouse> GetAllWareHouses();
        IEnumerable<Warehouse> GetActiveWareHouses(char chActive);

        int UpdateWarehouse(Warehouse warehouse);

        int ActivateWarehouse(string WareID, char WareActivate);


    }
}
