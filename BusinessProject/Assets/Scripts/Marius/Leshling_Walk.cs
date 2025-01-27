using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leshling_Walk : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    [Range(0f, 1f)]
    public float clipVolume = 1.0f;

    private float stepCooldown = 0f;
    private Vector2 startPos;
    private Vector2 currentPos;

    private void Start()
    {
        Vector2 startPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
    }

    void FixedUpdate()
    {
        //Updates more frequently than the other one
        Vector2 currentPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);

        stepCooldown--;

        if (startPos != currentPos && stepCooldown <= 0)
        {
            //choose random clip
            AudioClip randomClip = audioClips[Random.Range(0, audioClips.Length)];

            //play audio clip
            audioSource.PlayOneShot(randomClip, clipVolume);
            Vector2 startPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);

            stepCooldown = 35f;
        }

        //cut audio if the player becomes stationary
        //else if (horizontal == 0 && vertical == 0) 
        //{
        //    if (audioSource.isPlaying) audioSource.Stop();
        //}
    }
}
