using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeDisplay : MonoBehaviour
{
    public PlayerController Player;
    public List<UpgradeCard> CardSlots;
    public List<UpgradeData> AllUpgrades;
    private List<UpgradeData> _availableUpgrades = new List<UpgradeData>();
    private Dictionary<UpgradeData, int> _upgradeCounts = new Dictionary<UpgradeData, int>();
    public bool poolNeedsUpdate = true;

    public void Initialize()
    {
        // Pause the game
        Time.timeScale = 0f;

        ShowUpgradeCards();
    }

    private void ShowUpgradeCards()
    {
        // Set the available upgrades if the pool needs an update
        if (poolNeedsUpdate) GetAvailableUpgrades();

        // Determine how many upgrade cards to display (1 to 3)
        int numUpgradesToDisplay = Mathf.Min(3, _availableUpgrades.Count);

        // Get random upgrades based on the available count
        List<UpgradeData> randomUpgrades = GetRandomUpgrades(numUpgradesToDisplay);

        // Loop only through the available slots for the number of upgrades to display
        for (int i = 0; i < numUpgradesToDisplay; i++)
        {
            UpgradeData upgrade = randomUpgrades[i];

            // Get the count of how many times this upgrade has been taken
            int upgradeCount = _upgradeCounts.ContainsKey(upgrade) ? _upgradeCounts[upgrade] + 1 : 1;

            // Set the card slot with the upgrade and its count
            CardSlots[i].SetCard(upgrade, upgradeCount);
        }

        // Disable the remaining card slots if there are fewer than 3 upgrades
        for (int i = numUpgradesToDisplay; i < CardSlots.Count; i++)
        {
            CardSlots[i].gameObject.SetActive(false);
        }
    }

    private void GetAvailableUpgrades()
    {
        // Clear the list before adding new upgrades to avoid duplicates
        _availableUpgrades.Clear();

        foreach (UpgradeData upgrade in AllUpgrades)
        {
            if (_upgradeCounts.ContainsKey(upgrade) && _upgradeCounts[upgrade] >= 10)
            {
                continue;
            }

            _availableUpgrades.Add(upgrade);
        }

        poolNeedsUpdate = false;
    }

    private List<UpgradeData> GetRandomUpgrades(int count)
    {
        HashSet<UpgradeData> uniqueUpgrades = new HashSet<UpgradeData>();

        // Loop until we have the desired number of unique upgrades
        while (uniqueUpgrades.Count < count)
        {
            // Get a random upgrade from availableUpgrades
            UpgradeData upgrade = _availableUpgrades[Random.Range(0, _availableUpgrades.Count)];

            // Add the selected upgrade to the HashSet (duplicates are ignored)
            uniqueUpgrades.Add(upgrade);
        }

        // Convert HashSet to List
        return uniqueUpgrades.ToList();
    }

    /// <summary>
    /// Apply the selected upgrade
    /// </summary>
    /// <param name="upgrade">The selected upgrade</param>
    public void SelectUpgrade(UpgradeData upgrade)
    {
        switch (upgrade.Type)
        {
            case UpgradeData.UpgradeType.PASSIVE:
                Player.ApplyUpgrade(upgrade);
                break;
            case UpgradeData.UpgradeType.WEAPON:
                break;
        }

        // Update the upgrade count for the selected upgrade
        if (_upgradeCounts.ContainsKey(upgrade))
        {
            _upgradeCounts[upgrade]++;
            if (_upgradeCounts[upgrade] >= 10) poolNeedsUpdate = true;
        }
        else
        {
            _upgradeCounts[upgrade] = 1;
        }

        // Unpause the game after selecting an upgrade
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}
