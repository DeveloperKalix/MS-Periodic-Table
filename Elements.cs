// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Reflection;

//Console.WriteLine("Hello, World!");

namespace Elements {
    



    public class Element {
        
        public required string elementName { get; set; }
        public required int row {get; set;}
        public required string symbol { get; set; }
        public required int Protons { get; set; }
        public required int Neutrons { get; set; }
        public int atomicNumber { get{ return PropertyCalculation("Atomic Number", Protons, Neutrons); } }
        public float atomicMass { get{ return PropertyCalculation("Atomic Number", Protons, Neutrons); } }

        public string electronConfiguration {get{ return Orbital.determineOrbital(atomicNumber); } }

        public (Element_Series, Color) elementSeries {get{return categorizeElementSeries(electronConfiguration);}}
        

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

        public enum Element_Series  {
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
    
    public static class EnumExtensions {
        public static string GetElementSeries(Enum es) {
            
            var field = es.GetType().GetField(es.ToString());
            if(field != null) {
                if(Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute) {
                    return attribute.Description;
                }
            }
            return es.ToString();
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
                row = 1,
                Protons = 1,
                Neutrons = 0
            };

            Element Helium = new Element {
                elementName = "Hydrogen",
                symbol = "He",
                row = 1,
                Protons = 2,
                Neutrons = 2
            };

            Element lithium = new Element {
                elementName = "Lithium",
                symbol = "Li",
                row = 2,
                Protons = 3,
                Neutrons = 4
                // Set other properties as needed
            };

            // Group 2: Alkaline Earth Metal (e.g., Magnesium)
            Element magnesium = new Element {
                elementName = "Magnesium",
                symbol = "Mg",
                row = 3,
                Protons = 12,
                Neutrons = 12
                // Set other properties as needed
            };

            // Transition Metal (e.g., Iron)
            Element iron = new Element {
                elementName = "Iron",
                symbol = "Fe",
                row = 4,
                Protons = 26,
                Neutrons = 30
                // Set other properties as needed
            };

            // Lanthanide (e.g., Europium)
            Element europium = new Element {
                elementName = "Europium",
                symbol = "Eu",
                row = 6,
                Protons = 63,
                Neutrons = 89
            };

            // Actinide (e.g., Uranium)
            Element uranium = new Element {
                elementName = "Uranium",
                symbol = "U",
                row = 7,
                Protons = 92,
                Neutrons = 146
            };

            // Halogen (e.g., Chlorine)
            Element chlorine = new Element {
                elementName = "Chlorine",
                symbol = "Cl",
                row = 3,
                Protons = 17,
                Neutrons = 18
            };

            // Noble Gas (e.g., Neon)
            Element neon = new Element {
                elementName = "Neon",
                symbol = "Ne",
                row = 2,
                Protons = 10,
                Neutrons = 10
            };

            // Boron Group (e.g., Boron)
            Element boron = new Element {
                elementName = "Boron",
                symbol = "B",
                row = 2,
                Protons = 5,
                Neutrons = 6
            };
            var lanthanides = new LinkedList<Element>(new[] {
                new Element {
                    elementName = "Lanthanum",
                    symbol = "La",
                    row = 6,
                    Protons = 57,
                    Neutrons = 82
                },
                new Element {
                    elementName = "Cerium",
                    symbol = "Ce",
                    row = 6,
                    Protons = 58,
                    Neutrons = 82
                },
                new Element
                {
                    elementName = "Praseodymium",
                    symbol = "Pr",
                    row = 6,
                    Protons = 59,
                    Neutrons = 82
                },
                new Element
                {
                    elementName = "Neodymium",
                    symbol = "Nd",
                    row = 6,
                    Protons = 60,
                    Neutrons = 84
                    
                },
                new Element
                {
                    elementName = "Promethium",
                    symbol = "Pm",
                    row = 6,
                    Protons = 61,
                    Neutrons = 84
                },
                new Element
                {
                    elementName = "Samarium",
                    symbol = "Sm",
                    row = 6,
                    Protons = 62,
                    Neutrons = 88
                },
                new Element
                {
                    elementName = "Europium",
                    symbol = "Eu",
                    row = 6,
                    Protons = 63,
                    Neutrons = 89
                },
                new Element
                {
                    elementName = "Gadolinium",
                    symbol = "Gd",
                    row = 6,
                    Protons = 64,
                    Neutrons = 93
                },
                new Element
                {
                    elementName = "Terbium",
                    symbol = "Tb",
                    row = 6,
                    Protons = 65,
                    Neutrons = 94
                },
                new Element
                {
                    elementName = "Dysprosium",
                    symbol = "Dy",
                    row = 6,
                    Protons = 66,
                    Neutrons = 97
                },
                new Element
                {
                    elementName = "Holmium",
                    symbol = "Ho",
                    row = 6,
                    Protons = 67,
                    Neutrons = 98
                },
                new Element
                {
                    elementName = "Erbium",
                    symbol = "Er",
                    row = 6,
                    Protons = 68,
                    Neutrons = 99
                },
                new Element
                {
                    elementName = "Thulium",
                    symbol = "Tm",
                    row = 6,
                    Protons = 69,
                    Neutrons = 100
                },
                new Element
                {
                    elementName = "Ytterbium",
                    symbol = "Yb",
                    row = 6,
                    Protons = 70,
                    Neutrons = 103
                },
                new Element
                {
                    elementName = "Lutetium",
                    symbol = "Lu",
                    row = 6,
                    Protons = 71,
                    Neutrons = 104
                },
            });
            var Actinides = new LinkedList<Element>(new[] {
                new Element {
                    elementName = "Actinium",
                    symbol = "Ac",
                    row = 7,
                    Protons = 89,
                    Neutrons = 138
                },
                new Element
                {
                    elementName = "Thorium",
                    symbol = "Th",
                    row = 7,
                    Protons = 90,
                    Neutrons = 142
                },
                new Element
                {
                    elementName = "Protactinium",
                    symbol = "Pa",
                    row = 7,
                    Protons = 91,
                    Neutrons = 140
                },
                new Element
                {
                    elementName = "Uranium",
                    symbol = "U",
                    row = 7,
                    Protons = 92,
                    Neutrons = 146
                },
                new Element
                {
                    elementName = "Neptunium",
                    symbol = "Np",
                    row = 7,
                    Protons = 93,
                    Neutrons = 144
                },
                new Element
                {
                    elementName = "Plutonium",
                    symbol = "Pu",
                    row = 7,
                    Protons = 94,
                    Neutrons = 150
                },
                new Element
                {
                    elementName = "Americium",
                    symbol = "Am",
                    row = 7,
                    Protons = 95,
                    Neutrons = 148
                },
                new Element
                {
                    elementName = "Curium",
                    symbol = "Cm",
                    row = 7,
                    Protons = 96,
                    Neutrons = 151
                },
                new Element
                {
                    elementName = "Berkelium",
                    symbol = "Bk",
                    row = 7,
                    Protons = 97,
                    Neutrons = 150
                },
                new Element
                {
                    elementName = "Californium",
                    symbol = "Cf",
                    row = 7,
                    Protons = 98,
                    Neutrons = 153
                },
                new Element
                {
                    elementName = "Einsteinium",
                    symbol = "Es",
                    row = 7,
                    Protons = 99,
                    Neutrons = 153
                },
                new Element
                {
                    elementName = "Fermium",
                    symbol = "Fm",
                    row = 7,
                    Protons = 100,
                    Neutrons = 157
                },
                new Element
                {
                    elementName = "Mendelevium",
                    symbol = "Md",
                    row = 7,
                    Protons = 101,
                    Neutrons = 157
                },
                new Element
                {
                    elementName = "Nobelium",
                    symbol = "No",
                    row = 7,
                    Protons = 102,
                    Neutrons = 157
                },
                new Element
                {
                    elementName = "Lawrencium",
                    symbol = "Lr",
                    row = 7,
                    Protons = 103,
                    Neutrons = 159
                },
            });
            
            Console.WriteLine(uranium.electronConfiguration);
            PeriodicTable.PopulateFBlock(lanthanides, Actinides);
            PeriodicTable.printPeriodicTable("FBlock");
        }
    }
}



 