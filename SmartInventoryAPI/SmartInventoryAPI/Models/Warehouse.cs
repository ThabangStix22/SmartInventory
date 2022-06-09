using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartInventoryAPI.Models
{
    public class Warehouse
    {
        Warehouse(string WareID,string WareLocation,int WareCapacity,string WareManager,
            DateTime WareStartDate,string WareActive,int WareOfficeNo)
        {
            this.WareID = WareID;
            this.WareLocation = WareLocation;
            this.WareCapacity = WareCapacity;
            this.WareManager = WareManager;
            this.WareStartDate = WareStartDate;
            this.WareActive = WareActive.ElementAt<char>(0).ToString();
            this.WareOfficeNo = WareOfficeNo;
        }

        public Warehouse()
        {

        }
        
        public string WareID { get; set; }
        public string WareLocation { get; set; }
        public int WareCapacity { get; set; }
        public string WareManager { get; set; }
        public DateTime WareStartDate { get; set; }
        public string WareActive { get; set; }
        public int WareOfficeNo { get; set; }
    }
}
