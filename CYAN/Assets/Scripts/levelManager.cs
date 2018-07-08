using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelManager : MonoBehaviour {

	public Image realBackground;
	int count = 0;								//Checks if level is complete
	int xPosition = -336;

	public void onClick()
	{
		if(count >= 7)
		{
			Image fill = Instantiate(realBackground, new Vector3(xPosition, 0, 0), Quaternion.identity);
			fill.transform.SetParent(gameObject.transform);
			Invoke("ChangeLevel", 1);
		}
		else
		{
			Image fill = Instantiate(realBackground, new Vector3(xPosition, 0, 0), Quaternion.identity);
			fill.transform.SetParent(gameObject.transform);
			xPosition += 96;
			count++;
		}
	}

	void ChangeLevel()
	{
		SceneManager.LoadScene(2);
	}
}
