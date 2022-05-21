using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DeathVolumeEffects : MonoBehaviour
{
    Volume volume;

    [SerializeField] float timeLength = 2f;

    private void Awake()
    {
        volume = GetComponent<Volume>();
    }

    public void Play()
    {
        StartCoroutine(DeathEffects());
    }

    private IEnumerator DeathEffects()
    {
        float timer = 0f;
        ChromaticAberration c;
        LensDistortion l;
        Vignette v;

        while (timer < timeLength)
        {
            if (timer < .1f)
            {
                if (volume.profile.TryGet<ChromaticAberration>(out c))
                {
                    c.intensity.value = timer * 10f;
                }
                if (volume.profile.TryGet<Vignette>(out v))
                {
                    v.intensity.value = timer * 5f;
                }
            }
            if (timer > (timeLength - .1f))
            {
                if (volume.profile.TryGet<ChromaticAberration>(out c))
                {
                    c.intensity.value = (timer - timeLength) * -10f;
                }
                if (volume.profile.TryGet<Vignette>(out v))
                {
                    v.intensity.value = (timer - timeLength) * -5f;
                }
            }

            if (volume.profile.TryGet<LensDistortion>(out l))
            {
                l.intensity.value = Mathf.Clamp(l.intensity.value + Random.Range(-.1f,.1f),-1f,1f);
                Debug.Log(l.intensity.value);
            }

            yield return new WaitForFixedUpdate();
            timer += Time.fixedDeltaTime;
        }

        
        if (volume.profile.TryGet<ChromaticAberration>(out c))
        {
            c.intensity.value = 0f;
        }
        if (volume.profile.TryGet<LensDistortion>(out l))
        {
            l.intensity.value = 0f;
        }
        if (volume.profile.TryGet<Vignette>(out v))
        {
            v.intensity.value = 0f;
        }
    }
}
