/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Unity Technologies.
 *  Copyright (c) Microsoft Corporation. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System.Collections.Generic;
using System.IO;

namespace Superb.Ide.Antigravity.Editor
{
	internal static class Discovery
	{
		public static IEnumerable<IAntigravityInstallation> GetAntigravityInstallations()
		{
#if UNITY_EDITOR_WIN
			foreach (var installation in AntigravityForWindowsInstallation.GetAntigravityInstallations())
				yield return installation;
#endif

			foreach (var installation in AntigravityCodeInstallation.GetAntigravityInstallations())
				yield return installation;
		}

		public static bool TryDiscoverInstallation(string editorPath, out IAntigravityInstallation installation)
		{
			try
			{
#if UNITY_EDITOR_WIN
				if (AntigravityForWindowsInstallation.TryDiscoverInstallation(editorPath, out installation))
					return true;
#endif
				if (AntigravityCodeInstallation.TryDiscoverInstallation(editorPath, out installation))
					return true;
			}
			catch (IOException)
			{
				installation = null;
			}

			return false;
		}

		public static void Initialize()
		{
#if UNITY_EDITOR_WIN
			AntigravityForWindowsInstallation.Initialize();
#endif
			AntigravityCodeInstallation.Initialize();
		}
	}
}
