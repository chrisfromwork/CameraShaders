using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invert : CameraShaderDetails
{

    public override void Start()
    {
        base.Start();
        this.shader = Shader.Find("Invert");
    }
}
