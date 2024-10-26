using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    private float level;
    public TextMeshProUGUI levelText;
    private void Start()
    {
        level = PlayerPrefs.GetFloat("Level");
        levelText.text = level.ToString();
    }

    private void Update()
    {
        level = PlayerPrefs.GetFloat("Level");
        levelText.text = level.ToString();   
        Debug.Log(level);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
