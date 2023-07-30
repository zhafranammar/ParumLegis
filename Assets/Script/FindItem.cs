using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FindItem : MonoBehaviour
{
    public GameObject NextObject;
    // Start is called before the first frame update
    private void Awake()
    {
        NextObject.SetActive(false);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextObjectSpawn()
    {
        NextObject.SetActive(true);
    }
}
