using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;

public class UI_Manager : MonoBehaviour
{
    private float level = 1;
    public TextMeshProUGUI levelText;

    [Header("Audio")]
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider soundTrackSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Level"))
        {
            LoadVolume();
            level = PlayerPrefs.GetFloat("Level");
            
        }
        else
        {
            // somehow ini ngereset wind ny jd 0 pas lv 3
            //PlayerPrefs.SetFloat("Level", level);  // Save default level (1) if not set
            //PlayerPrefs.Save();
            SetSoundTrackVolume();
            
        }
        levelText.text = level.ToString();
    }

    private void Update()
    {
        level = PlayerPrefs.GetFloat("Level");
        levelText.text = level.ToString();   
        //Debug.Log(level);
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

    public void SetSoundTrackVolume()
    {
        float vol = soundTrackSlider.value;
        myMixer.SetFloat("SoundTrackVol", Mathf.Log10(vol) * 20);
        PlayerPrefs.SetFloat("BGM", vol);
    }

    private void LoadVolume()
    {
        soundTrackSlider.value = PlayerPrefs.GetFloat("BGM");

        SetSoundTrackVolume();
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
