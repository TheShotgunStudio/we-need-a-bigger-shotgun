using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    public Camera CameraToTrack;
    public GameObject TrackedCharacter;
    public RectTransform RedHealth;
    public RectTransform WhiteHealth;
    public TextMeshProUGUI RemainingHealthNumber;
    public TextMeshProUGUI MaxHealthNumber;

    private float _startHealthWidth;

    private float _maxHealth;
    private float _currentHealth;
    // private float _redHealthWidth = 0;
    // private float _whiteHealthWidth = 0;
    private float _scaleDifference;

    [Range(0.001f,0.1f)]
    public float DepletionStepPercentage = 0.1f;
    

    // void Start()
    // {
    //     if (TryGetComponent(out RectTransform healthDisplayTransform))
    //     {
    //         _startHealthWidth = healthDisplayTransform.sizeDelta.x;
    //     } else {
    //         Debug.LogError("No RectTransform found");
    //     }
    // }

    void Update() {
        if (CameraToTrack != null)
        {
            this.gameObject.transform.LookAt(CameraToTrack.transform.position);
        }
        float redHealthScale = RedHealth.transform.localScale.x;
        float whiteHealthScale = WhiteHealth.transform.localScale.x;

        _scaleDifference = whiteHealthScale - redHealthScale;
        
        if (_scaleDifference > 0)
        {
            // _whiteHealthWidth = _whiteHealthWidth - ((_widthDifference + DepletionStepPercentage) * DepletionStepPercentage/10);
            // WhiteHealth.sizeDelta = new Vector2(_whiteHealthWidth , 0);
            WhiteHealth.transform.localScale = new Vector3(whiteHealthScale - ((_scaleDifference + DepletionStepPercentage) * DepletionStepPercentage/10), 1, 1);
        }
        
    }


    public void InitiateHealth() 
    {
        _currentHealth = _maxHealth;

        // RedHealth.sizeDelta = new Vector2(_redHealthWidth, 0);
        // WhiteHealth.sizeDelta = new Vector2(_whiteHealthWidth, 0);
        WhiteHealth.transform.localScale = new Vector3(1, 1, 1);
        RedHealth.transform.localScale = new Vector3(1, 1, 1);
        if (MaxHealthNumber != null) MaxHealthNumber.SetText("/" + _maxHealth.ToString());
        if (RemainingHealthNumber != null) RemainingHealthNumber.SetText(_maxHealth.ToString());
    }

    /// <summary>
    /// Reduces the width of the healthbar proportionally to the max health
    /// </summary>
    /// <param name="healthLost">integer amount of health lost</param>
    public void LoseHealth(float healthLost) 
    {
        _currentHealth -= healthLost;
        if (RemainingHealthNumber != null) RemainingHealthNumber.SetText(_currentHealth.ToString());

        // _redHealthWidth = _redHealthWidth - (healthLost * (_startHealthWidth/_maxHealth));
        // if (_redHealthWidth >= _startHealthWidth){
        //     _redHealthWidth = _startHealthWidth;
        // }
        // RedHealth.sizeDelta = new Vector2(_redHealthWidth, 0);
        RedHealth.transform.localScale = new Vector3(_currentHealth / _maxHealth, 1, 1);
    }

    /// <summary>
    /// Increases the width of the healthbar proportionally to the max health
    /// </summary>
    /// <param name="healthGained">integer amount of health gained</param>
    public void GainHealth(float healthGained)
    {
        _currentHealth += healthGained;
        if (RemainingHealthNumber != null) RemainingHealthNumber.SetText(_currentHealth.ToString());

        // _redHealthWidth = _redHealthWidth + (healthGained * (_startHealthWidth/_maxHealth));
        // if (_redHealthWidth >= 0){
        //     _redHealthWidth = 0;
        // }
        // _whiteHealthWidth = _redHealthWidth;
        // RedHealth.sizeDelta = new Vector2(_redHealthWidth , 0);
        // WhiteHealth.sizeDelta = new Vector2(_whiteHealthWidth , 0);
        WhiteHealth.transform.localScale = new Vector3(_currentHealth / _maxHealth, 1, 1);
        RedHealth.transform.localScale = new Vector3(_currentHealth / _maxHealth, 1, 1);
    }

    public void SetMaxHealth(float maxHealth) {
        _maxHealth = maxHealth;
        if (MaxHealthNumber != null) MaxHealthNumber.SetText("/" + maxHealth.ToString());
    }
}
