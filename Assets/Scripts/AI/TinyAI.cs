using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class TinyAI : MonoBehaviour
{
    public Transform LeftShop, RightShop, Middle;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    enum NavState{Left, Random, Right}
    NavState state;
    int currentWayPoint = 0;
    Path path;
    public Seeker seeker;
    Rigidbody2D rb;
    bool reachedEndOfPath;
    Transform target;
    
    void Start()
    {
        //seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        state = NavState.Random;
        
        target = LeftShop;
        Random.InitState(System.DateTime.Now.Millisecond);
        
        ChangeToMIddle();
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
            //changeTarget(Random.Range(0,2));
            reachedEndOfPath = true;
            return;
        }
        
        if(rb.velocity.x > 0.01f){
            transform.localScale = new Vector3(-1, 1, 1);
        } else if(rb.velocity.x < -0.01f){
            transform.localScale = new Vector3(1, 1, 1);
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
    public void changeTarget(int index){

            
        if(index == 0){
            target = LeftShop;
        } else {
            target = RightShop;
        }

       
       Debug.Log(gameObject.name + "has changed target to" + index);
       currentWayPoint = 0;
    }
    void changeSpeed(){
        Random.InitState(System.DateTime.Now.Millisecond);
        speed = Random.Range(100, 300);
        Debug.Log(gameObject.name + "has changed speed");
    }

    public void ChangerRandomTarget(){
        int x = Random.Range(-10, 10);
        int y = Random.Range(-5, 5);

        target.position = new Vector3(x, y, 0);
    }

    public void ChangeToMIddle(){
        target = Middle;
        currentWayPoint = 0;
    }
}
