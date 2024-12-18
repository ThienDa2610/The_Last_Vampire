using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    public AudioSource audioSource; 

    public AudioClip takeDamageSound;
    public AudioClip deathSound;    
    public AudioClip attackSound;

    public void PlayTakeDamage()
    {
        audioSource.PlayOneShot(takeDamageSound);
    }

    public void PlayDeathSound()
    {
        audioSource.PlayOneShot(deathSound);
    }

    public void PlayAttackSound()
    {
        audioSource.clip = attackSound;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopAttackSound()
    {
        audioSource.loop = false;
        audioSource.Stop();
    }
}
