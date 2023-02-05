using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TazoPhysics : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 force, torque;
    void Awake()
    {
        CombatManager.singleton.ally = gameObject;
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        
    }
    private void OnEnable()
    {
        EventController.HitTazo += MakeForce;
    }

    private void OnDisable()
    {
        EventController.HitTazo -= MakeForce;
    }

    // Update is called once per frame
    void MakeForce()
    {
        if (CombatManager.singleton.wasHit)
        {
            force *= 1.5f;
            torque *= 1.5f;
        }
        rb.AddForce(new Vector3(0f, Random.Range(-400f, -900f), Random.Range(0,50f)));
        rb.AddTorque(new Vector3(Random.Range(150f,500f), Random.Range(0f, 50f), Random.Range(300f,800f)));
    }
}
