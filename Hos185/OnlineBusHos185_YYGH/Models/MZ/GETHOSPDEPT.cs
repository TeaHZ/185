using System;
using System.Collections.Generic;
using System.Text;

namespace Hos185_His.Models.MZ
{
    public class GETHOSPDEPT
    {
        public string bookFlag { get; set; }
        public string branchCode { get; set; }
        public string deptFlag { get; set; }
        public string deptId { get; set; }
        public string deptName { get; set; }
        public string deptType { get; set; }
        public string regdeptFlag { get; set; }
        public string validState { get; set; }
    }

    public class GETHOSPDEPTDATA
    {


        public string bookFlag { get; set; }
        public string branchCode { get; set; }
        public string deptAddress { get; set; }
        public string deptEname { get; set; }
        public string deptId { get; set; }
        public string deptName { get; set; }
        public string deptPro { get; set; }
        public string deptType { get; set; }
        public string description { get; set; }
        public string genre { get; set; }
        public string regdeptFlag { get; set; }
        public string remark { get; set; }
        public string sortNo { get; set; }
        public string tatdeptFlag { get; set; }
        public string validState { get; set; }

    }
}
