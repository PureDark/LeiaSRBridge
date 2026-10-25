__int64 sub_14000D9A0()
{
  int v0; // edi
  __int64 v1; // rax
  __int64 v3; // rax
  unsigned int v4; // eax
  int v5; // ecx
  __int64 v6; // r8
  __int64 v7; // r8
  __int64 v8; // r8
  __int64 v9; // rax
  __m128i v10; // xmm7
  __int64 v11; // xmm6_8^8
  __m128i v12; // xmm10
  __m128i v13; // xmm8
  __m128i si128; // xmm9
  int v15; // ebx
  int v16; // ecx
  int v17; // edx
  DWORD TickCount; // eax
  int v19; // ebx
  int v20; // ecx
  __int64 v21; // rax
  __int64 v22; // rax
  int v23; // [rsp+48h] [rbp-C0h]
  int v24; // [rsp+48h] [rbp-C0h]
  int v25; // [rsp+58h] [rbp-B0h] BYREF
  __int32 v26; // [rsp+5Ch] [rbp-ACh]
  __int128 *v27; // [rsp+60h] [rbp-A8h]
  __int64 v28; // [rsp+68h] [rbp-A0h]
  __int128 v29; // [rsp+78h] [rbp-90h] BYREF
  __int128 v30; // [rsp+88h] [rbp-80h]
  __m128i v31; // [rsp+98h] [rbp-70h] BYREF
  __int128 v32; // [rsp+A8h] [rbp-60h]
  __int64 v33; // [rsp+B8h] [rbp-50h]
  __int64 v34; // [rsp+C0h] [rbp-48h] BYREF
  __int64 v35; // [rsp+C8h] [rbp-40h] BYREF
  void *v36; // [rsp+D0h] [rbp-38h] BYREF
  __int128 v37; // [rsp+D8h] [rbp-30h] BYREF
  __int128 v38; // [rsp+E8h] [rbp-20h]
  __int128 v39; // [rsp+F8h] [rbp-10h]
  __int128 v40; // [rsp+108h] [rbp+0h]
  __int128 v41; // [rsp+118h] [rbp+10h]
  __int128 v42; // [rsp+128h] [rbp+20h]
  int v43; // [rsp+138h] [rbp+30h] BYREF
  int v44; // [rsp+13Ch] [rbp+34h] BYREF
  int v45; // [rsp+140h] [rbp+38h] BYREF
  __int64 v46; // [rsp+148h] [rbp+40h] BYREF
  int v47; // [rsp+158h] [rbp+50h] BYREF
  __m128i v48; // [rsp+17Ch] [rbp+74h]
  __m128i v49; // [rsp+18Ch] [rbp+84h]
  __int64 v50; // [rsp+1ACh] [rbp+A4h]
  __int64 v51; // [rsp+1B4h] [rbp+ACh]
  __m128i v52; // [rsp+1D4h] [rbp+CCh]
  __int64 v53; // [rsp+1F4h] [rbp+ECh]
  char Buffer[128]; // [rsp+208h] [rbp+100h] BYREF

  if ( qword_1400506F0 == 0 ) /*0x14000d9f0*/
    return 2147500037LL; /*0x14000d9f0*/
  v35 = 0; /*0x14000d9f8*/
  v44 = 0; /*0x14000d9fc*/
  v43 = 0; /*0x14000d9ff*/
  v46 = 0; /*0x14000da02*/
  v0 = (*(__int64 (__fastcall **)(__int64, __int64, _QWORD, int *, int *, __int64 *, __int64 *))(*(_QWORD *)qword_1400506F0 /*0x14000da33*/
                                                                                               + 72LL))(
         a1: qword_1400506F0,
         a2: 4294967292LL,
         a3: 0,
         a4: &v44,
         a5: &v43,
         a6: &v46,
         a7: &v35);
  if ( v0 < 0 ) /*0x14000da37*/
  {
LABEL_34:
    if ( v35 != 0 ) /*0x14000e305*/
      (*(void (__fastcall **)(__int64))(*(_QWORD *)v35 + 16LL))(a1: v35); /*0x14000e30a*/
    return (unsigned int)v0; /*0x14000e30a*/
  }
  if ( v35 != 0 )
  {
    v34 = 0; /*0x14000da4a*/
    v36 = nullptr; /*0x14000da4e*/
    v45 = 0; /*0x14000da52*/
    v0 = (*(__int64 (__fastcall **)(__int64, __int64 *))(*(_QWORD *)v35 + 328LL))(a1: v35, a2: &v34); /*0x14000da62*/
    if ( v0 >= 0 ) /*0x14000da66*/
      v0 = (*(__int64 (__fastcall **)(__int64, void **, _QWORD, int *))(*(_QWORD *)v34 + 24LL))( /*0x14000da7d*/
             a1: v34,
             a2: &v36,
             a3: 0,
             a4: &v45);
    v37 = 0; /*0x14000da82*/
    v38 = 0; /*0x14000da86*/
    v39 = 0; /*0x14000da8a*/
    v40 = 0; /*0x14000da8e*/
    v41 = 0; /*0x14000da92*/
    v42 = 0; /*0x14000da96*/
    cv::Mat::Mat(this: (cv::Mat *)&v37); /*0x14000da9e*/
    __eh34_enter_wind_state(-1, 0); /*0xf1c000000000009c*/
    if ( v0 >= 0 )
    {
      if ( memcmp(Buf1: &xmmword_1400506D0, Buf2: "NV12", Size: 0x10u) == 0 ) /*0x14000dac8*/
      {
        v1 = cv::Mat::Mat(this: (cv::Mat *)Buffer, a2: dword_1400506B0, a3: dword_1400506A8, a4: 0, a5: v36, a6: 0); /*0x14000daef*/
        __wind /*0xf1c0000000000090*/
        {
          cv::Mat::operator=(a1: &v37, a2: v1); /*0x14000dafd*/
        }
        __unwind
        {
          cv::Mat::~Mat(this: (cv::Mat *)Buffer); /*0xf1c0000000000088*/
        }
        cv::Mat::~Mat(this: (cv::Mat *)Buffer); /*0x14000db0b*/
      }
      if ( cv::Mat::empty(this: (cv::Mat *)&v37) ) /*0x14000db15*/
      {
        if ( __eh34_unwind(0) ) /*0xf1c0000000000078*/
          goto unwind_state_0; /*0xf1c0000000000078*/
        __eh34_exit_wind_state(0, -1); /*0xf1c000000000007c*/
        cv::Mat::~Mat(this: (cv::Mat *)&v37); /*0x14000db23*/
        return 2147500037LL; /*0x14000db2e*/
      }
      if ( (unsigned int)doEyeTrack_3D( /*0x14000db61*/
                           a1: v38,
                           a2: HIDWORD(v37),
                           a3: DWORD2(v37),
                           a4: (((int)v37 >> 3) & 0x1FFu) + 1,
                           a5: &v47) == 0 )
      {
        v3 = sub_140012730(a1: std::cout, a2: &unk_14003E8A8); /*0x14000db71*/
        std::ostream::operator<<(a1: v3, a2: sub_140012BC0); /*0x14000db80*/
      }
      (*(void (__fastcall **)(LPVOID))(*(_QWORD *)ppv + 80LL))(a1: ppv); /*0x14000dbb3*/
      v4 = 3 * (dword_140050720 / 3u); /*0x14000dbc7*/
      v5 = dword_140050720++; /*0x14000dbca*/
      if ( v5 == v4 ) /*0x14000dbd9*/
        BOE_ROI_setFaceROI(); /*0x14000dc0b*/
      sub_140015AD0(Buffer, Format: "midX: %.2f");
      v29 = 0x406FE00000000000uLL; /*0x14000dc39*/
      v30 = 0; /*0x14000dc42*/
      v31 = 0; /*0x14000dc59*/
      v32 = 0; /*0x14000dc60*/
      v6 = -1; /*0x14000dc6c*/
      do /*0x14000dc7b*/
        ++v6; /*0x14000dc73*/
      while ( Buffer[v6] != 0 ); /*0x14000dc7b*/
      sub_140003D00(a1: &v31, a2: Buffer); /*0x14000dc88*/
      __eh34_enter_wind_state(0, 2); /*0xf1c0000000000070*/
      v28 = 0; /*0x14000dc8e*/
      v25 = 50397184; /*0x14000dc93*/
      v27 = &v37; /*0x14000dc9f*/
      cv::putText( /*0x14000dce2*/
        a1: &v25,
        a2: &v31,
        a3: 0x1E0000000ALL,
        a4: 0,
        a5: 0x3FE6666666666666LL,
        a6: &v29,
        a7: 2,
        a8: 8,
        a9: 0);
      if ( __eh34_unwind(2) ) /*0xf1c0000000000064*/
      {
unwind_state_2:
        sub_1400029D0(a1: &v31); /*0xf1c000000000001c*/
        __eh34_continue_unwinding(2, 0); /*0xf1c0000000000020*/
      }
      __eh34_exit_wind_state(2, 0); /*0xf1c0000000000068*/
      sub_140003EE0(a1: &v31); /*0x14000dced*/
      sub_140015AD0(Buffer, Format: "midY: %.2f");
      v31 = (__m128i)0x406FE00000000000uLL; /*0x14000dd12*/
      v32 = 0; /*0x14000dd17*/
      v29 = 0; /*0x14000dd2d*/
      v30 = 0; /*0x14000dd35*/
      v7 = -1; /*0x14000dd41*/
      do /*0x14000dd58*/
        ++v7; /*0x14000dd50*/
      while ( Buffer[v7] != 0 ); /*0x14000dd58*/
      sub_140003D00(a1: &v29, a2: Buffer); /*0x14000dd66*/
      __eh34_enter_wind_state(0, 3); /*0xf1c000000000005c*/
      v28 = 0; /*0x14000dd6c*/
      v25 = 50397184; /*0x14000dd71*/
      v27 = &v37; /*0x14000dd7d*/
      LOBYTE(v23) = 0; /*0x14000dd82*/
      cv::putText( /*0x14000ddb7*/
        a1: &v25,
        a2: &v29,
        a3: 0x3C0000000ALL,
        a4: 0,
        a5: 0x3FE6666666666666LL,
        a6: &v31,
        a7: 2,
        a8: 8,
        a9: v23);
      if ( __eh34_unwind(3) ) /*0xf1c0000000000050*/
      {
unwind_state_3:
        sub_1400029D0(a1: &v29); /*0xf1c0000000000038*/
        __eh34_continue_unwinding(3, 0); /*0xf1c000000000003c*/
      }
      __eh34_exit_wind_state(3, 0); /*0xf1c0000000000054*/
      sub_140003EE0(a1: &v29); /*0x14000ddc3*/
      sub_140015AD0(Buffer, Format: "midZ: %.2f");
      v31 = (__m128i)0x406FE00000000000uLL; /*0x14000dde8*/
      v32 = 0; /*0x14000dded*/
      v29 = 0; /*0x14000de03*/
      v30 = 0; /*0x14000de0b*/
      v8 = -1; /*0x14000de17*/
      do /*0x14000de28*/
        ++v8; /*0x14000de20*/
      while ( Buffer[v8] != 0 ); /*0x14000de28*/
      sub_140003D00(a1: &v29, a2: Buffer); /*0x14000de36*/
      __eh34_enter_wind_state(0, 3); /*0xf1c0000000000048*/
      v28 = 0; /*0x14000de3c*/
      v25 = 50397184; /*0x14000de41*/
      v27 = &v37; /*0x14000de4d*/
      LOBYTE(v24) = 0; /*0x14000de52*/
      cv::putText( /*0x14000de87*/
        a1: &v25,
        a2: &v29,
        a3: 0x5A0000000ALL,
        a4: 0,
        a5: 0x3FE6666666666666LL,
        a6: &v31,
        a7: 2,
        a8: 8,
        a9: v24);
      if ( __eh34_unwind(3) ) /*0xf1c0000000000034*/
        goto unwind_state_3; /*0xf1c0000000000034*/
      __eh34_exit_wind_state(3, 0); /*0xf1c0000000000040*/
      sub_140003EE0(a1: &v29); /*0x14000de93*/
      if ( dword_1400506B4 == 2 ) /*0x14000de9f*/
      {
        if ( cv::Mat::empty(this: (cv::Mat *)&v37) ) /*0x14000dea9*/
        {
          v9 = sub_140012730(a1: std::cerr, a2: &unk_14003E8F0); /*0x14000dec1*/
          std::ostream::operator<<(a1: v9, a2: sub_140012BC0); /*0x14000ded0*/
        }
        v10 = v48; /*0x14000ded6*/
        v11 = v51; /*0x14000deda*/
        v12 = v49; /*0x14000def9*/
        v13 = v52; /*0x14000df01*/
        v33 = v53; /*0x14000df1c*/
        si128 = _mm_load_si128((const __m128i *)&xmmword_14003F3F0); /*0x14000df21*/
        v31 = si128; /*0x14000df2a*/
        v32 = 0; /*0x14000df32*/
        v28 = 0; /*0x14000df36*/
        v25 = 50397184; /*0x14000df3b*/
        v27 = &v37; /*0x14000df47*/
        cv::circle(a1: &v25, a2: v50, a3: 2, a4: &v31, a5: -1, a6: 8, a7: 0); /*0x14000df89*/
        v31 = si128; /*0x14000df8f*/
        v32 = 0; /*0x14000df97*/
        v28 = 0; /*0x14000df9b*/
        v25 = 50397184; /*0x14000dfa0*/
        v27 = &v37; /*0x14000dfac*/
        cv::circle( /*0x14000dff1*/
          a1: &v25,
          a2: (unsigned int)v11 | ((unsigned __int64)HIDWORD(v11) << 32),
          a3: 2,
          a4: &v31,
          a5: -1,
          a6: 8,
          a7: 0);
        v31 = si128; /*0x14000dff7*/
        v32 = 0; /*0x14000dfff*/
        *(_QWORD *)&v30 = 0; /*0x14000e003*/
        LODWORD(v29) = 50397184; /*0x14000e007*/
        *((_QWORD *)&v29 + 1) = &v37; /*0x14000e013*/
        v25 = _mm_cvtsi128_si32(v10); /*0x14000e01c*/
        v26 = v10.m128i_i32[1]; /*0x14000e02d*/
        LODWORD(v27) = v10.m128i_i32[2] - v25; /*0x14000e040*/
        HIDWORD(v27) = v10.m128i_i32[3] - v10.m128i_i32[1]; /*0x14000e04f*/
        cv::rectangle(a1: &v29, a2: &v25, a3: &v31, a4: 2, a5: 8, a6: 0); /*0x14000e073*/
        v15 = SHIDWORD(v37) / 2; /*0x14000e081*/
        v31 = si128; /*0x14000e083*/
        v32 = 0; /*0x14000e08b*/
        *(_QWORD *)&v30 = 0; /*0x14000e08f*/
        LODWORD(v29) = 50397184; /*0x14000e093*/
        *((_QWORD *)&v29 + 1) = &v37; /*0x14000e09f*/
        cv::circle( /*0x14000e0ec*/
          a1: &v29,
          a2: ((unsigned __int64)v13.m128i_u32[3] << 32) | (unsigned int)(SHIDWORD(v37) / 2 + v13.m128i_i32[2]),
          a3: 2,
          a4: &v31,
          a5: -1,
          a6: 8,
          a7: 0);
        v31 = si128; /*0x14000e0f2*/
        v32 = 0; /*0x14000e0fa*/
        *(_QWORD *)&v30 = 0; /*0x14000e0fe*/
        LODWORD(v29) = 50397184; /*0x14000e102*/
        *((_QWORD *)&v29 + 1) = &v37; /*0x14000e10e*/
        cv::circle( /*0x14000e14f*/
          a1: &v29,
          a2: ((unsigned __int64)v13.m128i_u32[1] << 32) | (unsigned int)(v15 + _mm_cvtsi128_si32(v13)),
          a3: 2,
          a4: &v31,
          a5: -1,
          a6: 8,
          a7: 0);
        v31 = si128; /*0x14000e155*/
        v32 = 0; /*0x14000e15d*/
        *(_QWORD *)&v30 = 0; /*0x14000e161*/
        LODWORD(v29) = 50397184; /*0x14000e165*/
        *((_QWORD *)&v29 + 1) = &v37; /*0x14000e171*/
        v16 = _mm_cvtsi128_si32(v12); /*0x14000e176*/
        v25 = v15 + v16; /*0x14000e17e*/
        v26 = v12.m128i_i32[1]; /*0x14000e190*/
        LODWORD(v27) = v12.m128i_i32[2] - v16; /*0x14000e1a4*/
        HIDWORD(v27) = v12.m128i_i32[3] - v12.m128i_i32[1]; /*0x14000e1b5*/
        cv::rectangle(a1: &v29, a2: &v25, a3: &v31, a4: 2, a5: 8, a6: 0); /*0x14000e1d9*/
        *(_QWORD *)&v30 = 0; /*0x14000e1df*/
        LODWORD(v29) = 16842752; /*0x14000e1e3*/
        *((_QWORD *)&v29 + 1) = &v37; /*0x14000e1ef*/
        v31 = 0; /*0x14000e1f7*/
        v32 = 0; /*0x14000e1fe*/
        sub_140003D00(a1: &v31, a2: "Eye Tracking"); /*0x14000e214*/
        __eh34_enter_wind_state(0, 2); /*0xf1c000000000002c*/
        cv::imshow(a1: &v31, a2: &v29); /*0x14000e223*/
        if ( __eh34_unwind(2) ) /*0xf1c0000000000018*/
          goto unwind_state_2; /*0xf1c0000000000018*/
        __eh34_exit_wind_state(2, 0); /*0xf1c0000000000024*/
        sub_140003EE0(a1: &v31); /*0x14000e22e*/
        cv::waitKey(this: (cv *)0xA, a2: v17); /*0x14000e238*/
      }
      (*(void (__fastcall **)(__int64))(*(_QWORD *)v34 + 32LL))(a1: v34); /*0x14000e245*/
      ++dword_1400506CC; /*0x14000e248*/
      TickCount = GetTickCount(); /*0x14000e24e*/
      v19 = TickCount; /*0x14000e254*/
      v20 = dword_1400506E8; /*0x14000e256*/
      if ( dword_1400506E8 == 0 ) /*0x14000e25e*/
      {
        v20 = TickCount; /*0x14000e260*/
        dword_1400506E8 = TickCount; /*0x14000e262*/
      }
      if ( TickCount - v20 >= 0x3E8 ) /*0x14000e275*/
      {
        *(double *)&qword_1400506C0 = (double)dword_1400506CC * 1000.0 / (double)(int)(TickCount - v20); /*0x14000e29c*/
        v21 = sub_140012730(a1: std::cout, a2: &unk_14003E918); /*0x14000e2b2*/
        v22 = std::ostream::operator<<(a1: v21); /*0x14000e2c2*/
        std::ostream::operator<<(a1: v22, a2: sub_140012BC0); /*0x14000e2d2*/
        dword_1400506CC = 0; /*0x14000e2d8*/
        dword_1400506E8 = v19; /*0x14000e2de*/
      }
    }
    if ( v34 != 0 ) /*0x14000e2eb*/
      (*(void (__fastcall **)(__int64))(*(_QWORD *)v34 + 16LL))(a1: v34); /*0x14000e2f0*/
    if ( __eh34_unwind(0) ) /*0xf1c0000000000004*/
    {
unwind_state_0:
      cv::Mat::~Mat(this: (cv::Mat *)&v37); /*0xf1c0000000000008*/
      __eh34_propagate_exception_into_caller(0, -1); /*0xf1c000000000000c*/
    }
    __eh34_exit_wind_state(0, -1); /*0xf1c0000000000010*/
    cv::Mat::~Mat(this: (cv::Mat *)&v37); /*0x14000e2f8*/
    goto LABEL_34; /*0xf1c0000000000014*/
  }
  return (unsigned int)v0; /*0x14000e30f*/
}