using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voronoi : CameraShaderDetails
{
    public float alpha = 1.0f;
    public int numberOfPoints = 100;
    public int numberOfFrames = 30;
    private bool randomize = true;
    private float[] points = new float[4056];
    private int counter = 0;

    public override void Start()
    {
        base.Start();
        this.shader = Shader.Find("Voronoi");
    }

    public override void UpdateShader()
    {
        base.UpdateShader();

        if (counter > numberOfFrames && randomize)
        {
            for (int i = 0; i < points.Length && i < numberOfPoints; i++)
            {
                points[i] = (float)random.NextDouble();
            }

            counter = 0;
        }

        if (material)
        {
            material.SetFloat("_Alpha", alpha);
            material.SetInt("_PointsCount", numberOfPoints);
            material.SetFloatArray("_Points", points);
        }

        counter++;
    }
}
