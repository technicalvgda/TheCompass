using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class EffectBrightness : MonoBehaviour
{
    public Material material;
    float brightness = 1;

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (brightness == 1) return;

        material.SetFloat("_Brightness", brightness);
        Graphics.Blit(src, dest, material);
    }

    public void Set(float value)
    {
        brightness = value;
    }
}
