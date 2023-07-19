using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager: MonoBehaviour {
    [SerializeField] AudioClip deathGingle;
    AudioSource audioSrc;

    // Start is called before the first frame update
    void Start() {
        deathGingle = Resources.Load<AudioClip>("lolyoudied");

        audioSrc = GetComponent<AudioSource>();
    }

    public void PlaySound(string audio) {
        switch(audio) {
            case "deathGingle": {
                audioSrc.clip = deathGingle;
                audioSrc.Play();
                break;
            }
            default:
                Debug.Log("Sound with ID '" + audio + "' is not setup.");
                break;
        }
    }
}
