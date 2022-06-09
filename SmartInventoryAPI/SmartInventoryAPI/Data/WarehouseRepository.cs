using SmartInventoryAPI.Interface;
using SmartInventoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace SmartInventoryAPI.Data
{
    public class WarehouseRepository : IWarehouse
    {
        internal ConnectionRepository conn = new ConnectionRepository();


        public int ActivateWarehouse(string WareID, char WareActivate)
        {
            //First get the specific warehouse
            char existWareActive;
            int control = -2;

            existWareActive = conn.OpenConnection().QuerySingleOrDefault<char>(@"SELECT WareActive FROM dbo.Warehouses "+ 
                                                                               "WHERE WareID ='"+WareID+"'");
            if (existWareActive != WareActivate)
            {
                control = conn.OpenConnection().Execute(@"UPDATE dbo.Warehouses
                                                        SET WareActive='"+WareActivate+"'" +
                                                        "WHERE WareID='"+WareID+"'");
            }else if(existWareActive==WareActivate)
            {
                control = 0;
            }

            conn.OpenConnection().Close();
            return control;
        }

        public int CreateWarehouse(Warehouse warehouse)
        {
            int control = -2;

            //first check if warehouse exists
            Warehouse existWareHouse = null;

            existWareHouse = conn.OpenConnection().QuerySingleOrDefault<Warehouse>(@"SELECT * FROM dbo.Warehouses
                                                                                    WHERE WareID='"+warehouse.WareID+"'");

            if(existWareHouse == null)
            {
                control = conn.OpenConnection().Execute(@"INSERT INTO dbo.Warehouses
                                                        VALUES('"+warehouse.WareID+"','"+warehouse.WareLocation+"'," +
                                                        "'"+warehouse.WareCapacity+"','"+warehouse.WareManager+"'," +
                                                        "'"+warehouse.WareStartDate+"','"+warehouse.WareActive+"'," +
                                                        ""+warehouse.WareOfficeNo+")",warehouse);
            }else if(existWareHouse != null)
            {
                //Warehouse exists
                control = -1;
            }

            conn.OpenConnection().Close();
            return control;
        }

        public IEnumerable<Warehouse> GetActiveWareHouses(char chActive)
        {
            IEnumerable<Warehouse> warehouses;
            IEnumerable<Warehouse> existWarehouses = null;

            warehouses = conn.OpenConnection().Query<Warehouse>(@"SELECT * FROM dbo.Warehouses
                                                                WHERE WareActive='"+chActive+"'");
            switch (chActive)
            {
                case 'Y' or 'y':
                    {
                        if(warehouses.ToList<Warehouse>().Count > 0)
                        {
                            existWarehouses = warehouses;
                        } else if(warehouses.ToList<Warehouse>().Count == 0)
                        {
                            existWarehouses = null;
                        }
                        break;
                    }

                case 'N' or 'n':
                    {
                        if(warehouses.ToList<Warehouse>().Count > 0)
                        {
                            existWarehouses = warehouses;
                        }else if(warehouses.ToList<Warehouse>().Count == 0)
                        {
                            existWarehouses = null;
                        }
                    }

                break;

                default:
                    Console.Error.WriteLine("Unexpected input. Error. Use 'Y' or 'N'");
                break;
            }
            conn.OpenConnection().Close();

            return existWarehouses;
        }

        public IEnumerable<Warehouse> GetAllWareHouses()
        {
            IEnumerable<Warehouse> warehouses;
            
            warehouses = conn.OpenConnection().Query<Warehouse>(@"SELECT * FROM dbo.Warehouses");

            return warehouses;
        }

        public Warehouse GetWareHouse(string WareID)
        {
            //Get specific warehouse
            Warehouse existWareHouse;
            
            existWareHouse = conn.OpenConnection().QuerySingleOrDefault<Warehouse>(@"SELECT * FROM dbo.Warehouses 
                                                                                    WHERE WareID = '"+WareID+"'");
            conn.OpenConnection().Close();
            return existWareHouse; 
        }

        public int UpdateWarehouse(Warehouse warehouse)
        {
            //First find warehouse
            Warehouse existWarehouse = null;
            int control = -2;

            existWarehouse = conn.OpenConnection().QuerySingleOrDefault<Warehouse>(@"SELECT * FROM dbo.Warehouses
                                                                                WHERE WareID='"+warehouse.WareID+"'");

            if(existWarehouse != null)
            {
                control = conn.OpenConnection().Execute(@"UPDATE dbo.Warehouses
                                                        SET WareLocation='" + warehouse.WareLocation + "'," +
                                                        "WareManager='" + warehouse.WareManager + "'," +
                                                        "WareCapactiy='"+warehouse.WareCapacity+"'," +
                                                        "WareActive='"+warehouse.WareActive+"'," +
                                                        "WareOfficeNo='"+warehouse.WareOfficeNo+"'");
            }
            else if(existWarehouse == null)
            {
                control = -1;
            }

            conn.OpenConnection().Close();

            return control;
        }


    }
}
