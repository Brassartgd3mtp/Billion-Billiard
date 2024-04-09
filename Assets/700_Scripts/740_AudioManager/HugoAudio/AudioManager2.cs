using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using System; 

public class AudioManager2 : MonoBehaviour
{


    public static AudioManager2 Instance;


    public Sound[] musicSounds, sfxSounds; 
    public AudioSource musicSource, sfxSource;


    public void Awake()
    {
       if (Instance == null)
        {
            Instance = this; 
        }

       else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //PlayMusic("Theme");
    }

  


    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }

        else
            {
                musicSource.clip = s.clip; 
                musicSource.Pause();
            musicSource.Play();
        }
        
    }


    public void PlaySDFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("SFX not found");
        }

        else
        {
            sfxSource.PlayOneShot(s.clip); 
        }
    }


}
