using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Swing : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] regularSwing;
    public AudioClip[] perfectSwing;
    [Range(0f, 1f)]
    public float clipVolume = 1.0f;


    [SerializeField] private AttackBarController attackBar;
    [SerializeField] private float attackCooldown = 2f;
    private bool isAttacking;
    private float nextAttackAllowedTime;

    void Update()
    {
        if (Time.time < nextAttackAllowedTime) return;

        if (Input.GetMouseButtonDown(0) && !isAttacking) isAttacking = true;

        if (Input.GetMouseButtonUp(0) && isAttacking)
        {
            PlaySwingSound();
            isAttacking = false;
            nextAttackAllowedTime = Time.time + attackCooldown;
        }
    }

    private void PlaySwingSound()
    {
        AudioClip randomClip;

        if (attackBar.WasArrowInRedZone())
        {
            // Choose a random clip from perfectSwing
            randomClip = perfectSwing[Random.Range(0, perfectSwing.Length)];
        }
        else
        {
            // Choose a random clip from regularSwing
            randomClip = regularSwing[Random.Range(0, regularSwing.Length)];
        }

        // Play the chosen clip
        Debug.Log(randomClip);

        audioSource.PlayOneShot(randomClip, clipVolume);
    }
}
