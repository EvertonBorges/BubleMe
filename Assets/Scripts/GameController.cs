using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    //[SerializeField]
    //private Image[] lifes;

    [SerializeField]
    private Image[] coins;

    [SerializeField]
    private ItemController[] itens;

    [SerializeField]
    private Image panelGameOver;

    [SerializeField]
    private CameraBehavior cameraBehavior;

    [SerializeField]
    private SoundController soundController;

    [SerializeField]
    private AudioSource somLivrosCaindo;

    [SerializeField]
    private Transform livrosCima;

    [SerializeField]
    private Transform livrosBaixo;

    private int qtdeItens;

    private bool livrosCairam;

    private void Awake() {
        panelGameOver.gameObject.SetActive(false);
        livrosCairam = false;
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void gameOver() {
        Debug.Log("Game Over");
        soundController.Morreu();
        foreach (ItemController item in itens) {
            if (item != null) {
                item.Morreu();
            }
        }
        cameraBehavior.Morreu();
        panelGameOver.gameObject.SetActive(true);
        //Application.Quit();
    }

    /*
    public void qtdeVidas(int lifes) {
        for (int i = 0; i < this.lifes.Length; i++) {
            if (i < lifes) {
                this.lifes[i].gameObject.SetActive(true);
            } else {
                this.lifes[i].gameObject.SetActive(false);
            }
        }
    }
    */

    public void qtdeCoins(int coins) {
        qtdeItens = coins;

        for (int i = 0; i < this.coins.Length; i++) {
            if (i < coins) {
                this.coins[i].gameObject.SetActive(true);
            } else {
                this.coins[i].gameObject.SetActive(false);
            }
        }

        if (coins == 5) {
            Win();
        }
    }

    private void Win() {
        Debug.Log("Ganhou");
    }

    public Image getImagem() {
        return (qtdeItens > 0) ? coins[qtdeItens - 1] : coins[0];
    }

    public void CairLivros(){
        if (!livrosCairam) {
            livrosCima.gameObject.SetActive(false);
            livrosBaixo.gameObject.SetActive(true);

            somLivrosCaindo.Play();
            livrosCairam = true;
        }
    }

}