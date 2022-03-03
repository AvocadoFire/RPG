using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRandom : MonoBehaviour
{
    [SerializeField] AudioClip[] songs;
    [SerializeField] float defaultVolume = .1f;
    
    AudioClip song;
    AudioSource audioData;    
    
    static MusicRandom instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        int rand = Random.Range(0, songs.Length);
        song = songs[rand];
        audioData = GetComponent<AudioSource>();
        audioData.clip = song;
        audioData.loop = true;
        audioData.volume = defaultVolume;
        audioData.Play(0);
    }
}
