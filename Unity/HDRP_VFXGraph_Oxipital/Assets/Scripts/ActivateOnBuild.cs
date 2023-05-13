using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnBuild : MonoBehaviour
{
	private void OnEnable()
	{
		if(Application.isEditor)
		{
			this.gameObject.SetActive(false);
		}
	}
}
