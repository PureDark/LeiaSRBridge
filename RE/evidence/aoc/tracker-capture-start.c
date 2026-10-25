__int64 sub_14000E350()
{
  int *v0; // rsi
  volatile signed __int32 *v1; // rbx
  int v2; // r8d
  __int64 v3; // rcx
  int v4; // r14d
  volatile signed __int32 *v5; // rbx
  volatile signed __int32 *v6; // rbx
  volatile signed __int32 *v7; // rbx
  __int64 result; // rax
  char v9; // al
  int v10; // ecx
  volatile signed __int32 *v11; // rbx
  int v12; // r8d
  int v13; // r14d
  volatile signed __int32 *v14; // rbx
  volatile signed __int32 *v15; // rbx
  volatile signed __int32 *v16; // rsi
  int v17; // ebx
  DWORD LastError; // eax
  int v19; // r8d
  void *v20; // [rsp+40h] [rbp-39h] BYREF
  __int64 v21; // [rsp+48h] [rbp-31h]
  __int64 v22; // [rsp+50h] [rbp-29h] BYREF
  volatile signed __int32 *v23; // [rsp+58h] [rbp-21h]
  __int128 v24; // [rsp+60h] [rbp-19h]
  void *v25; // [rsp+70h] [rbp-9h]
  __int128 v26; // [rsp+80h] [rbp+7h] BYREF
  void *v27; // [rsp+90h] [rbp+17h]
  int v28; // [rsp+E0h] [rbp+67h] BYREF
  int v29; // [rsp+E8h] [rbp+6Fh] BYREF

  v0 = (int *)(*(_QWORD *)NtCurrentTeb()->ThreadLocalStoragePointer + 8LL); /*0x14000e37a*/
  if ( dword_14005073C > *v0 ) /*0x14000e385*/
  {
    sub_1400341D0(a1: &dword_14005073C); /*0x14000e38e*/
    if ( dword_14005073C == -1 ) /*0x14000e39a*/
    {
      __wind /*0xf1c0000000000128*/
      {
        xmmword_140050938 = 0; /*0x14000e39f*/
        sub_14000BA40(); /*0x14000e3a6*/
        atexit(a1: sub_140038CF0); /*0x14000e3b2*/
        Init_thread_footer(a1: &dword_14005073C); /*0x14000e3bf*/
      }
      __unwind
      {
        sub_140034128(a1: &dword_14005073C); /*0x140037277*/
      }
    }
  }
  __eh34_enter_wind_state(-1, 1); /*0xf1c0000000000120*/
  v1 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000e3c4*/
  if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x14000e3ce*/
  {
    _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x14000e3d0*/
    v1 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000e3d4*/
  }
  v22 = xmmword_140050938; /*0x14000e3e2*/
  v23 = v1; /*0x14000e3e6*/
  *(_QWORD *)&v24 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000e3f1*/
  DWORD2(v24) = 529; /*0x14000e3f5*/
  v25 = &unk_14003E934; /*0x14000e403*/
  v20 = &unk_14003E928; /*0x14000e40e*/
  v21 = 11; /*0x14000e412*/
  v26 = v24; /*0x14000e41e*/
  v27 = &unk_14003E934; /*0x14000e427*/
  sub_140008FF0(a1: xmmword_140050938, a2: &v26, a3: 2, a4: &v20); /*0x14000e43a*/
  if ( v1 != nullptr && _InterlockedExchangeAdd(v1 + 2, 0xFFFFFFFF) == 1 ) /*0x14000e454*/
  {
    (**(void (__fastcall ***)(volatile signed __int32 *))v1)(a1: v1); /*0x14000e45c*/
    if ( _InterlockedExchangeAdd(v1 + 3, 0xFFFFFFFF) == 1 ) /*0x14000e468*/
      (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v1 + 8LL))(a1: v1); /*0x14000e470*/
  }
  v3 = qword_1400506B8; /*0x14000e473*/
  if ( qword_1400506B8 == 0 ) /*0x14000e47d*/
  {
    v28 = 5; /*0x14000e483*/
    v4 = 0; /*0x14000e48a*/
    while ( 1 ) /*0x14000e494*/
    {
      if ( v3 != 0 ) /*0x14000e497*/
        goto LABEL_44; /*0x14000e497*/
      if ( dword_14005073C > *v0 ) /*0x14000e4a5*/
      {
        sub_1400341D0(a1: &dword_14005073C); /*0x14000e4ae*/
        if ( dword_14005073C == -1 ) /*0x14000e4ba*/
        {
          if ( __eh34_unwind(1) ) /*0xf1c0000000000200*/
          {
unwind_state_1:
            sub_140016910(a1: &v22); /*0xf1c000000000005c*/
            __eh34_propagate_exception_into_caller(1, -1); /*0xf1c0000000000060*/
          }
          __eh34_exit_wind_state(1, -1); /*0xf1c0000000000204*/
          __wind /*0xf1c0000000000208*/
          {
            xmmword_140050938 = 0; /*0x14000e4bf*/
            sub_14000BA40(); /*0x14000e4c6*/
            atexit(a1: sub_140038CF0); /*0x14000e4d2*/
            Init_thread_footer(a1: &dword_14005073C); /*0x14000e4df*/
          }
          __unwind
          {
            sub_140034128(a1: &dword_14005073C); /*0x14003728f*/
          }
          __eh34_enter_wind_state(-1, 1); /*0xf1c00000000001f8*/
        }
      }
      v5 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000e4e4*/
      if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x14000e4ee*/
      {
        _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x14000e4f0*/
        v5 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000e4f4*/
      }
      v22 = xmmword_140050938; /*0x14000e502*/
      v23 = v5; /*0x14000e506*/
      v29 = ++v4; /*0x14000e511*/
      *(_QWORD *)&v24 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000e514*/
      DWORD2(v24) = 535; /*0x14000e518*/
      HIDWORD(v24) = HIDWORD(v26); /*0x14000e522*/
      v25 = &unk_14003E934; /*0x14000e525*/
      v20 = &unk_14003E938; /*0x14000e529*/
      v21 = 40; /*0x14000e52d*/
      v26 = v24; /*0x14000e539*/
      v27 = &unk_14003E934; /*0x14000e542*/
      sub_140019D60( /*0x14000e561*/
        a1: xmmword_140050938,
        a2: (unsigned int)&v26,
        a3: v2,
        a4: (unsigned int)&v20,
        a5: (__int64)&v29,
        a6: (__int64)&v28);
      if ( v5 != nullptr && _InterlockedExchangeAdd(v5 + 2, 0xFFFFFFFF) == 1 ) /*0x14000e576*/
      {
        (**(void (__fastcall ***)(volatile signed __int32 *))v5)(a1: v5); /*0x14000e57e*/
        if ( _InterlockedExchangeAdd(v5 + 3, 0xFFFFFFFF) == 1 ) /*0x14000e58a*/
          (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v5 + 8LL))(a1: v5); /*0x14000e592*/
      }
      if ( __eh34_unwind(1) ) /*0xf1c000000000010c*/
        goto unwind_state_1; /*0xf1c000000000010c*/
      __eh34_exit_wind_state(1, -1); /*0xf1c0000000000110*/
      if ( sub_14000C190() >= 0 && qword_1400506B8 != 0 ) /*0x14000e5a6*/
        break; /*0x14000e5a6*/
      Sleep(dwMilliseconds: 0x1F4u); /*0x14000e5ad*/
      if ( v4 >= 5 ) /*0x14000e5b7*/
      {
        __eh34_enter_wind_state(-1, 1); /*0xf1c00000000000e4*/
LABEL_34:
        if ( qword_1400506B8 != 0 ) /*0x14000e6bc*/
          goto LABEL_44; /*0x14000e6bc*/
        if ( dword_14005073C > *v0 ) /*0x14000e6ca*/
        {
          sub_1400341D0(a1: &dword_14005073C); /*0x14000e6d3*/
          if ( dword_14005073C == -1 ) /*0x14000e6df*/
          {
            if ( __eh34_unwind(1) ) /*0xf1c00000000001d8*/
              goto unwind_state_1; /*0xf1c00000000001d8*/
            __eh34_exit_wind_state(1, -1); /*0xf1c00000000001dc*/
            __wind /*0xf1c00000000001e0*/
            {
              xmmword_140050938 = 0; /*0x14000e6e4*/
              sub_14000BA40(); /*0x14000e6eb*/
              atexit(a1: sub_140038CF0); /*0x14000e6f7*/
              Init_thread_footer(a1: &dword_14005073C); /*0x14000e704*/
            }
            __unwind
            {
              sub_140034128(a1: &dword_14005073C); /*0x1400372bf*/
            }
            __eh34_enter_wind_state(-1, 1); /*0xf1c00000000001d0*/
          }
        }
        v7 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000e709*/
        if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x14000e713*/
        {
          _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x14000e715*/
          v7 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000e719*/
        }
        v22 = xmmword_140050938; /*0x14000e727*/
        v23 = v7; /*0x14000e72b*/
        *(_QWORD *)&v24 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000e72f*/
        DWORD2(v24) = 560; /*0x14000e733*/
        HIDWORD(v24) = HIDWORD(v26); /*0x14000e73d*/
        v25 = &unk_14003E934; /*0x14000e740*/
        v20 = &unk_14003E720; /*0x14000e74b*/
        v21 = 30; /*0x14000e74f*/
        v26 = v24; /*0x14000e75b*/
        v27 = &unk_14003E934; /*0x14000e764*/
        result = sub_140008FF0(a1: xmmword_140050938, a2: &v26, a3: 4, a4: &v20); /*0x14000e777*/
LABEL_40:
        if ( v7 != nullptr ) /*0x14000e780*/
        {
          result = (unsigned int)_InterlockedExchangeAdd(v7 + 2, 0xFFFFFFFF); /*0x14000e788*/
          if ( (_DWORD)result == 1 ) /*0x14000e790*/
          {
            result = (**(__int64 (__fastcall ***)(volatile signed __int32 *))v7)(a1: v7); /*0x14000e79c*/
            if ( _InterlockedExchangeAdd(v7 + 3, 0xFFFFFFFF) == 1 ) /*0x14000e7a6*/
              result = (*(__int64 (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v7 + 8LL))(a1: v7); /*0x14000e7b2*/
          }
        }
        goto LABEL_101; /*0x14000e7b5*/
      }
      v3 = qword_1400506B8; /*0x14000e5bd*/
      __eh34_enter_wind_state(-1, 1); /*0xf1c0000000000118*/
    }
    if ( dword_14005073C > *v0 ) /*0x14000e5d1*/
    {
      sub_1400341D0(a1: &dword_14005073C); /*0x14000e5da*/
      if ( dword_14005073C == -1 ) /*0x14000e5e6*/
      {
        __wind /*0xf1c00000000000f4*/
        {
          xmmword_140050938 = 0; /*0x14000e5eb*/
          sub_14000BA40(); /*0x14000e5f2*/
          atexit(a1: sub_140038CF0); /*0x14000e5fe*/
          Init_thread_footer(a1: &dword_14005073C); /*0x14000e60b*/
        }
        __unwind
        {
          sub_140034128(a1: &dword_14005073C); /*0x1400372a7*/
        }
      }
    }
    __eh34_enter_wind_state(-1, 1); /*0xf1c00000000000ec*/
    v6 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000e610*/
    if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x14000e61a*/
    {
      _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x14000e61c*/
      v6 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000e620*/
    }
    v22 = xmmword_140050938; /*0x14000e62e*/
    v23 = v6; /*0x14000e632*/
    *(_QWORD *)&v24 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000e636*/
    DWORD2(v24) = 553; /*0x14000e63a*/
    HIDWORD(v24) = HIDWORD(v26); /*0x14000e644*/
    v25 = &unk_14003E934; /*0x14000e647*/
    v20 = &unk_14003E968; /*0x14000e652*/
    v21 = 27; /*0x14000e656*/
    v26 = v24; /*0x14000e662*/
    v27 = &unk_14003E934; /*0x14000e66b*/
    sub_140008FF0(a1: xmmword_140050938, a2: &v26, a3: 2, a4: &v20); /*0x14000e67e*/
    if ( v6 != nullptr && _InterlockedExchangeAdd(v6 + 2, 0xFFFFFFFF) == 1 ) /*0x14000e693*/
    {
      (**(void (__fastcall ***)(volatile signed __int32 *))v6)(a1: v6); /*0x14000e69b*/
      if ( _InterlockedExchangeAdd(v6 + 3, 0xFFFFFFFF) == 1 ) /*0x14000e6a7*/
        (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v6 + 8LL))(a1: v6); /*0x14000e6af*/
    }
    goto LABEL_34; /*0x14000e6af*/
  }
LABEL_44:
  v9 = BOE_ROI_init(); /*0x14000e7ba*/
  v10 = *v0; /*0x14000e7c0*/
  if ( v9 != 0 ) /*0x14000e7c4*/
  {
    if ( dword_14005073C > v10 ) /*0x14000e88e*/
    {
      sub_1400341D0(a1: &dword_14005073C); /*0x14000e897*/
      if ( dword_14005073C == -1 ) /*0x14000e8a3*/
      {
        if ( __eh34_unwind(1) ) /*0xf1c0000000000188*/
          goto unwind_state_1; /*0xf1c0000000000188*/
        __eh34_exit_wind_state(1, -1); /*0xf1c000000000018c*/
        __wind /*0xf1c0000000000190*/
        {
          xmmword_140050938 = 0; /*0x14000e8a8*/
          sub_14000BA40(); /*0x14000e8af*/
          atexit(a1: sub_140038CF0); /*0x14000e8bb*/
          Init_thread_footer(a1: &dword_14005073C); /*0x14000e8c8*/
        }
        __unwind
        {
          sub_140034128(a1: &dword_14005073C); /*0x1400372ef*/
        }
        __eh34_enter_wind_state(-1, 1); /*0xf1c0000000000180*/
      }
    }
    v11 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000e8cd*/
    if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x14000e8d7*/
    {
      _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x14000e8d9*/
      v11 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000e8dd*/
    }
    v22 = xmmword_140050938; /*0x14000e8eb*/
    v23 = v11; /*0x14000e8ef*/
    *(_QWORD *)&v24 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000e8f3*/
    DWORD2(v24) = 572; /*0x14000e8f7*/
    HIDWORD(v24) = HIDWORD(v26); /*0x14000e901*/
    v25 = &unk_14003E934; /*0x14000e904*/
    v20 = &unk_14003E9E0; /*0x14000e90f*/
    v21 = 17; /*0x14000e913*/
    v26 = v24; /*0x14000e91f*/
    v27 = &unk_14003E934; /*0x14000e928*/
    sub_140008FF0(a1: xmmword_140050938, a2: &v26, a3: 2, a4: &v20); /*0x14000e93b*/
  }
  else
  {
    if ( dword_14005073C > v10 ) /*0x14000e7d0*/
    {
      sub_1400341D0(a1: &dword_14005073C); /*0x14000e7d9*/
      if ( dword_14005073C == -1 ) /*0x14000e7e5*/
      {
        if ( __eh34_unwind(1) ) /*0xf1c00000000001b0*/
          goto unwind_state_1; /*0xf1c00000000001b0*/
        __eh34_exit_wind_state(1, -1); /*0xf1c00000000001b4*/
        __wind /*0xf1c00000000001b8*/
        {
          xmmword_140050938 = 0; /*0x14000e7ea*/
          sub_14000BA40(); /*0x14000e7f1*/
          atexit(a1: sub_140038CF0); /*0x14000e7fd*/
          Init_thread_footer(a1: &dword_14005073C); /*0x14000e80a*/
        }
        __unwind
        {
          sub_140034128(a1: &dword_14005073C); /*0x1400372d7*/
        }
        __eh34_enter_wind_state(-1, 1); /*0xf1c00000000001a8*/
      }
    }
    v11 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000e80f*/
    if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x14000e819*/
    {
      _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x14000e81b*/
      v11 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000e81f*/
    }
    v22 = xmmword_140050938; /*0x14000e82d*/
    v23 = v11; /*0x14000e831*/
    *(_QWORD *)&v24 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000e835*/
    DWORD2(v24) = 569; /*0x14000e839*/
    HIDWORD(v24) = HIDWORD(v26); /*0x14000e843*/
    v25 = &unk_14003E934; /*0x14000e846*/
    v20 = &unk_14003E990; /*0x14000e851*/
    v21 = 74; /*0x14000e855*/
    v26 = v24; /*0x14000e861*/
    v27 = &unk_14003E934; /*0x14000e86a*/
    sub_140008FF0(a1: xmmword_140050938, a2: &v26, a3: 4, a4: &v20); /*0x14000e87d*/
  }
  if ( v11 != nullptr && _InterlockedExchangeAdd(v11 + 2, 0xFFFFFFFF) == 1 ) /*0x14000e950*/
  {
    (**(void (__fastcall ***)(volatile signed __int32 *))v11)(a1: v11); /*0x14000e958*/
    if ( _InterlockedExchangeAdd(v11 + 3, 0xFFFFFFFF) == 1 ) /*0x14000e964*/
      (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v11 + 8LL))(a1: v11); /*0x14000e96c*/
  }
  if ( qword_1400506F0 == 0 ) /*0x14000e977*/
  {
    v28 = 5; /*0x14000e97d*/
    v13 = 0; /*0x14000e984*/
    if ( __eh34_unwind(1) ) /*0xf1c0000000000168*/
      goto unwind_state_1; /*0xf1c0000000000168*/
    __eh34_exit_wind_state(1, -1); /*0xf1c000000000016c*/
    while ( 1 ) /*0xf1c00000000000c4*/
    {
      __wind /*0xf1c00000000000c4*/
      {
        if ( dword_14005073C > *v0 ) /*0x14000e998*/
        {
          sub_1400341D0(a1: &dword_14005073C); /*0x14000e9a1*/
          if ( dword_14005073C == -1 ) /*0x14000e9ad*/
          {
            xmmword_140050938 = 0; /*0x14000e9b2*/
            sub_14000BA40(); /*0x14000e9b9*/
            atexit(a1: sub_140038CF0); /*0x14000e9c5*/
            Init_thread_footer(a1: &dword_14005073C); /*0x14000e9d2*/
          }
        }
        v14 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000e9d7*/
        if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x14000e9e1*/
        {
          _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x14000e9e3*/
          v14 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000e9e7*/
        }
      }
      __unwind
      {
        sub_140034128(a1: &dword_14005073C); /*0x140037307*/
      }
      __eh34_enter_wind_state(-1, 1); /*0xf1c00000000000dc*/
      v22 = xmmword_140050938; /*0x14000e9f5*/
      v23 = v14; /*0x14000e9f9*/
      v29 = ++v13; /*0x14000ea04*/
      *(_QWORD *)&v24 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000ea07*/
      DWORD2(v24) = 585; /*0x14000ea0b*/
      HIDWORD(v24) = HIDWORD(v26); /*0x14000ea15*/
      v25 = &unk_14003E934; /*0x14000ea18*/
      v20 = &unk_14003E9F8; /*0x14000ea1c*/
      v21 = 37; /*0x14000ea20*/
      v26 = v24; /*0x14000ea2c*/
      v27 = &unk_14003E934; /*0x14000ea35*/
      sub_140019D60( /*0x14000ea54*/
        a1: xmmword_140050938,
        a2: (unsigned int)&v26,
        a3: v12,
        a4: (unsigned int)&v20,
        a5: (__int64)&v29,
        a6: (__int64)&v28);
      if ( v14 != nullptr && _InterlockedExchangeAdd(v14 + 2, 0xFFFFFFFF) == 1 ) /*0x14000ea69*/
      {
        (**(void (__fastcall ***)(volatile signed __int32 *))v14)(a1: v14); /*0x14000ea71*/
        if ( _InterlockedExchangeAdd(v14 + 3, 0xFFFFFFFF) == 1 ) /*0x14000ea7d*/
          (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v14 + 8LL))(a1: v14); /*0x14000ea85*/
      }
      if ( __eh34_unwind(1) ) /*0xf1c00000000000b8*/
        goto unwind_state_1; /*0xf1c00000000000b8*/
      __eh34_exit_wind_state(1, -1); /*0xf1c00000000000bc*/
      if ( (int)sub_14000CFF0() >= 0 && qword_1400506F0 != 0 ) /*0x14000ea99*/
        break; /*0x14000ea99*/
      Sleep(dwMilliseconds: 0x1F4u); /*0x14000eaa4*/
      if ( v13 >= 5 ) /*0x14000eaae*/
      {
        if ( dword_14005073C > *v0 ) /*0x14000eabc*/
        {
          sub_1400341D0(a1: &dword_14005073C); /*0x14000eac5*/
          if ( dword_14005073C == -1 ) /*0x14000ead1*/
          {
            __wind /*0xf1c00000000000a0*/
            {
              xmmword_140050938 = 0; /*0x14000ead6*/
              sub_14000BA40(); /*0x14000eadd*/
              atexit(a1: sub_140038CF0); /*0x14000eae9*/
              Init_thread_footer(a1: &dword_14005073C); /*0x14000eaf6*/
            }
            __unwind
            {
              sub_140034128(a1: &dword_14005073C); /*0x140037337*/
            }
          }
        }
        __eh34_enter_wind_state(-1, 1); /*0xf1c0000000000098*/
        v7 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000eafb*/
        if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x14000eb05*/
        {
          _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x14000eb07*/
          v7 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000eb0b*/
        }
        v22 = xmmword_140050938; /*0x14000eb19*/
        v23 = v7; /*0x14000eb1d*/
        *(_QWORD *)&v24 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000eb21*/
        DWORD2(v24) = 599; /*0x14000eb25*/
        HIDWORD(v24) = HIDWORD(v26); /*0x14000eb2f*/
        v25 = &unk_14003E934; /*0x14000eb32*/
        v20 = &unk_14003EA40; /*0x14000eb3d*/
        v21 = 39; /*0x14000eb41*/
        v26 = v24; /*0x14000eb4d*/
        v27 = &unk_14003E934; /*0x14000eb56*/
        result = sub_140008FF0(a1: xmmword_140050938, a2: &v26, a3: 4, a4: &v20); /*0x14000eb69*/
        goto LABEL_40; /*0x14000eb6f*/
      }
    }
    __wind /*0xf1c0000000000078*/
    {
      if ( dword_14005073C > *v0 ) /*0x14000eb7c*/
      {
        sub_1400341D0(a1: &dword_14005073C); /*0x14000eb85*/
        if ( dword_14005073C == -1 ) /*0x14000eb91*/
        {
          xmmword_140050938 = 0; /*0x14000eb96*/
          sub_14000BA40(); /*0x14000eb9d*/
          atexit(a1: sub_140038CF0); /*0x14000eba9*/
          Init_thread_footer(a1: &dword_14005073C); /*0x14000ebb6*/
        }
      }
      v15 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000ebbb*/
      if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x14000ebc5*/
      {
        _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x14000ebc7*/
        v15 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000ebcb*/
      }
    }
    __unwind
    {
      sub_140034128(a1: &dword_14005073C); /*0x14003731f*/
    }
    __eh34_enter_wind_state(-1, 1); /*0xf1c0000000000090*/
    v22 = xmmword_140050938; /*0x14000ebd9*/
    v23 = v15; /*0x14000ebdd*/
    *(_QWORD *)&v24 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000ebe1*/
    DWORD2(v24) = 589; /*0x14000ebe5*/
    HIDWORD(v24) = HIDWORD(v26); /*0x14000ebef*/
    v25 = &unk_14003E934; /*0x14000ebf2*/
    v20 = &unk_14003EA20; /*0x14000ebfd*/
    v21 = 25; /*0x14000ec01*/
    v26 = v24; /*0x14000ec0d*/
    v27 = &unk_14003E934; /*0x14000ec16*/
    sub_140008FF0(a1: xmmword_140050938, a2: &v26, a3: 2, a4: &v20); /*0x14000ec29*/
    if ( v15 != nullptr && _InterlockedExchangeAdd(v15 + 2, 0xFFFFFFFF) == 1 ) /*0x14000ec3e*/
    {
      (**(void (__fastcall ***)(volatile signed __int32 *))v15)(a1: v15); /*0x14000ec46*/
      if ( _InterlockedExchangeAdd(v15 + 3, 0xFFFFFFFF) == 1 ) /*0x14000ec52*/
        (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v15 + 8LL))(a1: v15); /*0x14000ec5a*/
    }
  }
  byte_14004D124 = 1; /*0x14000ec5d*/
  result = (__int64)CreateThread( /*0x14000ec7d*/
                      lpThreadAttributes: nullptr,
                      dwStackSize: 0,
                      lpStartAddress: sub_140010920,
                      lpParameter: nullptr,
                      dwCreationFlags: 0,
                      lpThreadId: nullptr);
  hObject = (HANDLE)result; /*0x14000ec83*/
  if ( result != 0 ) /*0x14000ec8d*/
  {
    if ( __eh34_unwind(1) ) /*0xf1c0000000000058*/
      goto unwind_state_1; /*0xf1c0000000000058*/
LABEL_101:
    __eh34_exit_wind_state(1, -1); /*0xf1c0000000000064*/
  }
  else
  {
    if ( __eh34_unwind(1) ) /*0xf1c0000000000154*/
      goto unwind_state_1; /*0xf1c0000000000154*/
    __eh34_exit_wind_state(1, -1); /*0xf1c0000000000158*/
    __wind /*0xf1c000000000015c*/
    {
      if ( dword_14005073C > *v0 ) /*0x14000ec9b*/
      {
        sub_1400341D0(a1: &dword_14005073C); /*0x14000eca4*/
        if ( dword_14005073C == -1 ) /*0x14000ecb0*/
        {
          xmmword_140050938 = 0; /*0x14000ecb5*/
          sub_14000BA40(); /*0x14000ecbc*/
          atexit(a1: sub_140038CF0); /*0x14000ecc8*/
          Init_thread_footer(a1: &dword_14005073C); /*0x14000ecd5*/
        }
      }
      v16 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000ecda*/
      if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x14000ece4*/
      {
        _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x14000ece6*/
        v16 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14000ecea*/
      }
      v17 = xmmword_140050938; /*0x14000ecf1*/
      v22 = xmmword_140050938; /*0x14000ecf8*/
      v23 = v16; /*0x14000ecfc*/
      LastError = GetLastError(); /*0x14000ed00*/
    }
    __unwind
    {
      sub_140034128(a1: &dword_14005073C); /*0x14003734f*/
    }
    __eh34_enter_wind_state(-1, 1); /*0xf1c000000000014c*/
    v28 = LastError; /*0x14000ed06*/
    *(_QWORD *)&v24 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000ed09*/
    DWORD2(v24) = 614; /*0x14000ed0d*/
    HIDWORD(v24) = HIDWORD(v26); /*0x14000ed17*/
    v25 = &unk_14003E934; /*0x14000ed1a*/
    v20 = &unk_14003EA68; /*0x14000ed25*/
    v21 = 52; /*0x14000ed29*/
    v26 = v24; /*0x14000ed35*/
    v27 = &unk_14003E934; /*0x14000ed3e*/
    sub_14001A080(a1: v17, a2: (unsigned int)&v26, a3: v19, a4: (unsigned int)&v20, a5: (__int64)&v28); /*0x14000ed57*/
    if ( v16 != nullptr && _InterlockedExchangeAdd(v16 + 2, 0xFFFFFFFF) == 1 ) /*0x14000ed6c*/
    {
      (**(void (__fastcall ***)(volatile signed __int32 *))v16)(a1: v16); /*0x14000ed74*/
      if ( _InterlockedExchangeAdd(v16 + 3, 0xFFFFFFFF) == 1 ) /*0x14000ed7e*/
        (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v16 + 8LL))(a1: v16); /*0x14000ed86*/
    }
    byte_14004D124 = 0; /*0x14000ed89*/
    if ( __eh34_unwind(1) ) /*0xf1c000000000006c*/
      goto unwind_state_1; /*0xf1c000000000006c*/
    __eh34_exit_wind_state(1, -1); /*0xf1c0000000000070*/
    return sub_14000CD70(); /*0x14000ed90*/
  }
  return result; /*0x14000ed95*/
}