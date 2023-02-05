using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CourtainsController : MonoBehaviour
{
    public GameObject left, right, cortinas;

    private void Start()
    {
        StartCoroutine(Cortinas());
    }

    IEnumerator Cortinas()
    {
        if (GameController.singleton.score == 0)
        {

            cortinas.SetActive(true);
            LeanTween.moveLocalX(left, -9500F, .7f).setEaseInOutBounce();
            LeanTween.moveLocalX(right, 900F, .95f).setEaseInOutBounce();
            yield return new WaitForSeconds(1f);
            cortinas.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
