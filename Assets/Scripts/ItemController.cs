using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ItemController : MonoBehaviour {

    private Animator animator;
    private PlayerController playerController;

    void Awake() {
        animator = GetComponent<Animator>();
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
        playerController.addCoin();
        GameObject.Destroy(gameObject);
    }

}
