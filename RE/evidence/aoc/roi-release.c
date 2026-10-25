__int64 BOE_ROI_release()
{
  _BYTE v1[8]; // [rsp+20h] [rbp-18h] BYREF

  sub_180001CC0(a1: v1, a2: &unk_180005080); /*0x1800018cf*/
  sub_180001270(); /*0x1800018d4*/
  sub_1800011A0(a1: "[BOE_ROI] release completed.\n"); /*0x1800018e0*/
  return sub_180001CA0(a1: v1); /*0x1800018f0*/
}