using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DogBehavior : MonoBehaviour
{
    enum State
    {
        WANDER, SHIT, ASLEEP
    }

    TimeManager timeManager;
    State state = State.ASLEEP;

    float timeUntilNextPoop = 2;
    float timeUntilWalking = 3;

    [SerializeField]
    float walkSpeed = 2; 

    [SerializeField]
    float runSpeed = 5; 

    [SerializeField]
    float earliestTimeBeforeNextPoop = 5;

    [SerializeField]
    float latestTimeBeforeNextPoop = 15;

    [SerializeField]
    float earliestTimeBeforeWalking = 3;

    [SerializeField]
    float latestTimeBeforeWalking = 10;

    [SerializeField]
    GameObject shit;

    Animator dogAnimator;

    void Start()
    {
        timeManager = TimeManager.Instance;
        dogAnimator = gameObject.GetComponent<Animator>();

    }

    void Update()
    {
        Vector2 direction = GetComponent<WanderingDestinationSetter>().GetDirection(); 
        GetComponent<SpriteRenderer>().flipX = direction.x < 0;

        UpdateState();
        UpdateBehavior();
    }

    private void UpdateState()
    {
        if (timeManager.CompareTimeInHoursHasPassed(timeManager.GetDogAwakeTimeHours()))
        {
            dogAnimator.SetBool("IsSleeping", false);
            state = State.WANDER;
        }

        if(state == State.WANDER) {
            
            if (timeUntilNextPoop < 0) 
            {
                timeUntilNextPoop = Random.Range(earliestTimeBeforeNextPoop,latestTimeBeforeNextPoop);
                state = State.SHIT;     
            } 
            else 
            {
                timeUntilNextPoop -= Time.deltaTime;
            }

            if (dogAnimator.GetBool("IsRunning") && timeUntilWalking == 0) 
            {
                timeUntilWalking = Random.Range(earliestTimeBeforeWalking,latestTimeBeforeWalking);
            }
            else if (timeUntilWalking < 0) 
            {
                GetComponent<AIPath>().maxSpeed = walkSpeed; 
                dogAnimator.SetBool("IsRunning", false);
                timeUntilWalking = 0;
                
            }
            else if (timeUntilWalking > 0 && dogAnimator.GetBool("IsRunning"))
            {
                timeUntilWalking -= Time.deltaTime;
            }
        } 
    }


    private void UpdateBehavior()
    {
        switch (state)
        {
            case State.ASLEEP:
                turnOnOffPathfinding(false);
                break;
            case State.SHIT:
                timeToShit();
                break;
            case State.WANDER:
                turnOnOffPathfinding(true);
                break;
            default: break;
        }
    }

    private void timeToShit() {
        Instantiate(shit, transform.position, Quaternion.identity);
        GameManager.Instance.IncreasePoopCounter();
        dogAnimator.SetTrigger("Shit");
        GetComponent<AIPath>().maxSpeed = runSpeed; 
        dogAnimator.SetBool("IsRunning", true);
        state = State.WANDER;
    }

    private void turnOnOffPathfinding(bool b)
    {
        this.GetComponent<AIPath>().enabled = b;
        this.GetComponent<WanderingDestinationSetter>().enabled = b;
    }

}

