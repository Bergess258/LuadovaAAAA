//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BdOfResume.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeePost
    {
        public int Id { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> PostId { get; set; }
        public Nullable<System.DateTime> DateOfBegin { get; set; }
        public Nullable<System.DateTime> DateOfEnd { get; set; }
        public Nullable<int> DepartamentId { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Post Post { get; set; }
    }
}
