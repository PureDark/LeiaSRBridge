using System;
using System.Numerics;
using AiStereoKitWrapper;
using AstraDepth3D.Composition.WindowsRuntimeHelpers;
using AstraDepth3D.EventBus;
using AstraDepth3D.Properties;
using AstraDepth3D.WindowHelper;
using AstraDepth3D.WMI;
using SharpDX;
using SharpDX.Direct3D11;
using Windows.Graphics.Capture;
using Windows.Graphics.DirectX.Direct3D11;
using Windows.UI.Composition;

namespace AstraDepth3D.CaptureSampleCore
{
	// Token: 0x02000055 RID: 85
	public class BasicSampleApplication : IDisposable
	{
		// Token: 0x06000377 RID: 887 RVA: 0x0001C520 File Offset: 0x0001A720
		public BasicSampleApplication(Compositor c)
		{
			this.compositor = c;
			this.device = Direct3D11Helper.CreateDevice();
			this.root = this.compositor.CreateContainerVisual();
			this.root.put_RelativeSizeAdjustment(Vector2.One);
			this.brush = this.compositor.CreateSurfaceBrush();
			this.brush.put_HorizontalAlignmentRatio(0.5f);
			this.brush.put_VerticalAlignmentRatio(0.5f);
			this.brush.put_Stretch(0);
			this.content = this.compositor.CreateSpriteVisual();
			this.content.put_AnchorPoint(new Vector2(0.5f));
			this.content.put_RelativeOffsetAdjustment(new Vector3(0.5f, 0.5f, 0f));
			this.content.put_RelativeSizeAdjustment(Vector2.One);
			this.content.put_Size(new Vector2(0f, 0f));
			this.content.put_Brush(this.brush);
			this.root.Children.InsertAtTop(this.content);
			NLogService.WriteLog("Begin initializing SetreoDetector");
			this.stereoMatcher = new YLStereoMatcher();
			NLogService.WriteLog("Initializing SetreoDetector");
			this.stereoMatcher.Init();
			NLogService.WriteLog("Successfully initialized");
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000378 RID: 888 RVA: 0x0001C684 File Offset: 0x0001A884
		public Visual Visual
		{
			get
			{
				return this.root;
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0001C68C File Offset: 0x0001A88C
		public void Dispose()
		{
			this.StopCapture();
			ContainerVisual containerVisual = this.root;
			if (containerVisual != null)
			{
				containerVisual.Children.RemoveAll();
			}
			ContainerVisual containerVisual2 = this.root;
			if (containerVisual2 != null)
			{
				containerVisual2.Dispose();
			}
			this.root = null;
			SpriteVisual spriteVisual = this.content;
			if (spriteVisual != null)
			{
				spriteVisual.Dispose();
			}
			this.content = null;
			CompositionSurfaceBrush compositionSurfaceBrush = this.brush;
			if (compositionSurfaceBrush != null)
			{
				compositionSurfaceBrush.Dispose();
			}
			this.brush = null;
			IDirect3DDevice direct3DDevice = this.device;
			if (direct3DDevice != null)
			{
				direct3DDevice.Dispose();
			}
			this.device = null;
			this.localPublisher = null;
			this.compositor = null;
			YLStereoMatcher ylstereoMatcher = this.stereoMatcher;
			if (ylstereoMatcher != null)
			{
				ylstereoMatcher.Dispose();
			}
			this.stereoMatcher = null;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0001C73B File Offset: 0x0001A93B
		public void SetEventBus(LocalEventPublisher publisher)
		{
			this.localPublisher = publisher;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001C744 File Offset: 0x0001A944
		public void ResetDevice()
		{
			NLogService.WriteLog("Device maybe Removed,reset device!");
			IDirect3DDevice direct3DDevice = this.device;
			if (direct3DDevice != null)
			{
				direct3DDevice.Dispose();
			}
			this.device = Direct3D11Helper.CreateDevice();
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001C76C File Offset: 0x0001A96C
		public void StartCaptureFromItem(GraphicsCaptureItem item)
		{
			this.StopCapture();
			try
			{
				if (!this.IsValidDevice())
				{
					NLogService.WriteLog("check device:GPU 设备实例已经暂停!");
					this.ResetDevice();
				}
				this.capture = new BasicCaptureAsync(this.device, item, this.stereoMatcher);
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("StartCaptureFromItem error:" + ex.Message);
				LocalEventPublisher localEventPublisher = this.localPublisher;
				if (localEventPublisher != null)
				{
					localEventPublisher.DoSomethingSync("StartInterWave_error", 1);
				}
				return;
			}
			float parax = Settings.Default.stereo_parax;
			parax = Math.Max(Math.Min(parax, 1f), 0.05f);
			float zeroPanel = Settings.Default.zero_panel;
			zeroPanel = Math.Max(Math.Min(zeroPanel, 1f), 0f);
			this.capture.SetParax(parax, zeroPanel);
			this.capture.ResetCurrentInterwaveStatus(this._interwaveStatus);
			if (this.localPublisher != null)
			{
				this.capture.SetEventBus(this.localPublisher);
			}
			ICompositionSurface surface = this.capture.CreateSurface(this.compositor);
			this.brush.put_Surface(surface);
			this.capture.StartCapture();
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001C898 File Offset: 0x0001AA98
		public void StartCaptureFromDXGI()
		{
			this.StopCapture();
			try
			{
				this.capture = new BasicCaptureDXGIAsync(this.stereoMatcher);
			}
			catch (Exception ex)
			{
				NLogService.WriteLog("StartCaptureFromDXGI error:" + ex.Message);
				LocalEventPublisher localEventPublisher = this.localPublisher;
				if (localEventPublisher != null)
				{
					localEventPublisher.DoSomethingSync("StartInterWave_error", 1);
				}
				return;
			}
			float parax = Settings.Default.stereo_parax;
			parax = Math.Max(Math.Min(parax, 1f), 0.05f);
			float zeroPanel = Settings.Default.zero_panel;
			zeroPanel = Math.Max(Math.Min(zeroPanel, 1f), 0f);
			this.capture.SetParax(parax, zeroPanel);
			this.capture.ResetCurrentInterwaveStatus(this._interwaveStatus);
			if (this.localPublisher != null)
			{
				this.capture.SetEventBus(this.localPublisher);
			}
			ICompositionSurface surface = this.capture.CreateSurface(this.compositor);
			this.brush.put_Surface(surface);
			this.capture.StartCapture();
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001C9A4 File Offset: 0x0001ABA4
		public void StopCapture()
		{
			ICapture capture = this.capture;
			if (capture != null)
			{
				capture.Dispose();
			}
			this.capture = null;
			if (this.brush != null)
			{
				this.brush.put_Surface(null);
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001C9D2 File Offset: 0x0001ABD2
		public void SetInterwaveStatus(LocalEnumeration.InterWaveType status)
		{
			this._interwaveStatus = status;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0001C9DB File Offset: 0x0001ABDB
		public void ResetCurrentInterwaveStatus(LocalEnumeration.InterWaveType status)
		{
			this._interwaveStatus = status;
			ICapture capture = this.capture;
			if (capture == null)
			{
				return;
			}
			capture.ResetCurrentInterwaveStatus(status);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001C9F8 File Offset: 0x0001ABF8
		public void FlipInterWaveType()
		{
			if (this.capture != null)
			{
				this._interwaveStatus = this.capture.GetCurrentInterwaveStatus();
				if (this._interwaveStatus == LocalEnumeration.InterWaveType.InterWave_SBS)
				{
					this._interwaveStatus = LocalEnumeration.InterWaveType.InterWave_AI;
				}
				else if (this._interwaveStatus == LocalEnumeration.InterWaveType.InterWave_AI)
				{
					this._interwaveStatus = LocalEnumeration.InterWaveType.InterWave_SBS;
				}
				ICapture capture = this.capture;
				if (capture == null)
				{
					return;
				}
				capture.ResetCurrentInterwaveStatus(this._interwaveStatus);
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001CA56 File Offset: 0x0001AC56
		public void SetParax(float parax, float zeroPanel = 0.5f)
		{
			ICapture capture = this.capture;
			if (capture == null)
			{
				return;
			}
			capture.SetParax(parax, zeroPanel);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001CA6C File Offset: 0x0001AC6C
		private void StartJoystickObj()
		{
			if (this.gamepadController == null)
			{
				this.gamepadController = new GamepadController();
				this.gamepadController.Connect();
				this.gamepadController.JoystickButtonEvent += this._joystickOBJ_JoystickButtonEvent;
				NLogService.WriteLog("=====已连接游戏手柄=====");
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001CAB8 File Offset: 0x0001ACB8
		private void StopJoystickObj()
		{
			if (this.gamepadController != null)
			{
				this.gamepadController.JoystickButtonEvent -= this._joystickOBJ_JoystickButtonEvent;
				this.gamepadController.Disconnect();
				this.gamepadController = null;
				NLogService.WriteLog("BasicCapture|StopJoystickObj");
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001CAF8 File Offset: 0x0001ACF8
		private void _joystickOBJ_JoystickButtonEvent(int KeyNum)
		{
			switch (KeyNum)
			{
			case 1:
			{
				ICapture capture = this.capture;
				if (capture != null && capture.GetCurrentInterwaveStatus() == LocalEnumeration.InterWaveType.InterWave_AI)
				{
					this._parax += 0.1f;
					this._parax = Math.Min(this._parax, 1f);
					ICapture capture2 = this.capture;
					if (capture2 == null)
					{
						return;
					}
					capture2.SetParax(this._parax, this._zeroPanel);
					return;
				}
				break;
			}
			case 2:
			{
				ICapture capture3 = this.capture;
				if (capture3 != null && capture3.GetCurrentInterwaveStatus() == LocalEnumeration.InterWaveType.InterWave_AI)
				{
					this._parax -= 0.1f;
					this._parax = Math.Min(this._parax, 1f);
					ICapture capture4 = this.capture;
					if (capture4 == null)
					{
						return;
					}
					capture4.SetParax(this._parax, this._zeroPanel);
					return;
				}
				break;
			}
			case 3:
				break;
			case 4:
				this.ResetCurrentInterwaveStatus(LocalEnumeration.InterWaveType.InterWave_SBS);
				return;
			default:
				if (KeyNum != 8)
				{
					return;
				}
				this.ResetCurrentInterwaveStatus(LocalEnumeration.InterWaveType.InterWave_AI);
				break;
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0001CBF0 File Offset: 0x0001ADF0
		private bool IsValidDevice()
		{
			bool result;
			using (Device d3dDevice = Direct3D11Helper.CreateSharpDXDevice(this.device))
			{
				if (d3dDevice == null)
				{
					result = false;
				}
				else
				{
					result = (d3dDevice.DeviceRemovedReason == Result.Ok);
				}
			}
			return result;
		}

		// Token: 0x0400030D RID: 781
		private Compositor compositor;

		// Token: 0x0400030E RID: 782
		private ContainerVisual root;

		// Token: 0x0400030F RID: 783
		private SpriteVisual content;

		// Token: 0x04000310 RID: 784
		private CompositionSurfaceBrush brush;

		// Token: 0x04000311 RID: 785
		private IDirect3DDevice device;

		// Token: 0x04000312 RID: 786
		private ICapture capture;

		// Token: 0x04000313 RID: 787
		private float _parax = 0.35f;

		// Token: 0x04000314 RID: 788
		private float _zeroPanel = 0.5f;

		// Token: 0x04000315 RID: 789
		private LocalEnumeration.InterWaveType _interwaveStatus;

		// Token: 0x04000316 RID: 790
		private LocalEventPublisher localPublisher;

		// Token: 0x04000317 RID: 791
		private GamepadController gamepadController;

		// Token: 0x04000318 RID: 792
		private YLStereoMatcher stereoMatcher;
	}
}
