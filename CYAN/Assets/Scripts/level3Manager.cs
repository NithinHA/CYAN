using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class level3Manager : MonoBehaviour {

	public lvl3CellScript[] allCells;
	public Image indicator;
	public Sprite enableImage, disableImage;

	[HideInInspector] public int curCell;

	bool[] filledCells = new bool[8];                  //checks for win condition

	float startTime;

	void Start () {
		allCells = GetComponentsInChildren<lvl3CellScript>();
		for (int i = 0; i < allCells.Length; i++)
		{
			allCells[i].ID = i + 1;                         //sets unique ID for each cell in sequence
			if (i % 2 == 0)
				allCells[i].changeColor(0);                 //all even cells have dark color
			else
				allCells[i].changeColor(1);                 //all odd cells have light color
		}

		startTime = Time.time;						//start_time is time that program began execution
	}
	
	void Update () {
		if (Time.time - startTime < 1)											//enable_cell_filling function runs repeatedly for 1 second
			enableCellFilling();
		else if (Time.time - startTime >= 1 && Time.time - startTime < 3)       //disable_cell_filling function runs repeatedly for next 2 second
			disableCellFilling();
		else
		{
			startTime = Time.time;							//after every 3 seconds, start_time is changed to current time.
			//Debug.Log(Time.time);
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	void enableCellFilling()
	{
		indicator.sprite = enableImage;

		foreach (lvl3CellScript cell in allCells)
		{
			if (cell.currentCell != 0)			//this means that player has clicked on a cell 
			{
				//Debug.Log(cell.currentCell+"enabled");
				curCell = cell.currentCell;             //value of current_cell in cell_script is stored in variable cur_cell for further reference.
				cell.currentCell = 0;                   //it is done to avoid automatic change of cell color to black when disable occurs.
				filledCells[curCell - 1] = true;		//to keep track of all cells that're being displayed.
				cell.changeColor(2);

				for (int j = 0; j < 8; j++)                         //checks if all values in filled_cells[] are True
				{
					if (!filledCells[j])
						return;
				}
				Invoke("ChangeLevel", 1);
			}
		}
	}
	void disableCellFilling()
	{
		bool wrongClick = false;							//checks if the player has clicked on a cell while it was disabled.
		indicator.sprite = disableImage;

		foreach (lvl3CellScript cell in allCells)			//loop through the cell_script of each cell
		{
			if (cell.currentCell != 0)						//this means player has clicked on that cell 
			{
				//Debug.Log(cell.currentCell+"disabled");
				cell.currentCell = 0;						//it is done to avoid automatic change of cell color to CYAN when enable occurs
				wrongClick = true;
			}
		}
		if (wrongClick)						//if player has clicked while disabled
		{
			foreach (lvl3CellScript cell in allCells)
			{
				if (filledCells[cell.ID - 1])				//if the cell was previously filled/ colored
				{
					//Debug.Log(cell.ID + "disabled");
					curCell = cell.ID;             
					filledCells[curCell - 1] = false;       //removes all the coloured cells
					if ((curCell - 1) % 2 == 0)				//for alternate dark and light color
						cell.changeColor(0);
					else
						cell.changeColor(1);
				}
			}
		}
	}

	void ChangeLevel()
	{
		SceneManager.LoadScene(4);
	}
}
