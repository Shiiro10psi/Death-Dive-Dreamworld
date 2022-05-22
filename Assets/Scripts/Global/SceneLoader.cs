using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    List<Image> NeutralTransitionImages;
    
    private void Awake()
    {
        SetUpSingleton();
        NeutralTransitionImages = new List<Image>(GetComponentsInChildren<Image>());
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadScene(int index)
    {
        StartCoroutine(SceneLoadingNeutral(index));
    }

    public void ReloadScene()
    {
        StartCoroutine(SceneLoadingNeutral(SceneManager.GetActiveScene().buildIndex));
    }

    public void ReturnToMenu()
    {
        LoadScene(0);
    }

    private IEnumerator SceneLoadingNeutral(int index)
    {
        
        //Debug.Log("Transition Start");
        do
        {
            foreach (Image i in NeutralTransitionImages)
            {
                i.fillAmount += Time.unscaledDeltaTime;
            }
            yield return new WaitForEndOfFrame();
        } while (NeutralTransitionImages[0].fillAmount < 1);
        //Debug.Log("Transition Middle");
        SceneManager.LoadScene(index);
        do
        {
            foreach (Image i in NeutralTransitionImages)
            {
                i.fillAmount -= Time.unscaledDeltaTime;
            }
            yield return new WaitForEndOfFrame();
        } while (NeutralTransitionImages[0].fillAmount > 0);
        //Debug.Log("Transition End");
        Time.timeScale = 1f;
    }
    
}
