using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetSelectIfJoystick : MonoBehaviour
{
	private void Awake()
	{
		string[] joysticks = Input.GetJoystickNames();

		if (joysticks.Length > 0 && !string.IsNullOrEmpty(joysticks[0]))
		{
			EventSystem.current.SetSelectedGameObject(this.gameObject);
			Debug.Log("Joystick Connected: " + joysticks[0]);
		}
	}
	private void Update()
	{
		string[] joysticks = Input.GetJoystickNames();

		if (joysticks.Length > 0 && !string.IsNullOrEmpty(joysticks[0]))
		{
			Debug.Log("Joystick Connected: " + joysticks[0]);
			if (EventSystem.current.currentSelectedGameObject == null)
			{
				EventSystem.current.SetSelectedGameObject(this.gameObject);
			}
		}
		else
		{
			Debug.Log("No Joystick Connected");
			if (EventSystem.current.currentSelectedGameObject != null)
			{
				EventSystem.current.SetSelectedGameObject(null);
			}
		}
	}
}
