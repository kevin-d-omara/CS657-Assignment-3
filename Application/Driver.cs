using System;
using System.Collections.Generic;
using System.Linq;
using KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms;
using KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms.UnitTests;

namespace KevinDOMara.SDSU.CS657.Assignment3.Application
{
    public class Driver
    {
        public static void Main(string[] args)
        {
            RouteTest myTest = new RouteTest();
            myTest.SetUp();
            myTest.Constructor_NonCircular_ValidParameters();
            myTest.Constructor_Circular_ValidParameters();
        }
    }
}
