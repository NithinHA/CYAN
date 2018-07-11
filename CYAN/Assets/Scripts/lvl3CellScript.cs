using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class lvl3CellScript : MonoBehaviour {

	public int ID, color;                               //defines unique ID and color of cell
	public Color darkColor, lightColor, cyanColor;      //alternate dark and light color resembling a chessboard, and CYAN for selected cell. 

	[HideInInspector] public int currentCell;           //actual valid cell that you clicked on. Referred in level_manager script.

	GraphicRaycaster m_raycaster;                       // used to raycast on graphic objects such as UI>Image
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
			m_PointerEventData = new PointerEventData(m_EventSystem);
			m_PointerEventData.position = Input.mousePosition;
			List<RaycastResult> results = new List<RaycastResult>();
			m_raycaster.Raycast(m_PointerEventData, results);

			foreach (RaycastResult result in results)
			{
				currentCell = result.gameObject.GetComponent<lvl3CellScript>().ID;		//cell that player clicks on is made as current cell.
			}
		}
	}

	public void changeColor(int cellColor)                  //changes color to dark, light or CYAN
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
