using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Navigation;
using \u00A0;

namespace Odyssey3DHub.Dialogs
{
	// Token: 0x0200000B RID: 11
	[NullableContext(1)]
	[Nullable(0)]
	public class AboutDialog : BaseDialog, IComponentConnector
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public AboutDialog()
		{
			Key[] array = new Key[8];
			RuntimeHelpers.InitializeArray(array, fieldof(\u204B.\u00A0).FieldHandle);
			this.keys = array;
			this.bufferLimit = 8;
			base..ctor();
			this.InitializeComponent();
			\u2014.\u00A0(new Action(this.RefreshLanguage));
			this.RefreshLanguage();
			base.PreviewKeyDown += new KeyEventHandler(this.OnPreviewKeyDown);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002B40 File Offset: 0x00000D40
		private void OnPreviewKeyDown(object sender, KeyEventArgs e)
		{
			this.buffer.Add(e.Key);
			if (this.buffer.Count > this.bufferLimit)
			{
				this.buffer.RemoveAt(0);
			}
			if (this.buffer.Count == this.keys.Length && Enumerable.SequenceEqual<Key>(this.buffer, this.keys))
			{
				\u2007.\u2000("--devtool");
				this.buffer.Clear();
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002BBA File Offset: 0x00000DBA
		private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = e.Uri.ToString(),
				UseShellExecute = true
			});
			e.Handled = true;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002BE6 File Offset: 0x00000DE6
		private void Hyperlink_Click(object sender, RoutedEventArgs e)
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = "open source license.txt",
				UseShellExecute = true
			});
			base.Close();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002C0C File Offset: 0x00000E0C
		public void RefreshLanguage()
		{
			FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			string text = AboutDialog.FilterVersion(versionInfo.FileVersion ?? "N/A");
			string gameAssetsVersion = AboutDialog.GetGameAssetsVersion(Path.Combine(baseDirectory, "Odyssey3DHubUI_Data", "StreamingAssets", "GameInfo.json"));
			string moduleVersion = AboutDialog.GetModuleVersion(Path.Combine(baseDirectory, "GameLauncher.Lib.dll"));
			string moduleVersion2 = AboutDialog.GetModuleVersion(Path.Combine(baseDirectory, "Conversion", "Odyssey3DPlayer.dll"));
			string leiaSRVersion = AboutDialog.GetLeiaSRVersion();
			string text2 = "Odyssey 3D Hub";
			string text3 = \u2014.\u1680("TV_SID_DEVCARE_CDJUN_SW_VERSION_INFORMATION");
			string text4 = "Game Data";
			string text5 = "Game Launcher";
			string text6 = "Odyssey 3D Player";
			string text7 = "LeiaSR";
			this.AboutTitle.Text = text2;
			this.AboutMainVersion.Text = text3 + " : v" + text;
			TextBlock aboutModuleVersion = this.AboutModuleVersion;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			defaultInterpolatedStringHandler..ctor(19, 8);
			defaultInterpolatedStringHandler.AppendFormatted(text4);
			defaultInterpolatedStringHandler.AppendLiteral(" : v");
			defaultInterpolatedStringHandler.AppendFormatted(gameAssetsVersion);
			defaultInterpolatedStringHandler.AppendLiteral("\n");
			defaultInterpolatedStringHandler.AppendFormatted(text5);
			defaultInterpolatedStringHandler.AppendLiteral(" : v");
			defaultInterpolatedStringHandler.AppendFormatted(moduleVersion);
			defaultInterpolatedStringHandler.AppendLiteral("\n");
			defaultInterpolatedStringHandler.AppendFormatted(text6);
			defaultInterpolatedStringHandler.AppendLiteral(" : v");
			defaultInterpolatedStringHandler.AppendFormatted(moduleVersion2);
			defaultInterpolatedStringHandler.AppendLiteral("\n");
			defaultInterpolatedStringHandler.AppendFormatted(text7);
			defaultInterpolatedStringHandler.AppendLiteral(" : v");
			defaultInterpolatedStringHandler.AppendFormatted(leiaSRVersion);
			aboutModuleVersion.Text = defaultInterpolatedStringHandler.ToStringAndClear();
			this.LicenseLink.Inlines.Clear();
			this.LicenseLink.Inlines.Add(new Run(\u2014.\u1680("COM_SID_OPEN_SOURCE_LICENSE")));
			this.CancelBtn.Content = \u2014.\u1680("COM_IDS_BTN_CLOSE");
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002DE0 File Offset: 0x00000FE0
		private static string GetModuleVersion(string filePath)
		{
			try
			{
				if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
				{
					string fileVersion = FileVersionInfo.GetVersionInfo(filePath).FileVersion;
					if (!string.IsNullOrEmpty(fileVersion))
					{
						return AboutDialog.FilterVersion(fileVersion);
					}
				}
			}
			catch
			{
			}
			return "N/A";
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002E38 File Offset: 0x00001038
		private static string GetLeiaSRVersion()
		{
			string text = "C:\\Program Files\\LeiaSR\\Platform\\bin\\SRService.exe";
			try
			{
				if (File.Exists(text))
				{
					string fileVersion = FileVersionInfo.GetVersionInfo(text).FileVersion;
					if (!string.IsNullOrEmpty(fileVersion))
					{
						return AboutDialog.FilterSRVersion(fileVersion);
					}
				}
			}
			catch
			{
			}
			return "N/A";
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002E8C File Offset: 0x0000108C
		private static string GetGameAssetsVersion(string filePath)
		{
			try
			{
				if (File.Exists(filePath))
				{
					using (JsonDocument jsonDocument = JsonDocument.Parse(File.ReadAllText(filePath), default(JsonDocumentOptions)))
					{
						JsonElement jsonElement;
						if (jsonDocument.RootElement.TryGetProperty("Version", out jsonElement))
						{
							return jsonElement.GetString() ?? "N/A";
						}
					}
				}
			}
			catch
			{
			}
			return "N/A";
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002F18 File Offset: 0x00001118
		private static string FilterVersion(string version)
		{
			return string.Join(".", Enumerable.Take<string>(version.Split('.', 0), 3));
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002F34 File Offset: 0x00001134
		private static string FilterSRVersion(string version)
		{
			Match match = Regex.Match(version, "v?(\\d+\\.\\d+\\.\\d+)");
			if (!match.Success)
			{
				return version;
			}
			return match.Groups[1].Value;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002F68 File Offset: 0x00001168
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri uri = new Uri("/Odyssey3DHubService;component/src/views/dialogs/aboutdialog.xaml", 2);
			Application.LoadComponent(this, uri);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002F98 File Offset: 0x00001198
		[NullableContext(0)]
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002FA4 File Offset: 0x000011A4
		[NullableContext(0)]
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "9.0.3.0")]
		[EditorBrowsable(1)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.AboutTitle = (TextBlock)target;
				return;
			case 2:
				this.AboutMainVersion = (TextBlock)target;
				return;
			case 3:
				this.AboutModuleVersion = (TextBlock)target;
				return;
			case 4:
				this.LicenseLink = (Hyperlink)target;
				this.LicenseLink.Click += new RoutedEventHandler(this.Hyperlink_Click);
				return;
			case 5:
				this.CancelBtn = (Button)target;
				this.CancelBtn.Click += new RoutedEventHandler(base.CancelBtnClick);
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x0400001B RID: 27
		private readonly List<Key> buffer = new List<Key>();

		// Token: 0x0400001C RID: 28
		private readonly Key[] keys;

		// Token: 0x0400001D RID: 29
		private readonly int bufferLimit;

		// Token: 0x0400001E RID: 30
		[Nullable(0)]
		internal TextBlock AboutTitle;

		// Token: 0x0400001F RID: 31
		[Nullable(0)]
		internal TextBlock AboutMainVersion;

		// Token: 0x04000020 RID: 32
		[Nullable(0)]
		internal TextBlock AboutModuleVersion;

		// Token: 0x04000021 RID: 33
		[Nullable(0)]
		internal Hyperlink LicenseLink;

		// Token: 0x04000022 RID: 34
		[Nullable(0)]
		internal Button CancelBtn;

		// Token: 0x04000023 RID: 35
		private bool _contentLoaded;
	}
}
