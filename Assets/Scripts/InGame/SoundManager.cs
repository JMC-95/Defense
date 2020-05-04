using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip[] audioClips;

    public void PlaySound(int soundType, Vector3 position)
    {
        AudioSource a = GetEmptySource();

        a.loop = false;
        a.clip = audioClips[soundType];
        a.Play();
    }

    void Start()
    {
        audioClips = new AudioClip[Type.Audio.Max];
        string path = "Audio/";

        for (int i = 0; i < Type.Audio.Max; ++i)
        {
            audioClips[i] = (AudioClip)Resources.Load(path + Type.Audio.GetName(i), typeof(AudioClip));
        }
    }
}
