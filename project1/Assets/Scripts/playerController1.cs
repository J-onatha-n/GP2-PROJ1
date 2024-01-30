using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //speed is a multiplier used to control actual speed of a player 
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    public void Move() //void means the code is self sufficient, itll run by itself 
    {
        //keycode check - W for up
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed);
        }
    }
}
