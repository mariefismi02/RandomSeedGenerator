using System;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace mariefismi02.SeedSystem
{
    public class SeedGeneratorService : MonoBehaviour
    {
        private string currentSeed;
        public string CurrentSeed => currentSeed;
        
        private const int MaxSeedLength = 8;

        public static SeedGeneratorService Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void InitializeSeed(string seedStr = "")
        {
            currentSeed = string.IsNullOrEmpty(seedStr) ? GenerateRandomSeed() : seedStr;
            if (string.IsNullOrEmpty(currentSeed))
                currentSeed = GenerateRandomSeed();
            Debug.Log($"Initialized seed: {currentSeed} (hash: {StringToSeed(currentSeed)})");
        }

        public int GetSeedHash()
        {
            return StringToSeed(currentSeed);
        }

        private string GenerateRandomSeed()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
            Random.InitState((int)(DateTime.Now.Ticks % int.MaxValue));
            StringBuilder seed = new StringBuilder(MaxSeedLength);
            for (int i = 0; i < MaxSeedLength; i++)
            {
                seed.Append(chars[Random.Range(0, chars.Length)]);
            }

            return seed.ToString();
        }

        private int StringToSeed(string seedStr)
        {
            if (int.TryParse(seedStr, out int seed)) return seed;
            return seedStr.GetHashCode();
        }

        public void CopySeedToClipboard()
        {
            GUIUtility.systemCopyBuffer = currentSeed;
            Debug.Log($"Copied seed to clipboard: {currentSeed}");
        }
    }
}