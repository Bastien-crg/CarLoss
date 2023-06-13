using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine ;
using SDD.Events;

public class AudioManager : MonoBehaviour, IEventHandler
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<PlayerHasShootEvent>(PlayerHasShoot);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<PlayerHasShootEvent>(PlayerHasShoot);
    }

    void OnEnable()
    {
        SubscribeEvents();
    }
    void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void Awake()
    {

        if(Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Theme");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource.PlayOneShot(s.clip);
        }

    }

    public void PlayerHasShoot(PlayerHasShootEvent e)
    {
        Debug.Log("Prada");
        PlaySFX("Shoot");
    }
}