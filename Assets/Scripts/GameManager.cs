
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia
    {
        private set;
        get;
    }

    // Quantidade de baterias para escapar
    public const int BATERIAS_NECESSARIAS = 5;

    // Contador de baterias coletadas
    public int orbesColetadas = 0;

    // Controle das fases
    private int fase_atual = 0;
    private bool mudandoCena = false;

    [SerializeField]
    private Text titulo;

    [SerializeField]
    private GameObject menuOpcoes;

    [SerializeField]
    private List<Fase> fases;

    void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        instancia = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += AoCarregarCena;
    }

    void OnDestroy()
    {
        if (instancia == this)
        {
            SceneManager.sceneLoaded -= AoCarregarCena;
            instancia = null;
        }
    }

    void Start()
    {
        if (menuOpcoes != null)
        {
            menuOpcoes.SetActive(false);
        }

        AtualizarIndiceDaCena(
            SceneManager.GetActiveScene().name
        );
    }

    void AoCarregarCena(
        Scene cena,
        LoadSceneMode modo)
    {
        mudandoCena = false;

        AtualizarIndiceDaCena(cena.name);
    }

    void AtualizarIndiceDaCena(string nomeDaCena)
    {
        if (fases == null)
            return;

        for (int i = 0; i < fases.Count; i++)
        {
            if (fases[i] != null &&
                fases[i].nome == nomeDaCena)
            {
                fase_atual = i;
                return;
            }
        }
    }

    // COLETA DE BATERIAS

    public void ColetarBateria()
    {
        if (mudandoCena)
            return;

        if (orbesColetadas >= BATERIAS_NECESSARIAS)
            return;

        orbesColetadas++;

        Debug.Log(
            "Baterias coletadas: " +
            orbesColetadas + "/" +
            BATERIAS_NECESSARIAS
        );

        if (orbesColetadas == BATERIAS_NECESSARIAS)
        {
            Debug.Log(
                "Todas as baterias foram coletadas! " +
                "Encontre a capsula de fuga."
            );
        }
    }

    // Mantido para compatibilidade com
    // o projeto original do professor

    public void ColetarOrbe()
    {
        ColetarBateria();
    }

    // CAPSULA DE FUGA

    public void TrocarDeFase()
    {
        if (mudandoCena)
            return;

        if (orbesColetadas < BATERIAS_NECESSARIAS)
        {
            int faltam =
                BATERIAS_NECESSARIAS - orbesColetadas;

            Debug.Log(
                "A capsula precisa de mais " +
                faltam + " bateria(s)."
            );

            return;
        }

        if (fases == null || fases.Count == 0)
        {
            Vitoria();
            return;
        }

        int proximaFase = fase_atual + 1;

        if (proximaFase >= fases.Count)
        {
            Vitoria();
            return;
        }

        if (fases[proximaFase] == null ||
            string.IsNullOrEmpty(fases[proximaFase].nome))
        {
            Debug.LogError(
                "A proxima fase nao foi configurada."
            );

            return;
        }

        mudandoCena = true;

        fase_atual = proximaFase;
        orbesColetadas = 0;

        SceneManager.LoadScene(
            fases[fase_atual].nome
        );
    }

    // DERROTA

    public void Derrota()
    {
        if (mudandoCena)
            return;

        mudandoCena = true;

        orbesColetadas = 0;

        Debug.Log(
            "GAME OVER! Neil foi capturado " +
            "pelos alienigenas."
        );

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }

    // VITORIA

    public void Vitoria()
    {
        if (mudandoCena)
            return;

        mudandoCena = true;

        Debug.Log(
            "VITORIA! Neil coletou as 5 baterias " +
            "e conseguiu escapar da estacao espacial!"
        );

        // Futuramente, adicionar a tela de vitoria.
    }

    // TELA PRINCIPAL

    public void Demonstracao()
    {
        if (titulo != null)
        {
            titulo.text = "SPACE ESCAPE";
        }
    }

    // INICIAR JOGO

    public void IniciarJogo()
    {
        if (mudandoCena)
            return;

        mudandoCena = true;

        orbesColetadas = 0;
        fase_atual = 1;

        SceneManager.LoadScene("Fase2");
    }

    // MENU DE OPCOES

    public void MenuOpcoes()
    {
        if (menuOpcoes != null)
        {
            menuOpcoes.SetActive(
                !menuOpcoes.activeSelf
            );
        }
    }

    // SAIR DO JOGO

    public void SairDoJogo()
    {
        Debug.Log("Saindo do Space Escape...");

        Application.Quit();
    }
}
