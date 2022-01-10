using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Igtampe.Neco.Common.EzTax {

    /// <summary>Tax Bracket for a <see cref="TaxJurisdiction"/></summary>
    public class TaxBracket {

        /// <summary>ID of this Bracket</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ID { get; set; }

        /// <summary>Jurisdiction this tax bracket belongs to</summary>
        [JsonIgnore]
        public TaxJurisdiction Jurisdiction { get; set; }

        /// <summary>Name of this tax bracket</summary>
        public string Name { get; set; } = "";

        /// <summary>Income amount at which this bracket begins (Inclusive)</summary>
        [Range(0, int.MaxValue)]
        public long Start { get; set; } = 0;

        /// <summary>Income amount at which this bracket ends (Exclusive)</summary>
        [Range(0, long.MaxValue)]
        public long End { get; set; } = long.MaxValue;

        /// <summary><see cref="UserType"/> This bracket belongs to (IE Corporate)</summary>
        public UserType Type { get; set; }

        /// <summary>Rate of this Tax Bracket</summary>
        [Range(0.0, 1.0)]
        public double Rate { get; set; } = 0.0;

        /// <summary>Compares this TaxBracket to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a TaxBracket and the <see cref="ID"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is TaxBracket C) { return C.ID == ID; }
            return false;
        }

        /// <summary>Gets a hash code for this TaxBracket. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode(); }

        /// <summary>Creates a string representation of this TaxBracket</summary>
        /// <returns>{ID} : {Name}, From {Start} to {End} at {Rate}%</returns>
        public override string ToString() { return $"{ID} : {Name}, From {Start} to {End} at {Rate}%"; }

    }
}
