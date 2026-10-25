__int64 sub_14000D4D0()
{
  int v0; // ebx
  __int64 v1; // rax
  __int64 v3; // rax
  __int64 v4; // rax
  unsigned int v5; // r14d
  unsigned int v6; // r12d
  int v7; // esi
  int v8; // r15d
  unsigned int v9; // edi
  __int64 v10; // rbx
  __int64 v11; // r13
  __m128i si128; // xmm6
  int v13; // edx
  DWORD TickCount; // eax
  int v15; // edx
  __m128i v16; // [rsp+40h] [rbp-C0h] BYREF
  __int128 v17; // [rsp+50h] [rbp-B0h]
  __int128 v18; // [rsp+60h] [rbp-A0h] BYREF
  __int128 v19; // [rsp+70h] [rbp-90h]
  int v20; // [rsp+80h] [rbp-80h]
  int v21; // [rsp+84h] [rbp-7Ch]
  unsigned int v22; // [rsp+88h] [rbp-78h]
  int v23; // [rsp+8Ch] [rbp-74h]
  int v24; // [rsp+90h] [rbp-70h]
  int v25; // [rsp+94h] [rbp-6Ch]
  int v26; // [rsp+98h] [rbp-68h]
  int v27; // [rsp+9Ch] [rbp-64h]
  __m128i v28; // [rsp+A0h] [rbp-60h] BYREF
  __int128 v29; // [rsp+B0h] [rbp-50h]
  _BYTE v30[96]; // [rsp+C0h] [rbp-40h] BYREF
  __int64 v31; // [rsp+120h] [rbp+20h] BYREF
  __int64 v32; // [rsp+128h] [rbp+28h] BYREF
  void *v33; // [rsp+130h] [rbp+30h] BYREF
  __int128 v34; // [rsp+140h] [rbp+40h] BYREF
  __int128 v35; // [rsp+150h] [rbp+50h]
  __int128 v36; // [rsp+160h] [rbp+60h]
  __int128 v37; // [rsp+170h] [rbp+70h]
  __int128 v38; // [rsp+180h] [rbp+80h]
  __int128 v39; // [rsp+190h] [rbp+90h]
  int v40; // [rsp+1A0h] [rbp+A0h] BYREF
  int v41; // [rsp+1A4h] [rbp+A4h] BYREF
  int v42; // [rsp+1A8h] [rbp+A8h] BYREF
  __int64 v43; // [rsp+1B0h] [rbp+B0h] BYREF
  _DWORD v44[21]; // [rsp+1C0h] [rbp+C0h] BYREF
  __int64 v45; // [rsp+214h] [rbp+114h]
  unsigned int v46; // [rsp+21Ch] [rbp+11Ch]
  unsigned int v47; // [rsp+220h] [rbp+120h]
  int v48; // [rsp+23Ch] [rbp+13Ch]
  unsigned int v49; // [rsp+240h] [rbp+140h]
  int v50; // [rsp+244h] [rbp+144h]
  unsigned int v51; // [rsp+248h] [rbp+148h]

  if ( qword_140050188 == 0 ) /*0x14000d50f*/
    return 2147500037LL; /*0x14000d50f*/
  v32 = 0; /*0x14000d517*/
  v41 = 0; /*0x14000d51b*/
  v40 = 0; /*0x14000d521*/
  v43 = 0; /*0x14000d527*/
  v0 = (*(__int64 (__fastcall **)(__int64, __int64, _QWORD, int *, int *, __int64 *, __int64 *))(*(_QWORD *)qword_140050188 /*0x14000d564*/
                                                                                               + 72LL))(
         a1: qword_140050188,
         a2: 4294967292LL,
         a3: 0,
         a4: &v41,
         a5: &v40,
         a6: &v43,
         a7: &v32);
  if ( v0 < 0 ) /*0x14000d568*/
  {
LABEL_25:
    if ( v32 != 0 ) /*0x14000daba*/
      (*(void (__fastcall **)(__int64))(*(_QWORD *)v32 + 16LL))(a1: v32); /*0x14000dabf*/
    return (unsigned int)v0; /*0x14000dabf*/
  }
  if ( v32 != 0 ) /*0x14000d575*/
  {
    v31 = 0; /*0x14000d57b*/
    v33 = nullptr; /*0x14000d57f*/
    v42 = 0; /*0x14000d583*/
    v0 = (*(__int64 (__fastcall **)(__int64, __int64 *))(*(_QWORD *)v32 + 328LL))(a1: v32, a2: &v31); /*0x14000d596*/
    v20 = v0; /*0x14000d598*/
    if ( v0 >= 0 ) /*0x14000d59d*/
    {
      v0 = (*(__int64 (__fastcall **)(__int64, void **, _QWORD, int *))(*(_QWORD *)v31 + 24LL))( /*0x14000d5b7*/
             a1: v31,
             a2: &v33,
             a3: 0,
             a4: &v42);
      v20 = v0; /*0x14000d5b9*/
    }
    v34 = 0; /*0x14000d5bf*/
    v35 = 0; /*0x14000d5c3*/
    v36 = 0; /*0x14000d5c7*/
    v37 = 0; /*0x14000d5cb*/
    v38 = 0; /*0x14000d5cf*/
    v39 = 0; /*0x14000d5d6*/
    cv::Mat::Mat(this: (cv::Mat *)&v34); /*0x14000d5e1*/
    if ( v0 >= 0 ) /*0x14000d5ea*/
    {
      if ( memcmp(Buf1: &xmmword_140050168, Buf2: "NV12", Size: 0x10u) == 0 ) /*0x14000d60b*/
      {
        v1 = cv::Mat::Mat(this: (cv::Mat *)v30, a2: dword_140050150, a3: dword_140050148, a4: 0, a5: v33, a6: 0); /*0x14000d62f*/
        cv::Mat::operator=(a1: &v34, a2: v1); /*0x14000d63d*/
        cv::Mat::~Mat(this: (cv::Mat *)v30); /*0x14000d648*/
      }
      if ( cv::Mat::empty(this: (cv::Mat *)&v34) ) /*0x14000d652*/
      {
        cv::Mat::~Mat(this: (cv::Mat *)&v34); /*0x14000d660*/
        return 2147500037LL; /*0x14000d66b*/
      }
      if ( (unsigned int)doEyeTrack_3D( /*0x14000d6a1*/
                           a1: v35,
                           a2: HIDWORD(v34),
                           a3: DWORD2(v34),
                           a4: (((int)v34 >> 3) & 0x1FFu) + 1,
                           a5: v44) == 0 )
      {
        v3 = sub_1400114A0(a1: std::cout, a2: &unk_14003E8E8); /*0x14000d6b1*/
        std::ostream::operator<<(a1: v3, a2: sub_140011890); /*0x14000d6c0*/
      }
      (*(void (__fastcall **)(LPVOID))(*(_QWORD *)ppv + 80LL))(a1: ppv); /*0x14000d6fc*/
      if ( dword_140050154 == 2 ) /*0x14000d706*/
      {
        if ( cv::Mat::empty(this: (cv::Mat *)&v34) ) /*0x14000d710*/
        {
          v4 = sub_1400114A0(a1: std::cerr, a2: &unk_14003E900); /*0x14000d728*/
          std::ostream::operator<<(a1: v4, a2: sub_140011890); /*0x14000d737*/
        }
        v5 = v44[9]; /*0x14000d73d*/
        v6 = v44[10]; /*0x14000d744*/
        v7 = v44[11]; /*0x14000d74b*/
        v8 = v44[12]; /*0x14000d751*/
        v9 = v46; /*0x14000d758*/
        v10 = v47; /*0x14000d75e*/
        v24 = v44[13]; /*0x14000d76a*/
        v25 = v44[14]; /*0x14000d773*/
        v26 = v44[15]; /*0x14000d77c*/
        v27 = v44[16]; /*0x14000d785*/
        v23 = v48; /*0x14000d78e*/
        v22 = v49; /*0x14000d797*/
        v21 = v50; /*0x14000d7a0*/
        v11 = v51; /*0x14000d7a3*/
        si128 = _mm_load_si128((const __m128i *)&xmmword_14003F2C0); /*0x14000d7aa*/
        v16 = si128; /*0x14000d7b2*/
        v17 = 0; /*0x14000d7ba*/
        *(_QWORD *)&v19 = 0; /*0x14000d7c1*/
        LODWORD(v18) = 50397184; /*0x14000d7c6*/
        *((_QWORD *)&v18 + 1) = &v34; /*0x14000d7d2*/
        cv::circle(a1: &v18, a2: v45, a3: 2, a4: &v16, a5: -1, a6: 8, a7: 0); /*0x14000d80e*/
        v16 = si128; /*0x14000d814*/
        v17 = 0; /*0x14000d81c*/
        *(_QWORD *)&v19 = 0; /*0x14000d823*/
        LODWORD(v18) = 50397184; /*0x14000d828*/
        *((_QWORD *)&v18 + 1) = &v34; /*0x14000d834*/
        cv::circle(a1: &v18, a2: (v10 << 32) | v9, a3: 2, a4: &v16, a5: -1, a6: 8, a7: 0); /*0x14000d868*/
        v28 = si128; /*0x14000d86e*/
        v29 = 0; /*0x14000d875*/
        *(_QWORD *)&v17 = 0; /*0x14000d879*/
        v16.m128i_i32[0] = 50397184; /*0x14000d87e*/
        v16.m128i_i64[1] = (__int64)&v34; /*0x14000d88a*/
        *(_QWORD *)&v18 = __PAIR64__(v6, v5); /*0x14000d88f*/
        DWORD2(v18) = v7 - v5; /*0x14000d89c*/
        HIDWORD(v18) = v8 - v6; /*0x14000d8a3*/
        cv::rectangle(a1: &v16, a2: &v18, a3: &v28, a4: 2, a5: 8, a6: 0); /*0x14000d8c8*/
        LODWORD(v10) = SHIDWORD(v34) / 2; /*0x14000d8d6*/
        v28 = si128; /*0x14000d8d8*/
        v29 = 0; /*0x14000d8df*/
        *(_QWORD *)&v17 = 0; /*0x14000d8e3*/
        v16.m128i_i32[0] = 50397184; /*0x14000d8e8*/
        v16.m128i_i64[1] = (__int64)&v34; /*0x14000d8f4*/
        cv::circle( /*0x14000d928*/
          a1: &v16,
          a2: (v11 << 32) | (unsigned int)(SHIDWORD(v34) / 2 + v21),
          a3: 2,
          a4: &v28,
          a5: -1,
          a6: 8,
          a7: 0);
        v28 = si128; /*0x14000d92e*/
        v29 = 0; /*0x14000d935*/
        *(_QWORD *)&v17 = 0; /*0x14000d939*/
        v16.m128i_i32[0] = 50397184; /*0x14000d93e*/
        v16.m128i_i64[1] = (__int64)&v34; /*0x14000d94a*/
        cv::circle( /*0x14000d981*/
          a1: &v16,
          a2: ((unsigned __int64)v22 << 32) | (unsigned int)(v10 + v23),
          a3: 2,
          a4: &v28,
          a5: -1,
          a6: 8,
          a7: 0);
        v28 = si128; /*0x14000d987*/
        v29 = 0; /*0x14000d98e*/
        *(_QWORD *)&v17 = 0; /*0x14000d992*/
        v16.m128i_i32[0] = 50397184; /*0x14000d997*/
        v16.m128i_i64[1] = (__int64)&v34; /*0x14000d9a3*/
        LODWORD(v18) = v24 + v10; /*0x14000d9ae*/
        DWORD1(v18) = v25; /*0x14000d9b5*/
        DWORD2(v18) = v26 - v24; /*0x14000d9be*/
        HIDWORD(v18) = v27 - v25; /*0x14000d9c7*/
        cv::rectangle(a1: &v16, a2: &v18, a3: &v28, a4: 2, a5: 8, a6: 0); /*0x14000d9eb*/
        *(_QWORD *)&v17 = 0; /*0x14000d9f1*/
        v16.m128i_i32[0] = 16842752; /*0x14000d9f6*/
        v16.m128i_i64[1] = (__int64)&v34; /*0x14000da02*/
        v18 = 0; /*0x14000da0a*/
        v19 = 0; /*0x14000da12*/
        sub_140004290(a1: &v18, a2: "Eye Tracking", a3: 12); /*0x14000da2a*/
        cv::imshow(a1: &v18, a2: &v16); /*0x14000da3a*/
        sub_1400044B0(a1: &v18); /*0x14000da46*/
        cv::waitKey(this: (cv *)0xA, a2: v13); /*0x14000da50*/
        v0 = v20; /*0x14000da56*/
      }
      (*(void (__fastcall **)(__int64))(*(_QWORD *)v31 + 32LL))(a1: v31); /*0x14000da60*/
      ++dword_140050164; /*0x14000da63*/
      TickCount = GetTickCount(); /*0x14000da69*/
      v15 = dword_140050180; /*0x14000da6f*/
      if ( dword_140050180 == 0 ) /*0x14000da77*/
      {
        v15 = TickCount; /*0x14000da79*/
        dword_140050180 = TickCount; /*0x14000da7b*/
      }
      if ( TickCount - v15 >= 0x3E8 ) /*0x14000da8b*/
      {
        dword_140050164 = 0; /*0x14000da8d*/
        dword_140050180 = TickCount; /*0x14000da93*/
      }
    }
    if ( v31 != 0 ) /*0x14000daa0*/
      (*(void (__fastcall **)(__int64))(*(_QWORD *)v31 + 16LL))(a1: v31); /*0x14000daa5*/
    cv::Mat::~Mat(this: (cv::Mat *)&v34); /*0x14000daad*/
    goto LABEL_25; /*0x14000daad*/
  }
  return (unsigned int)v0; /*0x14000dac4*/
}