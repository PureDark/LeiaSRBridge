_BOOL8 __fastcall BOE_ROI_setFaceROI(int a1, int a2, int a3, int a4)
{
  int v5; // [rsp+3Ch] [rbp-3Ch]
  int v6; // [rsp+40h] [rbp-38h]
  int v7; // [rsp+48h] [rbp-30h]
  int v8; // [rsp+4Ch] [rbp-2Ch]
  _BYTE v9[8]; // [rsp+60h] [rbp-18h] BYREF

  sub_180001CC0(a1: v9, a2: &unk_180005080); /*0x180001941*/
  if ( qword_1800056D0 != 0 )
  {
    if ( a1 <= a3 && a2 <= a4 )
    {
      if ( dword_1800056C0 == 1 )
      {
        v8 = sub_1800014E0(a1: qword_1800056D0, a2: a1, a3: a2, a4: a3, a5: a4); /*0x180001ad2*/
        if ( v8 < 0 ) /*0x180001af9*/
          sub_1800011A0(a1: "[BOE_ROI] setFaceROI cached protocol=Sunplus hr=0x%08lX returns %s.\n", v8, "false"); /*0x180001b25*/
        else
          sub_1800011A0(a1: "[BOE_ROI] setFaceROI cached protocol=Sunplus hr=0x%08lX returns %s.\n", v8, "true"); /*0x180001b07*/
        sub_180001CA0(a1: v9); /*0x180001b39*/
        return v8 >= 0; /*0x180001b3e*/
      }
      else if ( dword_1800056C0 == 2 )
      {
        v7 = sub_1800012C0(a1: qword_1800056D0, a2: a1, a3: a2, a4: a3, a5: a4); /*0x180001a2e*/
        if ( v7 < 0 ) /*0x180001a55*/
          sub_1800011A0(a1: "[BOE_ROI] setFaceROI cached protocol=Realtek hr=0x%08lX returns %s.\n", v7, "false"); /*0x180001a81*/
        else
          sub_1800011A0(a1: "[BOE_ROI] setFaceROI cached protocol=Realtek hr=0x%08lX returns %s.\n", v7, "true"); /*0x180001a63*/
        sub_180001CA0(a1: v9); /*0x180001a95*/
        return v7 >= 0; /*0x180001a9a*/
      }
      else
      {
        v5 = sub_1800014E0(a1: qword_1800056D0, a2: a1, a3: a2, a4: a3, a5: a4); /*0x180001b76*/
        if ( v5 < 0 )
        {
          v6 = sub_1800012C0(a1: qword_1800056D0, a2: a1, a3: a2, a4: a3, a5: a4); /*0x180001be3*/
          if ( v6 < 0 )
          {
            sub_1800011A0(
              a1: "[BOE_ROI] setFaceROI returns false: Sunplus hr=0x%08lX, Realtek hr=0x%08lX, protocol not cached.\n",
              v5,
              v6);
            sub_180001CA0(a1: v9); /*0x180001c44*/
            return false; /*0x180001c49*/
          }
          else
          {
            dword_1800056C0 = 2; /*0x180001bee*/
            sub_1800011A0( /*0x180001c08*/
              a1: "[BOE_ROI] setFaceROI probe Sunplus hr=0x%08lX failed, Realtek hr=0x%08lX returns true, cached Realtek.\n",
              v5,
              v6);
            sub_180001CA0(a1: v9); /*0x180001c18*/
            return true; /*0x180001c1d*/
          }
        }
        else
        {
          dword_1800056C0 = 1; /*0x180001b81*/
          sub_1800011A0( /*0x180001b96*/
            a1: "[BOE_ROI] setFaceROI probe protocol=Sunplus hr=0x%08lX returns true, cached Sunplus.\n",
            v5);
          sub_180001CA0(a1: v9); /*0x180001ba6*/
          return true; /*0x180001bab*/
        }
      }
    }
    else
    {
      sub_1800011A0(
        a1: "[BOE_ROI] setFaceROI returns false: invalid rect left=%d top=%d right=%d bottom=%d.\n",
        a1,
        a2,
        a3,
        a4);
      sub_180001CA0(a1: v9); /*0x1800019d0*/
      return false; /*0x1800019d5*/
    }
  }
  else
  {
    sub_1800011A0(a1: "[BOE_ROI] setFaceROI returns false: BOE_ROI_init has not succeeded.\n");
    sub_180001CA0(a1: v9); /*0x180001968*/
    return false; /*0x18000196d*/
  }
}