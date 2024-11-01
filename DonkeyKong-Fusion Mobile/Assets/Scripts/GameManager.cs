using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool alive;
    private float level = 1;
    private float previousLevel;

    private bool hasIncreasedSpawner;
    private bool hasIncreasedWind;

    public GameObject wind;
    private AreaEffector2D effector;
    private int currSong = 0;

    private void Start()
    {
        previousLevel = level;
        DontDestroyOnLoad(gameObject);
        effector = wind.gameObject.GetComponent<AreaEffector2D>();
        hasIncreasedWind = false;
        hasIncreasedSpawner = false;

        if (PlayerPrefs.HasKey("Level"))
        {
            // load level data if exist
            level = PlayerPrefs.GetFloat("Level");
            currSong = PlayerPrefs.GetInt("Song");
            effector.forceMagnitude = PlayerPrefs.GetFloat("windSpeed");
        }
        else
        {
            PlayerPrefs.SetFloat("Level", level);
            PlayerPrefs.SetInt("Song", currSong);
            PlayerPrefs.SetFloat("windSpeed", effector.forceMagnitude);
        }
    }

    private void Update()
    {
        if(level != previousLevel)
        {
            Debug.Log(level);
            if (level % 2 == 0)
            {
                // Increase barrel spawn rate
                Spawner spawner = FindObjectOfType<Spawner>();
                if (spawner != null && spawner.minTime > 0f && !hasIncreasedSpawner)
                {
                    hasIncreasedSpawner = true;
                    spawner.minTime -= 0.15f;
                    PlayerPrefs.SetFloat("minTime", spawner.minTime);
                    Debug.Log("Barrel Spawn Increased: " + spawner.minTime);
                }
            }
            if (level % 3 == 0)
            {
                // activate wind
                ActivateWind();
                Debug.Log("wind activated");
            }
            else
            {
                wind.SetActive(false);
                hasIncreasedWind = false;
            }
            previousLevel = level;
        } 
    }

    public void LevelComplete()
    {
        // save the level
        level++;
        currSong++;
        PlayerPrefs.SetFloat("Level", level);
        PlayerPrefs.SetInt("Song", currSong);

        hasIncreasedWind = false;
        hasIncreasedSpawner = false;
        SceneManager.LoadScene("GameScene");
    }

    public void LevelFailed()
    {
        alive = false;

        if (alive == false)
        {
            // reload curr level
            //PlayerPrefs.DeleteKey("Level");
            //PlayerPrefs.DeleteKey("Song");
            //PlayerPrefs.DeleteKey("windSpeed");
            //PlayerPrefs.DeleteKey("minTime");
            //PlayerPrefs.DeleteKey("BGM");
            level = 1;
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            LevelComplete();
        }
    }

    public void ActivateWind()
    {
        if(effector.forceMagnitude < 66 && !hasIncreasedWind)
        {
            effector.forceMagnitude += 1.5f;
            PlayerPrefs.SetFloat("windSpeed", effector.forceMagnitude);
            hasIncreasedWind = true;
        }
        wind.SetActive(true);
    }
}
