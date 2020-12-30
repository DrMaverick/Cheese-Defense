using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

    // Use this for initialization
    [System.Obsolete]
    void RestartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
	}

}
