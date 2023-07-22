using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background: MonoBehaviour {
    [SerializeField] Sprite[] backgrounds;
    [SerializeField] float backgroundSpeed = 1;
    
    float startTop;
    float startBottom;
    RectTransform rect;

    // Start is called before the first frame update
    void Start() {
        rect = GetComponent<RectTransform>();
        startTop = -rect.sizeDelta.y;
        startBottom = rect.offsetMin.y;

        // Getting random background
        Sprite background = backgrounds[Random.Range(0, backgrounds.Length)];
        GetComponent<Image>().sprite = background;
    }

    // Update is called once per frame
    void Update() {
        transform.Translate(Vector3.down * Time.deltaTime * backgroundSpeed);

        if(rect.offsetMin.y <= startTop) {
            rect.offsetMax = new Vector2(rect.offsetMax.x, -startTop);
            rect.offsetMin = new Vector2(rect.offsetMin.x, startBottom);
        }
    }
}
