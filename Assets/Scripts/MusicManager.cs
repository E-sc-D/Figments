using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public List<AudioClip> Musics;
    public List<List<int>> Timestamps;

    AudioSource musicPlayer;
    int currentIndex
    {
        get
        {
            return Musics.IndexOf(musicPlayer.clip);
        }
    }

    void Awake()
    {
        musicPlayer = this.GetComponent<AudioSource>();
    }

    public void Play(int index, bool looped)
    {
        musicPlayer.loop = looped;
        musicPlayer.clip = Musics[index];
        musicPlayer.Play();
    }

    public void Play(string name, bool looped)
    {
        musicPlayer.loop = looped;
        foreach(AudioClip clip in Musics)
            if(clip.name == name)
            {
                musicPlayer.clip = clip;
                break;
            }
        musicPlayer.Play();
    }

    public void Stop()
    {
        musicPlayer.Stop();
    }

    public void Pause()
    {
        musicPlayer.Pause();
    }

    public void Resume()
    {
        musicPlayer.Play();
    }

    public IEnumerator FadeFromSilence(float fadeSpeed)
    {
        if(fadeSpeed != 0)
            while(musicPlayer.volume < 1)
            {
                float fadeAmount = musicPlayer.volume + (fadeSpeed * Time.deltaTime);
                musicPlayer.volume = fadeAmount;
                yield return null;
            }
        else
            musicPlayer.volume = 1;
    }

    public IEnumerator FadeToSilence(float fadeSpeed)
    {
        if(fadeSpeed != 0)
            while(musicPlayer.volume > 0)
            {
                float fadeAmount = musicPlayer.volume - (fadeSpeed * Time.deltaTime);
                musicPlayer.volume = fadeAmount;
                yield return null;
            }
        else
            musicPlayer.volume = 0;
    }

    public IEnumerator WaitForTransitionPoint()
    {
        while(!Timestamps[currentIndex].Contains(((int)musicPlayer.time)))
        {
            yield return null;
        }
    }
}
