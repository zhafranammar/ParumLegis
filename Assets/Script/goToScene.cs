using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToScene : MonoBehaviour
{
    public string sceneNameToLoad;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(sceneNameToLoad))
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }
        else
        {
            Debug.LogError("Nama scene belum di-set. Masukkan nama scene yang ingin dimuat pada Inspector.");
        }
    }

    public void RestartScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Load ulang scene dengan index yang sama
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void OnMouseDown()
    {
        ChangeScene();
    }
}
