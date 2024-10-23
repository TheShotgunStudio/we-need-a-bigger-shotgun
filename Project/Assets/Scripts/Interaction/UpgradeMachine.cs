using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMachine : MonoBehaviour, IInteractable
{
    public UpgradeDisplay UpgradeDisplay;
    [SerializeField]
    private string _prompt;
    public string InteractionPrompt => _prompt;
    [SerializeField]
    private bool _used = false;
    public bool Used => _used;

    public bool Interact(Interactor interactor)
    {
        UpgradeDisplay.gameObject.SetActive(true);
        UpgradeDisplay.Initialize();

        _used = true;

        return true;
    }
}
