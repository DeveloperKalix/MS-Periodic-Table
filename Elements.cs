// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using System.Collections.Generic;

//Console.WriteLine("Hello, World!");

namespace Elements {
    enum Element_Series  {
        [Description("Alkali Metal")] AM,
        [Description("Alkali Earth Metal")] AEM,
        [Description("Transition Metal")] TM,
        [Description("Post-Transition Metal")] PTM,
        [Description("Metalloid")] M,
        [Description("Halogen")] H,
        [Description("Noble Gas")] NG,
        [Description("Lanthanide")] L,
        [Description("Actinide")] A,
        [Description("Other NonMetals")] ONM
    }



    public class Element {
        public required string elementName { get; set; }
        public required string symbol { get; set; }
        public required int Protons { get; set; }
        public required int Neutrons { get; set; }
        private int atomicNumber { get{ return PropertyCalculation("Atomic Number", Protons, Neutrons); } }
        private float atomicMass { get{ return PropertyCalculation("Atomic Number", Protons, Neutrons); } }

        private int PropertyCalculation(String calculation, int Protons, int Neutrons) {
            switch(calculation) {
                case "Atomic Mass":
                    return Protons + Neutrons;
                case "Atomic Number":
                    return Protons;
                default:
                    Console.WriteLine("Invalid Calculation");
                    return 0;
            } 
                
        }

        public class Orbital 
        {
            public static Dictionary<char, int> OrbitalConfigurations = new Dictionary<char, int>();
            
            public Orbital() {
                OrbitalConfigurations.Add('s', 2);
                OrbitalConfigurations.Add('p', 6);
                OrbitalConfigurations.Add('d', 10);
                OrbitalConfigurations.Add('f', 14);
            }

            public static void printOrbitals() {
                foreach(var kvp in OrbitalConfigurations) {
                    Console.WriteLine($"Orbital Type: {kvp.Key}, Electron Count: {kvp.Value}");
                }
                
            } 
            
            public static string determineOrbital(int atomicNumber) {
                int suffix = 0;
                string orbital = "";
                switch(atomicNumber) {
                    case int n when (n >= 57 && n <= 71):
                        suffix = atomicNumber % 57 + 1;
                        orbital = '4' + 'f' + suffix.ToString();
                        Console.WriteLine(orbital);
                        return orbital;
                    case int n when n >= 89 && n <= 103:
                        suffix = atomicNumber % 57 + 1;
                        orbital = '5' + 'f' + suffix.ToString();
                        Console.WriteLine(orbital);
                        return orbital;
                    case int n when n <=2:
                        orbital = '1' + 's' + n.ToString();
                        Console.WriteLine(orbital);
                        return orbital;
                    case int n when (n >= 3 && n <= 18):
                        suffix = (atomicNumber % 8);
                        int split = (3+18)/ 2;
                        if(atomicNumber > split) {
                            orbital = '4'.ToString();
                        }
                        else
                        {
                            orbital = '3'.ToString();
                        }
                        if(suffix == 3 || suffix == 4) {
                            suffix -= 2;
                            orbital = 's' + suffix.ToString();
                        }
                        else
                        {
                            if(suffix == 0 || suffix == 1 || suffix == 2) {
                                suffix += 4;

                            }
                            else
                            {
                                suffix -= 4;
                            }
                        }
                        Console.WriteLine(orbital);
                        return orbital;
                    default:
                        return "";
                }
            }
        }

    }

    public static class Elements 
    {
        public static void Main(string[] args) {
            Element.Orbital o = new Element.Orbital();
            Element.Orbital.printOrbitals();
        }
    }
}



 