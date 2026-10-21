using System;
using System.Collections.Generic;
using <PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41};

// Token: 0x02000041 RID: 65
[Serializable]
public class Game
{
	// Token: 0x0600015D RID: 349 RVA: 0x00008183 File Offset: 0x00006383
	public string jy()
	{
		if (this.Parameters == null)
		{
			return string.Empty;
		}
		return string.Join(<PrivateImplementationDetails>{9F09017B-5ADA-4814-91AE-A0A642A05F41}.a.nb(), this.Parameters);
	}

	// Token: 0x04000168 RID: 360
	public string Id;

	// Token: 0x04000169 RID: 361
	public string Status;

	// Token: 0x0400016A RID: 362
	public GameInfo GameInfo;

	// Token: 0x0400016B RID: 363
	public List<string> Parameters;

	// Token: 0x0400016C RID: 364
	public ImageInfo I_Image;

	// Token: 0x0400016D RID: 365
	public ImageInfo B_Image;

	// Token: 0x0400016E RID: 366
	public ImageInfo R_Thumbnail;

	// Token: 0x0400016F RID: 367
	public ImageInfo S_Thumbnail;

	// Token: 0x04000170 RID: 368
	public ImageInfo ID_Thumbnail;
}
