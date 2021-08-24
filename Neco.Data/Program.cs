using System;

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
