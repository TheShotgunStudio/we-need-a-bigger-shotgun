using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Shotgun : Weapon
{
    public Transform ExplosionPosition;
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

        if (Crosshair == null) return;
        if(Crosshair.GetComponent<RectTransform>().sizeDelta.x != _crosshairSize){
            _currentCrosshairSize = Crosshair.GetComponent<RectTransform>().sizeDelta.x;
            _currentCrosshairSize = Mathf.Lerp(_currentCrosshairSize, _crosshairSize, Time.deltaTime);
            Crosshair.GetComponent<RectTransform>().sizeDelta = new Vector2(_currentCrosshairSize, _currentCrosshairSize);
        }
    }

    public override bool CanShoot()
    {
        return Input.GetMouseButton(0) && ReloadTimer.IsFinished();
    }
    
    protected override void Shoot(Rigidbody playerRigidbody, CameraController cameraController)
    {
        ReloadTimer.Start(Stats.ReloadTime);
        playerRigidbody.velocity -= cameraController.CameraFollowTarget.transform.forward * Stats.RecoilStrength;
        if (Crosshair != null)
        {
            Crosshair.GetComponent<RectTransform>().sizeDelta = new Vector2(_crosshairSize * 2.0f, _crosshairSize * 2.0f);
        }
        if (SoundEffectManager.Instance != null)
        {
            SoundEffectManager.Instance.PlaySound(ShotgunShot, ExplosionPosition, 1.0f);
            SoundEffectManager.Instance.PlaySoundNoPitchDelayed(ShotgunReload, ExplosionPosition, 1.0f, 1.0f);
        }
        if (ShotgunMuzzleVFX != null && ShotgunReloadVFX != null)
        {
            StartCoroutine(SpawnVisualEffectAfterDelay(ShotgunMuzzleVFX, ExplosionPosition, 0.0f, 1.0f));
            StartCoroutine(SpawnParticleEffectAfterDelay(ShotgunReloadVFX, ExplosionPosition, 1.0f, 2.0f));
        }
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
