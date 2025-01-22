using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SetSelectOnEnable : MonoBehaviour
{
	private Button button;
	//private bool controller = false;
	//private string[] names;

	private void update()
	{
		//for (int x = 0; x < names.Length; x++) {
		//	if (names[x].Length == 19 || names[x].Length == 33) {
		//		controller = true;
		//	}
		//}
		EventSystem.current.SetSelectedGameObject(this.gameObject);
	}


	private void OnEnable()
	{
		//names = Input.GetJoystickNames();
		button = GetComponent<Button>();

		//for (int x = 0; x < names.Length; x++) {
		//	if (names[x].Length == 19 || names[x].Length == 33) {
		//		controller = true;
		//	}
		//}
		//if (controller) {		
			button.Select();
			button.OnSelect(null);
		//}
	}
}
