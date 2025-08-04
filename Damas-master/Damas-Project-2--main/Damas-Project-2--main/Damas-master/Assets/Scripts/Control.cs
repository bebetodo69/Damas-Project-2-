using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Control : MonoBehaviour
{
    public IAPiece[] iAPieces;
    public PlayerPiece[] playerPieces;
    public Directions[] allDirections = { Directions.DL, Directions.DR, Directions.UL, Directions.UR };


    public static Turn Token;

    public int[,] board = new int[8, 8];


    void Start()
    {
        //UpdateValues();
    }

    private void Update()
    {
        UpdateValues();
    }

    public void UpdateValues()
    {
        iAPieces = FindObjectsOfType<IAPiece>();
        playerPieces = FindObjectsOfType<PlayerPiece>();

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                board[i, j] = 0;
            }
        }

        foreach (IAPiece piece in iAPieces)
        {
            UpdateVisualPiece(piece);
            board[(int)piece.gameObject.transform.position.x, (int)piece.gameObject.transform.position.z] = 2;
        }

        foreach (PlayerPiece piece in playerPieces)
        {
            UpdateVisualPiece(piece);
            board[(int)piece.gameObject.transform.position.x, (int)piece.gameObject.transform.position.z] = 1;
        }

        if (iAPieces.Length <= 0)
        {
            Canvas.GameOverCall(Turn.Player);
        }

        if (playerPieces.Length <= 0)
        {
            Canvas.GameOverCall(Turn.IA);
        }
    }

    private void UpdateVisualPiece(Piece piece)
    {
        if (piece.isKing)
        {
            piece.transform.position = new Vector3(piece.transform.position.x, -0.6f, piece.transform.position.z);
        }
        else
        {
            piece.transform.position = new Vector3(piece.transform.position.x, -0.8f, piece.transform.position.z);
        }
    }
}
