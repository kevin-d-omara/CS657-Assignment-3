using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// The atomic unit from which a chromosome is composed.
    /// </summary>
    public class Gene : IEquatable<Gene>
    {
        public readonly object value;

        public Gene(object value)
        {
            this.value = value;
        }

        public bool Equals(Gene other)
        {
            return value.Equals(other);
        }

        // TODO: override comparison operators (see Point.cs)
    }
}
