using System;
using System.Collections.Generic;
using System.Data.Entity;
using Igtampe.Neco.Common;
using Igtampe.Neco.Common.Contractus;
using Igtampe.Neco.Common.EzTax.Subitems;
using Igtampe.Neco.Common.LandView;
using Igtampe.Neco.Common.UMSAT;

namespace Igtampe.Neco.Data {
    class Program {

        static void Main(string[] args) {
            string DataSource = "Data Source=Localhost;Initial Catalog=Neco;Integrated Security=True";

            using var Context = new EverythingContext(DataSource); 
            Context.Contracts.Add(new Contract());
            Context.SaveChanges();

            //using (var Context = new ContractusContext(DataSource)) {
            //    Context.Contracts.Add(new Contract());
            //    Context.SaveChanges();
            //}

            //using (var Context = new UMSATContext(DataSource)) {
            //    Context.Assets.Add(new Asset() { CreationDate=DateTime.Now, UpdateDate=DateTime.Now});
            //    Context.SaveChanges();
            //}

            //using (var Context = new LandViewContext(DataSource)) {
            //    Context.Plots.Add(new Plot());
            //    Context.SaveChanges();
            //}

            //using (var Context = new EzTaxContext(DataSource)) {
            //    Context.Businesses.Add(new Business());
            //    Context.SaveChanges();
            //}

            //using (var Context = new NecoContext(DataSource)) {
            //    Context.Users.Add(new User());
            //    Context.SaveChanges();
            //}

            //using (var Context = new AuthContext(DataSource)) {
            //    Context.Users.Add(new UserAuth());
            //    Context.SaveChanges();
            //}

            //DbContext[] Contexts = {
            //    new AuthContext(DataSource),
            //    new ContractusContext(DataSource),
            //    new EzTaxContext(DataSource),
            //    new LandViewContext(DataSource),
            //    new NecoContext(DataSource),
            //    new UMSATContext(DataSource)
            //};

            //foreach (var C in Contexts) {

            //    Type T = Contexts.GetType();
            //    Console.WriteLine($"\n\n{T.Name}:\n");

            //    foreach (System.Reflection.PropertyInfo Prop in Contexts.GetType().GetProperties()) {
            //        if (Prop.GetValue(C) is not DbSet<object> D) { continue; }
            //        Console.WriteLine($"{Prop.Name}: {D}");
            //    }

            //}


        }
    }
}
