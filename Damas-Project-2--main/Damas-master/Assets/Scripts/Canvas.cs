using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

struct GameOver {
    public bool Status;
    public Turn Player;

    public GameOver(bool status, Turn player)
    {
        Status = status;
        Player = player;
    }
}

public class Canvas : MonoBehaviour
{

    private TextMeshProUGUI turnText;
    private static GameOver gameOver;

    // Start is called before the first frame update
    void Start()
    {
        turnText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Control.Token == Turn.Player)
        {
            turnText.text = "Sua vez Jogador ...";
        }
        else
        {
            turnText.text = "Aguarde ...";
        }

        if (gameOver.Status)
        {
            if (gameOver.Player == Turn.Player)
            {
                turnText.text = "Fim de Jogo! Jogador Venceu";
            }
            else
            {
                turnText.text = "Fim de Jogo! Jogador Perdeu";
            }

            StartCoroutine(LOADNEWGAME());
        }
    }

    public static void GameOverCall(Turn winner)
    {
        gameOver = new GameOver(true, winner);
    }

    IEnumerator LOADNEWGAME()
    {
        yield return new WaitForSeconds(5.0f);

        SceneManager.LoadScene(0);
    }
}
