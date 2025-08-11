using System;
using System.Collections.Generic;

namespace mariefismi02.SeedSystem
{
    public class Randomizer
    {
        private readonly Random rng;

        public Randomizer(int seed)
        {
            rng = new Random(seed);
        }

        public T GetRandomItem<T>(T[] items)
        {
            if (items == null || items.Length == 0) return default;
            return items[rng.Next(0, items.Length)];
        }

        public IEnumerable<T> SuffleItems<T>(List<T> items)
        {
            if (items == null || items.Count == 0) return default;
            for (int i = items.Count - 1; i > 0; i--)
            {
                int j = rng.Next(0, i + 1);
                (items[i], items[j]) = (items[j], items[i]);
            }

            return items;
        }

        public int GetRandomInt(int min, int max)
        {
            return rng.Next(min, max);
        }
    }
}