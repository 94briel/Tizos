using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class HandMovement : MonoBehaviour
{

    public float speed = 1f, originalPosition;

    public Vector3 initialPosition;
    public float movement = .4f;
    private float widthSize;
    public Vector3 handOriginalPosition;
    public List<Tazo> tazosAliados, tazosEnemigos;
    
    
    private Coroutine coroutineMove, coroutineAnimation;

    private void Awake()
    {
    }

    private void Start()
    {
        if (GameController.singleton.score == 0)
        {
            speed = 0.8f;
            movement = .4f;
        }
        speed = Mathf.Clamp(speed + GameController.singleton.handSpeed, 0.1f, 6f);
        movement = Mathf.Clamp(movement + GameController.singleton.handInterval, 0.1f, 1.5f);
        GameController.singleton.handInterval += Random.Range(0.05f, 0.1f);
        GameController.singleton.handSpeed += Random.Range(0.1f, 0.25f);
        StartCoroutine(InitialMovement(-1.4f, .5f));
        widthSize = GetWidthSize(); 
        //movement = Mathf.Clamp(Random.Range(movement, movement), -widthSize, widthSize*2);
    }

    // Update is called once per frame
    private void OnEnable()
    {
        coroutineAnimation= StartCoroutine(HandAnimation());
        EventController.singleton.playTazo += GiveDistance;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        StopCoroutine(coroutineAnimation);
        EventController.singleton.playTazo -= GiveDistance;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetTazos();
        CombatManager.singleton.estadoC = EstadoC.POSITION;
        CombatManager.singleton.hitbar = LoadedInstances.instances.hitbar;
        CombatManager.singleton.wasHit = false;
        LoadedInstances.instances.score.text = GameController.singleton.score.ToString();
    }
    public void SetTazos(){
        int allyIndex = Random.Range(0, tazosAliados.Count);
        int enemyIndex = Random.Range(0, tazosEnemigos.Count);
        GameObject ally = Instantiate(tazosAliados[allyIndex].prefab, new Vector3(-0.0481047630310059f,0.562133364379405978f,3.0069098472595217f), 
            Quaternion.Euler(new Vector3(293.942322f,186.749359f,178.529694f)), transform);
        ally.GetComponent<Renderer>().material = tazosAliados[allyIndex].material;
        CombatManager.singleton.ally = ally;
        GameObject enemy = Instantiate(tazosEnemigos[enemyIndex].prefab, new Vector3(-0.27027618885040285f,0.17213329672813416f,3.0539097785949709f),
            Quaternion.Euler(296.847992f,179.999588f,295.000488f));
        enemy.GetComponent<Renderer>().material = tazosAliados[enemyIndex].material;
        CombatManager.singleton.enemy = enemy;
    }
    private float CalculateXDistance(Transform a, Transform b)
    {
        return Mathf.Abs(a.position.x - b.position.x);
    }

    private float GetWidthSize()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, transform.position.z)).x;
    }

    private float GiveDistance()
    {
        EventController.singleton.playTazo -= GiveDistance;
        StopCoroutine(coroutineMove);
        LeanTween.moveY(gameObject, transform.position.y + .5f, .8f).setEaseLinear().setOnComplete(
            (o )=> LeanTween.rotateZ(gameObject, transform.rotation.z+20f, .8f).setEaseLinear());
        StopCoroutine(coroutineAnimation);
        return CalculateXDistance(CombatManager.singleton.ally.transform, CombatManager.singleton.enemy.transform) ;
    }

    private IEnumerator Move()  
    {
        while (true)
        {
            yield return new WaitForSeconds(1 / 60f);
            transform.position = initialPosition + Mathf.Sin(Time.time * speed) * new Vector3(movement, 0f, 0f);
            initialPosition = initialPosition + new Vector3(Random.Range(-.001f, .001f), 0, 0);
        }
    }
    private IEnumerator HandAnimation()  
    {
        while (true)
        {
            AnimationController.singleton.HandShake();
            yield return new WaitForSeconds(AnimationController.singleton.handTime+1f);
        }
    }
    private IEnumerator InitialMovement(float a, float time)  
    {
        //AnimationController.singleton.MoveHand(gameObject, a, time);
        yield return new WaitForSeconds(0f);
        initialPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        coroutineMove = StartCoroutine(Move());
        
    }
}