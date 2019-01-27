using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class ItemController : MonoBehaviour {

    [SerializeField]
    private SpriteRenderer image;

    private GameController gameController;

    private Animator animator;
    private PlayerController playerController;

    void Awake() {
        animator = GetComponent<Animator>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void collected(PlayerController playerController) {
        Debug.Log("Coletado");
        this.playerController = playerController;
        animator.SetTrigger("Collected");
    }

    public void destroiItem() {
        Image imagem = gameController.getImagem();
        imagem.sprite = image.sprite;
        playerController.addCoin();

        GameObject.Destroy(gameObject);
    }

    public void Morreu() {
        animator.speed = 0f;
        //animator.StopPlayback();
    }

}
