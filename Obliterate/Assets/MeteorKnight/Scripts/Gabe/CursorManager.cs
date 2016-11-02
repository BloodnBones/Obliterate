using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour {

    public Texture2D cursorTexture;
    int w = 32;
    int h = 32;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 mouse = Vector2.zero;

    void Start()
    {
       
    }
    void Update()
    {
        Cursor.SetCursor(cursorTexture, mouse, cursorMode);
        
    }

    void OnGUI()
    {
    }
}
