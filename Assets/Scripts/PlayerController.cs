using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;

    private void Update()
    {
        if ((Input.GetButton("Jump") || Input.GetButton("Fire1")) && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            //GameObject clone = Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject;    
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);    
            GetComponent<AudioSource>().Play();
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = movement * speed;
          

        rigidbody.position = new Vector3(
            Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax), 
            0.0f, 
            Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
        );

        rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);
    }
}
