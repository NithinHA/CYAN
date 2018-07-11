using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class level2Manager : MonoBehaviour {

	public Image realBackground;
	Image fill = null;
	int count = 0;
	int xPosition = -432;									//initially points to 1 block behind(left) of screen. During first iteration, incremente by 96 to point at leftmost edge of panel
	bool createNew = true;									//checks if block current block is fixed and new block has to be instantiated
	bool[] blocksArr = new bool[8];                         //checks if a block has already been fixed and also if level is complete 

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	public void onClickNew()
	{
		if (createNew)
		{
			if (xPosition >= 336)									//To loop through the panel 
				xPosition = -336;
			else													//Point to the next position in the panel
				xPosition += 96;

			fill = Instantiate(realBackground, new Vector3(xPosition, 0, 0), Quaternion.identity);
			fill.transform.SetParent(gameObject.transform);			//To make image child of the canvas in order to display it

			createNew = false;
		}
		else												//executes on repeated click on left button
		{
			if (xPosition >= 336)
				xPosition = -336;
			else
				xPosition += 96;

			Debug.Log(xPosition);
			fill.transform.position = new Vector3(xPosition, 0, 0);
		}
	}

	public void onClickFix()
	{
		if (xPosition < -336 || xPosition > 336)		//To overcome the error in the beginning since initial value of xPosition is -432 and it will result in i=-1 --> "Array index out of bound"
			return;
		int i = (xPosition + 336) / 96;					//Reduce the xPosition value from 0-7 in order to use it in blocksArr.

		if (!blocksArr[i])
		{
			blocksArr[i] = true;
			count++;
			createNew = true;
		}
		
		for(int j=0; j<8; j++)							//checks if all values in blocksArr[] are True
		{
			if (!blocksArr[j])
				return;
		}
		Invoke("ChangeLevel", 1);
	}

	void ChangeLevel()
	{
		SceneManager.LoadScene(3);
	}
}
