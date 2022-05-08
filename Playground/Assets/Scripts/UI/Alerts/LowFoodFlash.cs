using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LowFoodFlash : MonoBehaviour
{
    Image flashingImage;
    Color startColor;
    bool flashing;

    private void Start()
    {
        flashingImage = gameObject.GetComponent<Image>();
        startColor = flashingImage.color;
    }

    void Update()
    {
        if (PlayerSurvival.main.hunger < 0.35f)
        {
            flashing = true;
            StartCoroutine(Flash());
        }
        else
        {
            flashing = false;
        }
    }

    IEnumerator Flash()
    {
        while (flashing)
        {
            flashingImage.color = Color.Lerp(Color.red, Color.black, Time.time % 1);
            yield return null;
        }
        flashingImage.color = startColor;
    }
}
