using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class level4Manager : MonoBehaviour
{
	public lvl4CellScript[] allCells;					//array of all the children cells(their cell_scripts)

	bool[] filledCells = new bool[40];					//checks if the player has already clicked the cell

	[HideInInspector] public int curCell = 0;			//used for comparison in cell_script. Currently marked cell to the next cell that can be marked.
	[HideInInspector] public bool displayColor;			//avoids redundant calculations and display on each frame. Once displayed, it is set to false. It is set to true in cell_script after player clicks on a cell. 

	void Start()
	{
		createCell();				//this should've been named as create_panel instead :p	  Creates scene as seen during start of the level.
	}

	private void Update()
	{
		fillCell();					//responsible for rest of gameplay

		if (Input.GetKeyDown(KeyCode.R))			//reset level
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	void createCell()
	{
		allCells = GetComponentsInChildren<lvl4CellScript>();		//loads all 40 children cells' script onto allCells array
		for (int i = 0; i < allCells.Length; i++)
		{
			allCells[i].ID = i + 1;							//sets unique ID for each cell in sequence
			if (i % 2 == 0)
				allCells[i].changeColor(0);					//all even cells have dark color
			else
				allCells[i].changeColor(1);					//all odd cells have light color
		}
		int num = Random.Range(1, 40);
		allCells[num].changeColor(2);						//selects a random cell and colors it CYAN
		filledCells[num] = true;							
		curCell = allCells[num].ID;							//this is the starting cell, and hence initialized to curCell
	}
	
	void fillCell()
	{
		if (displayColor)
		{
			foreach (lvl4CellScript cell in allCells)
			{
				if (cell.currentCell != 0 && !filledCells[cell.currentCell - 1])		//Makes sure that only cells that player has clicked on is displayed && avoids displaying cells on the repeated clicks. 
				{
					curCell = cell.currentCell;				//cell that player has clicked on is made as current cell for further reference.
					filledCells[curCell - 1] = true;
					//Debug.Log(curCell);
					cell.changeColor(2);					//finalizes the cell by changing its color to CYAN

					EndGameCheck();							//checks if all cells are colored CYAN and game has ended
				}
			}
			displayColor = false;							//avoids displaying every frame.
		}
	}

	void EndGameCheck()
	{
		for (int j = 0; j < filledCells.Length; j++)                         //checks if all values in blocksArr[] are True
		{
			if (!filledCells[j])
				return;
		}
		Invoke("ChangeLevel", 1);
	}

	void ChangeLevel()
	{
		SceneManager.LoadScene(5);
	}
}
