
using UnityEngine;

public class Esqueleto : MonoBehaviour
{
    private int modo = 0;

    [SerializeField]
    private int velocidade = 2;

    [SerializeField]
    private int distancia = 3;

    private Vector2 destino;
    private Transform jogador;

    private float paralisadoAte;
    private Vector2 origem;

    void Start()
    {
        origem = transform.position;

        NovaPosicao();
    }

    void Update()
    {
        if (Time.time < paralisadoAte)
            return;

        if (modo == 0)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                destino,
                velocidade * Time.deltaTime
            );

            if (Vector2.Distance(
                transform.position,
                destino) < 0.1f)
            {
                NovaPosicao();
            }
        }
        else if (jogador != null)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                jogador.position,
                velocidade * Time.deltaTime
            );
        }
        else
        {
            modo = 0;
            NovaPosicao();
        }
    }

    void NovaPosicao()
    {
        float x = Random.Range(
            -distancia,
            distancia
        );

        float y = Random.Range(
            -distancia,
            distancia
        );

        destino = origem + new Vector2(x, y);
    }

    public void Paralisar(float segundos)
    {
        paralisadoAte = Mathf.Max(
            paralisadoAte,
            Time.time + segundos
        );
    }

    void OnTriggerEnter2D(Collider2D colisao)
    {
        if (colisao.CompareTag("Player"))
        {
            jogador = colisao.transform;
            modo = 1;
        }
    }

    void OnTriggerExit2D(Collider2D colisao)
    {
        if (colisao.CompareTag("Player"))
        {
            jogador = null;
            modo = 0;

            NovaPosicao();
        }
    }
}
