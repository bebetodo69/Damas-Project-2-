using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPiece : Piece
{
    public bool isKilling = false;

    private void Start()
    {
        StartAgent();
    }

    void Update()
    {
        if (Myenable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RegularMove();
            }
        }
    }

    void OnMouseUp()
    {
        Control.UpdateValues();
        if (Control.Token == Turn.Player && Myenable == false)
        {
            foreach (PlayerPiece piece in Control.playerPieces)
            {
                if (piece.Myenable)
                    piece.Myenable = false;
            }

            Myenable = true;
        } else
        {
            Myenable = false;
        }
    }

    void RegularMove()
    {
        bool finishedKills = true;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            Control.UpdateValues();
            int totalEnemys = Control.iAPieces.Length;

            if (IsValidMovement(hit.point))
            {
                Control.UpdateValues();

                int totalEnemysNew = Control.iAPieces.Length;

                if (totalEnemys != totalEnemysNew)
                {
                    foreach (Directions dir in Control.allDirections)
                    {
                        if (IsThereSomeoneToKill(dir))
                        {
                            finishedKills = false;
                        }
                    }
                }

                if (finishedKills)
                {
                    Myenable = false;
                    ChangeTurn();
                }
            }
        }
    }

    public Directions DirectionsValue(Vector3 point)
    {
        if (point.x - transform.position.x > 0)
        {
            if (point.z - transform.position.z > 0)
            {
                return Directions.UR;
            }
            else
            {
                return Directions.DR;
            }

        }
        else
        {
            if (point.z - transform.position.z > 0)
            {
                return Directions.UL;
            }
            else
            {
                return Directions.DL;
            }

        }
    }

    public bool IsValidMovement(Vector3 point)
    {
        bool validmove;

        if (point.x - transform.position.x > 0)
        {
            if (point.z - transform.position.z > 0)
            {
                validmove = MovePart(Directions.UR);
            }
            else
            {
                validmove = MovePart(Directions.DR);
            }

        }
        else
        {
            if (point.z - transform.position.z > 0)
            {
                validmove = MovePart(Directions.UL);
            }
            else
            {
                validmove = MovePart(Directions.DL);
            }

        }

        return validmove;
    }

    public bool IsKilling(Vector3 point)
    {
        bool isKilling;

        if (point.x - transform.position.x > 0)
        {
            if (point.z - transform.position.z > 0)
            {
                isKilling = IsThereSomeoneToKill(Directions.UR);
            }
            else
            {
                isKilling = IsThereSomeoneToKill(Directions.DR);
            }

        }

        else
        {
            if (point.z - transform.position.z > 0)
            {
                isKilling = IsThereSomeoneToKill(Directions.UL);
            }
            else
            {
                isKilling = IsThereSomeoneToKill(Directions.DL);
            }

        }

        return isKilling;
    }
}
