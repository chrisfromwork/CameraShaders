using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaderDetails : MonoBehaviour
{
    public uint renderOrder = 100;
    public static System.Random random = new System.Random();

    public Material material
    {
        get { return _material; }
    }
    private Material _material = null;

    public bool active = true;
    public void ToggleActive()
    {
        this.active = !active;
    }

    protected Shader shader = null;

    public virtual void Start()
    {
        CameraShaderDriver.Instance.AddShader(this);
    }

    static public Texture2D GetRTPixels(RenderTexture rt)
    {
        // Remember currently active render texture
        RenderTexture currentActiveRT = RenderTexture.active;

        // Set the supplied RenderTexture as the active one
        RenderTexture.active = rt;

        // Create a new Texture2D and read the RenderTexture image into it
        Texture2D tex = new Texture2D(rt.width, rt.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);

        // Restorie previously active render texture
        RenderTexture.active = currentActiveRT;
        return tex;
    }

    public virtual void PreUpdate(RenderTexture source)
    {
    }

    public virtual void UpdateShader()
    {
        if (shader == null)
        {
            _material = null;
            return;
        }
        else if (material == null)
        {
            _material = new Material(shader);
        }

        _material.SetFloat("_Aspect", Camera.main.aspect);
    }

    public virtual void PostUpdate(RenderTexture destination)
    {
    }
}