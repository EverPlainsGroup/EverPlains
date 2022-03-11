using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class for victory or when the player beats the game.
/// </summary>
public class Victory : MonoBehaviour {
  private GameObject player;
  /// <summary>
  /// Start is called before the first frame update
  /// </summary>
  void Start() {
    player = GameObject.FindGameObjectWithTag("Player");
  }

  /// <summary>
  /// Update is called once per frame
  /// </summary>
  void Update() {

  }
  /// <summary>
  /// Function that gets called when the player decides
  /// to restart the game.
  /// </summary>
  public void Restart() {
    SceneManager.LoadScene("Level1");
    GameObject.Destroy(player);
  }
}
