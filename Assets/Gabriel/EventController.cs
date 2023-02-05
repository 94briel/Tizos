using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = Unity.Mathematics.Random;

public class EventController : MonoBehaviour
{
    public static EventController singleton;
    public estadoCombate eCombate;
    public event Func<float> playTazo;
    public event Func<bool> moveSlide;
    
    public List<Tazo> tazosAliados, tazosEnemigos;
    public event UnityAction<Scene, LoadSceneMode> reloadScene;
    
    
    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        CombatEvents();
    }

    private void OnDestroy()
    {
        
    }

    public static event Action HitTazo;
    private void CombatEvents()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        switch (CombatManager.singleton.estadoC)
        {
            case 0:
                CombatManager.singleton.hitRatio = playTazo?.Invoke() ?? -1;
                StartCoroutine(CombatManager.singleton.CambiarEstado(EstadoC.ACCURACY));
                CombatManager.singleton.hitbar.SetActive(true);
                AnimationController.singleton.ScaleIn(CombatManager.singleton.hitbar, .2f);
                break;
            case (EstadoC)1:
                StartCoroutine(CombatManager.singleton.CambiarEstado(EstadoC.HIT));
                CombatManager.singleton.wasHit = moveSlide?.Invoke() ?? false;
                
                HitTazo?.Invoke();
                StartCoroutine(GameController.singleton.BringNewTazo(2f));
                break;
            case (EstadoC)2:
                break;
        }
    }
}

public enum estadoCombate
{
    POSICIONAMIENTO = 0,
    POTENCIACION = 1,
    LANZAMIENTO = 2,
    DISPARO = 3
}