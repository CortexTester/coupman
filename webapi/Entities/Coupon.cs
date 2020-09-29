using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using webapi.Helpers;

namespace webapi.Entities
{
    [JsonConverter(typeof(CustomStringToEnumConverter))]
    public enum OfferType
    {
        [EnumMember(Value = "Percentage")]
        Percentage,
        [EnumMember(Value = "Dollar")]
        Dollar,
        [EnumMember(Value = "Custom")]
        Custom
    }


[JsonConverter(typeof(CustomStringToEnumConverter))]
    public enum CouponStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Active")]
        Active,
        [EnumMember(Value = "Expired")]
        Expired,
        [EnumMember(Value = "Rejected")]

        Rejected,
        [EnumMember(Value = "Deleted")]

        Deleted
    }


    public class Coupon : Entity
    {
        public int CouponId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public Account Account { get; set; }
        public CouponStatus Status { get; set; }
        public string Description { get; set; }

        public OfferType OfferType { get; set; }
        public string OfferValue { get; set; }

        public bool IsSomeConditionApply { get; set; } 
        public bool IsNotValidWithOtherPromotion { get; set; }
        public string CustomCondition { get; set; }
        public ICollection<CouponCategory> CouponCategories { get; set; } = new List<CouponCategory>();
        public ICollection<CouponCity> CouponCities { get; set; }= new List<CouponCity>();
        // [Column(TypeName = "datetime2")]        
        public DateTime StartDate { get; set; }
        // [Column(TypeName = "datetime2")]        
        public DateTime EndDate { get; set; }

    }
}