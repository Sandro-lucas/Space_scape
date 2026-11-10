
using UnityEngine;
using UnityEngine.InputSystem;

public class Jogador : MonoBehaviour
{
    public int velocidade = 5;

    private Vector2 direcao;

    public Rigidbody2D rig;
    public Animator animador;

    void Awake()
    {
        if (rig == null)
            rig = GetComponent<Rigidbody2D>();

        if (animador == null)
            animador = GetComponent<Animator>();
    }

    
    void Update()
    {
        if (animador == null)
            return;

        if (direcao == Vector2.zero)
        {
            // Parado: olhando para a frente
            animador.SetFloat("eixoX", 0f);
            animador.SetFloat("eixoY", -1f);
            animador.SetBool("correndo", false);
        }
        else
        {
            // Andando: direção do teclado
            animador.SetFloat("eixoX", direcao.x);
            animador.SetFloat("eixoY", direcao.y);
            animador.SetBool("correndo", true);
        }
    }


    void FixedUpdate()
    {
        if (rig == null)
            return;

        rig.linearVelocity =
            direcao.normalized * velocidade;
    }

    public void OnMove(
        InputAction.CallbackContext input)
    {
        direcao = input.ReadValue<Vector2>();
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (GameManager.instancia == null)
            return;

        if (outro.CompareTag("orbe") ||
            outro.CompareTag("Bateria"))
        {
            GameManager.instancia.ColetarBateria();

            Destroy(outro.gameObject);
        }
        else if (outro.CompareTag("Finish"))
        {
            GameManager.instancia.TrocarDeFase();
        }
    }

    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (GameManager.instancia == null)
            return;

        Esqueleto inimigo =
            colisao.collider.GetComponentInParent<Esqueleto>();

        if (inimigo != null)
        {
            GameManager.instancia.Derrota();
        }
    }
}
