using System;
using UnityEngine;

// Token: 0x020000E9 RID: 233
[ExecuteInEditMode]
public class LeiaEventCamera : MonoBehaviour
{
	// Token: 0x06000637 RID: 1591 RVA: 0x0001C044 File Offset: 0x0001A244
	private void Start()
	{
		Canvas[] array = Object.FindObjectsOfType<Canvas>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].worldCamera = base.GetComponent<Camera>();
		}
	}
}
