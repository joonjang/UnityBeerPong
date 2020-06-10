using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
	public GameObject panel;
	public void RestartScene()
	{
		Animator animator = panel.GetComponent<Animator>();
		if (animator != null)
		{
			animator.SetBool("Open", false);
		}

		CameraController.camBool = true;
		SwipeScript.touchEnabled = false;
		SwipeScript.GameStart = false;
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		StartCoroutine(RestartDelay());
		Debug.Log("Clicked");
	}

	IEnumerator RestartDelay()
    {
		yield return new WaitForSeconds(2);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}


}
