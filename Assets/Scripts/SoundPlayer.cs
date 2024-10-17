using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private static SoundPlayer _instance;
    public static SoundPlayer instance => _instance;
    AudioSource audioSource;
    [SerializeField]
    AudioClip AttackSound;
    [SerializeField]
    AudioClip ExplosionSound;
    [SerializeField]
    AudioClip WalkSound;
    [SerializeField]
    AudioClip GoldSound;
    [SerializeField]
    AudioClip RatDeadSound;
    [SerializeField]
    AudioClip MoleDeadSound;
    [SerializeField]
    AudioClip ShootSound;
    [SerializeField]
    AudioClip BtnSound;
    [SerializeField]
    AudioClip GainSound;
    [SerializeField]
    AudioClip ResourceHitSound;
    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAtkSound()
    {
        audioSource.PlayOneShot(AttackSound);
    }

    public void PlayExplosionSound()
    {
        audioSource.PlayOneShot(ExplosionSound);
    }
    public void PlayWalkSound()
    {
        audioSource.PlayOneShot(WalkSound, 0.7f);
    }
    public void PlayGoldSound()
    {
        audioSource.PlayOneShot(GoldSound, 0.7f);
    }

    public void PlayRatDeadSound()
    {
        audioSource.PlayOneShot(RatDeadSound);
    }
    public void PlayMoleDeadSound()
    {
        audioSource.PlayOneShot(MoleDeadSound);
    }
    public void PlayShootSound()
    {
        audioSource.PlayOneShot(ShootSound);
    }
    public void PlayBtnSound()
    {
        audioSource.PlayOneShot(BtnSound);
    }
    public void PlayGainSound()
    {
        audioSource.PlayOneShot(GainSound, 0.3f);
    }
    public void PlayResourceHitSound()
    {
        audioSource.PlayOneShot(ResourceHitSound, 2);
    }
}
