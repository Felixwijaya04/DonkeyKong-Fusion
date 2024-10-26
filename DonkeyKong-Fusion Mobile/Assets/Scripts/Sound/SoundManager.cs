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

    private void Awake()
    {
        if (instance == null) { instance = this; }
    }
    void Start()
    {
        Debug.Log("start CLIP");
        PlayBgm("Walk BGM");
    }
    public void PlayBgm(string name)
    {
        int index = FindIndexByName(name, BGM);
        if (index != -1)
        {
            BGMSource.clip = BGM[index].clip;
            BGMSource.Play();
        }
        else
        {
            Debug.Log("No BGM name is found");
        }
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