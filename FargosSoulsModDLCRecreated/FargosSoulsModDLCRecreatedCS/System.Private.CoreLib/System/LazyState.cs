using System;

namespace System
{
	// Token: 0x02000140 RID: 320
	internal enum LazyState
	{
		// Token: 0x040003F6 RID: 1014
		NoneViaConstructor,
		// Token: 0x040003F7 RID: 1015
		NoneViaFactory,
		// Token: 0x040003F8 RID: 1016
		NoneException,
		// Token: 0x040003F9 RID: 1017
		PublicationOnlyViaConstructor,
		// Token: 0x040003FA RID: 1018
		PublicationOnlyViaFactory,
		// Token: 0x040003FB RID: 1019
		PublicationOnlyWait,
		// Token: 0x040003FC RID: 1020
		PublicationOnlyException,
		// Token: 0x040003FD RID: 1021
		ExecutionAndPublicationViaConstructor,
		// Token: 0x040003FE RID: 1022
		ExecutionAndPublicationViaFactory,
		// Token: 0x040003FF RID: 1023
		ExecutionAndPublicationException
	}
}
