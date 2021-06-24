using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace DLavender.Windows
{
	public static class FileBridge
	{

		#region Internal

		private static class Imports
		{
			[DllImport("FileDragBridge.dll")]
			public static extern void AddHook(DragEndCallback callback);

			[DllImport("FileDragBridge.dll")]
			public static extern void RemoveHook();
		}

		private delegate void DragEndCallback(int length, IntPtr arrayPointer);

		[AOT.MonoPInvokeCallback(typeof(DragEndCallback))]
		private static void onBegin(int length, IntPtr arrayPointer)
		{
			var paths = new List<string>(length);

			var arrayResult = new IntPtr[length];
			Marshal.Copy(arrayPointer, arrayResult, 0, length);

			for (int i = 0; i < length; i++)
			{
				string res = Marshal.PtrToStringUni(arrayResult[i]);
				paths.Add(res);
			}

			OnDragFiles?.Invoke(paths);
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void OnDomainReload()
		{
			OnDragFiles = null;
		}

		#endregion

		#region API

		/// <summary>
		/// Register delegates functions here.
		/// </summary>
		public static event OnDragEvent OnDragFiles;

		/// <summary>
		/// Calling convention of delegate.
		/// </summary>
		public delegate void OnDragEvent(List<string> path);

		/// <summary>
		/// Subscribe window messages from win api to grab drag event.
		/// </summary>
		public static void Enable()
		{
#if !UNITY_EDITOR_WIN && UNITY_STANDALONE_WIN
			Imports.AddHook(onBegin);
#elif UNITY_EDITOR_WIN
			Debug.Log("FileBridge doesn't work on Editor Platform");
#endif
		}

		/// <summary>
		/// Unsubscribe to disable drag feature.
		/// </summary>
		public static void Disable()
		{
#if !UNITY_EDITOR_WIN && UNITY_STANDALONE_WIN
			Imports.RemoveHook();
#elif UNITY_EDITOR_WIN
			Debug.Log("FileBridge doesn't work on Editor Platform");
#endif
		}

		#endregion
	}
}