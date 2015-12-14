using UnityEngine;
using System.Collections;

public class FadingScene : MonoBehaviour {

    //The texture that will overlay the screen
    public Texture2D fadeOutTexture;
    //The fade speed
    public float fadeSpeed = 0.8f;
    //The texture's order in the draw hierarchy
    private int drawDepth = -1000;
    //The texture alpha value between 0 and 1
    private float alpha;
    //The direction to fade: in -1 out 1;
    private int fadeDir = -1;

    void OnGUI () {
        //fade out/in the alpha value using a direction, a speed and Time.deltaTime to convert the operation to second
        alpha += fadeDir * fadeSpeed * Time.deltaTime;

        //forse (clamp) the number between 0 and 1 because GUI.color uses alpha values between 0 and 1
        alpha = Mathf.Clamp01 ( alpha );

        //set color of our GUI (in this case our texture) All color values remain the same
        //the Alpha is set to the alpha variable
        GUI.color = new Color ( GUI.color.r, GUI.color.g, GUI.color.b, alpha );
        //make sure the black texture render on top (draw last)
        GUI.depth = drawDepth;
        //draw the texture to fit the entire screen area
        GUI.DrawTexture ( new Rect ( 0 , 0 , Screen.width , Screen.height ) , fadeOutTexture ); 
    }

    //sets fadeDir to the direction parameter making the scene fade in if -1 and out if 1
    public float beginFade ( int direction ) {
        //return the fadeSpeed variable so it's easy to time the Application.LoadLevel();
        fadeDir = direction; 

        return fadeSpeed;
    }

    //On OnLevelWasLoaded is called when a level is loaded. It takes loaded level index (int) as a parameter so you can limit the fade in to certain scene
    void OnLevelWasLoaded () {
        //alpha = 1
        beginFade ( -1 );
    }
}
