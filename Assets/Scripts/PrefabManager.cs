using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public static PrefabManager Instance;

    public Carriage Carriage;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
