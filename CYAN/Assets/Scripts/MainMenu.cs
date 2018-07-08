using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void StartGame() {
		SceneManager.LoadScene(1);
	}

	public void Options() {
		SceneManager.LoadScene("options");
	}
	
	public void Info() {
		SceneManager.LoadScene("info");
	}

	public void BackToMenu()
	{
		SceneManager.LoadScene("Menu");
	}
}
