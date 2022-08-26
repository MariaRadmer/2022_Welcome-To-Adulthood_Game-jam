using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // pickup https://www.youtube.com/watch?v=-V1O5FGQVY8

    [SerializeField]
    float pickupRadius = 0.5f;

    [SerializeField]
    Transform holdSpot;

    [SerializeField]
    LayerMask pickUpMask;

    public Vector3 Direction { get; set; }
    private GameObject itemHolding;

    [SerializeField]
    float babyPutDownTimer =5;

    private float time;

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (itemHolding)
            {
                PutDownItem();

            } 
            else 
            {
                PickUpItem();
            }
                
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            PickUpPoop();

        }

        time += Time.deltaTime;

        if (itemHolding) {
            if (time > babyPutDownTimer && isHoldingBaby())
            {
                PutDownItem();
                time = 0;
            }
        }
    }




    void PutDownItem()
    {
        itemHolding.transform.position = transform.position + Direction;

        itemHolding.transform.parent = null;
        if (itemHolding.GetComponent<Rigidbody2D>())
        {
            itemHolding.GetComponent<Rigidbody2D>().simulated = true;
        }

        if (isHoldingBaby())
        {
            itemHolding.GetComponent<BabyBehavior>().SetPickedUp(false);
        }

        itemHolding = null;
    }


    void PickUpItem()
    {
        Collider2D pickupItem = Physics2D.OverlapCircle(transform.position + Direction, pickupRadius, pickUpMask);

        if (pickupItem)
        {

            itemHolding = pickupItem.gameObject;
            if (itemHolding.CompareTag("shit") && (!itemHolding.GetComponent<PoopSmear>().IsPickupable()))
            {
                itemHolding = null;
                return;
            }

            itemHolding.transform.position = holdSpot.position;
            itemHolding.transform.parent = transform;

            if (itemHolding.GetComponent<Rigidbody2D>())
            {
                itemHolding.GetComponent<Rigidbody2D>().simulated = false;
            }

            if (isHoldingBaby())
            {
                itemHolding.GetComponent<BabyBehavior>().SetPickedUp(true);
            }


        }
    }


    void PickUpPoop()
    {
        if (!itemHolding)
        {
            Collider2D pickupItem = Physics2D.OverlapCircle(transform.position + Direction, pickupRadius, pickUpMask);

            if (pickupItem)
            {
                if (pickupItem.gameObject.CompareTag("shit") && (!pickupItem.gameObject.GetComponent<PoopSmear>().IsPickupable()))
                {
                    GameManager.Instance.GetRoomba().GetComponent<RoombaBehavior>().isCleanSwitchToWander();

                    Destroy(pickupItem.gameObject);
                    GameManager.Instance.DecreasePoopCounter();
                }

            }
        }
    }

    bool isHoldingBaby()
    {
        return itemHolding.CompareTag("baby") ? true: false; 
    }


}
