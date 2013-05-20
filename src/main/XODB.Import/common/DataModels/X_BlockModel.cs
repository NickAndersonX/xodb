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
    
    public partial class X_BlockModel
    {
        public X_BlockModel()
        {
            this.X_BlockModelMetadata = new HashSet<X_BlockModelMetadata>();
            this.X_BlockModelBlock = new HashSet<X_BlockModelBlock>();
            this.X_BlockModelBlockData = new HashSet<X_BlockModelBlockData>();
        }
    
        public System.Guid BlockModelID { get; set; }
        public System.Guid ProjectID { get; set; }
        public string Alias { get; set; }
        public Nullable<decimal> OriginX { get; set; }
        public Nullable<decimal> OriginY { get; set; }
        public Nullable<decimal> OriginZ { get; set; }
        public Nullable<bool> IsIJKCalculated { get; set; }
        public Nullable<int> OriginI { get; set; }
        public Nullable<int> OriginJ { get; set; }
        public Nullable<int> OriginK { get; set; }
        public Nullable<int> MaximumI { get; set; }
        public Nullable<int> MaximumJ { get; set; }
        public Nullable<int> MaximumK { get; set; }
        public Nullable<int> MinimumI { get; set; }
        public Nullable<int> MinimumJ { get; set; }
        public Nullable<int> MinimumK { get; set; }
        public string DimensionEasting { get; set; }
        public string DimensionNorthing { get; set; }
        public string DimensionElevation { get; set; }
        public Nullable<bool> IsOriginNorthPositive { get; set; }
        public Nullable<bool> IsOriginEastPositive { get; set; }
        public Nullable<bool> IsOriginElevationUpwardsPositive { get; set; }
        public Nullable<decimal> CubeCentroid0OriginOffsetX { get; set; }
        public Nullable<decimal> CubeCentroid0OriginOffsetY { get; set; }
        public Nullable<decimal> CubeCentroid0OriginOffsetZ { get; set; }
        public string UnitIJK { get; set; }
        public Nullable<decimal> DefaultISize { get; set; }
        public Nullable<decimal> DefaultJSize { get; set; }
        public Nullable<decimal> DefaultKSize { get; set; }
        public Nullable<System.Guid> AuthorContactID { get; set; }
        public Nullable<System.Guid> ResponsibleContactID { get; set; }
        public Nullable<System.Guid> ApproverContactID { get; set; }
        public Nullable<System.Guid> ExternalReviewerContactID { get; set; }
        public int Version { get; set; }
        public Nullable<System.Guid> VersionAntecedentID { get; set; }
        public Nullable<int> VersionCertainty { get; set; }
        public Nullable<System.Guid> VersionWorkflowInstanceID { get; set; }
        public Nullable<System.Guid> VersionUpdatedBy { get; set; }
        public Nullable<System.Guid> VersionDeletedBy { get; set; }
        public Nullable<System.Guid> VersionOwnerContactID { get; set; }
        public Nullable<System.Guid> VersionOwnerCompanyID { get; set; }
        public Nullable<System.DateTime> VersionUpdated { get; set; }
    
        public virtual ICollection<X_BlockModelMetadata> X_BlockModelMetadata { get; set; }
        public virtual ICollection<X_BlockModelBlock> X_BlockModelBlock { get; set; }
        public virtual X_Project X_Project { get; set; }
        public virtual ICollection<X_BlockModelBlockData> X_BlockModelBlockData { get; set; }
    }
}