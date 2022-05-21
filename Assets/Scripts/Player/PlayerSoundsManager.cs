using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsManager : MonoBehaviour
{
    SoundPlayer sp;

    [SerializeField] AudioClip hurtSound, deathSound, jumpSound;

    private void Awake()
    {
        sp = FindObjectOfType<SoundPlayer>();
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
}
