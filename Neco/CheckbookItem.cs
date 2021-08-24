using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Igtampe.Neco.Common {

    /// <summary>Holds a Checkbook 2000 Item</summary>
    public class CheckbookItem {

        /// <summary>ID of this item</summary>
        public Guid Id { get; set; }

        /// <summary>Available type of checkbook items</summary>
        public enum ItemType { 
            /// <summary>Item is a bill and must be approved by the Origin</summary>
            BILL, 

            /// <summary>Item is a check and must be approved by the destination</summary>
            CHECK 
        }

        /// <summary>Attatched transaction to this checkbook item</summary>
        public Transaction AttachedTransaciton { get; set; }

        /// <summary>Type of transaction held in this Checkbook Item</summary>
        public ItemType Type { get; set; } = ItemType.CHECK;

        /// <summary>Graphical Variant of the given Checkbook item</summary>
        public int Variant { get; set; } = 0;

        /// <summary>Comment provided for this checkbook item</summary>
        public string Comment { get; set; } = "";


    }
}