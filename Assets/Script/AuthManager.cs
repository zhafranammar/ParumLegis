using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    private const string url = "https://script.google.com/macros/s/AKfycbxi8PHKz3PoPzn5ocvgjs8pAgyRHnstD6Zl9pSOKDJiG3nQJDpzzaU7SM70Fl8HX_s/exec";
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;

    [Serializable]
    public class LoginData
    {
        public string username;
        public string password;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Login()
    {
        string apiAction = "?action=login";

        // Create a JSON object with username and password
        LoginData loginData = new LoginData
        {
            username = emailLoginField.text,
            password = passwordLoginField.text
        };

        string jsonData = JsonUtility.ToJson(loginData);
        string fullUrl = string.Concat(url, apiAction);
        StartCoroutine(PostRequest(fullUrl, jsonData));
    }

    private IEnumerator PostRequest(string url, string jsonData)
    {
        using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Login Successful");
                Debug.Log(bodyRaw);
                Debug.Log(webRequest.downloadHandler.text);
            }
            else
            {
                Debug.Log("Login Failed");
                Debug.LogError(webRequest.error);
            }
        }
    }

}
