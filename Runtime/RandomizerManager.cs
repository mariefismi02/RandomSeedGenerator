using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace mariefismi02.SeedSystem
{
    public class RandomizerManager : MonoBehaviour
    {
        public static RandomizerManager Instance { get; private set; }

        private Dictionary<string, Randomizer> randomizers;
        private int seedOffset;

        private void Awake()
        {
            Instance = this;
            randomizers = new Dictionary<string, Randomizer>();
            seedOffset = 1;
        }

        public void RegisterRandomizer(string category)
        {
            if (string.IsNullOrEmpty(category))
                throw new ArgumentException("Category cannot be null or empty.");
            if (randomizers.ContainsKey(category))
                throw new ArgumentException($"Category already registered: {category}");

            var baseSeed = SeedGeneratorService.Instance.GetSeedHash();
            randomizers[category] = new Randomizer(baseSeed + seedOffset++);
            Debug.Log($"Registered randomizer for category: {category}");
        }

        public T GetRandomItem<T>(T[] items, string category)
        {
            if (!randomizers.TryGetValue(category, out var randomizer))
                throw new ArgumentException($"No randomizer for category: {category}");
            return randomizer.GetRandomItem(items);
        }

        public int GetRandomInt(string category, int min, int max)
        {
            if (!randomizers.TryGetValue(category, out var randomizer))
                throw new ArgumentException($"No randomizer for category: {category}");
            return randomizer.GetRandomInt(min, max);
        }

        public IEnumerable<T> SuffleItems<T>(IEnumerable<T> items, string category)
        {
            if (!randomizers.TryGetValue(category, out var randomizer))
                throw new ArgumentException($"No randomizer for category: {category}");
            return randomizer.SuffleItems(items.ToList());
        }
    }
}