
using UnityEngine;
using UnityEngine.InputSystem;

public class DefesaIntimidacao : MonoBehaviour
{
    [SerializeField]
    private float alcance = 2f;

    [SerializeField]
    private float duracao = 4f;

    [SerializeField]
    private float recarga = 10f;

    private float disponivelEm;

    void Update()
    {
        if (Keyboard.current == null ||
            Time.time < disponivelEm)
            return;

        if (!Keyboard.current.qKey.wasPressedThisFrame)
            return;

        Collider2D[] proximos =
            Physics2D.OverlapCircleAll(
                transform.position,
                alcance
            );

        Esqueleto alvo = null;

        float menorDistancia =
            float.PositiveInfinity;

        foreach (Collider2D col in proximos)
        {
            Esqueleto inimigo =
                col.GetComponentInParent<Esqueleto>();

            if (inimigo == null)
                continue;

            float distancia = Vector2.Distance(
                transform.position,
                inimigo.transform.position
            );

            if (distancia < menorDistancia)
            {
                menorDistancia = distancia;

                alvo = inimigo;
            }
        }

        if (alvo == null)
            return;

        alvo.Paralisar(duracao);

        disponivelEm = Time.time + recarga;

        Debug.Log(
            "Inimigo paralisado por " +
            duracao + " segundos!"
        );
    }
}
