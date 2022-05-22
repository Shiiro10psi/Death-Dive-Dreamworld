using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStateUI : MonoBehaviour
{
    int heartRate = 0, keys = 0;

    [SerializeField] Image keyImage, heartImage, keyPanel;
    [SerializeField] TextMeshProUGUI keyText, heartText;

    //bool heartGoingUp = false;
    //float heartAlpha = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        keyText.text = keys.ToString();
        heartText.text = heartRate.ToString();

        if (keys > 0)
        {
            keyImage.color = new Color(keyImage.color.r, keyImage.color.g, keyImage.color.b,1);
            keyText.color = Color.black;
            keyPanel.color = new Color(keyImage.color.r, keyImage.color.g, keyImage.color.b, 1);
        }
        if (keys <= 0)
        {
            keyImage.color = new Color(keyImage.color.r, keyImage.color.g, keyImage.color.b, 0);
            keyText.color = new Color(keyImage.color.r, keyImage.color.g, keyImage.color.b, 0);
            keyPanel.color = new Color(keyImage.color.r, keyImage.color.g, keyImage.color.b, 0);
        }

       /* if (heartGoingUp)
        {
            heartAlpha += (heartRate / 10000f) + Time.deltaTime;
            if (heartAlpha > 1f) heartGoingUp = false;
        }
        if (!heartGoingUp)
        {
            heartAlpha -= (heartRate / 10000f) + Time.deltaTime;
            if (heartAlpha < .5f) heartGoingUp = true;
        }
        Debug.Log(heartAlpha.ToString());

        heartImage.color = new Color(heartImage.color.r, heartImage.color.g, heartImage.color.b, heartAlpha);
        heartText.color = new Color(heartImage.color.r, heartImage.color.g, heartImage.color.b, heartAlpha);*/
    }

    public void Inform(int heartRate, int keys)
    {
        this.heartRate = heartRate;
        this.keys = keys;
    }
}
