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

        // Overriden operators (use 'value' variable for comparison) -------------------------------

        /// <summary>
        /// Return true if the value of this gene matches the value of the other gene.
        /// </summary>
        public static bool operator ==(Gene a, Gene b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Return false if the value of this gene matches the value of the other gene.
        /// </summary>
        public static bool operator !=(Gene a, Gene b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Return true if the value of this gene matches the value of the other gene.
        /// </summary>
        public bool Equals(Gene other)
        {
            return value.Equals(other.value);
        }

        /// <summary>
        /// Return true if the other object is a Gene AND the value of this gene matches the value
        /// of the other gene.
        /// </summary>
        public override bool Equals(object obj)
        {
            var otherPoint = obj as Gene;

            if (obj == null || otherPoint == null)
            {
                return false;
            }

            return this == otherPoint;
        }

        /// <summary>
        /// Return the hashcode of the value of this gene.
        /// </summary>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }
}
