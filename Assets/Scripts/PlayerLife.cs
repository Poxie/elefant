using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class PlayerLife: MonoBehaviour {
    int deathCount;
    bool dead = false;

    Rigidbody2D rb;
    Animator animator;
    PlayerAnimations animationScript;
    PlayerController playerController;
    [SerializeField] SoundManager soundManager;
    UIManager uiManager;

    // Structure for stats json
    class Stats {
        public int deaths;
    }

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        animator = transform.Find("Sprite").GetComponent<Animator>();
        animationScript = transform.Find("Sprite").GetComponent<PlayerAnimations>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        // Fetching death counter from json file
        using (StreamReader reader = new StreamReader(GetStatsPath())) {
            string json = reader.ReadToEnd();
            deathCount = JsonUtility.FromJson<Stats>(json).deaths;
            uiManager.UpdateDeathCounter(deathCount);
        }
    }

    // Update is called once per frame
    void Update() {
        if(
            animator.GetCurrentAnimatorStateInfo(0).IsName("Death") && 
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f
        ) {
            // Fix better system to disable controls/remove sprite
            Destroy(playerController);
            transform.Find("Sprite").gameObject.SetActive(false);
        }

        if(dead && Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    string GetStatsPath() {
        return Application.dataPath + "/Resources/Stats.json";
    }

    void UpdateDeathCounter() {
        Stats stats = new Stats();
        stats.deaths = deathCount + 1;
        
        // Updating the UI with new death count
        uiManager.UpdateDeathCounter(stats.deaths);
    
        // Updating stats json
        FileStream fileStream = new FileStream(GetStatsPath(), FileMode.Create);
        using (StreamWriter writer = new StreamWriter(fileStream)) {
            writer.Write(JsonUtility.ToJson(stats));
        }
    }

    public void Die() {
        UpdateDeathCounter();
        dead = true;
        animationScript.enabled = false;
        animator.Play("Death");
        Destroy(rb);
        uiManager.ShowDeathScreen();
        soundManager.PlaySound("deathGingle");
    }
}
