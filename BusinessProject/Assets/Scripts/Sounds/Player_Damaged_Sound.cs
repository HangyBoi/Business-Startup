using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Damaged_Sound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    [Range(0f, 1f)]
    public float clipVolume = 1.0f;

    public void PlayDamageSound(int a, int b)
    {

        //choose random clip
        AudioClip randomClip = audioClips[Random.Range(0, audioClips.Length)];

        //play audio clip
        audioSource.PlayOneShot(randomClip, clipVolume);

        Debug.Log("damage Sound");
    }
}
