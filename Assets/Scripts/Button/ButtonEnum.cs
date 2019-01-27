using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeButton {
    JOGAR, PAUSAR, VOLTAR, RESTART, SAIR
}

[RequireComponent(typeof(Animator))]
public class ButtonEnum : MonoBehaviour {

    [SerializeField]
    private TypeButton buttonType;

    [SerializeField]
    private AudioSource bubbleSound;

    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void Acao(){
        if (bubbleSound != null) {
            bubbleSound.Play();
        }
        animator.SetTrigger("Pressed");
    }

    public void EventoAcao() {
        switch (buttonType) {
            case TypeButton.JOGAR: Jogar(); break;
            case TypeButton.PAUSAR: Pausar(); break;
            case TypeButton.VOLTAR: Voltar(); break;
            case TypeButton.RESTART: Restart(); break;
            case TypeButton.SAIR: Sair(); break;
        }
    }

    private void Jogar() {
        SceneController.Jogar();
    }

    private void Pausar() {
        PlayerController playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerController.death();
    }

    private void Voltar() {
        SceneController.Menu();
    }

    private void Restart() {
        SceneController.Jogar();
    }

    private void Sair() {
        SceneController.Sair();
    }

}
