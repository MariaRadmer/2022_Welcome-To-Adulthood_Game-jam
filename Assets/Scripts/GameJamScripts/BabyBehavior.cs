using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class BabyBehavior : MonoBehaviour
{

    [SerializeField]
    int TimeInCrib = 5;

    [SerializeField]
    Transform holdSpot;

    [SerializeField]
    Transform escapeSpot;

    enum State
    {
        WANDER, SEEK, STOP, CRIB
    }

    TimeManager timeManager;
    State state = State.WANDER;
    bool isPickedUp = false;
    bool isInCrib = false;

    float time=0;


    void Start()
    {
        timeManager = TimeManager.Instance;
    }

    void Update()
    {
        if (isInCrib)
        {
            time += Time.deltaTime;
            
        }

        
        

        UpdateState();
        UpdateBehavior();

        
    }

    private void UpdateState()
    {

        if (isPickedUp)
        {
            state = State.STOP;
            return;
        } else if(time > TimeInCrib)
        {
            OutOfCrib();
            
            time = 0;
            isInCrib = false;

            print("time in crib done");

            state = State.WANDER;
            return;

        }
        else if (timeManager.CompareTimeInHoursHasPassed(timeManager.GetRoombaAwakeTimeHours()) && (!isPickedUp))
        {
            state = State.SEEK;
            return;
        }
        else if (!isPickedUp)
        {
            state = State.WANDER;
            return;
        } 

    }


    private void UpdateBehavior()
    {
        switch (state)
        {
            case State.STOP:
                turnOnOffAIPathfinding(false);
                

                break;
            case State.WANDER:
                turnOnOffDestinationSetter(false);

                turnOnOffAIPathfinding(true);
                turnOnOffPathfindingWander(true);
                break;
            case State.SEEK:
                turnOnOffPathfindingWander(false);
                turnOnOffAIPathfinding(true);
                turnOnOffDestinationSetter(true);
                break;
            case State.CRIB:
                turnOnOffAIPathfinding(false);
                break;
            default: break;


        }
    }


    private void turnOnOffAIPathfinding(bool b)
    {
        this.GetComponent<AIPath>().enabled = b;
        
    }

    private void turnOnOffDestinationSetter(bool b)
    {
        this.GetComponent<AIDestinationSetter>().enabled = b;
    }

    private void turnOnOffPathfindingWander(bool b){ this.GetComponent<WanderingDestinationSetter>().enabled = b;}

    public void SetPickedUp(bool pickup)
    {
        isPickedUp = pickup;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("roomba")) {
            GameManager.Instance.Lose();
        }

        if (collision.CompareTag("crib"))
        {
            state = State.CRIB;
            print("time in crib begin");
            PlaceInCrib();
        }
    }

    


    private void PlaceInCrib()
    {
        isInCrib = true;

        gameObject.transform.position = holdSpot.position;
        if (gameObject.GetComponent<Rigidbody2D>())
        {
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
        }
    }

    private void OutOfCrib()
    {
        gameObject.transform.position = escapeSpot.position;
        if (gameObject.GetComponent<Rigidbody2D>())
        {
            gameObject.GetComponent<Rigidbody2D>().simulated = true;
        }
    }

}
