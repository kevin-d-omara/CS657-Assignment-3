using System;
using System.Collections.Generic;
using System.Linq;

namespace KevinDOMara.SDSU.CS657.Assignment3.GeneticAlgorithms
{
    /// <summary>
    /// A method of selecting individual chromosomes from among a population. Each tournament, a
    /// number of chromosomes are chosen at random from the population. The winner is the chromosome
    /// with the best fitness.
    /// <para>
    /// Selection pressure is easily adjusted by changing the tournament size. If the tournament 
    /// size is larger, weak individuals have a smaller chance to be selected.
    /// </para>
    /// </summary>
    /// <remarks>See https://en.wikipedia.org/wiki/Tournament_selection</remarks>
    public class TournamentSelection : ISelection
    {
        /// <summary>
        /// Number of chromosomes in each tournament.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Create a new tournament selector with the given tournament size.
        /// </summary>
        /// <param name="tournamentSize">Number of chromosomes in each tournament.</param>
        public TournamentSelection(int tournamentSize)
        {
            Size = tournamentSize;
        }

        /// <summary>
        /// Selects a chromosome by using tournament selection. Each contestant is unique.
        /// </summary>
        public IChromosome Select(IList<IChromosome> chromosomes)
        {
            var remainingChromosomes = new List<IChromosome>(chromosomes);

            IChromosome winner = null;
            for (int i = 0; i < Size; ++i)
            {
                var index = RandomizationProvider.random.Next(remainingChromosomes.Count);
                var randomChromosome = remainingChromosomes[index];

                if (winner == null || randomChromosome.Fitness > winner.Fitness)
                {
                    winner = randomChromosome;
                }

                remainingChromosomes.Remove(randomChromosome);
            }

            return winner;
        }

        /// <summary>
        /// Selects a chromosome by using tournament selection. Contestants may be non-unique (i.e.
        /// a chromosome may be entered into the tournament more than once).
        /// </summary>
        /// <returns></returns>
        public IChromosome SelectWithDuplicates(IList<IChromosome> chromosomes)
        {
            IChromosome winner = null;
            for (int i = 0; i < Size; ++i)
            {
                var index = RandomizationProvider.random.Next(chromosomes.Count);
                var randomChromosome = chromosomes[index];

                if (winner == null || randomChromosome.Fitness > winner.Fitness)
                {
                    winner = randomChromosome;
                }
            }

            return winner;
        }
    }
}
