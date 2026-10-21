using System;
using UnityEngine;

namespace LeiaUnity
{
	// Token: 0x02000131 RID: 305
	public static class LeiaCameraBounds
	{
		// Token: 0x06000899 RID: 2201 RVA: 0x00024A70 File Offset: 0x00022C70
		public static LeiaCameraData ComputeLeiaCameraData(Camera camera, float convergenceDistance, float baselineScaling)
		{
			LeiaCameraData result = default(LeiaCameraData);
			float num = (float)1280;
			float num2 = 4f;
			float num3 = (float)Screen.width / (float)Screen.height;
			float num4 = num / 2f / Mathf.Tan(camera.fieldOfView * 3.1415927f / 360f);
			result.baseline = num2 * baselineScaling * convergenceDistance / num4;
			result.screenHalfHeight = convergenceDistance * Mathf.Tan(camera.fieldOfView * 3.1415927f / 360f);
			result.screenHalfWidth = num3 * result.screenHalfHeight;
			result.baselineScaling = baselineScaling;
			return result;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00024B08 File Offset: 0x00022D08
		public static LeiaBoundsData ComputeLeiaBoundsData(Camera camera, LeiaCameraData leiaCamera, float convergenceDistance, Vector2 cameraShift)
		{
			LeiaBoundsData result = default(LeiaBoundsData);
			Matrix4x4 localToWorldMatrix = camera.transform.localToWorldMatrix;
			localToWorldMatrix.SetColumn(0, localToWorldMatrix.GetColumn(0).normalized);
			localToWorldMatrix.SetColumn(1, localToWorldMatrix.GetColumn(1).normalized);
			localToWorldMatrix.SetColumn(2, localToWorldMatrix.GetColumn(2).normalized);
			Rect rect = camera.rect;
			rect.yMax = Mathf.Min(rect.yMax, 1f);
			rect.yMin = Mathf.Max(rect.yMin, 0f);
			rect.xMax = Mathf.Min(rect.xMax, 1f);
			rect.xMin = Mathf.Max(rect.xMin, 0f);
			float height = rect.height;
			float num = rect.width / height;
			float num2 = 1.6f;
			if (camera.orthographic)
			{
				float orthographicSize = camera.orthographicSize;
				float num3 = -(orthographicSize * num2) * num;
				float num4 = -num3;
				Vector3 vector = localToWorldMatrix.MultiplyPoint(new Vector3(num3, orthographicSize, convergenceDistance));
				Vector3 vector2 = localToWorldMatrix.MultiplyPoint(new Vector3(num4, orthographicSize, convergenceDistance));
				Vector3 vector3 = localToWorldMatrix.MultiplyPoint(new Vector3(num3, -orthographicSize, convergenceDistance));
				Vector3 vector4 = localToWorldMatrix.MultiplyPoint(new Vector3(num4, -orthographicSize, convergenceDistance));
				float num5 = convergenceDistance - 1f / leiaCamera.baselineScaling;
				Vector3 vector5 = localToWorldMatrix.MultiplyPoint(new Vector3(num3, orthographicSize, num5));
				Vector3 vector6 = localToWorldMatrix.MultiplyPoint(new Vector3(num4, orthographicSize, num5));
				Vector3 vector7 = localToWorldMatrix.MultiplyPoint(new Vector3(num3, -orthographicSize, num5));
				Vector3 vector8 = localToWorldMatrix.MultiplyPoint(new Vector3(num4, -orthographicSize, num5));
				float num6 = convergenceDistance + 1f / leiaCamera.baselineScaling;
				Vector3 vector9 = localToWorldMatrix.MultiplyPoint(new Vector3(num3, orthographicSize, num6));
				Vector3 vector10 = localToWorldMatrix.MultiplyPoint(new Vector3(num4, orthographicSize, num6));
				Vector3 vector11 = localToWorldMatrix.MultiplyPoint(new Vector3(num3, -orthographicSize, num6));
				Vector3 vector12 = localToWorldMatrix.MultiplyPoint(new Vector3(num4, -orthographicSize, num6));
				result.screen = new Vector3[]
				{
					vector,
					vector2,
					vector4,
					vector3
				};
				result.south = new Vector3[]
				{
					vector5,
					vector6,
					vector8,
					vector7
				};
				result.north = new Vector3[]
				{
					vector9,
					vector10,
					vector12,
					vector11
				};
				result.top = new Vector3[]
				{
					vector5,
					vector6,
					vector10,
					vector9
				};
				result.bottom = new Vector3[]
				{
					vector7,
					vector8,
					vector12,
					vector11
				};
				result.east = new Vector3[]
				{
					vector6,
					vector8,
					vector12,
					vector10
				};
				result.west = new Vector3[]
				{
					vector5,
					vector7,
					vector11,
					vector9
				};
			}
			else
			{
				float screenHalfHeight = leiaCamera.screenHalfHeight;
				float num7 = -leiaCamera.screenHalfWidth * num;
				float num8 = -num7;
				cameraShift = leiaCamera.baseline * cameraShift;
				Vector3 vector13 = localToWorldMatrix.MultiplyPoint(new Vector3(num7, screenHalfHeight, convergenceDistance));
				Vector3 vector14 = localToWorldMatrix.MultiplyPoint(new Vector3(num8, screenHalfHeight, convergenceDistance));
				Vector3 vector15 = localToWorldMatrix.MultiplyPoint(new Vector3(num7, -screenHalfHeight, convergenceDistance));
				Vector3 vector16 = localToWorldMatrix.MultiplyPoint(new Vector3(num8, -screenHalfHeight, convergenceDistance));
				float num9 = leiaCamera.baselineScaling * convergenceDistance / (leiaCamera.baselineScaling + 1f);
				float num10 = num9 / convergenceDistance;
				float num11 = 1f - num10;
				Bounds bounds;
				bounds..ctor(new Vector3(num11 * cameraShift.x, num11 * cameraShift.y, num9), new Vector3(num8 * num10 * 2f, leiaCamera.screenHalfHeight * num10 * 2f, 0f));
				Vector3 vector17 = localToWorldMatrix.MultiplyPoint(new Vector3(bounds.min.x, bounds.max.y, bounds.center.z));
				Vector3 vector18 = localToWorldMatrix.MultiplyPoint(new Vector3(bounds.max.x, bounds.max.y, bounds.center.z));
				Vector3 vector19 = localToWorldMatrix.MultiplyPoint(new Vector3(bounds.min.x, bounds.min.y, bounds.center.z));
				Vector3 vector20 = localToWorldMatrix.MultiplyPoint(new Vector3(bounds.max.x, bounds.min.y, bounds.center.z));
				float num12 = leiaCamera.baselineScaling * convergenceDistance / (leiaCamera.baselineScaling - 1f);
				num12 = 1f / Mathf.Max(1f / num12, 1E-05f);
				float num13 = num12 / convergenceDistance;
				float num14 = 1f - num13;
				Bounds bounds2;
				bounds2..ctor(new Vector3(num14 * cameraShift.x, num14 * cameraShift.y, num12), new Vector3(num8 * num13 * 2f, screenHalfHeight * num13 * 2f, 0f));
				Vector3 vector21 = localToWorldMatrix.MultiplyPoint(new Vector3(bounds2.min.x, bounds2.max.y, bounds2.center.z));
				Vector3 vector22 = localToWorldMatrix.MultiplyPoint(new Vector3(bounds2.max.x, bounds2.max.y, bounds2.center.z));
				Vector3 vector23 = localToWorldMatrix.MultiplyPoint(new Vector3(bounds2.min.x, bounds2.min.y, bounds2.center.z));
				Vector3 vector24 = localToWorldMatrix.MultiplyPoint(new Vector3(bounds2.max.x, bounds2.min.y, bounds2.center.z));
				result.screen = new Vector3[]
				{
					vector13,
					vector14,
					vector16,
					vector15
				};
				result.south = new Vector3[]
				{
					vector17,
					vector18,
					vector20,
					vector19
				};
				result.north = new Vector3[]
				{
					vector21,
					vector22,
					vector24,
					vector23
				};
				result.top = new Vector3[]
				{
					vector17,
					vector18,
					vector22,
					vector21
				};
				result.bottom = new Vector3[]
				{
					vector19,
					vector20,
					vector24,
					vector23
				};
				result.east = new Vector3[]
				{
					vector18,
					vector20,
					vector24,
					vector22
				};
				result.west = new Vector3[]
				{
					vector17,
					vector19,
					vector23,
					vector21
				};
			}
			return result;
		}

		// Token: 0x0400069B RID: 1691
		private static readonly Color _leiaScreenPlaneColor = new Color(0.078431375f, 0.39215687f, 0.627451f, 0.2f);

		// Token: 0x0400069C RID: 1692
		private static readonly Color _leiaScreenWireColor = new Color(0.13725491f, 0.78431374f, 1f, 0.6f);

		// Token: 0x0400069D RID: 1693
		private static readonly Color _leiaBoundsPlaneColor = new Color(1f, 1f, 1f, 0.1f);

		// Token: 0x0400069E RID: 1694
		private static readonly Color _leiaBoundsWireColor = new Color(1f, 1f, 1f, 0.2f);
	}
}
