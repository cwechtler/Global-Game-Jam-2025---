using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHider : MonoBehaviour
{
    [SerializeField] GameObject doublePointCounterGO;
	[SerializeField] GameObject extraWhaleCounterGO;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.IsDoublePoints)
        {
            doublePointCounterGO.SetActive(true);
        }
        else {
			doublePointCounterGO.SetActive(false);
		}

		if (GameController.instance.HasExtraWhale)
		{
			extraWhaleCounterGO.SetActive(true);
		}
		else
		{
			extraWhaleCounterGO.SetActive(false);
		}
	}
}
