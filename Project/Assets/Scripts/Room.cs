using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Create an Enemy List with all active enemies in the room
    // public List<Enemy> enemies = new List<Enemy>()
    public List<Door> Doors;
    private bool _roomCleared = false;

    private void Update()
    {
        if (_roomCleared) return; 
        
        // If all enemies in the room have been defeated open the doors
        RoomCompleted();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_roomCleared) return;

        if (other.TryGetComponent(out PlayerController player))
        {
            CloseDoors();
            // Spawn enemies
        }
    }

    private void RoomCompleted()
    {
        // ShowUpgrades()
        OpenDoors();

        _roomCleared = true;
    }

    private void OpenDoors()
    {
        foreach (Door door in Doors)
        {
            // door.OpenDoor();
            door.gameObject.SetActive(true);
        }
    }

    private void CloseDoors()
    {
        foreach (Door door in Doors)
        {
            // door.CloseDoor();
            door.gameObject.SetActive(false);
        }
    }
}
