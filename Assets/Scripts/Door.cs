using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for the Door behavior.
/// </summary>
public class Door : MonoBehaviour {
    public string nextLevel;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary> 
    void Start() {

    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary> 
    void Update() {

    }

    /// <summary>
    /// Function that loads the scene when the player
    /// runs into the door.
    /// </summary>
    /// <param name="collision">store the object colliding
    /// into the door</param>
    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Player") {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
