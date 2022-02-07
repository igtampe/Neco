namespace Igtampe.Neco.Common.Taxes {

    /// <summary>Types of Jurisdictions</summary>
    public enum JurisdictionType {

        /// <summary>A Country (Top level of any tax system)</summary>
        COUNTRY = 0,

        /// <summary>A State or District (Part of a Country)</summary>
        STATE = 1,

        /// <summary>A County (Part of a state)</summary>
        COUNTY = 2,
        
        /// <summary>A City or Town (Part of a county) (Lowest level of any tax system)</summary>
        CITY = 3,

    }
}
