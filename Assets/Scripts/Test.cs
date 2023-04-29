using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Test : MonoBehaviour
{

    public static int Counter = 0;

    void Start()
    {
        Counter++;
        Debug.Log(Counter.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
