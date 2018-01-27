using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour {

    public Texture2D fadeOutTexture;    // the texture will overlay the screen
    public float fadeSpeed = 0.8f;      // the fading speed

    private int drawDepth = -1000;      // the texture's order in the draw hierarchy: a low number means it renders on top
    private float alpha = 1.0f;         // the texture's alpha value between 0 and 1
    private int fadeDir = -1;           // the direction to fade: in = -1 or iut = 1

    void OnGUI()
    {
        // fade out / in the alpha value using a direction, a speed and Time.deltatime to covert the operation to seconds
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        // force (clamp) the numbrer between 0 and 1 because GUI.color uses alpha values between 0 and 1
        alpha = Mathf.Clamp01(alpha);

        // set color of GUI and alpha value
        GUI.color = new Color(GUI.color.r, GUI.color.r, GUI.color.r, alpha);
        // make the black texture render on top (drawn last)
        GUI.depth = drawDepth;
        // draw the texture to fit the entire screen area
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    public float BeginFade (int direction)
    {
        fadeDir = direction;
        return fadeSpeed;
    }
}
