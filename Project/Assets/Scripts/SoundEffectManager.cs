using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance;
    [Range(0.1f, 0.5f)]
    public float pitchMultiplier = 0.1f;
    [Range(0.01f, 0.3f)]
    public float volumeMultiplier = 0.01f;
    
    public AudioSource soundEffectObject;

    private void Awake(){
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void PlaySound(AudioClip audioClip, Transform spawnTransform, float volume){
        AudioSource audioSource = Instantiate(soundEffectObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;
        
        audioSource.volume = Random.Range(volume-volumeMultiplier, 1);
        audioSource.pitch = Random.Range(1-pitchMultiplier, 0.5f+pitchMultiplier);
        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);

    }

    public void PlaySoundDelayed(AudioClip audioClip, Transform spawnTransform, float volume, float delay){
        AudioSource audioSource = Instantiate(soundEffectObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;
        
        audioSource.volume = Random.Range(volume-volumeMultiplier, 1);
        audioSource.pitch = Random.Range(1-pitchMultiplier, 0.5f+pitchMultiplier);
        audioSource.PlayDelayed(delay);

        float clipLength = audioSource.clip.length + delay;

        Destroy(audioSource.gameObject, clipLength);

    }
    
    public void PlaySoundNoPitch(AudioClip audioClip, Transform spawnTransform, float volume){
        AudioSource audioSource = Instantiate(soundEffectObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;
        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlaySoundNoPitchDelayed(AudioClip audioClip, Transform spawnTransform, float volume, float delay){
        AudioSource audioSource = Instantiate(soundEffectObject, spawnTransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;
        audioSource.PlayDelayed(delay);

        float clipLength = audioSource.clip.length + delay;

        Destroy(audioSource.gameObject, clipLength);
    }
}
