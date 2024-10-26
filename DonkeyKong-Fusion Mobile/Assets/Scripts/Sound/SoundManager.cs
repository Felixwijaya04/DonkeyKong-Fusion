using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public Sound[] BGM;

    public AudioSource BGMSource;
    private int currSong;

    private void Awake()
    {
        if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); } else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Debug.Log("start CLIP");
        currSong = PlayerPrefs.GetInt("Song");
        PlayBgm(currSong);
    }
    public void PlayBgm(int index)
    {
        if(index > BGM.Length)
        {
            index = 0;
            PlayerPrefs.SetInt("Song", index);
        }
        BGMSource.clip = BGM[index].clip;
        BGMSource.Play();
    }

    private int FindIndexByName(string name, Sound[] clip)
    {
        Debug.Log("Mencari idx");
        for (int i = 0; i < clip.Length; i++)
        {
            if (clip[i].name == name)
            {
                return i;
            }
        }
        return -1;
    }
}