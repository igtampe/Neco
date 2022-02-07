namespace Igtampe.Neco.Common.Income {
    
    /// <summary>Type of income coming into a bank account</summary>
    public enum IncomeType { 
    
        /// <summary>Personal income that is taxed with personal tax brackets</summary>
        PERSONAL = 0,

        /// <summary>Corporate income that is taxed with corporate tax brackets</summary>
        CORPORATE = 1,

        /// <summary>Government income that is untaxed.<br/><br/> Money sent from bank accounts labeled with this income type will be untaxed</summary>
        GOVERNMENT = 2,

        /// <summary>Charity income that is untaxed <br/><br/> Money sent to bank accounts labeled with this income type will be untaxed</summary>
        CHARITY = 3

    }
}
