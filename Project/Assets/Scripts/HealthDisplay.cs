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

    private int _health;
    private float _redHealthWidth;
    private float _whiteHealthWidth;
    private float _widthDifference;

    [Range(0.001f,0.1f)]
    public float DepletionStepPercentage = 0.1f;
    

    void Start()
    {
        // TrackedCharacter.TryGetComponent<HealthTracking>(out HealthTracking characterHealth);
        _health = 10; //TODO get actual health
        _startHealthWidth = GetComponent<RectTransform>().sizeDelta.x;
        InitiateHealth();
        _redHealthWidth = 0;
        _whiteHealthWidth = 0;
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

    public void LoseHealth(int healthLost) 
    {
        Debug.Log(_redHealthWidth);
        _redHealthWidth = _redHealthWidth - (healthLost * (_startHealthWidth/_health));
        Debug.Log(_redHealthWidth);
        if (_redHealthWidth >= _startHealthWidth){
            _redHealthWidth = _startHealthWidth;
        }
        RedHealth.sizeDelta = new Vector2(_redHealthWidth, 0);
    }

    public void GainHealth(int healthGained) 
    {
        _redHealthWidth = _redHealthWidth + (healthGained * (_startHealthWidth/_health));
        if (_redHealthWidth >= 0){
            _redHealthWidth = 0;
        }
        _whiteHealthWidth = _redHealthWidth;
        RedHealth.sizeDelta = new Vector2(_redHealthWidth , 0);
        WhiteHealth.sizeDelta = new Vector2(_whiteHealthWidth , 0);
    }
}
