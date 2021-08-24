using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Igtampe.Neco.Common;
using Igtampe.Neco.Common.Contractus;
using Igtampe.Neco.Common.EzTax.Subitems;
using Igtampe.Neco.Common.LandView;
using Igtampe.Neco.Common.UMSAT;

namespace Igtampe.Neco.Data {
    class Program {

        static void Main(string[] args) {

            using (var Context = new EverythingContext()) {
                Console.WriteLine($"\n\nEverything:\n");

                foreach (System.Reflection.PropertyInfo Prop in Context.GetType().GetProperties()) {
                    Console.WriteLine($"{Prop.Name}: {Prop.GetValue(Context)}");
                }
            }
        }
    }


}
