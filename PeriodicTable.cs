using System;
using System.Collections;
using System.Collections.Immutable;
using System.Collections.Generic;

namespace Elements {
    public class PeriodicTable {
        private static Dictionary<int, LinkedList<Element>> periodicTable = new Dictionary<int, LinkedList<Element>>();

        private static Dictionary<int, LinkedList<Element>> FBlock = new Dictionary<int, LinkedList<Element>>();
        

        public class ElementComparison : IComparer<Element> {
            public int Compare(Element? x, Element? y) {
                if(x == null && y == null) {
                    return 0;
                }
                else if(x == null) {
                    return -1;
                }
                else if(y == null) {
                    return 1;
                }
                else {
                    return x.atomicNumber.CompareTo(y.atomicNumber);
                }
            }
        }
        
        
        private static int hashElement(Element e) {
            switch(e) {
                case var el when el.elementSeries.Item1 == Element.Element_Series.L:
                    return 0;
                case var el when el.elementSeries.Item1 == Element.Element_Series.A:
                    return 1;
                default: 
                    return e.row - 1;
            }
        }
        
        public static void insertElement(Element e) {
            var comparer = new ElementComparison();
            int index = hashElement(e);
            if(e.elementSeries.Item1 == Element.Element_Series.L) {
                FBlock[index].AddLast(e);
                // FBlock[index].OrderBy(e => e.atomicNumber);
                FBlock[index] =  new LinkedList<Element>(FBlock[index].OrderBy(e => e, comparer));
                
            }
            else if(e.elementSeries.Item1 == Element.Element_Series.A) {
                FBlock[index].AddLast(e);
                // FBlock[index].OrderBy(e => e.atomicNumber);
                FBlock[index] =  new LinkedList<Element>(FBlock[index].OrderBy(e => e, comparer));
            }
            else
            {
                periodicTable[index].AddLast(e);
                FBlock[index] =  new LinkedList<Element>(FBlock[index].OrderBy(e => e, comparer));

            }
        }

        

        public static void printPeriodicTable(string command) {
            switch(command) {
                case "FBlock":
                    foreach (var r in FBlock) {
                        if(r.Value.First != null) {
                            Element ptr = r.Value.First.Value;
                            Console.WriteLine($"[{Element.EnumExtensions.GetElementSeries(ptr.elementSeries.Item1)}]\n");
                        }
                        
                        foreach(var e in r.Value) 
                        {
                            Console.WriteLine(e.elementName + "\n");
                        }
                    }
                    
                    break;
                case "Main":
                    break;
                default:
                    break;
            }
        }


        public static void PopulateFBlock(LinkedList<Element> Lanthanides, LinkedList<Element> Actinides) {
            FBlock.Add(0, Lanthanides);
            FBlock.Add(1, Actinides);
        }

    }
}