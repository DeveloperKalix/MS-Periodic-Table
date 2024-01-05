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
                    return e.row + 1;
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
                periodicTable[index] =  new LinkedList<Element>(periodicTable[index].OrderBy(e => e, comparer));

            }
        }

        public static void printMainBlock() {
            foreach (var r in periodicTable) {
                if(r.Value.First != null) {
                    Element ptr = r.Value.First.Value;
                    Console.WriteLine($"[{Element.EnumExtensions.GetElementSeries(ptr.elementSeries.Item1)}s]\n");
                }
                foreach(var e in r.Value) {
                    Console.WriteLine(e.elementName + "\n");
                }
            }
        }

        public static void printFBlock() {
            foreach (var r in FBlock) {
                if(r.Value.First != null) {
                    Element ptr = r.Value.First.Value;
                    Console.WriteLine($"[{Element.EnumExtensions.GetElementSeries(ptr.elementSeries.Item1)}s]\n");
                }
                
                foreach(var e in r.Value) 
                {
                    Console.WriteLine(e.elementName + "\n");
                }
            }
        }
        

        public static void printPeriodicTable(string command) {
            switch(command) {
                case "FBlock":
                    printFBlock();
                    break;
                case "Main":
                    printMainBlock();
                    break;
                default:
                    printMainBlock();
                    printFBlock();
                    break;
            }
        }

        public static void PopulatePeriodicTable() {
           periodicTable.Add(1, new LinkedList<Element>());
           periodicTable.Add(2, new LinkedList<Element>());
           periodicTable.Add(3, new LinkedList<Element>());
           periodicTable.Add(4, new LinkedList<Element>());
           periodicTable.Add(5, new LinkedList<Element>());
           periodicTable.Add(6, new LinkedList<Element>());
           periodicTable.Add(7, new LinkedList<Element>());
           

        }
        public static void PopulateFBlock(LinkedList<Element> Lanthanides, LinkedList<Element> Actinides) {
            FBlock.Add(0, Lanthanides);
            FBlock.Add(1, Actinides);
        }

    }
}