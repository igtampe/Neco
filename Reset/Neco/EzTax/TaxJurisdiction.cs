﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace Igtampe.Neco.Common.EzTax {
    
    /// <summary>Holds a Jurisdiction that can tax a Neco user</summary>
    public class TaxJurisdiction {

        /// <summary>ID of this jurisdiction</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ID { get; set; }

        /// <summary>Name of this jurisdiction</summary>
        public string Name { get; set; } = "";

        /// <summary>Brackets in this Jurisdiction</summary>
        public List<TaxBracket> Brackets { get; set; }

        /// <summary>Account to which taxes must be paid out to</summary>
        public BankAccount Account { get; set; }

        /// <summary>Calculates tax on User U with income I in this jurisdiction</summary>
        /// <param name="U"></param>
        /// <param name="I"></param>
        /// <returns></returns>
        public (long,TaxBracket) CalculateTax(User U, long I) {
            TaxBracket T = Brackets?.FirstOrDefault(P => P.Type.Equals(U.Type) && I >= P.Start && I < P.End); //LOOK AT THIS BEAUTY HOW HAD I NOT USED LINQ BEFORE
            if(T == null) return (0, null);
            return (Convert.ToInt64(I * T.Rate), T);
        }

        /// <summary>Compares this TaxJurisdiction to another object</summary>
        /// <param name="obj"></param>
        /// <returns>True if and only if the object is a TaxJurisdiction and the <see cref="ID"/> matches with this one's</returns>
        public override bool Equals(object obj) {
            if (obj is TaxJurisdiction C) { return C.ID == ID; }
            return false;
        }

        /// <summary>Gets a hash code for this TaxJurisdiction. Delegates to <see cref="ID"/></summary>
        /// <returns></returns>
        public override int GetHashCode() { return ID.GetHashCode(); }

        /// <summary>Creates a string representation of this TaxJurisdiction</summary>
        /// <returns>{ID} : {Name}</returns>
        public override string ToString() { return $"{ID} : {Name}"; }


    }
}