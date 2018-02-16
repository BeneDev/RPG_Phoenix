using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMe : MonoBehaviour {

	[SerializeField] float xRotationsPerMinute = 1f;
	[SerializeField] float yRotationsPerMinute = 1f;
	[SerializeField] float zRotationsPerMinute = 1f;
	
	void Update () {
        //degrees per frame is written degrees frame ^-1

        float xDegreesPerFrame = ((xRotationsPerMinute/60)*Time.deltaTime) * 360;
        //xDegreesPerFrame = Time.deltaTime / 60 * 360 * xRotationsPerMinute; <- the same as above
        transform.RotateAround (transform.position, transform.right, xDegreesPerFrame);

		float yDegreesPerFrame = ((yRotationsPerMinute / 60) * Time.deltaTime) * 360;
        //yDegreesPerFrame = Time.deltaTime / 60 * 360 * yRotationsPerMinute;
        transform.RotateAround (transform.position, transform.up, yDegreesPerFrame);

        float zDegreesPerFrame = ((zRotationsPerMinute / 60) * Time.deltaTime) * 360;
        //zDegreesPerFrame = Time.deltaTime / 60 * 360 * zRotationsPerMinute;
        transform.RotateAround (transform.position, transform.forward, zDegreesPerFrame);
	}
}
