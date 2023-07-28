using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    private Plane dragPlane;
    private Vector3 offset;
    private Camera myMainCamera;
    private Vector3 initialPosition; // Posisi awal sebelum drag

    void Start()
    {
        myMainCamera = Camera.main;
        initialPosition = transform.position; // Simpan posisi awal saat Start
    }

    void OnMouseDown()
    {
        dragPlane = new Plane(myMainCamera.transform.forward, transform.position);
        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        offset = transform.position - camRay.GetPoint(planeDist);
    }

    void OnMouseDrag()
    {
        Ray camRay = myMainCamera.ScreenPointToRay(Input.mousePosition);

        float planeDist;
        dragPlane.Raycast(camRay, out planeDist);
        transform.position = camRay.GetPoint(planeDist) + offset;
    }

    void OnMouseUp()
    {
        // Kembali ke posisi awal saat mouse diangkat
        transform.position = initialPosition;
    }
}
