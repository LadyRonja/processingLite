using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Assignment1 : ProcessingLite.GP21
{
    [SerializeField] float xOffSetName = 0f;
    [SerializeField] float yOffSetName = 0f;

    float spaceBetweenLines = 0.2f;
    [SerializeField] private List<Vector4> lines = new List<Vector4>();
    [SerializeField] private List<Vector2> triangleVectors = new List<Vector2>();
    [SerializeField] private List<Vector2> triangleVectors1 = new List<Vector2>();
    [SerializeField] private List<Vector2> triangleVectors2 = new List<Vector2>();

    [Header("ax^2 + bx + c")]
    [SerializeField] private float a = 0.5f;
    [SerializeField] private float b = 1f;
    [SerializeField] private float c = 1f;
    private float granuality = 0.5f;

    private int clampXmin = 0;
    private int clampXmax = 17;
    private int clampYmax = 10;
    private int clampYmin = 0;
    [Space]
    public float pointA = 0;
    public float pointB = 0;
    [Header("Parabolic Lines")]
    [SerializeField] private int lineAmount = 30;


    void Start()
    {
    }

    void Update()
    {
        ClearBackground();
        WriteName();
        ScanLines();
        DrawAbstractShapes();
        DrawParabola();
        Point(pointA, pointB);
        DrawPrabolicLines();
    }

    private void DrawPrabolicLines()
    {
        StrokeWeight(0.1f);
        float yIncremeant = Height / lineAmount;
        float xIncrement = Width / lineAmount;
        for (int i = 0; i < lineAmount; i++)
        {
            float yCord = Height - (yIncremeant * i);
            float xCord = xIncrement * i;
            Line(0, yCord, xCord, 0);
        }

        /*for (int x = 0; x <= Width; x++)
        {
            for (int y = 0; y <= Height; y++)
            {
                Point(x, y);
            }
        }*/
    }

    private void DrawParabola()
    {
        StrokeWeight(0.5f);
        Stroke(255, 0, 0);
        Vector2 lastpoint = Vector2.zero;
        bool first = true; 
        for (float x = clampXmin; x < clampXmax; x += granuality)
        {
            for (int i = clampYmin; i < clampYmax; i++)
            {
                float exp = Mathf.Exp(x);
                float y = a* exp + b*x + c;
                if (!first) Line(lastpoint.x, lastpoint.y, x, y);
                else first= false;
                lastpoint = new Vector2(x, y);
                //Point(x, y);
            }
        }
    }

    private void WriteName()
    {
        Stroke(255, 255, 255, 255);
        StrokeWeight(1f);
        for (int i= 0; i < lines.Count; i++)
        {
            if (i % 3 == 0)
            {
                Stroke(150, 150, 0, 255);
            }
            else
            {
                Stroke(255, 255, 255, 255);
            }
            Line(lines[i].x +xOffSetName, lines[i].y+yOffSetName, lines[i].z + xOffSetName, lines[i].w + yOffSetName);
        }

    }

    private void DrawAbstractShapes()
    {
        // Triforce
        StrokeWeight(0.1f);
        Stroke(255, 255, 0);
        Fill(255, 255, 0);
        Triangle(triangleVectors[0], triangleVectors[1], triangleVectors[2]);
        Triangle(triangleVectors1[0], triangleVectors1[1], triangleVectors1[2]);
        Triangle(triangleVectors2[0], triangleVectors2[1], triangleVectors2[2]);


    }

    private void ClearBackground()
    {
        Background(0, 0, 0);
    }

    private void ScanLines()
    {
        //Prepare our stroke settings
        Stroke(128, 128, 128, 64);
        StrokeWeight(0.5f);

        //Draw our scan lines
        for (int i = 0; i < Height / spaceBetweenLines; i++)
        {
            //Increase y-cord each time loop run
            float y = i * spaceBetweenLines;

            //Draw a line from left side of screen to the right
            Line(0, y, Width, y);
        }
    }

}
