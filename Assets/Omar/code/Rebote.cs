using UnityEngine;

public class Rebote : MonoBehaviour
{
    public float speed;

    private void Start()
    {
    }

    private void Update()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "target") GameManager.instance.bonus = true;
    }
}