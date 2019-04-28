using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restartButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        Destroy(GameObject.Find("GameMusic"));
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene(sceneName: "DetermineLifePath");
    }
}
