using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class TinyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    int currentWayPoint = 0;
    Path path;
    public Seeker seeker;
    Rigidbody2D rb;
    bool reachedEndOfPath;

    // Start is called before the first frame update
    void Start()
    {
        //seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        seeker.StartPath(rb.position, target.position, onPathComplete);
        InvokeRepeating("UpdatingPath", 0f, 0.5f);
    }
    void UpdatingPath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, onPathComplete);
        }
    }
    void onPathComplete(Path p){
        if(!p.error){
            path = p;
            currentWayPoint = 0;
        }

    }
        // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
            return;
        if(currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if(distance < nextWaypointDistance)
        {
            currentWayPoint++;
        }
    }
}
