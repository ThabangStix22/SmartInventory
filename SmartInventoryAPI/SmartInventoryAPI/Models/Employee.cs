using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartInventoryAPI.Models
{
    public class Employee
    {
        Employee(int EmpID,string EmpName,string EmpSurname,DateTime EmpDateOfEmplyt,
            int EmpPhoneNo,string EmpEmail,string EmpPassword,string EmpRole,
            string EmpLoc,string EmpActive,string EmpImage,string WareID)
        {
            this.EmpID = EmpID;
            this.EmpName = EmpName;
            this.EmpSurname = EmpSurname;
            this.EmpDateOfEmplyt = EmpDateOfEmplyt;
            this.EmpPhoneNo = EmpPhoneNo;
            this.EmpEmailAddress = EmpEmailAddress;
            this.EmpPassword = EmpPassword;
            this.EmpRole = EmpRole;
            this.EmpLocation = EmpLocation;
            this.EmpActive = EmpActive.ElementAt<char>(0).ToString();
            this.EmpImage = EmpImage;
            this.WareID = WareID;
        }

       public Employee()
        {

        }
        public int EmpID { set; get; }
        public string EmpName { set; get; }
        public string EmpSurname{ set; get; }
        public DateTime EmpDateOfEmplyt { set; get; }
        public int EmpPhoneNo { set; get; }
        public string EmpEmailAddress { set; get; }
        public string EmpPassword{set; get;}
        public string EmpRole{set; get; }
        public string EmpLocation{set; get;}
        public string EmpActive { set; get; }
        public string EmpImage{set; get;}
        public string WareID { get; set; }
}
}
