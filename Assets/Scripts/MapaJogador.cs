using UnityEngine;
using UnityEngine.InputSystem;

public class MapaEspacial : MonoBehaviour
{
    [SerializeField]
    private GameObject painelMapa;

    void Start()
    {
        if (painelMapa != null)
        {
            painelMapa.SetActive(false);
        }
    }

    void Update()
    {
        if (painelMapa == null ||
            Keyboard.current == null)
            return;

        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            painelMapa.SetActive(
                !painelMapa.activeSelf
            );
        }
    }
}
