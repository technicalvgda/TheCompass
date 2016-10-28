using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	public float amplitude;
	public float length;

	/* Starts shaking the camera based on the amplitude variable at a repeated 
	 * rate. Then it will stop the shaking at a certain value based on the length variable. */
	public void shakeCam ()
	{
		InvokeRepeating ("startShake", 0, 0.01f);
		Invoke ("stopShake", length);
	}
		
	/* Depending on the value of amplitude and the value of Random.value, it will add the value
	 * of the equation below to the position of the Camera and HUD */
	public void startShake ()
	{
		if (amplitude > 0) 
		{
			Vector3 shakeCam = transform.localPosition;

			float shakeX = ( Random.value * amplitude * 2 ) - amplitude;
			float shakeY = ( Random.value * amplitude * 2 ) - amplitude;
			shakeCam.x += shakeX;
			shakeCam.y += shakeY;

			transform.position = shakeCam;
		}
	}


	/* Stops the shake method */
	public void stopShake()
	{
		CancelInvoke ( "startShake" );
	}
}
