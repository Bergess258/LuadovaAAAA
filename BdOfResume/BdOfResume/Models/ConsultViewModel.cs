using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BdOfResume.Models
{
    public class ListAndAvProj
    {
        public List<Participation> list { get; set; }
        public List<string> avProj { get; set; }
        public string Task { get; set; }
    }
    public class forEmpTask
    {
        public List<EmpTask> list { get; set; }
        public List<string> avProj { get; set; }
    }
}