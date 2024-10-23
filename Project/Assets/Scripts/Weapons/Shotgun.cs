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

    public AudioClip ShotgunShot;
    public AudioClip ShotgunReload;

    public GameObject ShotgunReloadVFX;
    public GameObject ShotgunMuzzleVFX;

    private Timer _reloadTimer = new();

    void Update()
    {
        _reloadTimer.Tick();

        if (Input.GetMouseButton(0) && _reloadTimer.IsFinished())
        {
            Shoot();
        }
    }

    public override void Shoot()
    {
        _reloadTimer.Start(Stats.ReloadTime);
        PlayerRigidbody.GetComponent<Rigidbody>().velocity -= MainCamera.transform.forward * Stats.RecoilStrength;
        SoundEffectManager.Instance.PlaySound(ShotgunShot, ExplosionPosition, 1.0f);
        StartCoroutine(SpawnVisualEffectAfterDelay(ShotgunMuzzleVFX, ExplosionPosition, 0.0f, 1.0f));
        SoundEffectManager.Instance.PlaySoundNoPitchDelayed(ShotgunReload, ExplosionPosition, 1.0f, 1.0f);
        StartCoroutine(SpawnParticleEffectAfterDelay(ShotgunReloadVFX, ExplosionPosition, 1.0f, 2.0f));
    }

    //TODO make a vfx manage class and put this inside.
    private IEnumerator SpawnParticleEffectAfterDelay(GameObject effect, Transform position, float delay, float destroyDelay)
    {
        yield return new WaitForSeconds(delay);
        GameObject vfx_effect = Instantiate(effect, ExplosionPosition);
        vfx_effect.GetComponent<ParticleSystem>().Play();
        Destroy(vfx_effect, destroyDelay);
    }

    private IEnumerator SpawnVisualEffectAfterDelay(GameObject effect, Transform position, float delay, float destroyDelay)
    {
        yield return new WaitForSeconds(delay);
        GameObject vfx_effect = Instantiate(effect, ExplosionPosition);
        vfx_effect.GetComponent<VisualEffect>().Play();
        Destroy(vfx_effect, destroyDelay);
    }
}
