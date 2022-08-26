using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RoombaBehavior : MonoBehaviour
{

    [SerializeField]
    float pickupRadius = 0.5f;

    [SerializeField]
    LayerMask pickUpMask;

    enum State
    {
        WANDER, SHIT, ASLEEP
    }

    TimeManager timeManager;
    State state = State.ASLEEP;
    Animator roombaAnimator;
        
    
    bool hasCollidedwithShit=false;

    // Start is called before the first frame update
    void Start()
    {
        timeManager = TimeManager.Instance;
        roombaAnimator = gameObject.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = GetComponent<WanderingDestinationSetter>().GetDirection(); 
        GetComponent<SpriteRenderer>().flipX = direction.x < 0;
        
        ifCollidesWithShit();
        UpdateState();
        UpdateBehavior();

    }

    private void UpdateState()
    {
        

        if (hasCollidedwithShit){
            state = State.SHIT;
            roombaAnimator.SetBool("IsPooped", true);

            
        }else if (timeManager.CompareTimeInHoursHasPassed(timeManager.GetRoombaAwakeTimeHours()))
        {
            state = State.WANDER;
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
                turnOnOffPathfinding(false);
                break;
            case State.WANDER:
                turnOnOffPathfinding(true);
                break;
            default: break;


        }
    }

    private void ifCollidesWithShit()
    {
        
        Collider2D collision = Physics2D.OverlapCircle(transform.position, pickupRadius, pickUpMask);
        if (collision)
        {
            hasCollidedwithShit = true;
        }
    }

    private void turnOnOffPathfinding(bool b)
    {

        this.GetComponent<AIPath>().enabled = b;

        this.GetComponent<WanderingDestinationSetter>().enabled = b;


    }



    public void SwitchToSHIT()
    {
        state = State.SHIT;
    }

    public void isCleanSwitchToWander()
    {
        hasCollidedwithShit = false;
        roombaAnimator.SetBool("IsPooped", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       

        if (other.gameObject.CompareTag("shit"))
        {
            hasCollidedwithShit = true;
        }


        /*
        if (other.gameObject.CompareTag("Player"))
        {
                hasCollidedwithShit = false;
                roombaAnimator.SetBool("IsPooped", false);
            
        }*/
    }




}
