using System.Collections;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager singleton;
    public GameObject ally, enemy;
    public float hitRatio, hitbarSpeed, hitbarTolerance;
    public GameObject hitbar;
    public EstadoC estadoC;
    public bool wasHit;

    private void Awake()
    {
        hitbar = LoadedInstances.instances.hitbar;
        estadoC = EstadoC.POSITION;
        if (singleton == null) singleton = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);


        hitbar.SetActive(false);
    }

    private void Start()
    {
        
    }

    public IEnumerator CambiarEstado(EstadoC e)
    {
        yield return new WaitForEndOfFrame();
        singleton.estadoC = e;
    }
}

public enum EstadoC
{
    POSITION = 0,
    ACCURACY = 1,
    HIT = 2
}