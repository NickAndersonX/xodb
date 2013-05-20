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
    
    public partial class X_Lithology
    {
        public System.Guid LithologyID { get; set; }
        public System.Guid HeaderID { get; set; }
        public Nullable<decimal> FromDepth { get; set; }
        public Nullable<decimal> ToDepth { get; set; }
        public Nullable<decimal> RecoveredThickness { get; set; }
        public Nullable<System.Guid> DomainID { get; set; }
        public Nullable<System.Guid> SeamID { get; set; }
        public Nullable<System.Guid> PlyID { get; set; }
        public Nullable<System.Guid> StratigraphyID { get; set; }
        public Nullable<System.Guid> HorizonID { get; set; }
        public Nullable<System.Guid> SampleTypeID { get; set; }
        public string SampleNumber { get; set; }
        public Nullable<decimal> SampleMassKg { get; set; }
        public Nullable<System.Guid> IntervalStatusID { get; set; }
        public Nullable<int> LithologyPercentage { get; set; }
        public Nullable<System.Guid> LithologyTypeID { get; set; }
        public Nullable<System.Guid> LithologyQualifierID { get; set; }
        public Nullable<System.Guid> ShadeID { get; set; }
        public Nullable<System.Guid> HueID { get; set; }
        public Nullable<System.Guid> ColourID { get; set; }
        public Nullable<System.Guid> AdjectiveID1 { get; set; }
        public Nullable<System.Guid> AdjectiveID2 { get; set; }
        public Nullable<System.Guid> AdjectiveID3 { get; set; }
        public Nullable<System.Guid> AdjectiveID4 { get; set; }
        public Nullable<System.Guid> LithologyInterrelationID { get; set; }
        public Nullable<System.Guid> WeatheringID { get; set; }
        public Nullable<System.Guid> EstimatedStrengthID { get; set; }
        public Nullable<System.Guid> BedSpacingID { get; set; }
        public Nullable<System.Guid> DefectTypeID { get; set; }
        public Nullable<System.Guid> DefectCompletenessID { get; set; }
        public Nullable<System.Guid> DefectSpacingID { get; set; }
        public Nullable<int> DefectDip { get; set; }
        public Nullable<System.Guid> CoreStateID { get; set; }
        public Nullable<System.Guid> MechanicalStateID { get; set; }
        public Nullable<System.Guid> TextureID { get; set; }
        public Nullable<System.Guid> BasalContactID { get; set; }
        public Nullable<System.Guid> SedimentaryFeature1ID { get; set; }
        public Nullable<System.Guid> SedimentaryFeature2ID { get; set; }
        public Nullable<int> BeddingDip { get; set; }
        public Nullable<System.Guid> MineralAndFossilAbundanceID { get; set; }
        public Nullable<System.Guid> MineralAndFossilTypeID { get; set; }
        public Nullable<System.Guid> MineralAndFossilAssociationID { get; set; }
        public Nullable<System.Guid> GasDetectedID { get; set; }
        public int Version { get; set; }
        public Nullable<System.Guid> VersionAntecedentID { get; set; }
        public Nullable<int> VersionCertainty { get; set; }
        public Nullable<System.Guid> VersionWorkflowInstanceID { get; set; }
        public Nullable<System.Guid> VersionUpdatedBy { get; set; }
        public Nullable<System.Guid> VersionDeletedBy { get; set; }
        public Nullable<System.Guid> VersionOwnerContactID { get; set; }
        public Nullable<System.Guid> VersionOwnerCompanyID { get; set; }
        public Nullable<System.DateTime> VersionUpdated { get; set; }
        public string Description { get; set; }
    
        public virtual X_Header X_Header { get; set; }
    }
}