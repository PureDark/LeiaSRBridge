HRESULT sub_14000C190()
{
  int v0; // r14d
  HRESULT result; // eax
  int v2; // ebx
  struct IMFAttributesVtbl *lpVtbl; // rax
  HRESULT v4; // eax
  UINT32 v5; // ecx
  volatile signed __int32 *v6; // rbx
  _DWORD *v7; // r15
  volatile signed __int32 *v8; // rbx
  char v9; // di
  UINT32 v10; // esi
  UINT32 v11; // edx
  volatile signed __int32 *v12; // rbx
  __int64 v13; // rdi
  volatile signed __int32 *v14; // rbx
  volatile signed __int32 *v15; // rbx
  unsigned int v16; // r15d
  char v17; // r13
  unsigned __int64 v18; // rdi
  int v19; // esi
  int v20; // ebx
  __int64 v21; // rdx
  int v22; // r8d
  unsigned int v23; // eax
  volatile signed __int32 *v24; // rbx
  UINT32 i; // ebx
  IMFActivate *v26; // rcx
  volatile signed __int32 *v27; // rbx
  char v28; // [rsp+50h] [rbp-B0h]
  _DWORD v29[3]; // [rsp+54h] [rbp-ACh] BYREF
  unsigned __int128 v30; // [rsp+60h] [rbp-A0h] BYREF
  const char *v31; // [rsp+70h] [rbp-90h]
  unsigned __int128 v32; // [rsp+80h] [rbp-80h] BYREF
  const char *v33; // [rsp+90h] [rbp-70h]
  unsigned int v34; // [rsp+A0h] [rbp-60h] BYREF
  unsigned __int64 v35; // [rsp+A4h] [rbp-5Ch] BYREF
  unsigned __int128 v36; // [rsp+B0h] [rbp-50h] BYREF
  const char *v37; // [rsp+C0h] [rbp-40h]
  void *v38; // [rsp+D0h] [rbp-30h] BYREF
  __int64 v39; // [rsp+D8h] [rbp-28h]
  int v40; // [rsp+E0h] [rbp-20h]
  int v41; // [rsp+E4h] [rbp-1Ch]
  int v42; // [rsp+E8h] [rbp-18h]
  unsigned int v43; // [rsp+ECh] [rbp-14h] BYREF
  __int64 v44; // [rsp+F0h] [rbp-10h]
  _DWORD *v45; // [rsp+F8h] [rbp-8h]
  _QWORD v46[2]; // [rsp+100h] [rbp+0h] BYREF
  _QWORD v47[2]; // [rsp+110h] [rbp+10h] BYREF
  _QWORD v48[2]; // [rsp+120h] [rbp+20h] BYREF
  _QWORD v49[2]; // [rsp+130h] [rbp+30h] BYREF
  _QWORD v50[2]; // [rsp+140h] [rbp+40h] BYREF
  _QWORD v51[2]; // [rsp+150h] [rbp+50h] BYREF
  IMFActivate **pppSourceActivate; // [rsp+160h] [rbp+60h] BYREF
  IMFAttributes *ppMFAttributes; // [rsp+168h] [rbp+68h] BYREF
  __int64 v54; // [rsp+170h] [rbp+70h] BYREF
  IMFSourceReader *ppSourceReader; // [rsp+178h] [rbp+78h] BYREF
  IMFMediaSource *pMediaSource; // [rsp+180h] [rbp+80h] BYREF
  UINT32 pcSourceActivate; // [rsp+188h] [rbp+88h] BYREF
  void *v58; // [rsp+190h] [rbp+90h] BYREF
  __int64 v59; // [rsp+198h] [rbp+98h]
  unsigned __int64 v60; // [rsp+1A0h] [rbp+A0h] BYREF

  v42 = dword_14004D120; /*0x14000c1ce*/
  v41 = dword_14004D128; /*0x14000c1d7*/
  v40 = dword_14004D12C; /*0x14000c1e0*/
  v0 = 0; /*0x14000c1e3*/
  qword_1400506B8 = 0; /*0x14000c1e6*/
  dword_1400506C8 = 0; /*0x14000c1ed*/
  ppMFAttributes = nullptr; /*0x14000c1f4*/
  pppSourceActivate = nullptr; /*0x14000c1f8*/
  pcSourceActivate = 0; /*0x14000c1fc*/
  result = MFCreateAttributes(&ppMFAttributes, cInitialSize: 2u); /*0x14000c20c*/
  if ( result < 0 ) /*0x14000c214*/
    return result; /*0x14000c214*/
  v2 = ((__int64 (__fastcall *)(IMFAttributes *, void *, void *))ppMFAttributes->lpVtbl->SetGUID)( /*0x14000c235*/
         a1: ppMFAttributes,
         a2: &unk_14003C748,
         a3: &unk_14003D270);
  lpVtbl = ppMFAttributes->lpVtbl; /*0x14000c23d*/
  if ( v2 < 0 ) /*0x14000c240*/
  {
    ((void (*)(void))lpVtbl->Release)(); /*0x14000c242*/
    return v2; /*0x14000c247*/
  }
  v4 = ((__int64 (__fastcall *)(IMFAttributes *, void *, void *))lpVtbl->SetGUID)( /*0x14000c25a*/
         a1: ppMFAttributes,
         a2: &unk_14003CA28,
         a3: &unk_14003DB88);
  if ( v4 >= 0 ) /*0x14000c262*/
    v4 = MFEnumDeviceSources(pAttributes: ppMFAttributes, &pppSourceActivate, &pcSourceActivate); /*0x14000c273*/
  v5 = pcSourceActivate; /*0x14000c292*/
  if ( pcSourceActivate == 0 ) /*0x14000c29a*/
  {
    if ( dword_14005073C > *(_DWORD *)(*(_QWORD *)NtCurrentTeb()->ThreadLocalStoragePointer + 8LL) ) /*0x14000c2b5*/
    {
      sub_1400341D0(a1: &dword_14005073C); /*0x14000c2be*/
      if ( dword_14005073C == -1 ) /*0x14000c2ca*/
      {
        __wind /*0xf1c0000000000184*/
        {
          xmmword_140050938 = 0; /*0x14000c2cf*/
          sub_14000BA40(); /*0x14000c2d6*/
          atexit(a1: sub_140038CF0); /*0x14000c2e2*/
        }
        __unwind
        {
          sub_140034128(a1: &dword_14005073C); /*0x140037087*/
        }
        Init_thread_footer(a1: &dword_14005073C); /*0x14000c2ef*/
      }
    }
    v6 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000c2f4*/
    if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x14000c2fe*/
    {
      _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x14000c300*/
      v6 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000c304*/
    }
    __eh34_enter_wind_state(-1, 1); /*0xf1c0000000000170*/
    v38 = (void *)xmmword_140050938; /*0x14000c312*/
    v39 = (__int64)v6; /*0x14000c316*/
    *(_QWORD *)&v36 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000c31a*/
    DWORD2(v36) = 143; /*0x14000c31e*/
    HIDWORD(v36) = HIDWORD(v32); /*0x14000c328*/
    v37 = "FindAndSelectCamera"; /*0x14000c32b*/
    v58 = &unk_14003E558; /*0x14000c336*/
    v59 = 41; /*0x14000c33d*/
    v30 = v36; /*0x14000c34c*/
    v31 = "FindAndSelectCamera"; /*0x14000c356*/
    sub_140008FF0(a1: xmmword_140050938, a2: &v30, a3: 2, a4: &v58); /*0x14000c36e*/
    if ( v6 != nullptr && _InterlockedExchangeAdd(v6 + 2, 0xFFFFFFFF) == 1 ) /*0x14000c384*/
    {
      (**(void (__fastcall ***)(volatile signed __int32 *))v6)(a1: v6); /*0x14000c38c*/
      if ( _InterlockedExchangeAdd(v6 + 3, 0xFFFFFFFF) == 1 ) /*0x14000c399*/
        (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v6 + 8LL))(a1: v6); /*0x14000c3a1*/
    }
    if ( pppSourceActivate != nullptr ) /*0x14000c3ab*/
    {
      CoTaskMemFree(pv: pppSourceActivate); /*0x14000c3ad*/
      pppSourceActivate = nullptr; /*0x14000c3b3*/
    }
    if ( __eh34_unwind(1) ) /*0xf1c0000000000164*/
    {
unwind_state_1:
      sub_140016910(a1: &v38); /*0xf1c0000000000134*/
      __eh34_propagate_exception_into_caller(1, -1); /*0xf1c0000000000138*/
    }
    __eh34_exit_wind_state(1, -1); /*0xf1c0000000000168*/
    ((void (__fastcall *)(IMFAttributes *, void *, void *))ppMFAttributes->lpVtbl->SetGUID)( /*0x14000c3cc*/
      a1: ppMFAttributes,
      a2: &unk_14003CA28,
      a3: &unk_14003D440);
    v4 = MFEnumDeviceSources(pAttributes: ppMFAttributes, &pppSourceActivate, &pcSourceActivate); /*0x14000c3e1*/
    v5 = pcSourceActivate; /*0x14000c3e7*/
  }
  if ( v4 < 0 || v5 == 0 ) /*0x14000c3f7*/
  {
    if ( dword_14005073C > *(_DWORD *)(*(_QWORD *)NtCurrentTeb()->ThreadLocalStoragePointer + 8LL) ) /*0x14000cc33*/
    {
      sub_1400341D0(a1: &dword_14005073C); /*0x14000cc3c*/
      if ( dword_14005073C == -1 ) /*0x14000cc48*/
      {
        __wind /*0xf1c0000000000068*/
        {
          xmmword_140050938 = 0; /*0x14000cc4d*/
          sub_14000BA40(); /*0x14000cc54*/
          atexit(a1: sub_140038CF0); /*0x14000cc60*/
        }
        __unwind
        {
          sub_140034128(a1: &dword_14005073C); /*0x14003709f*/
        }
        Init_thread_footer(a1: &dword_14005073C); /*0x14000cc6d*/
      }
    }
    v27 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000cc72*/
    if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x14000cc7c*/
    {
      _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x14000cc7e*/
      v27 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000cc82*/
    }
    __eh34_enter_wind_state(-1, 3); /*0xf1c0000000000054*/
    v36 = __PAIR128__((unsigned __int64)v27, xmmword_140050938); /*0x14000cc90*/
    *(_QWORD *)&v30 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000cc98*/
    DWORD2(v30) = 150; /*0x14000cc9d*/
    HIDWORD(v30) = HIDWORD(v32); /*0x14000cca8*/
    v31 = "FindAndSelectCamera"; /*0x14000ccac*/
    v38 = &unk_14003E5E8; /*0x14000ccb8*/
    v39 = 20; /*0x14000ccbc*/
    v32 = v30; /*0x14000ccc9*/
    v33 = "FindAndSelectCamera"; /*0x14000ccd3*/
    sub_140008FF0(a1: xmmword_140050938, a2: &v32, a3: 4, a4: &v38); /*0x14000cce6*/
    if ( v27 != nullptr && _InterlockedExchangeAdd(v27 + 2, 0xFFFFFFFF) == 1 ) /*0x14000ccfc*/
    {
      (**(void (__fastcall ***)(volatile signed __int32 *))v27)(a1: v27); /*0x14000cd04*/
      if ( _InterlockedExchangeAdd(v27 + 3, 0xFFFFFFFF) == 1 ) /*0x14000cd10*/
        (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v27 + 8LL))(a1: v27); /*0x14000cd18*/
    }
    if ( pppSourceActivate != nullptr ) /*0x14000cd22*/
      CoTaskMemFree(pv: pppSourceActivate); /*0x14000cd24*/
    if ( __eh34_unwind(3) ) /*0xf1c0000000000040*/
    {
unwind_state_3:
      sub_140016910(a1: &v36); /*0xf1c0000000000044*/
      __eh34_propagate_exception_into_caller(3, -1); /*0xf1c0000000000048*/
    }
    __eh34_exit_wind_state(3, -1); /*0xf1c000000000004c*/
    ((void (__fastcall *)(IMFAttributes *))ppMFAttributes->lpVtbl->Release)(a1: ppMFAttributes); /*0x14000cd31*/
    return -2147023728; /*0xf1c0000000000050*/
  }
  v7 = (_DWORD *)(*(_QWORD *)NtCurrentTeb()->ThreadLocalStoragePointer + 8LL); /*0x14000c409*/
  v45 = v7; /*0x14000c40c*/
  if ( dword_14005073C > *v7 ) /*0x14000c419*/
  {
    sub_1400341D0(a1: &dword_14005073C); /*0x14000c422*/
    if ( dword_14005073C == -1 ) /*0x14000c42e*/
    {
      __wind /*0xf1c000000000014c*/
      {
        xmmword_140050938 = 0; /*0x14000c433*/
        sub_14000BA40(); /*0x14000c43a*/
        atexit(a1: sub_140038CF0); /*0x14000c446*/
        Init_thread_footer(a1: &dword_14005073C); /*0x14000c453*/
      }
      __unwind
      {
        sub_140034128(a1: &dword_14005073C); /*0x1400370b7*/
      }
    }
  }
  __eh34_enter_wind_state(-1, 1); /*0xf1c0000000000144*/
  v8 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000c458*/
  if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x14000c462*/
  {
    _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x14000c464*/
    v8 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000c468*/
  }
  v38 = (void *)xmmword_140050938; /*0x14000c476*/
  v39 = (__int64)v8; /*0x14000c47a*/
  *(_QWORD *)&v36 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000c47e*/
  DWORD2(v36) = 156; /*0x14000c482*/
  HIDWORD(v36) = HIDWORD(v32); /*0x14000c48c*/
  v37 = "FindAndSelectCamera"; /*0x14000c48f*/
  v58 = &unk_14003E600; /*0x14000c49a*/
  v59 = 20; /*0x14000c4a1*/
  v30 = v36; /*0x14000c4b0*/
  v31 = "FindAndSelectCamera"; /*0x14000c4ba*/
  sub_1400190B0( /*0x14000c4de*/
    a1: xmmword_140050938,
    a2: (unsigned int)&v30,
    a3: 2,
    a4: (unsigned int)&v58,
    a5: (__int64)&pcSourceActivate);
  if ( v8 != nullptr && _InterlockedExchangeAdd(v8 + 2, 0xFFFFFFFF) == 1 ) /*0x14000c4f4*/
  {
    (**(void (__fastcall ***)(volatile signed __int32 *))v8)(a1: v8); /*0x14000c4fc*/
    if ( _InterlockedExchangeAdd(v8 + 3, 0xFFFFFFFF) == 1 ) /*0x14000c509*/
      (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v8 + 8LL))(a1: v8); /*0x14000c511*/
  }
  if ( __eh34_unwind(1) ) /*0xf1c0000000000130*/
    goto unwind_state_1; /*0xf1c0000000000130*/
  __eh34_exit_wind_state(1, -1); /*0xf1c000000000013c*/
  v9 = 0; /*0x14000c514*/
  v28 = 0; /*0x14000c517*/
  v10 = 0; /*0x14000c51c*/
  v29[0] = 0; /*0x14000c51f*/
  v11 = pcSourceActivate; /*0x14000c524*/
  if ( pcSourceActivate == 0 ) /*0x14000c52c*/
    goto LABEL_96; /*0x14000c52c*/
  while ( 1 ) /*0xf1c000000000010c*/
  {
    __wind /*0xf1c000000000010c*/
    {
      if ( dword_14005073C > *v7 ) /*0x14000c549*/
      {
        sub_1400341D0(a1: &dword_14005073C); /*0x14000c552*/
        if ( dword_14005073C == -1 ) /*0x14000c55e*/
        {
          xmmword_140050938 = 0; /*0x14000c563*/
          sub_14000BA40(); /*0x14000c56a*/
          atexit(a1: sub_140038CF0); /*0x14000c576*/
          Init_thread_footer(a1: &dword_14005073C); /*0x14000c583*/
        }
      }
      v12 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000c58f*/
      if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x14000c599*/
      {
        _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x14000c59b*/
        v12 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000c59f*/
      }
    }
    __unwind
    {
      sub_140034128(a1: &dword_14005073C); /*0x1400370cf*/
    }
    __wind /*0xf1c0000000000124*/
    {
      v49[0] = xmmword_140050938; /*0x14000c5ad*/
      v49[1] = v12; /*0x14000c5b1*/
      *(_QWORD *)&v30 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000c5b5*/
      DWORD2(v30) = 162; /*0x14000c5ba*/
      HIDWORD(v30) = HIDWORD(v32); /*0x14000c5c5*/
      v31 = "FindAndSelectCamera"; /*0x14000c5d0*/
      v46[0] = &unk_14003E618; /*0x14000c5d5*/
      v46[1] = 20; /*0x14000c5d9*/
      v32 = v30; /*0x14000c5e6*/
      v33 = "FindAndSelectCamera"; /*0x14000c5f0*/
      sub_1400190B0(a1: xmmword_140050938, a2: (unsigned int)&v32, a3: 2, a4: (unsigned int)v46, a5: (__int64)v29); /*0x14000c60d*/
      if ( v12 != nullptr && _InterlockedExchangeAdd(v12 + 2, 0xFFFFFFFF) == 1 ) /*0x14000c623*/
      {
        (**(void (__fastcall ***)(volatile signed __int32 *))v12)(a1: v12); /*0x14000c62b*/
        if ( _InterlockedExchangeAdd(v12 + 3, 0xFFFFFFFF) == 1 ) /*0x14000c638*/
          (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v12 + 8LL))(a1: v12); /*0x14000c640*/
      }
    }
    __unwind
    {
      sub_140016910(a1: v49); /*0xf1c0000000000104*/
    }
    pMediaSource = nullptr; /*0x14000c643*/
    ppSourceReader = nullptr; /*0x14000c64a*/
    v13 = 8LL * v10; /*0x14000c650*/
    v44 = v13; /*0x14000c658*/
    if ( ((int (__fastcall *)(_QWORD, void *, IMFMediaSource **))(*(IMFActivate **)((char *)pppSourceActivate + v13))->lpVtbl->ActivateObject)( /*0x14000c67d*/
           a1: *(IMFActivate **)((char *)pppSourceActivate + v13),
           a2: &unk_14003DF80,
           a3: &pMediaSource) >= 0 )
      break; /*0x14000c67d*/
    if ( dword_14005073C > *v7 ) /*0x14000c68c*/
    {
      sub_1400341D0(a1: &dword_14005073C); /*0x14000c695*/
      if ( dword_14005073C == -1 ) /*0x14000c6a1*/
      {
        __wind /*0xf1c00000000000e8*/
        {
          xmmword_140050938 = 0; /*0x14000c6a6*/
          sub_14000BA40(); /*0x14000c6ad*/
          atexit(a1: sub_140038CF0); /*0x14000c6b9*/
          Init_thread_footer(a1: &dword_14005073C); /*0x14000c6c6*/
        }
        __unwind
        {
          sub_140034128(a1: &dword_14005073C); /*0x1400370e7*/
        }
      }
    }
    __wind /*0xf1c00000000000dc*/
    {
      v14 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000c6cb*/
      if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x14000c6d5*/
      {
        _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x14000c6d7*/
        v14 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000c6db*/
      }
      v50[0] = xmmword_140050938; /*0x14000c6e9*/
      v50[1] = v14; /*0x14000c6ed*/
      *(_QWORD *)&v30 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000c6f1*/
      DWORD2(v30) = 169; /*0x14000c6f6*/
      HIDWORD(v30) = HIDWORD(v32); /*0x14000c701*/
      v31 = "FindAndSelectCamera"; /*0x14000c70c*/
      v47[0] = &unk_14003E630; /*0x14000c718*/
      v47[1] = 18; /*0x14000c71c*/
      v32 = v30; /*0x14000c729*/
      v33 = "FindAndSelectCamera"; /*0x14000c733*/
      sub_1400190B0(a1: xmmword_140050938, a2: (unsigned int)&v32, a3: 4, a4: (unsigned int)v47, a5: (__int64)v29); /*0x14000c750*/
      if ( v14 != nullptr && _InterlockedExchangeAdd(v14 + 2, 0xFFFFFFFF) == 1 ) /*0x14000c766*/
      {
        (**(void (__fastcall ***)(volatile signed __int32 *))v14)(a1: v14); /*0x14000c76e*/
        if ( _InterlockedExchangeAdd(v14 + 3, 0xFFFFFFFF) == 1 ) /*0x14000c77b*/
          (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v14 + 8LL))(a1: v14); /*0x14000c783*/
      }
    }
    __unwind
    {
      sub_140016910(a1: v50); /*0xf1c00000000000d4*/
    }
LABEL_51:
    ((void (__fastcall *)(_QWORD))(*(IMFActivate **)((char *)pppSourceActivate + v13))->lpVtbl->ShutdownObject)(a1: *(IMFActivate **)((char *)pppSourceActivate + v13)); /*0x14000c786*/
LABEL_82:
    v29[0] = ++v10; /*0x14000ca0f*/
    v11 = pcSourceActivate; /*0x14000ca13*/
    if ( v10 >= pcSourceActivate ) /*0x14000ca22*/
      goto LABEL_95; /*0x14000ca22*/
  }
  if ( MFCreateSourceReaderFromMediaSource(pMediaSource, pAttributes: nullptr, &ppSourceReader) < 0 ) /*0x14000c7b1*/
  {
    if ( dword_14005073C > *v7 ) /*0x14000c7c0*/
    {
      sub_1400341D0(a1: &dword_14005073C); /*0x14000c7c9*/
      if ( dword_14005073C == -1 ) /*0x14000c7d5*/
      {
        __wind /*0xf1c00000000000b8*/
        {
          xmmword_140050938 = 0; /*0x14000c7da*/
          sub_14000BA40(); /*0x14000c7e1*/
          atexit(a1: sub_140038CF0); /*0x14000c7ed*/
          Init_thread_footer(a1: &dword_14005073C); /*0x14000c7fa*/
        }
        __unwind
        {
          sub_140034128(a1: &dword_14005073C); /*0x1400370ff*/
        }
      }
    }
    __wind /*0xf1c00000000000ac*/
    {
      v15 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000c7ff*/
      if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x14000c809*/
      {
        _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x14000c80b*/
        v15 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000c80f*/
      }
      v51[0] = xmmword_140050938; /*0x14000c81d*/
      v51[1] = v15; /*0x14000c821*/
      *(_QWORD *)&v30 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000c825*/
      DWORD2(v30) = 176; /*0x14000c82a*/
      HIDWORD(v30) = HIDWORD(v32); /*0x14000c835*/
      v31 = "FindAndSelectCamera"; /*0x14000c840*/
      v48[0] = &unk_14003E648; /*0x14000c84c*/
      v48[1] = 32; /*0x14000c850*/
      v32 = v30; /*0x14000c85d*/
      v33 = "FindAndSelectCamera"; /*0x14000c867*/
      sub_1400190B0(a1: xmmword_140050938, a2: (unsigned int)&v32, a3: 4, a4: (unsigned int)v48, a5: (__int64)v29); /*0x14000c884*/
      if ( v15 != nullptr && _InterlockedExchangeAdd(v15 + 2, 0xFFFFFFFF) == 1 ) /*0x14000c89a*/
      {
        (**(void (__fastcall ***)(volatile signed __int32 *))v15)(a1: v15); /*0x14000c8a2*/
        if ( _InterlockedExchangeAdd(v15 + 3, 0xFFFFFFFF) == 1 ) /*0x14000c8af*/
          (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v15 + 8LL))(a1: v15); /*0x14000c8b7*/
      }
    }
    __unwind
    {
      sub_140016910(a1: v51); /*0xf1c00000000000a4*/
    }
    ((void (__fastcall *)(IMFMediaSource *))pMediaSource->lpVtbl->Release)(a1: pMediaSource); /*0x14000c8c4*/
    goto LABEL_51; /*0x14000c8c7*/
  }
  v16 = 0; /*0x14000c8cc*/
  v34 = 0; /*0x14000c8cf*/
  v54 = 0; /*0x14000c8d3*/
  v17 = 0; /*0x14000c8d7*/
  if ( ((int (__fastcall *)(IMFSourceReader *, __int64, _QWORD, __int64 *))ppSourceReader->lpVtbl->GetNativeMediaType)( /*0x14000c8f2*/
         a1: ppSourceReader,
         a2: 4294967292LL,
         a3: 0,
         a4: &v54) >= 0 )
  {
    while ( 1 ) /*0x14000c900*/
    {
      LODWORD(v18) = 0; /*0x14000c900*/
      v19 = 0; /*0x14000c907*/
      v35 = 0; /*0x14000c90a*/
      v20 = 0; /*0x14000c926*/
      if ( (*(int (__fastcall **)(__int64, void *, unsigned __int64 *))(*(_QWORD *)v54 + 64LL))( /*0x14000c92b*/
             a1: v54,
             a2: &unk_140039950,
             a3: &v60) >= 0 )
      {
        v19 = v60; /*0x14000c92d*/
        v18 = HIDWORD(v60); /*0x14000c937*/
        v35 = v60; /*0x14000c93e*/
        v20 = HIDWORD(v60); /*0x14000c941*/
      }
      if ( (*(int (__fastcall **)(__int64, void *, void **))(*(_QWORD *)v54 + 64LL))( /*0x14000c96f*/
             a1: v54,
             a2: &unk_140039960,
             a3: &v58) >= 0
        && (_DWORD)v58 != 0 )
      {
        v21 = HIDWORD(v58) % (unsigned int)v58; /*0x14000c973*/
        v23 = HIDWORD(v58) / (unsigned int)v58; /*0x14000c973*/
      }
      else
      {
        v23 = 0; /*0x14000c977*/
      }
      v43 = v23; /*0x14000c97a*/
      if ( v20 == v40 && v19 == v41 && v23 == v42 ) /*0x14000c98a*/
        break; /*0x14000c98a*/
      (*(void (__fastcall **)(__int64, __int64))(*(_QWORD *)v54 + 16LL))(a1: v54, a2: v21); /*0x14000c997*/
      v34 = ++v16; /*0x14000c99d*/
      if ( ((int (__fastcall *)(IMFSourceReader *, __int64, _QWORD, __int64 *))ppSourceReader->lpVtbl->GetNativeMediaType)( /*0x14000c9b9*/
             a1: ppSourceReader,
             a2: 4294967292LL,
             a3: v16,
             a4: &v54) < 0 )
      {
        v13 = v44; /*0x14000c9bf*/
        goto LABEL_75; /*0x14000c9bf*/
      }
    }
    if ( dword_14005073C > *v45 ) /*0x14000ca39*/
    {
      sub_1400341D0(a1: &dword_14005073C); /*0x14000ca42*/
      if ( dword_14005073C == -1 ) /*0x14000ca4e*/
      {
        __wind /*0xf1c0000000000094*/
        {
          xmmword_140050938 = 0; /*0x14000ca53*/
          sub_14000BA40(); /*0x14000ca5a*/
          atexit(a1: sub_140038CF0); /*0x14000ca66*/
        }
        __unwind
        {
          sub_140034128(a1: &dword_14005073C); /*0x140037117*/
        }
        Init_thread_footer(a1: &dword_14005073C); /*0x14000ca73*/
      }
    }
    v24 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000ca78*/
    if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x14000ca82*/
    {
      _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x14000ca84*/
      v24 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000ca88*/
    }
    __eh34_enter_wind_state(-1, 3); /*0xf1c0000000000080*/
    v36 = __PAIR128__((unsigned __int64)v24, xmmword_140050938); /*0x14000ca96*/
    *(_QWORD *)&v30 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000caa5*/
    DWORD2(v30) = 199; /*0x14000caaa*/
    HIDWORD(v30) = HIDWORD(v32); /*0x14000cab5*/
    v31 = "FindAndSelectCamera"; /*0x14000cac0*/
    v38 = &unk_14003E670; /*0x14000cacc*/
    v39 = 53; /*0x14000cad0*/
    v32 = v30; /*0x14000cadd*/
    v33 = "FindAndSelectCamera"; /*0x14000cae7*/
    sub_1400193B0( /*0x14000cb22*/
      a1: xmmword_140050938,
      a2: (unsigned int)&v32,
      a3: v22,
      a4: (unsigned int)&v38,
      a5: (__int64)v29,
      a6: (__int64)&v35 + 4,
      a7: (__int64)&v35,
      a8: (__int64)&v43,
      a9: (__int64)&v34);
    if ( v24 != nullptr && _InterlockedExchangeAdd(v24 + 2, 0xFFFFFFFF) == 1 ) /*0x14000cb38*/
    {
      (**(void (__fastcall ***)(volatile signed __int32 *))v24)(a1: v24); /*0x14000cb40*/
      if ( _InterlockedExchangeAdd(v24 + 3, 0xFFFFFFFF) == 1 ) /*0x14000cb4d*/
        (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v24 + 8LL))(a1: v24); /*0x14000cb55*/
    }
    if ( __eh34_unwind(3) ) /*0xf1c0000000000074*/
      goto unwind_state_3; /*0xf1c0000000000074*/
    __eh34_exit_wind_state(3, -1); /*0xf1c0000000000078*/
    dword_1400506C8 = v16; /*0x14000cb58*/
    (*(void (__fastcall **)(__int64, void *, __int128 *))(*(_QWORD *)v54 + 80LL))( /*0x14000cb74*/
      a1: v54,
      a2: &unk_140039940,
      a3: &xmmword_1400506D0);
    dword_1400506A8 = v18; /*0x14000cb77*/
    dword_1400506B0 = v19; /*0x14000cb7d*/
    v13 = v44; /*0x14000cb87*/
    qword_1400506B8 = *(__int64 *)((char *)pppSourceActivate + v44); /*0x14000cb8f*/
    (*(void (**)(void))(*(_QWORD *)qword_1400506B8 + 8LL))(); /*0x14000cb99*/
    v17 = 1; /*0x14000cb9c*/
    v28 = 1; /*0x14000cb9f*/
    (*(void (__fastcall **)(__int64))(*(_QWORD *)v54 + 16LL))(a1: v54); /*0x14000cbab*/
LABEL_75:
    v10 = v29[0]; /*0x14000c9c3*/
  }
  if ( ppSourceReader != nullptr ) /*0x14000c9ce*/
    ((void (__fastcall *)(IMFSourceReader *))ppSourceReader->lpVtbl->Release)(a1: ppSourceReader); /*0x14000c9d3*/
  if ( pMediaSource != nullptr ) /*0x14000c9e0*/
    ((void (__fastcall *)(IMFMediaSource *))pMediaSource->lpVtbl->Release)(a1: pMediaSource); /*0x14000c9e5*/
  ((void (__fastcall *)(_QWORD))(*(IMFActivate **)((char *)pppSourceActivate + v13))->lpVtbl->ShutdownObject)(a1: *(IMFActivate **)((char *)pppSourceActivate + v13)); /*0x14000c9f3*/
  if ( v17 == 0 ) /*0x14000c9fc*/
  {
    v7 = v45; /*0x14000ca02*/
    goto LABEL_82; /*0x14000ca02*/
  }
  v11 = pcSourceActivate; /*0x14000cbb3*/
LABEL_95:
  v9 = v28; /*0x14000cbb9*/
LABEL_96:
  for ( i = 0; i < v11; ++i ) /*0x14000cbc3*/
  {
    v26 = pppSourceActivate[i]; /*0x14000cbd6*/
    if ( v26 != nullptr ) /*0x14000cbdd*/
    {
      ((void (__fastcall *)(IMFActivate *))v26->lpVtbl->Release)(a1: v26); /*0x14000cbe2*/
      v11 = pcSourceActivate; /*0x14000cbe5*/
    }
  }
  CoTaskMemFree(pv: pppSourceActivate); /*0x14000cbf5*/
  if ( ppMFAttributes != nullptr ) /*0x14000cc02*/
    ((void (__fastcall *)(IMFAttributes *))ppMFAttributes->lpVtbl->Release)(a1: ppMFAttributes); /*0x14000cc07*/
  if ( v9 == 0 ) /*0x14000cc12*/
    return -2147023728; /*0x14000cc12*/
  return v0; /*0x14000cd39*/
}