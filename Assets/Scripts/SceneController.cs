using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public static void Jogar() {
        SceneManager.LoadScene(Scenes.GamePlay.ToString());
    }

    public static void Menu() {
        SceneManager.LoadScene(Scenes.MainMenu.ToString());
    }

    public static void Sair() {
        Application.Quit();
    }

}