using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class DrowningVolumeEffects : MonoBehaviour
{
    Volume volume;

    float air = 30f;
    bool inWater = false;

    float clearTime = 1f;
    float clearTimer = 1f;

    [SerializeField] Vector4 lift, gamma, gain;

    LiftGammaGain neutralLGG;

    private void Awake()
    {
        volume = GetComponent<Volume>();
        neutralLGG = ScriptableObject.CreateInstance<LiftGammaGain>();
    }

    private void Start()
    {
        LiftGammaGain lgg;

        if (volume.profile.TryGet<LiftGammaGain>(out lgg))
        {
            Debug.Log(lgg.lift + "," + lgg.gamma + "," +lgg.gain);
        }
    }

    // Update is called once per frame
    void Update()
    {
        LiftGammaGain lgg;
        Vignette v;
        

        if (volume.profile.TryGet<LiftGammaGain>(out lgg) && volume.profile.TryGet<Vignette>(out v))
        {
            if (inWater)
            {
                if (air < 20)
                {
                    lgg.lift.value = new Vector4(Mathf.Lerp(neutralLGG.lift.value.x, lift.x, (20 - air) / 20),
                    Mathf.Lerp(neutralLGG.lift.value.y, lift.y, (20 - air) / 20),
                    Mathf.Lerp(neutralLGG.lift.value.z, lift.z, (20 - air) / 20),
                    Mathf.Lerp(neutralLGG.lift.value.w, lift.w, (20 - air) / 20));
                    lgg.gamma.value = new Vector4(Mathf.Lerp(neutralLGG.gamma.value.x, gamma.x, (20 - air) / 20),
                    Mathf.Lerp(neutralLGG.gamma.value.y, gamma.y, (20 - air) / 20),
                    Mathf.Lerp(neutralLGG.gamma.value.z, gamma.z, (20 - air) / 20),
                    Mathf.Lerp(neutralLGG.gamma.value.w, gamma.w, (20 - air) / 20));
                    lgg.gain.value = new Vector4(Mathf.Lerp(neutralLGG.gain.value.x, gain.x, (20 - air) / 20),
                    Mathf.Lerp(neutralLGG.gain.value.y, gain.y, (20 - air) / 20),
                    Mathf.Lerp(neutralLGG.gain.value.z, gain.z, (20 - air) / 20),
                    Mathf.Lerp(neutralLGG.gain.value.w, gain.w, (20 - air) / 20));
                    v.intensity.value = Mathf.Lerp(0, 1, (20 - air) / 20);
                }
                clearTimer = air /20f;
            }

            if (!inWater)
            {
                lgg.lift.value = new Vector4(Mathf.Lerp(lift.x,neutralLGG.lift.value.x,clearTimer / clearTime),
                    Mathf.Lerp(lift.y, neutralLGG.lift.value.y, clearTimer / clearTime),
                    Mathf.Lerp(lift.z, neutralLGG.lift.value.z, clearTimer / clearTime),
                    Mathf.Lerp(lift.w, neutralLGG.lift.value.w, clearTimer / clearTime));
                lgg.gamma.value = new Vector4(Mathf.Lerp(gamma.x, neutralLGG.gamma.value.x, clearTimer / clearTime),
                    Mathf.Lerp(gamma.y, neutralLGG.gamma.value.y, clearTimer / clearTime),
                    Mathf.Lerp(gamma.z, neutralLGG.gamma.value.z, clearTimer / clearTime),
                    Mathf.Lerp(gamma.w, neutralLGG.gamma.value.w, clearTimer / clearTime));
                lgg.gain.value = new Vector4(Mathf.Lerp(gain.x, neutralLGG.gain.value.x, clearTimer / clearTime),
                    Mathf.Lerp(gain.y, neutralLGG.gain.value.y, clearTimer / clearTime),
                    Mathf.Lerp(gain.z, neutralLGG.gain.value.z, clearTimer / clearTime),
                    Mathf.Lerp(gain.w, neutralLGG.gain.value.w, clearTimer / clearTime));

                v.intensity.value = Mathf.Lerp(1, 0, clearTimer / clearTime);
                clearTimer += Time.deltaTime;
            }
        }
    }

    public void Inform(float air, bool inWater)
    {
        this.air = air;
        this.inWater = inWater;
    }

    public void Inform(float air)
    {
        this.air = air;
    }

    public void Inform(bool inWater)
    {
        this.inWater = inWater;
    }
}
