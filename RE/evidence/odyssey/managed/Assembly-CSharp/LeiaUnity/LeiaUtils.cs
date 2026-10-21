using System;
using System.Reflection;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace LeiaUnity
{
	// Token: 0x02000134 RID: 308
	public class LeiaUtils
	{
		// Token: 0x060008B2 RID: 2226 RVA: 0x0002541C File Offset: 0x0002361C
		public static void CopyCameraParameters(Camera sourceCamera, Camera targetCamera)
		{
			if (targetCamera == null)
			{
				return;
			}
			targetCamera.tag = sourceCamera.tag;
			targetCamera.clearFlags = sourceCamera.clearFlags;
			targetCamera.cullingMask = sourceCamera.cullingMask;
			targetCamera.depth = sourceCamera.depth;
			targetCamera.backgroundColor = sourceCamera.backgroundColor;
			targetCamera.orthographic = sourceCamera.orthographic;
			targetCamera.orthographicSize = sourceCamera.orthographicSize;
			targetCamera.fieldOfView = sourceCamera.fieldOfView;
			targetCamera.rect = sourceCamera.rect;
			targetCamera.nearClipPlane = sourceCamera.nearClipPlane;
			targetCamera.farClipPlane = sourceCamera.farClipPlane;
			targetCamera.allowHDR = sourceCamera.allowHDR;
			targetCamera.renderingPath = sourceCamera.renderingPath;
			targetCamera.useOcclusionCulling = sourceCamera.useOcclusionCulling;
			LeiaUtils.CopyComponent<HDAdditionalCameraData>(sourceCamera.GetComponent<HDAdditionalCameraData>(), targetCamera.gameObject);
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x000254F0 File Offset: 0x000236F0
		private static T CopyComponent<T>(T original, GameObject destination) where T : Component
		{
			Type type = original.GetType();
			T t = destination.GetComponent(type) as T;
			if (!t)
			{
				t = (destination.AddComponent(type) as T);
			}
			foreach (FieldInfo fieldInfo in type.GetFields())
			{
				if (!fieldInfo.IsStatic)
				{
					fieldInfo.SetValue(t, fieldInfo.GetValue(original));
				}
			}
			foreach (PropertyInfo propertyInfo in type.GetProperties())
			{
				if (propertyInfo.CanWrite && !(propertyInfo.Name == <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.bje()))
				{
					propertyInfo.SetValue(t, propertyInfo.GetValue(original, null), null);
				}
			}
			return t;
		}
	}
}
