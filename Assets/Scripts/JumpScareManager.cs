using UnityEngine;
using System.Collections;

/// <summary>
/// JumpScareManager.cs - Handles jump scare triggers and effects
/// </summary>
public class JumpScareManager : MonoBehaviour
{
    [SerializeField] private AudioClip jumpScareSound;
    [SerializeField] private float jumpScareDuration = 2f;
    [SerializeField] private float maxScareIntensity = 1f;
    
    private AudioSource audioSource;
    private bool isScaring = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    /// <summary>
    /// Trigger a jump scare at the player
    /// </summary>
    public void TriggerJumpScare(Vector3 scarePosition, string scareType = "creature")
    {
        if (!isScaring)
        {
            StartCoroutine(PlayJumpScare(scarePosition, scareType));
        }
    }

    IEnumerator PlayJumpScare(Vector3 position, string type)
    {
        isScaring = true;
        
        // Play scary sound
        if (jumpScareSound != null && audioSource != null)
        {
            audioSource.clip = jumpScareSound;
            audioSource.volume = maxScareIntensity;
            audioSource.Play();
        }
        
        // TODO: Show creature/scary visual effect
        // TODO: Shake camera
        // TODO: Increase heart rate sound
        
        Debug.Log($"JUMP SCARE! Type: {type} at position {position}");
        
        yield return new WaitForSeconds(jumpScareDuration);
        
        isScaring = false;
    }

    /// <summary>
    /// Play ambient scary sounds
    /// </summary>
    public void PlayAmbientSound(AudioClip sound)
    {
        if (audioSource != null)
        {
            audioSource.clip = sound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    /// <summary>
    /// Stop all sounds
    /// </summary>
    public void StopAllSounds()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
