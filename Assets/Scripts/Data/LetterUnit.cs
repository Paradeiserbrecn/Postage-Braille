using System;
using System.Collections.Generic;

namespace Data
{
    /// <summary>
    /// Represents a single learning unit containing a set of letters,
    /// associated practice words, and the player's progress statistics.
    /// </summary>
    [Serializable]
    public class LetterUnit
    {
        /// <summary>
        /// Gets the unique index assigned to this letter unit. This is only used in displaying the index to the user, thus should be 1-indexed
        /// </summary>
        public readonly int UnitIndex;

        /// <summary>
        /// Gets or sets the letters introduced in this unit.
        /// </summary>
        public List<string> Letters { get; set; }

        /// <summary>
        /// Gets or sets the practice words for this unit.
        /// </summary>
        public List<string> Words { get; set; }

        /// <summary>
        /// The total number of attempts made for this unit.
        /// </summary>
        public int attempts;

        /// <summary>
        /// The total number of successful attempts for this unit.
        /// </summary>
        public int successes;

        /// <summary>
        /// Gets the success rate as a percentage.
        /// Returns 50% if no attempts have been made.
        /// </summary>
        public double SuccessPercentage =>
            attempts == 0 ? 50 : Math.Round((double)successes / attempts * 100);

        /// <summary>
        /// Initializes a new instance of the <see cref="LetterUnit"/> class.
        /// </summary>
        /// <param name="letters">The letters introduced in this unit.</param>
        /// <param name="words">The practice words for this unit.</param>
        /// <param name="unitIndex">The index of the unit.</param>
        internal LetterUnit(List<string> letters, List<string> words, int unitIndex)
        {
            UnitIndex = unitIndex;
            Letters = letters;
            Words = words;
            attempts = 0;
            successes = 0;
        }
    }
}
