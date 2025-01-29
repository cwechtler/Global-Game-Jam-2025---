using UnityEngine;

public class UIHider : MonoBehaviour
{
    [SerializeField] GameObject doublePointCounterGO;
	[SerializeField] GameObject extraWhaleCounterGO;

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
