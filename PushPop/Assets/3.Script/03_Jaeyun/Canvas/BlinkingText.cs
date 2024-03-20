using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkingText : MonoBehaviour
{
    [SerializeField] private TMP_Text blinkText = null;
    [SerializeField] private float alpha = 1f;
    private float sign = -1f;

    private void OnEnable()
    {
        StartCoroutine(BlinkColor_Co());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator BlinkColor_Co()
    {
        while (true)
        {
            alpha += sign * Time.deltaTime;
            blinkText.color = new Color(blinkText.color.r, blinkText.color.g, blinkText.color.b, alpha);

            if (alpha <= 0f || 1f <= alpha)
            {
                sign *= -1f;
            }

            yield return null;
        }
    }
}
