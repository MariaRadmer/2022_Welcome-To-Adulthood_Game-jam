using UnityEngine;
using System.Collections;
using Pathfinding;

//source: https://arongranberg.com/astar/documentation/dev_4_3_8_84e2f938/old/wander.php#method2_2
public class WanderingDestinationSetter : MonoBehaviour {
    [SerializeField]
    public float radius = 20;

    IAstarAI ai;
    GraphNode randomNode;
    GridGraph grid;

    float timeForNewDestination = 5; 

    void Start () {
        ai = GetComponent<IAstarAI>();
        //For grid graphs
        grid = AstarPath.active.data.gridGraph;
        randomNode = grid.nodes[Random.Range(0, grid.nodes.Length)];
    }

    Vector3 PickRandomNode () {
        randomNode = grid.nodes[Random.Range(0, grid.nodes.Length)];
        return (Vector3)randomNode.position;
    }

    void Update () {

        if(timeForNewDestination < 0) {
            ai.destination = PickRandomNode();
            ai.SearchPath();
            Debug.DrawLine(Vector2.zero, new Vector2(ai.destination.x, ai.destination.y), Color.white, 2.5f);
            timeForNewDestination = 5;
        } else {
            timeForNewDestination -= Time.deltaTime;
        }
        // Update the destination of the AI if
        // the AI is not already calculating a path and
        // the ai has reached the end of the path or it has no path at all
        if (!ai.pathPending && (ai.reachedDestination || !ai.hasPath)) {
            ai.destination = PickRandomNode();
            ai.SearchPath();
        }
    }

    public Vector2 GetDirection()
    {
        return ( new Vector2(ai.destination.x, ai.destination.y) - new Vector2(transform.position.x, transform.position.y));
    }
}
