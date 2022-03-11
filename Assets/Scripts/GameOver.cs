using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for game over scenario.
/// Reloads the first level.
/// </summary>
public class GameOver : MonoBehaviour {
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
    /// Restart the game to the first level.
    /// </summary>
    public void Restart() {
        SceneManager.LoadScene("Level1");
    }
}
