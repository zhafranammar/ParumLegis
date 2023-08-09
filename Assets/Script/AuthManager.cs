using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    private const string url = "https://script.google.com/macros/s/AKfycbwrMVl2Yd7HOfOcQYSgiKw6NQiwFREQ_2jjqaGSmG1yq09Vqc4R4hiHZ0s7pKxV7fU/exec";
    [Header("Login")]
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;

    [Serializable]
    public class LoginData
    {
        public string username;
        public string password;
    }

    [Serializable]
    public class Data
    {
        public int user_id;
        public int energy;
        public int maxLevel;
    }



    [Serializable]
    public class ApiResponse
    {
        public int code;
        public string message;
        public Data data;
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
                // Parse the JSON response
                ApiResponse response = JsonUtility.FromJson<ApiResponse>(webRequest.downloadHandler.text);
                if (response.code == 200)
                {
                    GetUserAttributes(response.data.user_id);
                    SceneManager.LoadScene("Main");
                }
                else
                {
                    warningLoginText.text = response.message;
                }
            }
            else
            {
                Debug.Log("Login Failed");
                Debug.LogError(webRequest.error);
            }
        }
    }

    public void GetUserAttributes(int userId)
    {
        string apiAction = "?action=get-user-attributes&user_id=" + userId;
        string fullUrl = string.Concat(url, apiAction);

        UnityWebRequest webRequest = new UnityWebRequest(fullUrl, "GET");
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");

        UnityWebRequestAsyncOperation asyncOperation = webRequest.SendWebRequest();
        asyncOperation.completed += (op) =>
        {
            if (!webRequest.isNetworkError && !webRequest.isHttpError)
            {
                ApiResponse response = JsonUtility.FromJson<ApiResponse>(webRequest.downloadHandler.text);
                if (response.code == 200)
                {
                    UserManagement.maxLevel = response.data.maxLevel;
                    UserManagement.energy = response.data.energy;
                }
                else
                {
                    Debug.LogWarning("Gagal mengambil atribut pengguna: " + response.message);
                }
            }
            else
            {
                Debug.LogError("Gagal mengambil atribut pengguna: " + webRequest.error);
            }
        };
    }


}
