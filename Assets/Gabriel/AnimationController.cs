using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public static AnimationController singleton;
    public GameObject pulgar, indice, mano, tazoAliado, jugador, manoCompleta;
    public float handTime = 0.4f;
    void Awake()
    {
        if (singleton == null)
            singleton = this;
        else
        {
            Destroy(gameObject);
        }
    }

    public void HandShake()
    {
        LeanTween.rotateZ(pulgar, Random.Range(-20f, 25f), handTime).setEasePunch();
        LeanTween.rotateZ(indice, Random.Range(-30f, 15f), handTime).setEasePunch();
        LeanTween.rotateZ(mano, Random.Range(-35f, 30f), handTime).setEasePunch().setDelay(handTime*1.5f);
    }
    public void MoveHand(GameObject go, float destination, float time)
    {
        LeanTween.moveX(go, destination, time).setEaseLinear();
    }

    public void ScaleIn(GameObject go, float t)
    {
        go.transform.localScale = Vector3.zero;
        LeanTween.scale(go, Vector3.one, t).setEaseInOutElastic();
    }
    public void ScaleOut(GameObject go, float t)
    {
        LeanTween.scale(go, Vector3.zero, t).setEaseInOutElastic();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
