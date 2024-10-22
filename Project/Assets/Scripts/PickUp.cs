using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private GameObject _item;
    [SerializeField]
    private Transform _visual;

    private void Start()
    {
        StartCoroutine(PickUpFloatingAnimation());
    }

    private IEnumerator PickUpFloatingAnimation()
    {
        while (true)
        {
            _visual.Rotate(Vector3.up, 60 * Time.deltaTime, Space.World);
            _visual.localPosition = new Vector3(0, 5 + Mathf.Sin(Time.time) * 0.5f, 0);
            yield return null;
        }
    }

    private void OnPickUp(PlayerController player)
    {
        // Assign item to player
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController player))
        {
            OnPickUp(player);
        }
    }
}
