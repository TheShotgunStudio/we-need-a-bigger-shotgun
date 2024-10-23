using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeMachine : MonoBehaviour, IInteractable
{
    public UpgradeDisplay UpgradeDisplay;
    public TextMeshProUGUI PromptText;
    [SerializeField]
    private string _prompt;
    public string InteractionPrompt => _prompt;
    [SerializeField]
    private bool _used = false;
    public bool Used => _used;
    private Renderer _renderer;
    [SerializeField]
    private Material _materialAfterUse;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        PromptText.enabled = false;

        PromptText.text = _prompt;
    }

    public bool Interact(Interactor interactor)
    {
        UpgradeDisplay.gameObject.SetActive(true);
        UpgradeDisplay.Initialize();

        _used = true;

        if (_renderer != null && _materialAfterUse != null)
        {
            _renderer.material = _materialAfterUse;
        }

        return true;
    }
}
