using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    public IAPiece piece;
    public Directions direction;
    public Vector3 intense;
}

public class IAControl : MonoBehaviour
{
    IAPiece sorted = null;
    Directions dir = Directions.NULL;

    public Directions[] allDirections = { Directions.DL, Directions.DR, Directions.UL, Directions.UR };

    bool wait;
    public delegate void IaCallBack(bool validmove);
    public delegate void UpdateCallBack();
    public IaCallBack iaCall;
    public UpdateCallBack updateCallBack;

    public static Control Control { get; set; } = new Control();

    // Start is called before the first frame update
    void Start()
    {
        iaCall = myCallBack;
    }

    void myCallBack(bool validmove)
    {
        Control.UpdateValues();
        sorted = null;

        if (!validmove)
        {
            wait = false;
        }
        else
        {
            Move();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Control.Token == Turn.Player)
        {
            wait = false;
        }

        if (!wait && Control.Token == Turn.IA)
        {
            wait = true;
            Vector3 intense = Vector3.zero;
            Control.UpdateValues();
            if (Control.iAPieces.Length <= 0)
            {
                Control.UpdateValues();
                return;
            }

            if (sorted == null)
            {
                Movement killer = KillerPiece();
                dir = killer.direction;
                sorted = killer.piece;
            }

            if (sorted == null)
            {
                Movement notKiller = SortCandidate();
                sorted = notKiller.piece;
                dir = notKiller.direction;
            }

            StartCoroutine(MovePart(sorted, dir, intense));
            sorted = null;
        }
    }

    private Movement SortCandidate()
    {
        Control.UpdateValues();
        IAPiece candidate = Control.iAPieces[UnityEngine.Random.Range(0, Control.iAPieces.Length)];
        Directions dirCandidate = Directions.NULL;
        foreach (Directions direction in allDirections)
        {
            if (candidate.IsThereAPossibleMove(direction).isPossible)
            {
                dirCandidate = direction;
                break;
            }
        }

        if (dirCandidate == Directions.NULL)
        {
            return SortCandidate();
        }

        Movement move = new Movement
        {
            direction = dirCandidate,
            piece = candidate
        };
        return move;
    }

    private Movement KillerPiece()
    {
        Control.UpdateValues();
        Directions _direction = Directions.NULL;
        IAPiece killer = null;//inDangerPiece();

        foreach (IAPiece piece in Control.iAPieces)
        {
            foreach (Directions direction in allDirections)
            {
                if (piece.IsThereSomeoneToKill(direction))
                {
                    killer = piece;
                    _direction = direction;
                    break;
                }
            }
            if (killer)
            {
                break;
            }
        }
        Movement move = new Movement
        {
            direction = _direction,
            piece = killer
        };
        return move;
    }

    IEnumerator MovePart(IAPiece piece, Directions direction, Vector3 intense)
    {
        yield return new WaitForSeconds(0.5f);
        piece.EnableIA(iaCall, updateCallBack, direction, intense);

    }
}
