using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    public Camera CameraToTrack;
    public GameObject TrackedCharacter;
    public RectTransform RedHealth;
    public RectTransform WhiteHealth;

    private float _startHealthWidth;

    private float _maxHealth;
    private float _redHealthWidth = 0;
    private float _whiteHealthWidth = 0;
    private float _widthDifference;

    [Range(0.001f,0.1f)]
    public float DepletionStepPercentage = 0.1f;
    

    void Start()
    {
        if (TryGetComponent(out RectTransform healthDisplayTransform))
        {
            _startHealthWidth = healthDisplayTransform.sizeDelta.x;
        } else {
            Debug.LogError("No RectTransform found");
        }
        InitiateHealth();
    }

    void Update() {
        this.gameObject.transform.LookAt(CameraToTrack.transform.position);
        _widthDifference = _whiteHealthWidth - _redHealthWidth;
        if (_widthDifference > 0)
        {
            _whiteHealthWidth = _whiteHealthWidth - (_widthDifference * DepletionStepPercentage/10);
            WhiteHealth.sizeDelta = new Vector2(_whiteHealthWidth , 0);
        }
    }


    private void InitiateHealth() 
    {
        RedHealth.sizeDelta = new Vector2(_redHealthWidth, 0);
        WhiteHealth.sizeDelta = new Vector2(_whiteHealthWidth, 0);
    }

    /// <summary>
    /// Reduces the width of the healthbar proportionally to the max health
    /// </summary>
    /// <param name="healthLost">integer amount of health lost</param>
    public void LoseHealth(float healthLost) 
    {
        Debug.Log(_redHealthWidth);
        _redHealthWidth = _redHealthWidth - (healthLost * (_startHealthWidth/_maxHealth));
        Debug.Log(_redHealthWidth);
        if (_redHealthWidth >= _startHealthWidth){
            _redHealthWidth = _startHealthWidth;
        }
        RedHealth.sizeDelta = new Vector2(_redHealthWidth, 0);
    }

    /// <summary>
    /// Increases the width of the healthbar proportionally to the max health
    /// </summary>
    /// <param name="healthGained">integer amount of health gained</param>
    public void GainHealth(float healthGained) 
    {
        _redHealthWidth = _redHealthWidth + (healthGained * (_startHealthWidth/_maxHealth));
        if (_redHealthWidth >= 0){
            _redHealthWidth = 0;
        }
        _whiteHealthWidth = _redHealthWidth;
        RedHealth.sizeDelta = new Vector2(_redHealthWidth , 0);
        WhiteHealth.sizeDelta = new Vector2(_whiteHealthWidth , 0);
    }

    public void SetMaxHealth(float maxHealth) {
        _maxHealth = maxHealth;
    }
}
