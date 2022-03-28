using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleGeneration : MonoBehaviour
{
    public GameObject PeoplePrefab;
    public int PeopleCount = 1;

    int winningCount, losingCount;
    public static PeopleGeneration instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {
        winningCount = PeopleCount * 2 / 3;
        losingCount = PeopleCount - winningCount;
        for(int i = 0; i < PeopleCount; i++){
            int x = Random.Range(-10, 10);
            int y = Random.Range(-5, 5);
            GameObject instance = Instantiate(PeoplePrefab, new Vector3(x, y, 0), PeoplePrefab.transform.rotation);
            instance.transform.SetParent(gameObject.transform);
            //instance.GetComponent<TinyAI>().index = i;
        }
    }

    public void changeTarget(int index){
        Debug.Log(gameObject.name + "has got the order");
        for(int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).GetComponent<TinyAI>().changeTarget(index);
        }
    }

    public void changeRandomTarget(){
        for(int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).GetComponent<TinyAI>().ChangerRandomTarget();
        }
    }
    // Update is called once per frame
    public void changeToMIddle(){
        for(int i = 0; i < transform.childCount; i++){
            transform.GetChild(i).GetComponent<TinyAI>().ChangeToMIddle();
        }
    }
}
