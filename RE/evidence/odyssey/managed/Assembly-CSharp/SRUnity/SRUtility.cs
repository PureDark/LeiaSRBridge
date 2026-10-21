using System;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using LeiaUnity;
using UnityEngine;

namespace SRUnity
{
	// Token: 0x02000102 RID: 258
	public class SRUtility
	{
		// Token: 0x060006C9 RID: 1737 RVA: 0x0001DA30 File Offset: 0x0001BC30
		public static GameObject FindChildObject(GameObject Parent, string ChildName)
		{
			Transform transform = Parent.transform.Find(ChildName);
			if (transform != null)
			{
				return transform.gameObject;
			}
			return null;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0000238E File Offset: 0x0000058E
		public static void Debug(string str)
		{
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001DA5B File Offset: 0x0001BC5B
		public static void Warning(string str)
		{
			LogUtil.Log(LogLevel.Warning, str, Array.Empty<object>());
			SRUtility.Trace(str);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001DA6F File Offset: 0x0001BC6F
		public static void Trace(string str)
		{
			SRUtility.PrintLog(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bdv() + str);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001DA84 File Offset: 0x0001BC84
		private static void PrintLog(string str)
		{
			object obj = SRUtility.logMutex;
			lock (obj)
			{
				LogUtil.Log(LogLevel.Debug, str, Array.Empty<object>());
			}
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001DACC File Offset: 0x0001BCCC
		public static Vector3 SrToUnityCoords(double x, double y, double z)
		{
			return SRUtility.SrToUnityCoords(new Vector3((float)x, (float)y, (float)z));
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001DADE File Offset: 0x0001BCDE
		public static Vector3 SrToUnityCoords(Vector3 SrCoords)
		{
			return new Vector3(SrCoords.x, SrCoords.y, -SrCoords.z);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001DAF8 File Offset: 0x0001BCF8
		public static void SetSrGameObjectVisibility(GameObject gameObject)
		{
			if (dr.dcn.dci)
			{
				gameObject.hideFlags = 52;
				return;
			}
			gameObject.hideFlags = 61;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001DB18 File Offset: 0x0001BD18
		public static Matrix4x4 CalculateProjection(Vector2 viewportHalfSizeCm, Vector3 eyeLocationScreenSpace, float near, float far)
		{
			if (viewportHalfSizeCm.x <= 0f || viewportHalfSizeCm.y <= 0f)
			{
				return Matrix4x4.identity;
			}
			Vector3 right = Vector3.right;
			Vector3 up = Vector3.up;
			Vector3 back = Vector3.back;
			Vector3 vector;
			vector..ctor(-viewportHalfSizeCm.x, -viewportHalfSizeCm.y, 0f);
			Vector3 vector2;
			vector2..ctor(viewportHalfSizeCm.x, -viewportHalfSizeCm.y, 0f);
			Vector3 vector3 = new Vector3(-viewportHalfSizeCm.x, viewportHalfSizeCm.y, 0f);
			Vector3 vector4 = vector - eyeLocationScreenSpace;
			Vector3 vector5 = vector2 - eyeLocationScreenSpace;
			Vector3 vector6 = vector3 - eyeLocationScreenSpace;
			float num = Vector3.Dot(vector4, back) * -1f;
			float num2 = near / num;
			float num3 = Vector3.Dot(right, vector4) * num2;
			float num4 = Vector3.Dot(right, vector5) * num2;
			float num5 = Vector3.Dot(up, vector4) * num2;
			float num6 = Vector3.Dot(up, vector6) * num2;
			float num7 = 2f * near / (num4 - num3);
			float num8 = 2f * near / (num6 - num5);
			float num9 = (num4 + num3) / (num4 - num3);
			float num10 = (num6 + num5) / (num6 - num5);
			float num11 = -(far + near) / (far - near);
			float num12 = -1f;
			float num13 = -2f * (far * near) / (far - near);
			return new Matrix4x4(new Vector4(num7, 0f, 0f, 0f), new Vector4(0f, num8, 0f, 0f), new Vector4(num9, num10, num11, num12), new Vector4(0f, 0f, num13, 0f));
		}

		// Token: 0x040005A2 RID: 1442
		private static object logMutex = new object();

		// Token: 0x040005A3 RID: 1443
		private static bool logFileStarted;

		// Token: 0x040005A4 RID: 1444
		private static bool logToFile;
	}
}
