using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasketLogic : MonoBehaviour
{
    public int sum;
    private int cntTrue=0;
    public GameObject NextObject;

    private void Awake()
    {
        NextObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("benar"))
        {
            cntTrue++;
            Destroy(other.gameObject); // Hapus objek yang bersentuhan dengan tag "benar"
            if (cntTrue == sum)
            {
                NextObject.SetActive(true);
            }
        }
        else if (other.CompareTag("salah"))
        {
            // Scene akan diulang
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
