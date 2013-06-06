//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace XODB.Import.DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class X_Geophysics_Parameter
    {
        public X_Geophysics_Parameter()
        {
            this.X_GeophysicsMetadata = new HashSet<X_GeophysicsMetadata>();
        }
    
        public System.Guid ParameterID { get; set; }
        public string ParameterType { get; set; }
        public string ParameterName { get; set; }
        public Nullable<decimal> DefaultParameterValue { get; set; }
        public string DefaultParameterText { get; set; }
        public Nullable<System.Guid> UnitID { get; set; }
        public string Format { get; set; }
        public string Description { get; set; }
        public string Locale { get; set; }
        public int Version { get; set; }
        public Nullable<System.Guid> VersionAntecedentID { get; set; }
        public Nullable<int> VersionCertainty { get; set; }
        public Nullable<System.Guid> VersionWorkflowInstanceID { get; set; }
        public Nullable<System.Guid> VersionUpdatedBy { get; set; }
        public Nullable<System.Guid> VersionDeletedBy { get; set; }
        public Nullable<System.Guid> VersionOwnerContactID { get; set; }
        public Nullable<System.Guid> VersionOwnerCompanyID { get; set; }
        public Nullable<System.DateTime> VersionUpdated { get; set; }
    
        public virtual ICollection<X_GeophysicsMetadata> X_GeophysicsMetadata { get; set; }
    }
}
