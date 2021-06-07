using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip bacteriaDiedSound;
    public AudioClip gameOverSound;
    
    private AudioSource audioSource;

    private int deadCount;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        deadCount = 0;
    }

    public void BacteriaDied()
    {
        deadCount++;

        if (deadCount == 3)
        {
            audioSource.clip = gameOverSound;
            audioSource.Play();
        }
        else
        {
            audioSource.clip = bacteriaDiedSound;
            audioSource.Play();
        }
        
    }

    public void GameOver()
    {
        audioSource.clip = gameOverSound;
        audioSource.Play(); 
    }
}
