using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public TextMeshProUGUI Debug1;

    private Vector2 Angle = new Vector2(0,0);
    private float Distance = 4.6f;
    public GameObject Target;
    
    public Vector2 Offset=new Vector2(0,1.23f);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = Input.GetKey(KeyCode.LeftAlt);
        Cursor.lockState = Input.GetKey(KeyCode.LeftAlt) ? CursorLockMode.None : CursorLockMode.Locked;
        float ax = Input.GetAxis("Mouse X");
        float ay = Input.GetAxis("Mouse Y");

        if (Input.GetMouseButton(2))
        {
            Offset.x -= ax / 10f;
            Offset.y -= ay / 10f;

            ax = 0;
            ay = 0;
        }

        if (Angle.y - (float)(ay / 180.0 * Math.PI * 1.25) >= Mathf.PI / 2 ||
            Angle.y - (float)(ay / 180.0 * Math.PI * 1.25) <= -Mathf.PI / 2) return;
        
        Angle.x -= (float)(ax / 180.0 * Math.PI * 1.25);
        Angle.y -= (float)(ay / 180.0 * Math.PI * 1.25);


        Angle.x %= Mathf.PI * 2;
        Angle.y %= Mathf.PI * 2;
        
        Distance -= Input.mouseScrollDelta.y / 10f;

        Vector3 newPos = new Vector3(
            MathF.Cos(Angle.x) * MathF.Cos(Angle.y),
            MathF.Sin(Angle.y),
            MathF.Sin(Angle.x) * MathF.Cos(Angle.y)
           );
        
        Debug1.text = Angle.ToString() + ";" + newPos.ToString();
        
        newPos *= Distance;
        transform.position = Target.transform.position + newPos + new Vector3(0,Offset.y,Offset.x);


        transform.rotation = Quaternion.LookRotation(-newPos.normalized);

    }
}
