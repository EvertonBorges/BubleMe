using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : Singleton<GameController>
{

    [Header("UI Reference")]
    [SerializeField] private Image _panelGameover;

    [Header("Item")]
    [SerializeField] private Image[] _itemsImage;

    private bool _isPaused = false;
    public bool IsPaused => _isPaused;

    protected override void Init()
    {
        foreach(var item in _itemsImage)
            item.gameObject.SetActive(false);

        _panelGameover.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        _panelGameover.gameObject.SetActive(true);
    }

    private void Win()
    {
        Debug.Log("Ganhou");
    }

    private void GetItem(Sprite sprite)
    {
        for (int i = 0; i < _itemsImage.Length; i++)
        {
            var item = _itemsImage[i];
            if (!item.gameObject.activeSelf)
            {
                item.gameObject.SetActive(true);
                item.sprite = sprite;
                break;
            }
        }
    }

    private void Pause()
    {
        _isPaused = true;
    }

    private void Unpause()
    {
        _isPaused = false;
    }

    private void OnEnable()
    {
        EventSystem.Player_Death.Add(GameOver);
        EventSystem.GameController_AddItem.Add(GetItem);
        EventSystem.GameController_Pause.Add(Pause);
        EventSystem.GameController_Unpause.Add(Unpause);
    }

    private void OnDisable()
    {
        EventSystem.Player_Death.Remove(GameOver);
        EventSystem.GameController_AddItem.Remove(GetItem);
        EventSystem.GameController_Pause.Remove(Pause);
        EventSystem.GameController_Unpause.Remove(Unpause);
    }

}