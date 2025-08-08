using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Piece : MonoBehaviour
{
    public bool isKing = false;
    private bool myenable = false;
    public NavMeshAgent agent;
    Color oldcolor;

    private void Awake()
    {
    }

    private void Start()
    {
        
    }

    protected bool Myenable
    {
        get
        {
            return myenable;
        }
        set
        {
            myenable = value;
            if (myenable)
            {
                oldcolor = GetComponent<Renderer>().material.color;
                GetComponent<Renderer>().material.color = oldcolor + Color.white / 2;
            }
            else
            {
                GetComponent<Renderer>().material.color = oldcolor;
            }
        }

    }

    public static Control Control { get; set; } = new Control();

    public void StartAgent()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void ChangeTurn()
    {
        if (Control.Token == Turn.Player)
        {
            Control.Token = Turn.IA;
         
        } else if (Control.Token == Turn.IA)
        {
            Control.Token = Turn.Player;
        }
    }

    public Vector3 GetDirection(Directions d)
    {
        Vector3 direction = Vector3.zero;
        print("isKing" + isKing);
        switch (d)
        {
            
            case (Directions.UL):
                if (GetType() == typeof(PlayerPiece) || isKing)
                {
                    direction = new Vector3(-1, 0, 1);
                }
                break;
            case (Directions.UR):
                if (GetType() == typeof(PlayerPiece) || isKing)
                {
                    direction = new Vector3(1, 0, 1);
                }
                break;
            case (Directions.DL):
                if (GetType() == typeof(IAPiece) || isKing)
                {
                    direction = new Vector3(-1, 0, -1);
                }
                break;
            case (Directions.DR):
                if (GetType() == typeof(IAPiece) || isKing)
                {
                    direction = new Vector3(1, 0, -1);
                }
                break;
            default:
                break;
        }

        return direction;
    }

    public bool ValidatePosition(Vector3 newposition)
    {
        print(newposition);
        if (newposition.x < 0 || newposition.x > 7 || newposition.z < 0 || newposition.z > 7)
        {
            return false;
        }

        return true;
    }

    public bool MovePart(Directions d)
    {
        Vector3 direction = GetDirection(d);
        if (direction == Vector3.zero)
        {
            return false;
        }

        Vector3 newposition = transform.position + direction;

        if (newposition.x < 0 && newposition.x > 7 && newposition.z < 0 && newposition.z > 7)
        {
            //////print("Fora do range");
            return false;
        }

        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, 1))
        {

            Object otherType = hit.collider.gameObject.GetComponent<Piece>();
            Object myType = GetComponent<Piece>();

            GameObject ia = hit.collider.gameObject;

            if (otherType.GetType() == myType.GetType())
            {
                print("Amigo");
                return false;
            }

            else
            {
                print("Quero te mata!");

                if (Physics.Raycast(newposition, direction, out RaycastHit hit2, 2))
                {

                    otherType = hit2.collider.gameObject.GetComponent<Piece>();
                    myType = GetComponent<Piece>();

                    if (otherType.GetType() == myType.GetType())
                    {
                        // Amigo
                        print("ops, Amigo");
                        return false;
                    }

                    else
                    {
                        print("Droga, tem alguem ai!");
                        return false;
                    }
                }
                else
                {
                    print("Vou te Matar Mesmo!");

                    newposition = transform.position + direction * 2;


                    if (ValidatePosition(newposition))
                    { 
                        DestroyImmediate(ia);
                        Control.UpdateValues();
                    }

                    else
                    {
                        // Fora da Tela
                        //////print("Fora da Tela");
                        return false;
                    }
                }

            }
        }

        if (ValidatePosition(newposition))
        { 
            //transform.position = newposition;
            float step = 1.0f;
            //agent.destination = Vector3.Lerp(transform.position, newposition, step);
            ////print(transform.position.z);
            ///
            transform.position = newposition;
            StartCoroutine(DELAY());
            return true;
            //return true;
        }
        //////print("Alguma coisa desconhecida..................................");
        return false;

    }

    IEnumerator DELAY()
    {
        yield return new WaitForSeconds(0.2f);
   
        SetKingIfNecessary();
    }

   
    void SetKingIfNecessary()
    {
            //yield return new WaitForSeconds(0.35f);
            //transform.position = newPosition;
            //print("EI CARALHO: " + transform.position.z + (GetType() == typeof(PlayerPiece)));
            if (GetType() == typeof(PlayerPiece) && transform.position.z == 7)
            {
                print("TIME TO BE KING");
                isKing = true;
   
            }

            if (GetType() == typeof(IAPiece) && transform.position.z == 0)
            {
                print("TIME TO BE KING");
                isKing = true;
              //  gameObject.GetComponent<Control>().MoveFinished();
            }
    }
    

    public bool IsThereSomeoneToKill(Directions d)
    {
        Vector3 direction = GetDirection(d);
        if (direction == Vector3.zero)
        {
            print("return 2" + false);
            return false;
        }

        Vector3 newposition = transform.position + direction;
        Vector3 finishPosition = transform.position + (direction * 2);

        if (!ValidatePosition(newposition) || !ValidatePosition(finishPosition))
        {
            print("return 3" + false);
            return false;
        }

        Control.UpdateValues();

        int enemy;

        if (GetType() == typeof(IAPiece))
        {
            enemy = 1;
        }
        else
        {
            enemy = 2;
        }
        print(Control.board);
        if (Control.board[(int)newposition.x, (int)newposition.z] == enemy && Control.board[(int)finishPosition.x, (int)finishPosition.z] == 0)
        {
            print("return 4" + true);
            return true;
        }
        print(transform.position);
        print(newposition);
        print(finishPosition);
        print("return 5" + false);
        return false;
    }


    public bool IsThereSomeoneToDie(Directions d)
    {
        Vector3 direction = GetDirection(d);

        if (direction == Vector3.zero)
        {
            return false;
        }

        Vector3 newposition = transform.position + direction;
        Vector3 finishPosition = transform.position + direction * 2;

        if (!ValidatePosition(newposition) || !ValidatePosition(finishPosition))
        {
            return false;
        }

        Control.UpdateValues();

        if (Control.board[(int)newposition.x, (int)newposition.z] == 2 && Control.board[(int)finishPosition.x, (int)finishPosition.z] == 1)
        {
            return true;
        }

        return false;
    }

    public (bool isPossible, Vector3 newPosition) IsThereAPossibleMove(Directions d)
    {
        Vector3 direction = GetDirection(d);
        if (direction == Vector3.zero)
        {
           return (false, Vector3.zero);
        }

        Vector3 newposition = transform.position + direction;

        if (!ValidatePosition(newposition))
        { 
            return (false, Vector3.zero);
        }

        Control.UpdateValues();
        return ((Control.board[(int)newposition.x, (int)newposition.z] == 0), newposition);
    }

}