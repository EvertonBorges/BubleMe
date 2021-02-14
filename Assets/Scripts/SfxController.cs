using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SfxController : Singleton<SfxController>
{
   
    private AudioSource _audioSource = null;

    [SerializeField] private AudioClip _sfxPopBubble = null;
    [SerializeField] private AudioClip _sfxBooksFall = null;
    [SerializeField] private AudioClip _sfxItemPick = null;

    protected override void Init()
    {
        _audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    private void PopBubble()
    {
        PlaySfx(_sfxPopBubble);
    }

    private void BooksFall()
    {
        PlaySfx(_sfxBooksFall);
    }

    private void ItemPick()
    {
        PlaySfx(_sfxItemPick);
    }

    private void PlaySfx(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    void OnEnable()
    {
        EventSystem.Sfx_PopBuble.Add(PopBubble);
        EventSystem.Sfx_BooksFall.Add(BooksFall);
        EventSystem.Sfx_ItemPick.Add(ItemPick);
    }

    void OnDisable()
    {
        EventSystem.Sfx_PopBuble.Remove(PopBubble);
        EventSystem.Sfx_BooksFall.Remove(BooksFall);
        EventSystem.Sfx_ItemPick.Remove(ItemPick);
    }

}
