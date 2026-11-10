
using UnityEngine;

public class TeleporteEspacial : MonoBehaviour
{
    [SerializeField]
    private Transform destino;

    private static float proximoTeleporte = 0f;

    private void OnTriggerEnter2D(Collider2D outro)
    {
        if (!outro.CompareTag("Player"))
            return;

        if (destino == null)
        {
            Debug.LogWarning(
                "Destino do teleporte nao configurado!"
            );
            return;
        }

        if (Time.time < proximoTeleporte)
            return;

        // Evita voltar imediatamente ao portal anterior.
        proximoTeleporte = Time.time + 1f;

        Rigidbody2D corpo =
            outro.GetComponent<Rigidbody2D>();

        if (corpo != null)
        {
            corpo.position = destino.position;
            corpo.linearVelocity = Vector2.zero;
        }
        else
        {
            outro.transform.position = destino.position;
        }
    }
}
