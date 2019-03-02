using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugAccel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Text t = GetComponent<Text>();
        
        
        //Vector3 acc = Input.acceleration;
        //t.text = "\n X: " + acc.x + " | Y: " + acc.y + " | Z: " + acc.z;
        MicManager m = GameObject.FindGameObjectWithTag("mic").GetComponent<MicManager>();
        //t.text = "LEVEL: " + m.LevelMax*100000;
        
        
    }
}
