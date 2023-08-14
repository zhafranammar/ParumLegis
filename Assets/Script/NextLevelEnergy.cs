using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static AuthManager;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;

public class NextLevelEnergy : MonoBehaviour
{
    // Start is called before the first frame update
    public class UserData
    {
        public int user_id;
    }
    public void hitApi()
    {
        UserData user = new UserData
        {
            user_id = UserManagement.user_id
        };

        if(UserManagement.user_id == 0)
        {
            user.user_id = 2;
        }

        string jsonData = JsonUtility.ToJson(user);
        string fullUrl = "https://script.google.com/macros/s/AKfycbwrMVl2Yd7HOfOcQYSgiKw6NQiwFREQ_2jjqaGSmG1yq09Vqc4R4hiHZ0s7pKxV7fU/exec?action=next-level";
        StartCoroutine(PostRequest(fullUrl, jsonData));
    }

    // Update is called once per frame
    void Update()
    {
        
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
                Debug.Log("here");
                // Parse the JSON response
                ApiResponse response = JsonUtility.FromJson<ApiResponse>(webRequest.downloadHandler.text);
                if (response.code == 200)
                {

                    //UserManagement.user_id = response.data.user_id;
                    Debug.Log("Berhasil Next Level API");
                }
                else
                {
                    Debug.Log(response.code);
                }
            }
            else
            {
                Debug.LogError(webRequest.error);
            }
        }
    }
}
