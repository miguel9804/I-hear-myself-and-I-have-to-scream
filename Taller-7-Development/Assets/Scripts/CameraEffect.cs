using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraEffect : MonoBehaviour
{
    [SerializeField] Shader shader;
    [SerializeField] float factor;

    Material materialActual;
    Material material {
        get {
            if (materialActual == null)
            {
                materialActual = new Material(shader);
                materialActual.hideFlags = HideFlags.HideAndDontSave;
            }
            return materialActual;
        }
        
    }

    private void OnRenderImage(RenderTexture inp, RenderTexture outp)
    {
        material.SetFloat("_Factor", factor);
        Graphics.Blit(inp, outp, material);
    }

    private void OnDisable()
    {
        DestroyImmediate(materialActual);
    }
}
