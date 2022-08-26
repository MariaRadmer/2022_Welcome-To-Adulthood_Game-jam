using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wander : MonoBehaviour
{
    [SerializeField]
    float speed; 

    [SerializeField]
    float range; 

    [SerializeField]
    float maxDistance;

    [SerializeField]
    GameObject upperRight;

    [SerializeField]
    GameObject lowerLeft;

    [SerializeField]
    float buffer = 5;

    Vector2 target;


    void Start()
    {

        SetNewRandomTarget();
    }

    
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, (speed * Time.deltaTime) );
        if (Vector2.Distance(transform.position, target) < range) {
            SetNewRandomTarget();
        }

        Debug.DrawLine(transform.position, target, Color.white, 2.5f);
    }



    void SetNewRandomTarget() 
    {
        target = new Vector2(Random.Range(lowerLeft.transform.position.x, upperRight.transform.position.x), Random.Range(lowerLeft.transform.position.y, upperRight.transform.position.x));
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        target = GetOppositeDirection();
    }

    public Vector2 GetOppositeDirection()
    {


        Vector2 vector = ( new Vector2 (transform.position.y *-1,transform.position.x));

        
        return vector;
    }

    public Vector2 GetDirection()
    {
        return (target - new Vector2(transform.position.x, transform.position.y ));
        
    }

    public void ChangeSpeed(float newSpeed) { speed = newSpeed; }

    public float GetSpeed() { return speed; }


}
