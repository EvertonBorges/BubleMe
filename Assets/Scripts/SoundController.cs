using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundController : Singleton<SoundController>
{

    private AudioSource _audioSource = null;

    [SerializeField] private AudioClip _soundMenu = null;
    [SerializeField] private AudioClip _soundGameplay = null;

    [SerializeField] private float _transitionTime = 1f;

    protected override void Init()
    {
        _audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    private void PlayMenuMusic()
    {
       StartCoroutine(ChangeMusic(_soundMenu));
    }

    private void PlayGameplayMusic()
    {
       StartCoroutine(ChangeMusic(_soundGameplay));
    }

    IEnumerator ChangeMusic(AudioClip clip)
    {
        float currentTime = 0f;
        while (currentTime < _transitionTime / 2f)
        {
            currentTime += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(1f, 0f, currentTime / (_transitionTime / 2f));

            yield return null;
        }

        _audioSource.volume = 0f;
        _audioSource.Stop();
        _audioSource.clip = clip;
        yield return null;
        
        _audioSource.Play();
        yield return null;
        
        currentTime = 0f;
        while (currentTime < _transitionTime / 2f)
        {
            currentTime += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(0f, 1f, currentTime / (_transitionTime / 2f));

            yield return null;
        }

        _audioSource.volume = 1f;
    }

    void OnEnable()
    {
        EventSystem.GameController_GameStart.Add(PlayGameplayMusic);
        EventSystem.GameController_MainMenu.Add(PlayMenuMusic);
    }

    void OnDisable()
    {
        EventSystem.GameController_GameStart.Remove(PlayGameplayMusic);
        EventSystem.GameController_MainMenu.Remove(PlayMenuMusic);
    }

}