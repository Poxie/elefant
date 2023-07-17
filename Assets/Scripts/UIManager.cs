using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {
    string scaleDeathDirection = "up";
    bool showingDeathScreen = false;
    CanvasGroup deathScreenGroup;

    [SerializeField] TextMeshProUGUI deathCounter;
    [SerializeField] RectTransform deathScreen;
    [SerializeField] RectTransform deathText;

    // Start is called before the first frame update
    void Start() {
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
            } else {
                float scale;
                if(scaleDeathDirection == "up") {
                    scale = deathText.localScale.x + Time.deltaTime * 20;
                } else {
                    scale = deathText.localScale.x - Time.deltaTime * 20;
                }
                if(scale > 48) {
                    scaleDeathDirection = "down";
                } else if(scale < 40) {
                    scaleDeathDirection = "up";
                }
                deathText.localScale = new Vector3(scale, scale, scale);
            }
        }
    }
    
    public void ShowDeathScreen() {
        deathScreenGroup.alpha = 0;
        deathText.localScale = new Vector3(0,0,0);
        showingDeathScreen = true;
    }

    public void UpdateDeathCounter(int deathCount) {
        deathCounter.text = "Deaths: " + deathCount.ToString();
    }
}
