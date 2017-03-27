using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// The atomic unit from which a chromosome is composed.
    /// </summary>
    public class Gene<T> : IEquatable<T>
    {
        public readonly T value;

        public Gene(T value)
        {
            this.value = value;
        }

        public bool Equals(T other)
        {
            return value.Equals(other);
        }

        // TODO: override comparison operators (see Point.cs)
    }
}
