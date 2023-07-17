using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {
    string scaleDeathDirection = "up";
    bool showingDeathScreen = false;
    CanvasGroup deathScreenGroup;

    [Header("Death variables")]
    [SerializeField] TextMeshProUGUI deathCounter;
    [SerializeField] RectTransform deathScreen;
    [SerializeField] RectTransform deathText;
    [SerializeField] float deathSmallestScale = 1f;
    [SerializeField] float deathLargestScale = 1.25f;
    [SerializeField] float deathBounceSpeed = 5;
    [SerializeField] float deathScaleSpeed = 2;

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
            if(deathText.localScale.x < 1) {
                float scale = deathText.localScale.x + Time.deltaTime * deathScaleSpeed;
                deathText.localScale = new Vector3(scale, scale, scale);
            } else {
                float scale;
                if(scaleDeathDirection == "up") {
                    scale = deathText.localScale.x + Time.deltaTime / deathBounceSpeed;
                } else {
                    scale = deathText.localScale.x - Time.deltaTime / deathBounceSpeed;
                }
                if(scale > deathLargestScale) {
                    scaleDeathDirection = "down";
                } else if(scale <= deathSmallestScale) {
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
}
