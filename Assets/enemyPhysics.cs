using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class enemyPhysics : MonoBehaviour
{
    public Rigidbody rb = new Rigidbody();
    public Vector3 fuerza,torque;
    void Awake()
    {

    }

    private void Start()
    {
        CombatManager.singleton.enemy = gameObject;
    }

    void OnCollisionEnter(Collision other)
    {
        rb = GetComponent<Rigidbody>();
        if (!other.gameObject.CompareTag("PlayerTazo")) return;
        if (CombatManager.singleton.wasHit)
        {
            rb.AddForce(fuerza);
            rb.AddTorque(torque);
        } else
        {
            rb.AddForce(new Vector3(0f,Random.Range(-51,10f),0f));
            rb.AddTorque(new Vector3(Random.Range(1,10f),0f,Random.Range(1,10f)));
        }
        rb.AddForce(new Vector3(0f,0,-1000f));
        other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0f,0,10000f));
    }
        
    // Update is called once per frame
    void Update()
    {
        
    }
}
