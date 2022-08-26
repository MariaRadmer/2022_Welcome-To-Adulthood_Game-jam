using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopSmear : MonoBehaviour
{

    [SerializeField]
    Sprite poopSmear;

    bool pickupable = true; 

    private void OnTriggerEnter2D(Collider2D other){
        
        if (other.CompareTag("roomba")) {
            this.GetComponent<SpriteRenderer>().sprite = poopSmear;
            this.GetComponent<Rigidbody2D>().isKinematic = true;
            pickupable = false; 
        }


        if (gameObject.tag == "shit")
        {
            if (other.gameObject.CompareTag("trash"))
            {
                Destroy(gameObject);
                GameManager.Instance.DecreasePoopCounter();
            }
        }

    }

    public bool IsPickupable() { return pickupable; }

}