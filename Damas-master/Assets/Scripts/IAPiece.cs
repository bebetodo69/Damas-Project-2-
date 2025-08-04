using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPiece : Piece
{
    IAControl.IaCallBack iaCallBack;
    public IAControl.UpdateCallBack updateCallBack;
    protected Directions _direction = Directions.NULL;
    protected Vector3 _intense = Vector3.zero;
    public bool isKilling = false;

    void Start()
    {
        StartAgent();
    }

    public void EnableIA(IAControl.IaCallBack iaCall, IAControl.UpdateCallBack upCallBack, Directions direction, Vector3 intense)
    {
        _direction = direction;
        _intense = intense;
        iaCallBack = iaCall;
        updateCallBack = upCallBack;
        isKilling = IsThereSomeoneToKill(direction);
        Myenable = true;
        StartCoroutine("MoveIA");
    }

    IEnumerator MoveIA()
    {
        yield return new WaitForSeconds(0.35f);
        bool noMoreKills = true;
        if (_direction != Directions.NULL && Myenable)
        {
            bool moved = MovePart(_direction);

            if (moved)
            {

                if (isKilling)
                {
                    foreach (Directions dir in Control.allDirections)
                    {
                        if (IsThereSomeoneToKill(dir))
                        {
                            _direction = dir;
                            noMoreKills = false;
                            StartCoroutine("MoveIA");
                        }
                    }
                }

                if (noMoreKills)
                {
                    Myenable = false;
                    iaCallBack(false);
                    isKilling = false;
                    ChangeTurn();
                }
            }
            else
            {
                Myenable = false;
                iaCallBack(true);
                isKilling = false;
            }
        }
        else
        {
            //print("NAO FAZ SENTIDO!!!");
        }
    }
}

