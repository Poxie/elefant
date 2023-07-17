using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    bool showingDeathScreen = false;
    CanvasGroup deathScreenGroup;
    Transform deathScreen;
    Transform deathText;

    // Start is called before the first frame update
    void Start() {
        deathScreen = transform.Find("DeathScreen");
        deathText = deathScreen.Find("DeathText");
        deathScreenGroup = deathScreen.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update() {
        if(showingDeathScreen) {
            if(deathScreenGroup.alpha < 1) {
                deathScreenGroup.alpha += Time.deltaTime;
            }
            if(deathText.localScale.x < 38) {
                float scale = deathText.localScale.x + Time.deltaTime * 200;
                deathText.localScale = new Vector3(scale, scale, scale);
            }
        }
    }
    
    public void ShowDeathScreen() {
        deathScreenGroup.alpha = 0;
        deathText.localScale = new Vector3(0,0,0);
        showingDeathScreen = true;
    }
}
