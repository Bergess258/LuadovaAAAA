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
    
    public partial class Participation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Participation()
        {
            this.EmpTask = new HashSet<EmpTask>();
        }
    
        public int Id { get; set; }
        public Nullable<int> IdEmp { get; set; }
        public Nullable<int> IdProj { get; set; }
        public Nullable<int> IdRole { get; set; }
    
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmpTask> EmpTask { get; set; }
        public virtual Project Project { get; set; }
        public virtual Role Role { get; set; }
    }
}
