using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
	public void RestartScene()
	{
		CameraController.camBool = true;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Debug.Log("Clicked");
	}


}
