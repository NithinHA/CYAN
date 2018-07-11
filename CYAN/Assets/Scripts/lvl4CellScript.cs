using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class lvl4CellScript : MonoBehaviour
{
	public int ID, color;								//defines unique ID and color of cell
	public Color darkColor, lightColor, cyanColor;		//alternate dark and light color resembling a chessboard, and CYAN for selected cell. 

	[HideInInspector] public int currentCell;			//actual valid cell that you clicked on. Referred in level_manager script.

	GraphicRaycaster m_raycaster;						// used to raycast on graphic objects such as UI>Image
	PointerEventData m_PointerEventData;
	EventSystem m_EventSystem;

	void Start()
	{
		m_raycaster = GetComponent<GraphicRaycaster>();
		m_EventSystem = GetComponent<EventSystem>();
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GetComponentInParent<level4Manager>().displayColor = true;		
			
			m_PointerEventData = new PointerEventData(m_EventSystem);
			m_PointerEventData.position = Input.mousePosition;
			List<RaycastResult> results = new List<RaycastResult>();
			m_raycaster.Raycast(m_PointerEventData, results);

			foreach (RaycastResult result in results)
			{
				int x = result.gameObject.GetComponent<lvl4CellScript>().ID;		//this is the actual cell that you clicked on.
				int y = GetComponentInParent<level4Manager>().curCell;				//previous value of currentCell that is stored in level_manager>curCell.
				if (InRange(x, y))						//checks if the cell that player has clicked on is valid.
					currentCell = x;					
			}
		}
	}

	bool InRange(int x, int y)				//a highly inefficient method that checks if the cell clicked by player is valid.
	{
		if (y == 2 || y == 3 || y == 4)													//validates left cells
		{
			if (Mathf.Abs(y - x) == 1 || y - x == -5)
				return true;
			else
				return false;
		}
		else if (y == 37 || y == 38 || y == 39)											//validates right cells
		{
			if (Mathf.Abs(y - x) == 1 || y - x == 5)
				return true;
			else
				return false;
		}
		else if (y == 6 || y == 11 || y == 16 || y == 21 || y == 26 || y == 31)			//validates up cells
		{
			if (Mathf.Abs(y - x) == 5 || y - x == -1)
				return true;
			else
				return false;
		}
		else if (y == 10 || y == 15 || y == 20 || y == 25 || y == 30 || y == 35)		//validates down cells
		{
			if (Mathf.Abs(y - x) == 5 || y - x == 1)
				return true;
			else
				return false;
		}
		else if (y == 5)											//validates bottom-left corner cell
		{
			if (x == 4 || x == 10)
				return true;
			else
				return false;
		}
		else if (y == 36)											//validates top-right corner cell
		{
			if (x == 31 || x == 37)
				return true;
			else
				return false;
		}
		else
		{
			if (Mathf.Abs(y - x) == 1 || Mathf.Abs(y - x) == 5)		//validates all other remaining cells
				return true;
			else
				return false;
		}
	}

	public void changeColor(int cellColor)					//changes color to dark, light or CYAN
	{
		color = cellColor;
		if (cellColor == 0)
			GetComponent<Image>().color = darkColor;
		else if (cellColor == 1)
			GetComponent<Image>().color = lightColor;
		else if (cellColor == 2)
			GetComponent<Image>().color = cyanColor;
	}

}
