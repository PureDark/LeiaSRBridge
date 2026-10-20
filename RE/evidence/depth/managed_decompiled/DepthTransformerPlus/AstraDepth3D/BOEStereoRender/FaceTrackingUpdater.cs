using System;

namespace AstraDepth3D.BOEStereoRender
{
	// Token: 0x02000058 RID: 88
	internal class FaceTrackingUpdater
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600039F RID: 927 RVA: 0x0001CC48 File Offset: 0x0001AE48
		// (remove) Token: 0x060003A0 RID: 928 RVA: 0x0001CC80 File Offset: 0x0001AE80
		public event EventHandler<FaceTrackingUpdater.CoordEventArgs> MsgEventHandler;

		// Token: 0x060003A1 RID: 929 RVA: 0x0001CCB5 File Offset: 0x0001AEB5
		protected virtual void OnMyEventAsync(FaceTrackingUpdater.CoordEventArgs e)
		{
			EventHandler<FaceTrackingUpdater.CoordEventArgs> msgEventHandler = this.MsgEventHandler;
			if (msgEventHandler == null)
			{
				return;
			}
			msgEventHandler.BeginInvoke(this, e, null, null);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001CCCC File Offset: 0x0001AECC
		protected virtual void OnMyEventSync(FaceTrackingUpdater.CoordEventArgs e)
		{
			EventHandler<FaceTrackingUpdater.CoordEventArgs> msgEventHandler = this.MsgEventHandler;
			if (msgEventHandler == null)
			{
				return;
			}
			msgEventHandler(this, e);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001CCE0 File Offset: 0x0001AEE0
		public void DoSomethingAsync(float x, float y)
		{
			this.OnMyEventAsync(new FaceTrackingUpdater.CoordEventArgs(x, y));
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001CCEF File Offset: 0x0001AEEF
		public void DoSomethingSync(float x, float y)
		{
			this.OnMyEventSync(new FaceTrackingUpdater.CoordEventArgs(x, y));
		}

		// Token: 0x020000CA RID: 202
		public class CoordEventArgs : EventArgs
		{
			// Token: 0x0600049E RID: 1182 RVA: 0x00020DBA File Offset: 0x0001EFBA
			public CoordEventArgs(float x, float y)
			{
				this._x = x;
				this._y = y;
			}

			// Token: 0x040005C2 RID: 1474
			public float _x;

			// Token: 0x040005C3 RID: 1475
			public float _y;
		}
	}
}
