﻿namespace Igtampe.Neco.Common {

    /// <summary>Abstract class for any object that has a GUID ID</summary>
    public abstract class Identifiable<E> {

        /// <summary>ID of this object</summary>
        public virtual E? ID { get; set; }

        /// <summary>Verifies if this object is equal to the given object</summary>
        /// <param name="obj"></param>
        /// <returns> True if the object is Identifiable and its ID matches this one's</returns>
        public override bool Equals(object? obj) => obj is Identifiable<E> I && ID as dynamic == I.ID as dynamic;

        /// <summary>Generates a hashcode for this identifiable</summary>
        /// <returns>The hashcode of the ID</returns>
        public override int GetHashCode() => ID is null ? base.GetHashCode() : ID.GetHashCode();
    }
}
