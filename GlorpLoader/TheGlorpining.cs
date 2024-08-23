using System;
using System.Reflection;
using System.Linq;
using GlorpLoader.TheStuff;
using UnityEngine;
namespace GlorpLoader
{
    public class TheGlorpining
    {
        private static List<GlorpMod> mods = new List<GlorpMod>();

        static void WriteLine(string msg)
        {
            Console.WriteLine(msg);
        }

        static void WriteLine(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void Write(string msg)
        {
            Console.Write(msg);
        }

        static void Write(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void LoadMods(Type[] typeMods)
        {
            var assemblies = Paths.LoadAssembliesFromFolder("C:/ExamplePath/Plugins");
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
                            mods.Add(modInstance);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Failed to check {assembly.Location}: {ex} (Returning!)");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;
                }
                Console.WriteLine($"Loading {mods.Count} mods!");
                foreach (var mod in mods)
                {
                    SlimeBank.LoadedGloop.Add(new GlorpInfo()
                    {
                        Name = mod.Name,
                        GUID = mod.GUID,
                        Version = mod.Version,
                    });
                    Write("[GlorpLoader] Loading ");
                    Write($"[{mod.Name}] ", ConsoleColor.Cyan);
                    Write($"{mod.Version}\n", ConsoleColor.Gray);
                    Type[] glorpModTypes = mods.Select(glorp => glorp.GetType()).ToArray();
                    GameObject.DontDestroyOnLoad(new GameObject("GlorpLoader",glorpModTypes));
                }
            }
        }
    }
}