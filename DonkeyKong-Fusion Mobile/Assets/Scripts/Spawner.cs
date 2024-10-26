using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    [SerializeField] public float minTime;
    [SerializeField] public float maxTime;

    private void Start()
    {
        if (PlayerPrefs.HasKey("minTime"))
        {
            minTime = PlayerPrefs.GetFloat("minTime");
        }
        else
        {
            PlayerPrefs.SetFloat("minTime", minTime);
        }
        Spawn();
    }

    private void Spawn()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
        Invoke(nameof(Spawn), Random.Range(minTime, maxTime));
    }
}
