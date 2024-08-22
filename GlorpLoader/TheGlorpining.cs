using System;
using System.Reflection;
using System.Linq;

namespace GlorpLoader
{
    public class TheGlorpining
    {
        public static List<GlorpMod> mods = new List<GlorpMod>();

        public static void LoadMods(Type[] typeMods)
        {
            Console.WriteLine("Registering mods");
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies.Where(assembly => assembly != null && assembly.GetCustomAttribute<GlorpMod>() != null))
            {
                try
                {
                    var modTypes = assembly.GetTypes().Where(page => page.GetCustomAttribute<GlorpMod>() != null).ToArray();
                    if (modTypes.Length > 0)
                    {
                        for (int i = 0; i < modTypes.Length; i++)
                        {
                            var modInstance = Activator.CreateInstance(modTypes[i]) as GlorpMod ?? throw new Exception("Failed to cast page type.");
                            Console.WriteLine($"Found mod {modInstance.Name}");
                            mods.Add(modInstance);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Failed to check {assembly.Location} {ex}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
}
