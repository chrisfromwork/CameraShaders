using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

public class CameraShaderDriver
{
    static CameraShaderDriver _instance;
    public static CameraShaderDriver Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CameraShaderDriver();
            }

            return _instance;
        }
    }

    private List<CameraShaderDetails> _shaderList;
    private RenderTexture _texture;

    public void AddShader(CameraShaderDetails shader)
    {
        if (_shaderList == null)
        {
            _shaderList = new List<CameraShaderDetails>();
        }

        if (!_shaderList.Contains(shader))
        {
            _shaderList.Add(shader);
        }
    }

    private CameraShaderDriver() { }

    public void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (_shaderList == null)
            return;

        if (_texture == null)
            _texture = new RenderTexture(source);

        int count = 0;
        var tempList = _shaderList.OrderBy(shader => shader.renderOrder).ToList();
        foreach (var details in tempList)
        {
            if (details.active)
            {
                if (count % 2 == 0)
                    details.PreUpdate(source);
                else
                    details.PreUpdate(_texture);

                details.UpdateShader();
                if (details.material != null)
                {
                    if (count % 2 == 0)
                    {
                        Graphics.Blit(source, _texture, details.material);
                        details.PostUpdate(_texture);
                    }
                    else
                    {
                        Graphics.Blit(_texture, source, details.material);
                        details.PostUpdate(source);
                    }

                    count++;
                }
            }
        }

        if (count % 2 == 0)
            Graphics.Blit(source, destination);
        else
            Graphics.Blit(_texture, destination);
    }
}