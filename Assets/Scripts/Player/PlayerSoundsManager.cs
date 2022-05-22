using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsManager : MonoBehaviour
{
    SoundPlayer sp;

    [SerializeField] AudioClip hurtSound, deathSound, jumpSound, waterSound;

    private void Awake()
    {
        sp = FindObjectOfType<SoundPlayer>();
    }
    private void Update()
    {
        if (sp == null) sp = FindObjectOfType<SoundPlayer>();
    }

    public void PlayHurtSound()
    {
        sp.PlaySound(hurtSound);
    }

    public void PlayDeathSound()
    {
        sp.PlaySound(deathSound);
    }

    public void PlayJumpSound()
    {
        sp.PlaySound(jumpSound);
    }

    public void PlayWaterSound()
    {
        sp.PlaySound(waterSound);
    }
}
