using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizLogic : MonoBehaviour
{
    public GameObject benar;
    public GameObject salah;
    public Button[] optionButtons; 

    // Start is called before the first frame update
    void Start()
    {
        benar.SetActive(false);
        salah.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckAnswer()
    {
        if (gameObject.tag == "benar")
        {
            benar.SetActive(true);
            DisableOptionButtons();
        }
        else
        {
            salah.SetActive(true);
            DisableOptionButtons();
        }
    }

    private void DisableOptionButtons()
    {
        foreach (Button button in optionButtons)
        {
            button.interactable = false; // Nonaktifkan interaksi dengan button-option
        }
    }
}
