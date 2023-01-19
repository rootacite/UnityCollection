using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject Camera;
    public Animator Tocx;
    public ConstantForce Force;
    
    [Range(0f,10f)]
    public float WalkSpeed = 0.85f;

    private float TargetAngle = 0;
    private float LocalAngle = 0;
        

    IEnumerator Rote()
    {
        while (true)
        {
            if (!(Input.GetKey(KeyCode.W) ||
                  Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
            {
                yield return null;
                continue;
            }

            LocalAngle += (TargetAngle - LocalAngle) / 10f;
            
            var R = transform.rotation;
            R.eulerAngles = new Vector3(
                R.eulerAngles.x,
                Camera.transform.rotation.eulerAngles.y + LocalAngle, 
                R.eulerAngles.z
            );
            transform.rotation = R;
            yield return null;
        }
    }
    
    IEnumerator Jump()
    {
        Force.force = new Vector3(0, 580, 0);
        yield return new WaitForSeconds(0.05f);
        Force.force = new Vector3(0, 0, 0);
        yield break;
    }

    void ForwardStep(float Yaw)
    {
        Vector3 Toward = new Vector3(MathF.Sin(Yaw), 0f, MathF.Cos(Yaw));
        if (state == 2)
            transform.position += Toward / 5f * WalkSpeed;
        else
            transform.position += Toward / 15f * WalkSpeed;
    }

    void XZCelibration()
    {
        var R = transform.rotation;
        R.eulerAngles = new Vector3(
            0,
            R.eulerAngles.y, 
            0
        );
        transform.rotation = R;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rote());
    }

    // Update is called once per frame
    void Update()
    {
        XZCelibration();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Jump());
        }
       
        if (Input.GetKey(KeyCode.W))
        {
            TargetAngle = 0;
           
            ForwardStep( (Camera.transform.rotation.eulerAngles.y + LocalAngle) * 0.017453292f );
            if (Input.GetKey(KeyCode.LeftShift))
                State(2);
            else 
                State(1);
        }else 
        if (Input.GetKey(KeyCode.S))
        {
            TargetAngle = 180;
            ForwardStep( (Camera.transform.rotation.eulerAngles.y + LocalAngle) * 0.017453292f );
            if (Input.GetKey(KeyCode.LeftShift))
                State(2);
            else 
                State(1);
        }else 
        if (Input.GetKey(KeyCode.A))
        { 
            TargetAngle = -90;
            ForwardStep( (Camera.transform.rotation.eulerAngles.y + LocalAngle) * 0.017453292f );
            if (Input.GetKey(KeyCode.LeftShift))
                State(2);
            else 
                State(1);
        }else 
        if (Input.GetKey(KeyCode.D))
        { 
            TargetAngle = 90;
            ForwardStep( (Camera.transform.rotation.eulerAngles.y + LocalAngle)* 0.017453292f );
            if (Input.GetKey(KeyCode.LeftShift))
                State(2);
            else 
                State(1);
        }
        else
        {
            State(0);
        }
    }

    void State(int i)
    {
        state = i;
        switch (i)
        {
            case 0:
                Tocx.SetBool("Walking", false);
                Tocx.SetBool("Runing", false);
                break;
            case 1:
                Tocx.SetBool("Walking", true);
                Tocx.SetBool("Runing", false);
                break;
            case 2:
                Tocx.SetBool("Walking", false);
                Tocx.SetBool("Runing", true);
                break;
            default:
                    break;
        }
    }

    private int state = 0;
}
