using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;
    public static GameController singleton;
    public float handSpeed, handInterval;
    public float tickSpeed, tolerance;
    void Awake()
    {
        if (singleton == null)
            singleton = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {
        scoreText = LoadedInstances.instances.score;

    }

    public IEnumerator BringNewTazo(float secondsToWait)
    {
        if(CombatManager.singleton.wasHit)
            AddScore();
        else
        {
            score = 0;
            handSpeed = 0;
            handInterval = 0;
            tickSpeed = 0;
            tolerance = 0;
            CombatManager.singleton.hitbarSpeed = .8f;
            CombatManager.singleton.hitbarTolerance = .400f;
        }
        yield return new WaitForSeconds(secondsToWait);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return null;

    }

    public void AddScore()
    {
        scoreText = LoadedInstances.instances.score;
        LeanTween.moveY(scoreText.gameObject, transform.position.y + .2f, .2f).setEasePunch();
        score += 1;
        scoreText.text = score.ToString();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
