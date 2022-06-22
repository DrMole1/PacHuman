using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public List<AudioClip> clips;

    public static AudioManager Instance;

    private AudioSource audioSource;

    private void Awake()
    {

        Instance = this;
        if (Instance == null)
        {
            Instance = GameObject.FindObjectOfType<AudioManager>();
            if (Instance == null)
            {
                GameObject container = new GameObject("AudioManager");
                Instance = container.AddComponent<AudioManager>();
            }
        }

        audioSource = GetComponent<AudioSource>();
    }


    public void playAudioClip(int index)
    {

        if (clips[index] != null && audioSource.enabled)
        {
            audioSource.PlayOneShot(clips[index]);
        }
    }

}
