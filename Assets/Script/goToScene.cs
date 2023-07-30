using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToScene : MonoBehaviour
{
    public SceneAsset sceneNameToLoad;
    
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
        // Menggunakan asset path dari SceneAsset untuk memuat scene
        if (sceneNameToLoad != null)
        {
            string scenePath = AssetDatabase.GetAssetPath(sceneNameToLoad);
            SceneAsset sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
            if (sceneAsset != null)
            {
                string sceneName = sceneAsset.name;
                SceneManager.LoadScene(sceneName);
            }
        }
        else
        {
            Debug.LogError("SceneAsset belum di-set. Seret scene ke variable sceneNameToLoad pada Inspector.");
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
