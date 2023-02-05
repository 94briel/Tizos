using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadedInstances : MonoBehaviour
{
    public static LoadedInstances instances;
    public TextMeshProUGUI score;
    public GameObject hitbar, ally, enemy;
    
    void Awake()
    {
        if (instances== null) instances = this;
        else Destroy(gameObject);
        instances = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
