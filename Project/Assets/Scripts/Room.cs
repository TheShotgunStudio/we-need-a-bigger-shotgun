using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Create an Enemy List with all active enemies in the room
    // public List<Enemy> Enemies = new();
    public UpgradeDisplay UpgradeDisplay;
    public List<GameObject> Enemies = new();
    public List<Door> Doors;
    private bool _roomCleared = false;

    private void Start()
    {
        OpenDoors();

        foreach (GameObject enemy in Enemies)
        {
            enemy.SetActive(false);
        }
    }

    private void Update()
    {
        if (_roomCleared) return;

        if (AllEnemiesDefeated()) RoomCompleted();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_roomCleared) return;

        if (other.TryGetComponent(out PlayerController player))
        {
            CloseDoors();
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        foreach (GameObject enemy in Enemies)
        {
            enemy.SetActive(true);
        }
    }

    private bool AllEnemiesDefeated()
    {
        foreach (GameObject enemy in Enemies)
        {
            if (enemy != null)
            {
                return false;
            }
        }

        return true;
    }

    private void RoomCompleted()
    {
        ShowUpgrades();
        OpenDoors();

        _roomCleared = true;
    }

    private void ShowUpgrades()
    {
        if (UpgradeDisplay == null) return;

        UpgradeDisplay.gameObject.SetActive(true);
        UpgradeDisplay.Initialize();
    }

    private void OpenDoors()
    {
        foreach (Door door in Doors)
        {
            door.OpenDoor();
            // door.gameObject.SetActive(false);
        }
    }

    private void CloseDoors()
    {
        foreach (Door door in Doors)
        {
            door.CloseDoor();
            // door.gameObject.SetActive(true);
        }
    }
}
