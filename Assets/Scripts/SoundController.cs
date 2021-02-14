using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{

    private AudioSource _audioSource = null;

    [SerializeField] private AudioClip _soundMenu = null;
    [SerializeField] private AudioClip _soundGameplay = null;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayMenuMusic()
    {
       Play(_soundMenu);
    }

    public void PlayGameplayMusic()
    {
       Play(_soundGameplay);
    }

    private void Play(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }

}