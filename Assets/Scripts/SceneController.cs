using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{

    [SerializeField] private FadeEffect _transitionFade = null;

    protected override void Init()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Play()
    {
        EventSystem.GameController_GameStart.Notify();
        MonoBehaviorHelper.StartCoroutine(Transition(Scenes.GamePlay));
    }

    public void Menu()
    {
        EventSystem.GameController_MainMenu.Notify();
        MonoBehaviorHelper.StartCoroutine(Transition(Scenes.MainMenu));
    }

    private IEnumerator Transition(Scenes scene)
    {
        var canTransite = false;
        var asyncScene = SceneManager.LoadSceneAsync(scene.ToString());
        asyncScene.allowSceneActivation = false;

        _transitionFade.FadeIn(() => canTransite = true);

        while (!canTransite)
            yield return null;

        while (asyncScene.progress < 0.9f)
            yield return null;

        asyncScene.allowSceneActivation = true;

        while (asyncScene.progress < 1f)
            yield return null;

        EventSystem.GameController_Pause.Notify();

        _transitionFade.FadeOut(() => EventSystem.GameController_Unpause.Notify());
    }

    public void Quit()
    {
        Application.Quit();
    }

    void OnEnable()
    {
        EventSystem.Scene_Gameplay.Add(Play);
        EventSystem.Scene_MainMenu.Add(Menu);
        EventSystem.Scene_Quit.Add(Quit);
    }

    void OnDisable()
    {
        EventSystem.Scene_Gameplay.Remove(Play);
        EventSystem.Scene_MainMenu.Remove(Menu);
        EventSystem.Scene_Quit.Remove(Quit);
    }

}