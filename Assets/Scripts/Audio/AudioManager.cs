using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private AudioSource source;
    [SerializeField] private AudioClip[] clips;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        source = GetComponent<AudioSource>();
    }
    public void PlaySound(string clipName)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            if (clips[i].name == clipName)
            {
                source.pitch = Random.Range(.85f, 1.1f);
                source.PlayOneShot(clips[i]);
            }
        }
    }
    public void ResetAudioSource()
    {
        source.pitch = 1;
    }
}
