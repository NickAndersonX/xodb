//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace XODB.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Location
    {
        public Location()
        {
            this.X_LocationAlias = new HashSet<LocationAlias>();
            this.X_LocationHistory = new HashSet<LocationHistories>();
            this.X_LocationData = new HashSet<LocationData>();
        }
    
        public System.Guid LocationID { get; set; }
        public Nullable<System.Guid> LocationTypeID { get; set; }
        public string DefaultLocationName { get; set; }
        public string LocationCode { get; set; }
        public System.Data.Spatial.DbGeography LocationGeography { get; set; }
        public Nullable<decimal> LatitudeWGS84 { get; set; }
        public Nullable<decimal> LongitudeWGS84 { get; set; }
        public Nullable<decimal> Elevation { get; set; }
        public string CountryID { get; set; }
        public Nullable<System.Guid> CountryStateID { get; set; }
        public string DefaultCulture { get; set; }
        public Nullable<System.Guid> OwnerCompanyID { get; set; }
        public Nullable<System.Guid> ResponsibleContactID { get; set; }
        public string Comment { get; set; }
        public int Version { get; set; }
        public Nullable<System.Guid> VersionAntecedentID { get; set; }
        public Nullable<int> VersionCertainty { get; set; }
        public Nullable<System.Guid> VersionWorkflowInstanceID { get; set; }
        public Nullable<System.Guid> VersionUpdatedBy { get; set; }
        public Nullable<System.Guid> VersionDeletedBy { get; set; }
        public Nullable<System.Guid> VersionOwnerContactID { get; set; }
        public Nullable<System.Guid> VersionOwnerCompanyID { get; set; }
        public Nullable<System.DateTime> VersionUpdated { get; set; }
    
        public virtual ICollection<LocationAlias> X_LocationAlias { get; set; }
        public virtual ICollection<LocationHistories> X_LocationHistory { get; set; }
        public virtual ICollection<LocationData> X_LocationData { get; set; }
    }
}