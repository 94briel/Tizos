using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleGIF : MonoBehaviour
{
    public Sprite[] sprites;
    public Image image;
    public float time = .2f;
    public Coroutine coroutineGIF;
    void Start()
    {
        coroutineGIF= StartCoroutine(MakeGif());
    }
    void OnDisable()
    {
        if(coroutineGIF != null) StopCoroutine(coroutineGIF);
    }
    
    public IEnumerator MakeGif()
    {
        while(true){
            yield return new WaitForSeconds(time);
            image.sprite = sprites[Random.Range(0,sprites.Length)];
        }
    }
}
