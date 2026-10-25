char __fastcall BOE_ROI_init(__int64 a1)
{
  int v2; // [rsp+24h] [rbp-44h]
  int v3; // [rsp+24h] [rbp-44h]
  __int64 v4; // [rsp+28h] [rbp-40h]
  __int64 (__fastcall *v5)(__int64, void *, __int64); // [rsp+30h] [rbp-38h]
  __int64 v6; // [rsp+40h] [rbp-28h] BYREF
  __int64 v7; // [rsp+48h] [rbp-20h] BYREF
  _BYTE v8[8]; // [rsp+50h] [rbp-18h] BYREF

  if ( a1 != 0 )
  {
    sub_180001CC0(a1: v8, a2: &unk_180005080); /*0x18000173f*/
    sub_180001270(); /*0x180001744*/
    v6 = 0; /*0x180001749*/
    v7 = 0; /*0x180001752*/
    v5 = *(__int64 (__fastcall **)(__int64, void *, __int64))(*(_QWORD *)a1 + 264LL); /*0x18000176a*/
    v4 = unknown_libname_1(a1: &v6); /*0x180001779*/
    v2 = v5(a1, a2: &unk_180003608, a3: v4); /*0x180001793*/
    if ( v2 >= 0 && v6 != 0 )
    {
      v3 = (**(__int64 (__fastcall ***)(__int64, void *, __int64 *))v6)(a1: v6, a2: &unk_1800035F8, a3: &v7); /*0x1800017f5*/
      if ( v3 >= 0 && v7 != 0 )
      {
        qword_1800056C8 = v6; /*0x180001854*/
        qword_1800056D0 = v7; /*0x180001860*/
        dword_1800056C0 = 0; /*0x180001867*/
        sub_1800011A0(a1: "[BOE_ROI] init returns true: ROI protocol will be detected on first setFaceROI.\n");
        sub_180001CA0(a1: v8); /*0x180001888*/
        return 1; /*0x18000188d*/
      }
      else
      {
        sub_1800011A0(a1: "[BOE_ROI] init returns false: QueryInterface(IKsControl) hr=0x%08lX.\n", v3);
        (*(void (__fastcall **)(__int64))(*(_QWORD *)v6 + 96LL))(a1: v6); /*0x180001825*/
        (*(void (__fastcall **)(__int64))(*(_QWORD *)v6 + 16LL))(a1: v6); /*0x180001835*/
        sub_180001CA0(a1: v8); /*0x180001843*/
        return 0; /*0x180001848*/
      }
    }
    else
    {
      sub_1800011A0(a1: "[BOE_ROI] init returns false: ActivateObject hr=0x%08lX.\n", v2);
      sub_180001CA0(a1: v8); /*0x1800017c1*/
      return 0; /*0x1800017c6*/
    }
  }
  else
  {
    sub_1800011A0(a1: "[BOE_ROI] init returns false: activate is null.\n");
    return 0; /*0x18000172c*/
  }
}