// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;

//Console.WriteLine("Hello, World!");

namespace Elements {
    



    public class Element {
        
        public required string elementName { get; set; }
        public required string symbol { get; set; }
        public required int Protons { get; set; }
        public required int Neutrons { get; set; }
        public int atomicNumber { get{ return PropertyCalculation("Atomic Number", Protons, Neutrons); } }
        public float atomicMass { get{ return PropertyCalculation("Atomic Number", Protons, Neutrons); } }

        public string electronConfiguration {get{ return Orbital.determineOrbital(atomicNumber); } }

        (Element_Series, Color) elementSeries {get{return categorizeElementSeries(electronConfiguration);}}

        // private Func<string, string, bool> seriesIdentifier = (electronConfiguration, identifier) => {
            
        //     return electronConfiguration.Equals(identifier) || Regex.IsMatch(electronConfiguration, Regex.Escape(identifier));
        // };

        private (Element_Series, Color) categorizeElementSeries(string electronConfiguration) {
            switch(electronConfiguration) {
                case var ec when ec.Contains("d"):
                    return(Element_Series.TM, Color.FromArgb(255, 248, 228, 115));
                case var ec when ec.Contains("1s2") || ec.Contains("p6"):
                    return (Element_Series.NG, Color.FromArgb(255, 129,34,141));
                case var ec when ec.Contains("4f"):
                    return(Element_Series.L, Color.FromArgb(255, 194, 24, 7));
                case var ec when ec.Contains("5f"):
                    return(Element_Series.A, Color.FromArgb(255, 197, 228, 237));
                case var ec when ec.Contains("s2"):
                    return (Element_Series.AEM, Color.FromArgb(255, 255, 116, 23));
                case var ec when ec.Contains("1s1") || Regex.IsMatch(ec, @"2p[2-4]") || Regex.IsMatch(ec, @"3p[3-4]") || ec.Contains("4p4"):
                    return (Element_Series.NM, Color.FromArgb(255, 65, 105, 225));
                case var ec when ec.Contains("s1"):
                    return (Element_Series.AM, Color.FromArgb(255, 238, 75,43));
                case var ec when ec.Contains("p5"):
                    return (Element_Series.H, Color.FromArgb(255, 243, 58, 106));
                case var ec when ec.Contains("2p1") || ec.Contains("3p2") || ec.Contains("4p2") || ec.Contains("4p3") || ec.Contains("5p3") || ec.Contains("5p4") || ec.Contains("6p4"):
                default:
                    return (Element_Series.PTM, Color.FromArgb(255, 19, 247, 41));
            }
        }

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

        private enum Element_Series  {
        [Description("Alkali Metal")] AM,
        [Description("Alkali Earth Metal")] AEM,
        [Description("Transition Metal")] TM,
        [Description("Post-Transition Metal")] PTM,
        [Description("Metalloid")] M,
        [Description("Halogen")] H,
        [Description("Noble Gas")] NG,
        [Description("Lanthanide")] L,
        [Description("Actinide")] A,
        [Description("NonMetal")] NM,
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
                int split = 0;
                int index = 0;
                string orbital = "";
                switch(atomicNumber) {
                    case int n when (n >= 57 && n <= 71):
                        suffix = atomicNumber % 14;
                        orbital = '4'.ToString()  + 'f'.ToString()  + suffix.ToString();
                        //Console.WriteLine(orbital);
                        return orbital;
                    case int n when n >= 89 && n <= 103:
                        suffix = atomicNumber % 14 - 4;
                        orbital = '5'.ToString() + 'f'.ToString()  + suffix.ToString();
                        //Console.WriteLine(orbital);
                        return orbital;
                    case int n when n <=2:
                        orbital = '1' + 's' + n.ToString();
                        //Console.WriteLine(orbital);
                        return orbital;
                    case int n when (n >= 3 && n <= 18):
                        suffix = (atomicNumber % 8);
                        split = (3+18)/ 2;
                        if(atomicNumber > split) {
                            orbital = '3'.ToString();
                        }
                        else
                        {
                            orbital = '2'.ToString();
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
                            orbital += 'p' + suffix.ToString();
                        }
                        //Console.WriteLine(orbital);
                        return orbital;
                    case int n when (n >= 19 && n <= 54):
                        split = (19 + 54)/ 2;
                        index = atomicNumber % 18;
                        if(atomicNumber > split) {
                            orbital += '5'.ToString();
                        }
                        else
                        {
                            orbital += '4'.ToString();
                        }
                        if(index >= 3 && index <= 12) {
                            int temp = Int32.Parse(orbital);
                            orbital = (temp-1).ToString();
                            suffix = (atomicNumber % 10) + 1;
                            orbital += 'd' + suffix.ToString();
                        }
                        else if(index > 12)
                        {
                            suffix = (atomicNumber % 6);
                            if(suffix == 0) {
                                suffix = 6;
                            }
                            orbital += 'p' + suffix.ToString();
                        }
                        else
                        {
                            orbital += 's' + index.ToString();
                        } 
                        return orbital;
                    default:
                        split = (55 + 118) / 2;
                        index = atomicNumber % (17); 
                        if(atomicNumber > split) {
                            orbital += '7';
                        }
                        else
                        {
                            orbital += '6';
                        }
                        if(atomicNumber <= 56 || (atomicNumber <= 88 && atomicNumber > 86)) {
                            orbital += 's';
                            if(atomicNumber <= 56) {
                                orbital += (index-3).ToString();
                            }
                            else {
                                orbital += (index - 2).ToString();
                            }
                        }
                        else if((atomicNumber <= 80 && atomicNumber >= 72) || (atomicNumber <= 112 && atomicNumber >= 104)) {
                            index = atomicNumber % 9;
                            int temp = Int32.Parse(orbital);
                            orbital = (temp-1).ToString();
                            if((atomicNumber <= 80 && atomicNumber >= 72)) {
                                
                                orbital += 'd' + (index + 2).ToString();
                            }
                            else
                            {
                                orbital += 'd' + (index - 3).ToString();
                            }
                        }
                        else
                        {
                            index = atomicNumber % 6;
                            if((atomicNumber <= 86 && atomicNumber >= 81)) {
                                orbital += 'p' + (index-2).ToString(); 
                            }
                            else
                            {
                                orbital += 'p' + (index-4).ToString(); 
                            }
                        }
                        return orbital;
                }
            }
        }

    }

    public static class Elements 
    {
        public static void Main(string[] args) {
            Element.Orbital o = new Element.Orbital();
            //Element.Orbital.printOrbitals();

            Element Hydrogen = new Element {
                elementName = "Hydrogen",
                symbol = "H",
                Protons = 1,
                Neutrons = 0
            };

            Element Helium = new Element {
                elementName = "Hydrogen",
                symbol = "He",
                Protons = 2,
                Neutrons = 2
            };

            Element lithium = new Element {
                elementName = "Lithium",
                symbol = "Li",
                Protons = 3,
                Neutrons = 4
                // Set other properties as needed
            };

            // Group 2: Alkaline Earth Metal (e.g., Magnesium)
            Element magnesium = new Element {
                elementName = "Magnesium",
                symbol = "Mg",
                Protons = 12,
                Neutrons = 12
                // Set other properties as needed
            };

            // Transition Metal (e.g., Iron)
            Element iron = new Element {
                elementName = "Iron",
                symbol = "Fe",
                Protons = 26,
                Neutrons = 30
                // Set other properties as needed
            };

            // Lanthanide (e.g., Europium)
            Element europium = new Element {
                elementName = "Europium",
                symbol = "Eu",
                Protons = 63,
                Neutrons = 89
            };

            // Actinide (e.g., Uranium)
            Element uranium = new Element {
                elementName = "Uranium",
                symbol = "U",
                Protons = 92,
                Neutrons = 146
            };

            // Halogen (e.g., Chlorine)
            Element chlorine = new Element {
                elementName = "Chlorine",
                symbol = "Cl",
                Protons = 17,
                Neutrons = 18
            };

            // Noble Gas (e.g., Neon)
            Element neon = new Element {
                elementName = "Neon",
                symbol = "Ne",
                Protons = 10,
                Neutrons = 10
            };

            // Boron Group (e.g., Boron)
            Element boron = new Element {
                elementName = "Boron",
                symbol = "B",
                Protons = 5,
                Neutrons = 6
            };
            Console.WriteLine(uranium.electronConfiguration);
        }
    }
}



 