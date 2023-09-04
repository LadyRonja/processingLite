using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.PlacematContainer;

// TODO
// Bounce on the walls



public class VectorAssignment : ProcessingLite.GP21
{
    [SerializeField] private float diamater = 0.5f;
    private Vector2 lastClickPos;

    private bool moving = false;
    [SerializeField] private float speedMax = 5f;
    private float speedCur = 5f;
    private Vector2 circleDirection = Vector2.zero;
    private Vector2 circlePos = Vector2.zero;

    private void Start()
    {
        circlePos = new Vector2(Width/2, Height/2);
        DrawCircle(circlePos.x, circlePos.y);
    }

    private void Update()
    {
        ClearBackground();

        InputManager();
        MovementManager();
        OutOfBoundsHandler();
    }

    private void InputManager()
    {
        // On get button down, stop movement
        if (Input.GetMouseButtonDown((int)MouseButton.Left))
        {
            lastClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            circlePos = lastClickPos;
            speedCur = 0;
            moving = false;
        }

        // While holding button down, draw line
        if (Input.GetMouseButton((int)MouseButton.Left))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Line(mousePos.x, mousePos.y, lastClickPos.x, lastClickPos.y);
        }

        // When letting go of button, start moving
        if (Input.GetMouseButtonUp((int)MouseButton.Left))
        {
            circleDirection = circlePos - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            speedCur = circleDirection.magnitude;
            Mathf.Clamp(speedCur, 0, speedMax);
            circleDirection = circleDirection.normalized;
            moving = true;
        }   
    }

    private void DrawCircle(float x, float y)
    {
        SetCircleColors();

        Circle(x, y, diamater);
    }

    private void MovementManager()
    {
        if (!moving) 
        { 
            DrawCircle(circlePos.x, circlePos.y); 
            return; 
        }

        // Bounds
        float radius = diamater / 2;
        if (circlePos.x - radius <= 0)
        {
            circleDirection.x *= -1;
        }
        if (circlePos.y - radius <= 0)
        {
            circleDirection.y *= -1;
        }
        if (circlePos.x + radius >= Width)
        {
            circleDirection.x *= -1;
        }
        if (circlePos.y + radius >= Height)
        {
            circleDirection.y *= -1;
        }

        circlePos += new Vector2(circleDirection.x, circleDirection.y) * Time.deltaTime * speedCur;
        DrawCircle(circlePos.x, circlePos.y);

    }

    private void OutOfBoundsHandler()
    {
        float radius = diamater / 2;
        float minX = 0 + radius;
        float maxX = Width - radius;
        float minY = 0 + radius;
        float maxY = Height - radius;

        circlePos.x = Mathf.Clamp(circlePos.x, minX, maxX);
        circlePos.y = Mathf.Clamp(circlePos.y, minY, maxY);
    }

    private void ClearBackground()
    {
        Background(0, 0, 0);
    }
    private void SetCircleColors()
    {
        StrokeWeight(0.1f);
        Stroke(255, 0, 0);
        Fill(255, 0, 255);
    }



}
