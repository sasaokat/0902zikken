using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField] 
    [Tooltip("自動終了の秒数")]
    float seconds = 10.0f;

    void Start()
    {
        Invoke("Quit", seconds);
    }

    void Quit() {
    #if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
    #elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
    #endif
    }
}
