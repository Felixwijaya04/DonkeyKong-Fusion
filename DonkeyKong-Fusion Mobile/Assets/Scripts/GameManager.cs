using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool alive;
    private float level = 1;
    private int windSpeed;
    private bool hasIncreasedWInd;

    public GameObject wind;
    private AreaEffector2D effector;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.HasKey("Level"))
        {
            // load level data if exist
            level = PlayerPrefs.GetFloat("Level");
        }
        else
        {
            PlayerPrefs.SetFloat("Level", level);
        }

        effector = wind.gameObject.GetComponent<AreaEffector2D>();
        hasIncreasedWInd = false;
    }

    private void Update()
    {
        if (level % 2 == 0)
        {
            // Increase barrel spawn rate
            Spawner spawner = FindObjectOfType<Spawner>();
            if (spawner != null && spawner.minTime < spawner.maxTime - 0.5f)
            {
                spawner.minTime += 0.1f;
                Debug.Log("Barrel Spawn Increased: " + spawner.minTime);
            }
        }
        if (level % 3 == 0)
        {
            // activate wind
            ActivateWind();
        }
        else
        {
            wind.SetActive(false);
            hasIncreasedWInd = false;
        }
        
    }

    public void LevelComplete()
    {
        // save the level
        level++;
        PlayerPrefs.SetFloat("Level", level);
        hasIncreasedWInd = false;
        SceneManager.LoadScene("GameScene");
    }

    public void LevelFailed()
    {
        alive = false;

        if (alive == false)
        {
            // reload curr level
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            LevelComplete();
        }
    }

    public void ActivateWind()
    {
        if(effector.forceMagnitude < 55 && !hasIncreasedWInd)
        {
            effector.forceMagnitude += 1.5f;
            hasIncreasedWInd = true;
        }
        wind.SetActive(true);
    }
}
