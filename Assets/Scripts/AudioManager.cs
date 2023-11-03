using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip hitWallClip;
    public AudioClip hitGoalClip;
    public AudioClip hitPowerUpClip;

    private AudioSource backgroundAudioSource;
    private AudioSource audioSource;
    public static AudioManager Instance { get; private set; }

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        backgroundAudioSource = GetComponents<AudioSource>()[0];
        audioSource = GetComponents<AudioSource>()[1];
    }
 
    public void PlayHitGoalClip() {
        audioSource.PlayOneShot(hitGoalClip);
    }

    public void PlayHitWallClip() {
        audioSource.PlayOneShot(hitWallClip);
    }

    public void PlayHitPowerUpClip() {
        audioSource.PlayOneShot(hitPowerUpClip);
    }


}
