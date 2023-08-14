using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using static AuthManager;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GetEnergy : MonoBehaviour
{
    // Start is called before the first frame update
    private static int user;
    private static string Geturl = "https://script.google.com/macros/s/AKfycbwrMVl2Yd7HOfOcQYSgiKw6NQiwFREQ_2jjqaGSmG1yq09Vqc4R4hiHZ0s7pKxV7fU/exec?action=get-user-attributes&user_id=";
    //ubah user jadi string
    private static string user_id;
    private string apiUrl;
    void Awake()
    {
        if(UserManagement.user_id == 0)
        {
            user = 1;
        }
        else
        {
            user = UserManagement.user_id;
        }
        user_id = Convert.ToString(user);

    }
    

    public TMP_Text energyText;

    // Use this for initialization
    void Start()
    {
        apiUrl = Geturl + user_id;
        StartCoroutine(GetRequest(apiUrl));
    }

    private IEnumerator GetRequest(string url)
    {
        using (UnityWebRequest webRequest = new UnityWebRequest(url, "GET"))
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                // Parse the JSON response
                ApiResponse response = JsonUtility.FromJson<ApiResponse>(webRequest.downloadHandler.text);
                if (response.code == 200)
                {
                    energyText.text = response.data.energy.ToString();
                }
                else
                {
                    
                }
            }
            else
            {
                Debug.LogError(webRequest.error);
            }
        }
    }
}

