using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private GameObject
        sfxPlayer,
        dialougePlayer;

    public Sound[] sfxClips;
    public Sound[] dialougeClips;

    private void Awake()
    {
        sfxPlayer = GameObject.Find("_SFX_AudioPlayer");
        dialougePlayer = GameObject.Find("_Dialouge_AudioPlayer");

        foreach (Sound sound in sfxClips)
        {
            sound.audio_source = sfxPlayer.AddComponent<AudioSource>();
            sound.audio_source.clip = sound.clip;
            sound.audio_source.loop = false;
        }

        foreach(Sound sound in dialougeClips)
        {
            sound.audio_source = dialougePlayer.AddComponent<AudioSource>();
            sound.audio_source.clip = sound.clip;
            sound.audio_source.loop = false;
        }
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayDialouge(string dialouge)
    {
        foreach (Sound sound in dialougeClips)
        {
            if (sound.name == dialouge)
            {
                sound.audio_source.Play();
                return;
            }
        }
        Debug.Log("Dialouge Clip Not Found!");
    }

    public void PlaySFX(string sfx)
    {
        foreach(Sound sound in sfxClips)
        {
            if(sound.name == sfx)
            {
                sound.audio_source.Play();
                return;
            }
        }
        Debug.Log("SFX Clip Not Found!");
    }

    public void PlayButtonTapSound()
    {
        PlaySFX("tap");
    }
}
