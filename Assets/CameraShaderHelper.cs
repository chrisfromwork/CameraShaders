using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaderHelper : MonoBehaviour
{
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        CameraShaderDriver.Instance.OnRenderImage(source, destination);
    }
}
