using UnityEngine;

public class Star : MonoBehaviour
{
    private float stopwatch;

    private void Start()
    {
    }

    private void Update()
    {
        Invoke("Despanw", 5);
    }

    public void Despanw()
    {
        Destroy(gameObject);
    }
}