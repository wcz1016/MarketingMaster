using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public Canvas gameCanvas;

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown){
            gameObject.SetActive(false);
            gameCanvas.GetComponent<GameControl>().enabled = true;
            gameCanvas.GetComponent<UIControl>().enabled = true;
        }
    }
}
