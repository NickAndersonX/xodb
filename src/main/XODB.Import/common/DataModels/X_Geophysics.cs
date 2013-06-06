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
    
    public partial class X_Geophysics
    {
        public X_Geophysics()
        {
            this.X_GeophysicsMetadata = new HashSet<X_GeophysicsMetadata>();
        }
    
        public System.Guid GeophysicsID { get; set; }
        public string FileName { get; set; }
        public Nullable<System.Guid> OriginalFileDataID { get; set; }
        public Nullable<System.Guid> HeaderID { get; set; }
        public Nullable<System.Guid> DimensionUnitID { get; set; }
        public string LasVersion { get; set; }
        public string LasFormat { get; set; }
        public string LasWrap { get; set; }
        public string LasDelimiter { get; set; }
        public string LasNullValue { get; set; }
        public int Version { get; set; }
        public Nullable<System.Guid> VersionAntecedentID { get; set; }
        public Nullable<int> VersionCertainty { get; set; }
        public Nullable<System.Guid> VersionWorkflowInstanceID { get; set; }
        public Nullable<System.Guid> VersionUpdatedBy { get; set; }
        public Nullable<System.Guid> VersionDeletedBy { get; set; }
        public Nullable<System.Guid> VersionOwnerContactID { get; set; }
        public Nullable<System.Guid> VersionOwnerCompanyID { get; set; }
        public Nullable<System.DateTime> VersionUpdated { get; set; }
    
        public virtual X_Header X_Header { get; set; }
        public virtual ICollection<X_GeophysicsMetadata> X_GeophysicsMetadata { get; set; }
    }
}
