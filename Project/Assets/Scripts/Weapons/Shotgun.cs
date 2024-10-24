using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Shotgun : Weapon
{
    public Transform ExplosionPosition;

    public AudioClip ShotgunShot;
    public AudioClip ShotgunReload;

    public GameObject ShotgunChargeVFX;
    public GameObject ShotgunMuzzleVFX;
    public RectTransform Crosshair;
    private GameObject _shotgunChargeVFX;
    private GameObject _shotgunChargeAudio;
    private float _crosshairSize = 100.0f;
    private float _currentCrosshairSize = 100.0f;
    private float _chargeMultiplier = 0.0f;

    [HideInInspector]
    public Timer ReloadTimer = new();

    void Update()
    {
        ReloadTimer.Tick();
        if (Crosshair == null) return;
        if(Crosshair.sizeDelta.x != _crosshairSize){
            _currentCrosshairSize = Crosshair.sizeDelta.x;
            _currentCrosshairSize = Mathf.Lerp(_currentCrosshairSize, _crosshairSize, Time.deltaTime);
            Crosshair.sizeDelta = new Vector2(_currentCrosshairSize, _currentCrosshairSize);
        }
    }

    public override bool CanShoot()
    {
        return Input.GetMouseButton(0) && ReloadTimer.IsFinished();
    }
    
    protected override void Shoot(Rigidbody playerRigidbody, CameraController cameraController)
    {
        if(Input.GetMouseButton(0)){
            if(_shotgunChargeVFX == null){
                _shotgunChargeVFX = Instantiate(ShotgunChargeVFX, ExplosionPosition);
                _shotgunChargeVFX.GetComponent<ParticleSystem>().Play();
            }

            _chargeMultiplier += Time.deltaTime;
            if(_chargeMultiplier >= 1){
                _chargeMultiplier = 1.0f;
            }

        }
        else if(Input.GetMouseButtonUp(0)){
            ReloadTimer.Start(Stats.ReloadTime);
            playerRigidbody.velocity -= cameraController.CameraFollowTarget.transform.forward * Stats.RecoilStrength * _chargeMultiplier;
            _chargeMultiplier = 0.0f;
            
            if (Crosshair != null)
            {
                Crosshair.sizeDelta = new Vector2(_crosshairSize * 2.0f, _crosshairSize * 2.0f);
            }
            if (SoundEffectManager.Instance != null)
            {
                SoundEffectManager.Instance.PlaySound(ShotgunShot, ExplosionPosition, 1.0f);
                //SoundEffectManager.Instance.PlaySoundNoPitchDelayed(ShotgunReload, ExplosionPosition, 1.0f, 1.0f);
            }
            if (ShotgunMuzzleVFX != null)
            {
                StartCoroutine(SpawnVisualEffectAfterDelay(ShotgunMuzzleVFX, ExplosionPosition, 0.0f, 1.0f));
                //StartCoroutine(SpawnParticleEffectAfterDelay(ShotgunReloadVFX, ExplosionPosition, 0.0f, Stats.ChargeTime));
            }
            DoHitReg(ExplosionPosition, Maincamera, Crosshair);
        }
    }


    //TODO make a vfx manage class and put this inside.
    private IEnumerator SpawnParticleEffectAfterDelay(GameObject effect, Transform position, float delay, float destroyDelay){
        yield return new WaitForSeconds(delay);
        GameObject vfx_effect = Instantiate(effect, position);
        vfx_effect.GetComponent<ParticleSystem>().Play();
        Destroy(vfx_effect, destroyDelay);
    }

    private IEnumerator SpawnVisualEffectAfterDelay(GameObject effect, Transform position, float delay, float destroyDelay){
        yield return new WaitForSeconds(delay);
        GameObject vfx_effect = Instantiate(effect, position);
        vfx_effect.GetComponent<VisualEffect>().Play();
        Destroy(vfx_effect, destroyDelay);
    }
}
