using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GlorpLoader
{
    internal class Paths
    {
        public static List<Assembly> LoadAssembliesFromFolder(string folderPath)
        {
            List<Assembly> assemblies = new List<Assembly>();
            string[] dllFiles = Directory.GetFiles(folderPath, "*.dll", SearchOption.AllDirectories);

            foreach (var dllFile in dllFiles)
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(dllFile);
                    assemblies.Add(assembly);
                    Console.WriteLine($"Loaded assembly from {dllFile}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to load assembly from {dllFile}: {ex.Message}");
                }
            }

            return assemblies;
        }
    }
}
