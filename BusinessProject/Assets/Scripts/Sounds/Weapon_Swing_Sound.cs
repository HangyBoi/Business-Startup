using UnityEngine;

public class Weapon_Swing : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip[] regularSwing;
    public AudioClip[] perfectSwing;

    [Range(0f, 1f)]
    public float clipVolume = 1.0f;

    /// <summary>
    /// Plays a swing sound based on whether the attack was perfect.
    /// </summary>
    /// <param name="isPerfect">Indicates if the attack was perfect.</param>
    public void PlaySwingSound(bool isPerfect)
    {
        AudioClip selectedClip;

        if (isPerfect && perfectSwing.Length > 0)
        {
            selectedClip = perfectSwing[Random.Range(0, perfectSwing.Length)];
        }
        else if (regularSwing.Length > 0)
        {
            selectedClip = regularSwing[Random.Range(0, regularSwing.Length)];
        }
        else
        {
            Debug.LogWarning("No swing sounds available to play.");
            return;
        }

        Debug.Log($"Playing sound: {selectedClip.name}");
        audioSource.PlayOneShot(selectedClip, clipVolume);
    }
}
