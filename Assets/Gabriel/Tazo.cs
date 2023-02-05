using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "Tazo", menuName = "ScriptableObjects/Tazo")]
public class Tazo : ScriptableObject
{
    public int ataque, defensa, vida;
    public type elemento;
    public int id;
    public string nombre;
    public GameObject prefab;
    public Material material;
    

    public float Effective()
    {
        switch (CombatManager.singleton.enemy.GetComponent<Tazo>().elemento)
        {
            case type.fire:
                return contraFuego();
                break;
            case type.water:
                return contraAgua();
                break;
            case type.grass:
                return contraPlanta();
        }

        return -1f;
    }

    public float contraFuego()
    {
        switch (elemento)
        {
            case type.fire:
                return 1;
                break;
            case type.water:
                return 2;
                break;
            case type.grass:
                return 0.5f;
        }
        return -1f;
    }

    public float contraAgua()
    {
        switch (elemento)
        {
            case type.fire:
                return 0.5f;

            case type.water:
                return 1;

            case type.grass:
                return 2;
        }
        return -1f;
    }
    public float contraPlanta()
    {
        switch (elemento)
        {
            case type.fire:
                return 2;
                break;
            case type.water:
                return 1;
                break;
            case type.grass:
                return 0.5f;
        }
        return -1f;
    }
}

public enum type
{
    fire,
    water,
    grass
}