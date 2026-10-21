using System;
using UnityEngine;

namespace LeiaUnity
{
	// Token: 0x0200011E RID: 286
	public static class LeiaDisplayUtils
	{
		// Token: 0x060007DC RID: 2012 RVA: 0x00021E4C File Offset: 0x0002004C
		public static Ray ScreenPointToRay(this LeiaDisplay leiaDisplay, Vector3 screenPosition)
		{
			Camera headCamera = leiaDisplay.HeadCamera;
			bool enabled = headCamera.enabled;
			headCamera.enabled = true;
			Ray result = headCamera.ScreenPointToRay(screenPosition);
			headCamera.enabled = enabled;
			return result;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00021E7C File Offset: 0x0002007C
		public static Vector3 ScreenToWorldPoint(this LeiaDisplay leiaDisplay, Vector3 screenPosition)
		{
			Camera headCamera = leiaDisplay.HeadCamera;
			bool enabled = headCamera.enabled;
			headCamera.enabled = true;
			Vector3 result = headCamera.ScreenToWorldPoint(screenPosition);
			headCamera.enabled = enabled;
			return result;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00021EAC File Offset: 0x000200AC
		public static float GetRecommendedDepthFactorWithFarPlane(LeiaDisplay leiaDisplay, float desiredFarPlane)
		{
			float virtualHeight = leiaDisplay.VirtualHeight;
			float heightMM = leiaDisplay.HeightMM;
			float fovfactor = leiaDisplay.FOVFactor;
			float z = leiaDisplay.ViewersHead.HeadPositionMM.z;
			return Mathf.Abs(6f / ((virtualHeight / (heightMM * fovfactor * desiredFarPlane) - 1f / z) / 0.005f + 1f / z));
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00021F08 File Offset: 0x00020108
		public static float GetRecommendedDepthFactorWithNearPlane(LeiaDisplay leiaDisplay, float desiredNearPlane)
		{
			float virtualHeight = leiaDisplay.VirtualHeight;
			float heightMM = leiaDisplay.HeightMM;
			float fovfactor = leiaDisplay.FOVFactor;
			float z = leiaDisplay.ViewersHead.HeadPositionMM.z;
			return Mathf.Abs(5f / ((virtualHeight / (heightMM * fovfactor * desiredNearPlane) - 1f / z) / 0.004f + 1f / z));
		}
	}
}
