using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public Material myMaterial;

    public Color colorFirst;
    public Color colorSecond;

    public Color lerpedColor = Color.white;

    void Update()
    {
        lerpedColor = Color.Lerp(colorFirst, colorSecond, Mathf.PingPong(Time.time, 1));
        myMaterial.color = lerpedColor;
    }
}
