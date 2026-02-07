using UnityEngine;
using System.Collections;

public class RandomAmbientPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] sounds;
    public float minWait = 15f;
    public float maxWait = 30f;

    void Start()
    {
        // Start the infinite loop
        StartCoroutine(PlayRandomSoundRoutine());
    }

    IEnumerator PlayRandomSoundRoutine()
    {
        while (true)
        {
            // 1. Wait for a random time between 15-30 seconds
            float waitTime = Random.Range(minWait, maxWait);
            yield return new WaitForSeconds(waitTime);

            // 2. Choose and play a random sound from the list
            if (sounds.Length > 0)
            {
                int index = Random.Range(0, sounds.Length);
                audioSource.PlayOneShot(sounds[index]);
            }
        }
    }
}