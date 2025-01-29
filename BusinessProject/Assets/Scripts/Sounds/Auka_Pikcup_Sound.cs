using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auka_Pikcup_Sound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    [Range(0f, 1f)]
    public float clipVolume = 1.0f;

    private AudioClip randomClip;

    private void Start()
    {

        //choose random clip
        randomClip = audioClips[Random.Range(0, audioClips.Length)];

    }

    void OnDestroy()
    {

        //play audio clip
        AudioSource.PlayClipAtPoint(randomClip, transform.position, clipVolume);

        //chat fix - not working
        //GameObject tempAudio = new GameObject("TempAudio");
        //AudioSource audioSource = tempAudio.AddComponent<AudioSource>();
        //audioSource.clip = randomClip;
        //audioSource.volume = clipVolume;
        //audioSource.Play();
        //Destroy(tempAudio, randomClip.length);

    }
}

