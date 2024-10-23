using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Shotgun : Weapon
{
    public Transform MainCamera;   
    public Transform ExplosionPosition;
    public Transform PlayerRigidbody;
    public int RecoilForce = 0;
    public float ReloadTime = 0.0f;

    public AudioClip ShotgunShot;
    public AudioClip ShotgunReload;

    public GameObject ShotgunReloadVFX;
    public GameObject ShotgunMuzzleVFX;
    public GameObject Crosshair;
    private float _crosshairSize = 100.0f;
    private float _currentCrosshairSize = 100.0f;

    [HideInInspector]
    public Timer ReloadTimer = new();

    void Update()
    {
        ReloadTimer.Tick();

        if (Input.GetMouseButton(0) && ReloadTimer.IsFinished())
        {
            Shoot();
        }

        if(Crosshair.GetComponent<RectTransform>().sizeDelta.x != _crosshairSize){
            _currentCrosshairSize = Crosshair.GetComponent<RectTransform>().sizeDelta.x;
            _currentCrosshairSize = Mathf.Lerp(_currentCrosshairSize, _crosshairSize, Time.deltaTime);
            Crosshair.GetComponent<RectTransform>().sizeDelta = new Vector2(_currentCrosshairSize, _currentCrosshairSize);
        }
    }

    
    void FixedUpdate()
    {
        Crosshair.GetComponent<RectTransform>().sizeDelta = new Vector2(_crosshairSize * 2.0f, _crosshairSize * 2.0f);
        ReloadTimer.Start(Stats.ReloadTime);
        PlayerRigidbody.GetComponent<Rigidbody>().velocity -= MainCamera.transform.forward * Stats.RecoilStrength;
        SoundEffectManager.Instance.PlaySound(ShotgunShot, ExplosionPosition, 1.0f);
        StartCoroutine(SpawnVisualEffectAfterDelay(ShotgunMuzzleVFX, ExplosionPosition, 0.0f, 1.0f));
        SoundEffectManager.Instance.PlaySoundNoPitchDelayed(ShotgunReload, ExplosionPosition, 1.0f, 1.0f);
        StartCoroutine(SpawnParticleEffectAfterDelay(ShotgunReloadVFX, ExplosionPosition, 1.0f, 2.0f));
    }


    //TODO make a vfx manage class and put this inside.
    private IEnumerator SpawnParticleEffectAfterDelay(GameObject effect, Transform position, float delay, float destroyDelay){
        yield return new WaitForSeconds(delay);
        GameObject vfx_effect = Instantiate(effect, ExplosionPosition);
        vfx_effect.GetComponent<ParticleSystem>().Play();
        Destroy(vfx_effect, destroyDelay);
    }

    private IEnumerator SpawnVisualEffectAfterDelay(GameObject effect, Transform position, float delay, float destroyDelay){
        yield return new WaitForSeconds(delay);
        GameObject vfx_effect = Instantiate(effect, ExplosionPosition);
        vfx_effect.GetComponent<VisualEffect>().Play();
        Destroy(vfx_effect, destroyDelay);
    }
}
