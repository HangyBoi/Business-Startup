using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Steps : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip[] audioClips;
    [Range(0f, 1f)]
    public float clipVolume = 1.0f;

    private float stepCooldown = 0f;


    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        stepCooldown--;

        if (horizontal != 0 && stepCooldown <= 0 || vertical != 0 && stepCooldown <= 0)
        {
            //choose random clip
            AudioClip randomClip = audioClips[Random.Range(0, audioClips.Length)];

            //play audio clip
            audioSource.PlayOneShot(randomClip, clipVolume);

            stepCooldown = 20f;
        }

        //cut audio if the player becomes stationary
        //else if (horizontal == 0 && vertical == 0) 
        //{
        //    if (audioSource.isPlaying) audioSource.Stop();
        //}
    }
}
