using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool alive;
    private float level = 3;
    private int windSpeed;

    public GameObject wind;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.HasKey("Level"))
        {
            // load level data if exist
            level = PlayerPrefs.GetFloat("Level");   
        }
    }

    public void LevelComplete()
    {
        // save the level
        level++;
        PlayerPrefs.SetFloat("Level", level);
        if (level % 2 == 0)
        {
            // Increase barrel spawn rate
            Spawner spawner = FindObjectOfType<Spawner>();
            if (spawner != null && spawner.minTime < spawner.maxTime - 0.5f)
            {
                spawner.minTime += 0.1f;
                Debug.Log("Barrel Spawn Increased: " + spawner.minTime);
            }
        } else if(level % 3 == 0)
        {
            // activate wind
            ActivateWind();
        }

        // reload scene with the new level
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
        wind.SetActive(true);
    }
}
