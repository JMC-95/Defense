using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip[] audioClips;
    public AudioSource audioSource;

    public static SoundManager Instance = null;

    // Initialize the singleton instance.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayBgm(int soundType)
    {
        audioSource.loop = true;
        audioSource.clip = audioClips[soundType];
        audioSource.Play();
    }

    public void PlaySound(int soundType)
    {
        audioSource.PlayOneShot(audioClips[soundType]);
    }

    public void Start()
    {

        audioClips = new AudioClip[Type.Audio.Max];

        string path = "Audio/";

        for (int i = 0; i < Type.Audio.Max; ++i)
        {
            audioClips[i] = (AudioClip)Resources.Load(path + Type.Audio.GetName(i), typeof(AudioClip));
        }
        SoundManager.Instance.PlayBgm(Type.Audio.InGameBgm);
    }
}
