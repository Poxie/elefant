using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    [SerializeField] Texture2D cursorSprite;

    // Start is called before the first frame update
    void Awake() {
        Cursor.visible = false;
        Cursor.SetCursor(cursorSprite, Vector2.zero, CursorMode.ForceSoftware);
    }
}
