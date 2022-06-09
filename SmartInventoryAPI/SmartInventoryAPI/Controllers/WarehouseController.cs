using Microsoft.AspNetCore.Mvc;
using SmartInventoryAPI.Data;
using SmartInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartInventoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        internal WarehouseRepository WareRepo = new WarehouseRepository();

        [HttpPost("CreateWarehouse")]
        public int CreateWarehouse(Warehouse warehouse)
        {
            return WareRepo.CreateWarehouse(warehouse);
        }

        
        [HttpGet("GetAllWareHouses")]
        public IEnumerable<Warehouse> GetAllWareHouses()
        {
            return WareRepo.GetAllWareHouses();
        }

        [HttpGet("GetWareHouse/{WareID}")]
        public Warehouse GetWareHouse(string WareID)
        {
            return WareRepo.GetWareHouse(WareID);
        }

        [HttpGet("GetActiveWareHouses/{chActive}")]
        public IEnumerable<Warehouse> GetActiveWarehouses(char chActive)
        {
            return WareRepo.GetActiveWareHouses(chActive);
        }

        
        [HttpPut("UpdateWarehouse")]
        public int UpdateWarehouse(Warehouse warehouse)
        {
            return WareRepo.UpdateWarehouse(warehouse);
        }

        
        [HttpPut("ActivateWarehouse/{WareID}/{WareActivate}")]
        public int ActivateWarehouse(string WareID, char WareActivate)
        {
            return WareRepo.ActivateWarehouse(WareID, WareActivate);
        }

       
    }
}
