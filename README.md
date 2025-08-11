# Balatro-Like Seed Generator

*Balatro*-like seed generator system for Unity, providing:
- 8-character random seed generation (A–Z, 1–9) with time-based uniqueness.
- Support for 0–8 character manual seed inputs with validation.
- Isolated randomizers for different systems (e.g., deck, shop, boss) to ensure consistent randomization per group across runs.
- Clipboard functionality for copying seeds.

This package is ideal for roguelike games or procedural content generation requiring reproducible, seed-based randomization.

## Installation

Add the package to your Unity project via Git URL:

1. Open Unity Package Manager (`Window > Package Manager`).
2. Click the `+` button and select `Add package from git URL`.
3. Enter the repository URL: `https://github.com/your-username/xai-seed-system.git`
4. Click `Add`. Unity will download and import the package.

**Requirements**:
- Unity 2019.4 or later.
- No external dependencies.

## Usage

1. **Add `SeedManager` to a GameObject**:
   - Attach the `SeedManager` component to a GameObject in your scene.
   - Optionally, provide a custom `ISeedHasher` implementation for seed hashing (defaults to `DefaultSeedHasher`).

2. **Initialize the Seed**:
   - Call `SeedManager.InitializeSeed(seedStr)` to set a seed.
   - Use a custom seed (0–8 characters, A–Z, 1–9) by passing `seedStr`.
   - If `seedStr` is empty or null, an 8-character random seed is generated automatically (e.g., `KXYZAB12`).

3. **Register Randomizers**:
   - Create a `RandomizerManager` instance with the seed hash from `SeedManager.GetSeedHash()`.
   - Call `RandomizerManager.RegisterRandomizer(category)` to add a `Randomizer` for each group (e.g., "deck", "shop", "boss").
   - Register a new `Randomizer` for each group to ensure consistent randomization for that group across runs.

4. **Use Randomizers**:
   - Call `RandomizerManager.GetRandomItem(items, category)` or `GetRandomInt(category, min, max)` to get random results for a specific group.
   - The same seed and actions will produce consistent results for each group.

5. **Copy Seed to Clipboard**:
   - Call `SeedManager.CopySeedToClipboard()` to copy the current seed for sharing.

## Example

```csharp
using UnityEngine;
using mariefismi02.SeedSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string[] cards = { "Ace", "King", "Queen" };
    [SerializeField] private string[] jokers = { "DNA", "Blueprint", "Triboulet" };
    [SerializeField] private string[] bosses = { "The Wall", "The Needle" };

    void Awake()
    {
        InitializeGame();
    }

    void Start(){
        // register groups
        randomizerManager.RegisterRandomizer("deck");
        randomizerManager.RegisterRandomizer("shop");
        randomizerManager.RegisterRandomizer("boss");

        // Generate game state
        string startingCard = randomizerManager.GetRandomItem(cards, "deck");
        string[] shopItems = new string[2];
        for (int i = 0; i < 2; i++)
            shopItems[i] = randomizerManager.GetRandomItem(jokers, "shop");
        string boss = randomizerManager.GetRandomItem(bosses, "boss");

        Debug.Log($"Seed: {seedManager.CurrentSeed}");
        Debug.Log($"Starting card: {startingCard}");
        Debug.Log($"Shop items: {string.Join(", ", shopItems)}");
        Debug.Log($"First boss: {boss}");

        // Copy seed to clipboard
        seedManager.CopySeedToClipboard();
    }

    void InitializeGame()
    {
        // Initialize seed (custom or random)
        string inputSeed = ""; // Replace with user input, e.g., "H"
        SeedManager.Instance.InitializeSeed(inputSeed);
    }
}
```

## Why Register Randomizers Per Group?
Each `Randomizer` ensures consistent randomization for its group (e.g., deck, shop, boss) across runs with the same seed. Register a new `Randomizer` for each group to maintain isolated, reproducible random sequences, mimicking *Balatro*'s seed system.

## License
MIT License. See [LICENSE](LICENSE) for details.
