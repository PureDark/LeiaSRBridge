HRESULT sub_14000BD20()
{
  HRESULT result; // eax
  int v1; // ebx
  HRESULT v2; // eax
  UINT32 v3; // ecx
  volatile signed __int32 *v4; // rbx
  __int64 v5; // r14
  volatile signed __int32 *v6; // rbx
  char v7; // si
  UINT32 v8; // edi
  UINT32 v9; // edx
  __int64 v10; // rax
  volatile signed __int32 *v11; // rbx
  __int64 v12; // rbx
  volatile signed __int32 *v13; // rbx
  volatile signed __int32 *v14; // rbx
  unsigned int v15; // r15d
  char v16; // r13
  unsigned __int64 v17; // rdi
  int v18; // esi
  int v19; // ebx
  int v20; // r14d
  __int64 v21; // rdx
  int v22; // r8d
  int v23; // eax
  volatile signed __int32 *v24; // rbx
  __int64 i; // rbx
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
  void *v36; // [rsp+B0h] [rbp-50h] BYREF
  __int64 v37; // [rsp+B8h] [rbp-48h]
  unsigned __int128 v38; // [rsp+C0h] [rbp-40h] BYREF
  const char *v39; // [rsp+D0h] [rbp-30h]
  int v40; // [rsp+E0h] [rbp-20h]
  int v41; // [rsp+E4h] [rbp-1Ch]
  int v42; // [rsp+E8h] [rbp-18h]
  int v43; // [rsp+ECh] [rbp-14h] BYREF
  __int64 v44; // [rsp+F0h] [rbp-10h] BYREF
  __int64 v45; // [rsp+F8h] [rbp-8h]
  __int64 v46; // [rsp+100h] [rbp+0h]
  __int64 v47; // [rsp+108h] [rbp+8h]
  const char *v48; // [rsp+110h] [rbp+10h] BYREF
  volatile signed __int32 *v49; // [rsp+118h] [rbp+18h]
  const char *v50; // [rsp+120h] [rbp+20h]
  _DWORD *v51; // [rsp+130h] [rbp+30h]
  _QWORD v52[2]; // [rsp+140h] [rbp+40h] BYREF
  _QWORD v53[2]; // [rsp+150h] [rbp+50h] BYREF
  _QWORD v54[6]; // [rsp+160h] [rbp+60h] BYREF
  IMFActivate **pppSourceActivate; // [rsp+190h] [rbp+90h] BYREF
  IMFAttributes *ppMFAttributes; // [rsp+198h] [rbp+98h] BYREF
  __int64 v57; // [rsp+1A0h] [rbp+A0h] BYREF
  IMFSourceReader *ppSourceReader; // [rsp+1A8h] [rbp+A8h] BYREF
  IMFMediaSource *pMediaSource; // [rsp+1B0h] [rbp+B0h] BYREF
  UINT32 pcSourceActivate; // [rsp+1B8h] [rbp+B8h] BYREF
  unsigned __int64 v61; // [rsp+1C0h] [rbp+C0h] BYREF
  _DWORD v62[2]; // [rsp+1C8h] [rbp+C8h] BYREF

  v42 = dword_14004D120; /*0x14000bd53*/
  v41 = dword_14004D128; /*0x14000bd5c*/
  v40 = dword_14004D12C; /*0x14000bd65*/
  qword_140050158 = 0; /*0x14000bd6b*/
  dword_140050160 = 0; /*0x14000bd72*/
  ppMFAttributes = nullptr; /*0x14000bd79*/
  pppSourceActivate = nullptr; /*0x14000bd80*/
  pcSourceActivate = 0; /*0x14000bd87*/
  result = MFCreateAttributes(&ppMFAttributes, cInitialSize: 2u); /*0x14000bd9a*/
  if ( result >= 0 ) /*0x14000bda2*/
  {
    v1 = ((__int64 (__fastcall *)(IMFAttributes *, void *, void *))ppMFAttributes->lpVtbl->SetGUID)( /*0x14000bdc6*/
           a1: ppMFAttributes,
           a2: &unk_14003C7B8,
           a3: &unk_14003D2E0);
    if ( v1 >= 0 ) /*0x14000bdd1*/
    {
      v2 = ((__int64 (__fastcall *)(IMFAttributes *, void *, void *))ppMFAttributes->lpVtbl->SetGUID)( /*0x14000bdf1*/
             a1: ppMFAttributes,
             a2: &unk_14003CA98,
             a3: &unk_14003DBE8);
      if ( v2 >= 0 ) /*0x14000bdf9*/
        v2 = MFEnumDeviceSources(pAttributes: ppMFAttributes, &pppSourceActivate, &pcSourceActivate); /*0x14000be10*/
      v3 = pcSourceActivate; /*0x14000be2f*/
      if ( pcSourceActivate == 0 ) /*0x14000be37*/
      {
        if ( dword_1400501CC > *(_DWORD *)(*(_QWORD *)NtCurrentTeb()->ThreadLocalStoragePointer + 8LL) ) /*0x14000be52*/
        {
          sub_140034330(a1: &dword_1400501CC); /*0x14000be5b*/
          if ( dword_1400501CC == -1 ) /*0x14000be67*/
          {
            xmmword_1400503C8 = 0; /*0x14000be6c*/
            sub_14000B820(); /*0x14000be73*/
            sub_140034700(Function: sub_140038DB0); /*0x14000be7f*/
            sub_1400342C0(a1: &dword_1400501CC); /*0x14000be8c*/
          }
        }
        v4 = *((volatile signed __int32 **)&xmmword_1400503C8 + 1); /*0x14000be91*/
        if ( *((_QWORD *)&xmmword_1400503C8 + 1) != 0 ) /*0x14000be9b*/
        {
          _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_1400503C8 + 1) + 8LL)); /*0x14000be9d*/
          v4 = *((volatile signed __int32 **)&xmmword_1400503C8 + 1); /*0x14000bea1*/
        }
        v36 = (void *)xmmword_1400503C8; /*0x14000beaf*/
        v37 = (__int64)v4; /*0x14000beb3*/
        v44 = (__int64)&unk_14003E590; /*0x14000bebe*/
        v45 = 41; /*0x14000bec2*/
        *(_QWORD *)&v38 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000beca*/
        DWORD2(v38) = 119; /*0x14000bece*/
        HIDWORD(v38) = HIDWORD(v32); /*0x14000bed8*/
        v39 = "FindAndSelectCamera"; /*0x14000bedb*/
        sub_140008E60(a1: xmmword_1400503C8, a2: &v38, a3: 2, a4: &v44); /*0x14000beed*/
        if ( v4 != nullptr && _InterlockedExchangeAdd(v4 + 2, 0xFFFFFFFF) == 1 ) /*0x14000bf03*/
        {
          (**(void (__fastcall ***)(volatile signed __int32 *))v4)(a1: v4); /*0x14000bf0b*/
          if ( _InterlockedExchangeAdd(v4 + 3, 0xFFFFFFFF) == 1 ) /*0x14000bf18*/
            (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v4 + 8LL))(a1: v4); /*0x14000bf20*/
        }
        if ( pppSourceActivate != nullptr ) /*0x14000bf2d*/
        {
          CoTaskMemFree(pv: pppSourceActivate); /*0x14000bf2f*/
          pppSourceActivate = nullptr; /*0x14000bf35*/
        }
        ((void (__fastcall *)(IMFAttributes *, void *, void *))ppMFAttributes->lpVtbl->SetGUID)( /*0x14000bf54*/
          a1: ppMFAttributes,
          a2: &unk_14003CA98,
          a3: &unk_14003D4A0);
        v2 = MFEnumDeviceSources(pAttributes: ppMFAttributes, &pppSourceActivate, &pcSourceActivate); /*0x14000bf6f*/
        v3 = pcSourceActivate; /*0x14000bf75*/
      }
      if ( v2 >= 0 && v3 != 0 ) /*0x14000bf85*/
      {
        v5 = *(_QWORD *)NtCurrentTeb()->ThreadLocalStoragePointer; /*0x14000bf94*/
        v47 = v5; /*0x14000bf97*/
        v51 = (_DWORD *)(v5 + 8); /*0x14000bf9f*/
        if ( dword_1400501CC > *(_DWORD *)(v5 + 8) ) /*0x14000bfab*/
        {
          sub_140034330(a1: &dword_1400501CC); /*0x14000bfb4*/
          if ( dword_1400501CC == -1 ) /*0x14000bfc0*/
          {
            xmmword_1400503C8 = 0; /*0x14000bfc5*/
            sub_14000B820(); /*0x14000bfcc*/
            sub_140034700(Function: sub_140038DB0); /*0x14000bfd8*/
            sub_1400342C0(a1: &dword_1400501CC); /*0x14000bfe5*/
          }
        }
        v6 = *((volatile signed __int32 **)&xmmword_1400503C8 + 1); /*0x14000bfea*/
        if ( *((_QWORD *)&xmmword_1400503C8 + 1) != 0 ) /*0x14000bff4*/
        {
          _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_1400503C8 + 1) + 8LL)); /*0x14000bff6*/
          v6 = *((volatile signed __int32 **)&xmmword_1400503C8 + 1); /*0x14000bffa*/
        }
        v36 = (void *)xmmword_1400503C8; /*0x14000c008*/
        v37 = (__int64)v6; /*0x14000c00c*/
        *(_QWORD *)&v38 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000c010*/
        DWORD2(v38) = 132; /*0x14000c014*/
        HIDWORD(v38) = HIDWORD(v32); /*0x14000c01e*/
        v44 = (__int64)&unk_14003E640; /*0x14000c028*/
        v45 = 20; /*0x14000c02c*/
        v30 = v38; /*0x14000c038*/
        v31 = "FindAndSelectCamera"; /*0x14000c03d*/
        sub_140017FB0( /*0x14000c05d*/
          a1: xmmword_1400503C8,
          a2: (unsigned int)&v30,
          a3: 2,
          a4: (unsigned int)&v44,
          a5: (__int64)&pcSourceActivate);
        if ( v6 != nullptr && _InterlockedExchangeAdd(v6 + 2, 0xFFFFFFFF) == 1 ) /*0x14000c073*/
        {
          (**(void (__fastcall ***)(volatile signed __int32 *))v6)(a1: v6); /*0x14000c07b*/
          if ( _InterlockedExchangeAdd(v6 + 3, 0xFFFFFFFF) == 1 ) /*0x14000c088*/
            (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v6 + 8LL))(a1: v6); /*0x14000c090*/
        }
        v7 = 0; /*0x14000c093*/
        v28 = 0; /*0x14000c096*/
        v8 = 0; /*0x14000c09b*/
        v29[0] = 0; /*0x14000c09e*/
        v9 = pcSourceActivate; /*0x14000c0a3*/
        if ( pcSourceActivate != 0 ) /*0x14000c0ab*/
        {
          v10 = 8; /*0x14000c0b1*/
          v44 = 8; /*0x14000c0b6*/
          do /*0x14000c0c1*/
          {
            if ( dword_1400501CC > *(_DWORD *)(v5 + v10) ) /*0x14000c0cb*/
            {
              sub_140034330(a1: &dword_1400501CC); /*0x14000c0d4*/
              if ( dword_1400501CC == -1 ) /*0x14000c0e0*/
              {
                xmmword_1400503C8 = 0; /*0x14000c0e5*/
                sub_14000B820(); /*0x14000c0ec*/
                sub_140034700(Function: sub_140038DB0); /*0x14000c0f8*/
                sub_1400342C0(a1: &dword_1400501CC); /*0x14000c105*/
              }
            }
            v11 = *((volatile signed __int32 **)&xmmword_1400503C8 + 1); /*0x14000c111*/
            if ( *((_QWORD *)&xmmword_1400503C8 + 1) != 0 ) /*0x14000c11b*/
            {
              _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_1400503C8 + 1) + 8LL)); /*0x14000c11d*/
              v11 = *((volatile signed __int32 **)&xmmword_1400503C8 + 1); /*0x14000c121*/
            }
            v54[2] = xmmword_1400503C8; /*0x14000c12f*/
            v54[3] = v11; /*0x14000c133*/
            *(_QWORD *)&v30 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000c137*/
            DWORD2(v30) = 138; /*0x14000c13c*/
            HIDWORD(v30) = HIDWORD(v32); /*0x14000c147*/
            v52[0] = &unk_14003E658; /*0x14000c14b*/
            v52[1] = 20; /*0x14000c14f*/
            v32 = v30; /*0x14000c15c*/
            v33 = "FindAndSelectCamera"; /*0x14000c167*/
            sub_140017FB0(a1: xmmword_1400503C8, a2: (unsigned int)&v32, a3: 2, a4: (unsigned int)v52, a5: (__int64)v29); /*0x14000c183*/
            if ( v11 != nullptr && _InterlockedExchangeAdd(v11 + 2, 0xFFFFFFFF) == 1 ) /*0x14000c199*/
            {
              (**(void (__fastcall ***)(volatile signed __int32 *))v11)(a1: v11); /*0x14000c1a1*/
              if ( _InterlockedExchangeAdd(v11 + 3, 0xFFFFFFFF) == 1 ) /*0x14000c1ae*/
                (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v11 + 8LL))(a1: v11); /*0x14000c1b6*/
            }
            pMediaSource = nullptr; /*0x14000c1b9*/
            ppSourceReader = nullptr; /*0x14000c1c0*/
            v12 = 8LL * v8; /*0x14000c1c9*/
            v46 = v12; /*0x14000c1d1*/
            if ( ((int (__fastcall *)(_QWORD, void *, IMFMediaSource **))(*(IMFActivate **)((char *)pppSourceActivate /*0x14000c1f9*/
                                                                                          + v12))->lpVtbl->ActivateObject)(
                   a1: *(IMFActivate **)((char *)pppSourceActivate + v12),
                   a2: &unk_14003DFF0,
                   a3: &pMediaSource) >= 0 )
            {
              if ( MFCreateSourceReaderFromMediaSource(pMediaSource, pAttributes: nullptr, &ppSourceReader) >= 0 ) /*0x14000c340*/
              {
                v15 = 0; /*0x14000c476*/
                v34 = 0; /*0x14000c479*/
                v57 = 0; /*0x14000c47d*/
                v16 = 0; /*0x14000c484*/
                if ( ((int (__fastcall *)(IMFSourceReader *, __int64, _QWORD, __int64 *))ppSourceReader->lpVtbl->GetNativeMediaType)( /*0x14000c4a5*/
                       a1: ppSourceReader,
                       a2: 4294967292LL,
                       a3: 0,
                       a4: &v57) >= 0 )
                {
                  while ( 1 ) /*0x14000c4b0*/
                  {
                    LODWORD(v17) = 0; /*0x14000c4b0*/
                    v18 = 0; /*0x14000c4b5*/
                    v35 = 0; /*0x14000c4b7*/
                    if ( (*(int (__fastcall **)(__int64, void *, unsigned __int64 *))(*(_QWORD *)v57 + 64LL))( /*0x14000c4d7*/
                           a1: v57,
                           a2: &unk_14003BEE0,
                           a3: &v61) < 0 )
                    {
                      v19 = 0; /*0x14000c4f4*/
                      v20 = 0; /*0x14000c4f6*/
                    }
                    else
                    {
                      v18 = v61; /*0x14000c4d9*/
                      v17 = HIDWORD(v61); /*0x14000c4e3*/
                      v35 = v61; /*0x14000c4ea*/
                      v19 = HIDWORD(v61); /*0x14000c4ed*/
                      v20 = v61; /*0x14000c4ef*/
                    }
                    if ( (*(int (__fastcall **)(__int64, void *, _DWORD *))(*(_QWORD *)v57 + 64LL))( /*0x14000c521*/
                           a1: v57,
                           a2: &unk_14003BEF0,
                           a3: v62) >= 0
                      && v62[0] != 0 )
                    {
                      v21 = (unsigned int)(v62[1] % v62[0]); /*0x14000c52c*/
                      v23 = v62[1] / v62[0]; /*0x14000c52c*/
                    }
                    else
                    {
                      v23 = 0; /*0x14000c530*/
                    }
                    v43 = v23; /*0x14000c532*/
                    if ( v19 == v40 && v20 == v41 && v23 == v42 ) /*0x14000c543*/
                      break; /*0x14000c543*/
                    (*(void (__fastcall **)(__int64, __int64))(*(_QWORD *)v57 + 16LL))(a1: v57, a2: v21); /*0x14000c553*/
                    v34 = ++v15; /*0x14000c559*/
                    if ( ((int (__fastcall *)(IMFSourceReader *, __int64, _QWORD, __int64 *))ppSourceReader->lpVtbl->GetNativeMediaType)( /*0x14000c57b*/
                           a1: ppSourceReader,
                           a2: 4294967292LL,
                           a3: v15,
                           a4: &v57) < 0 )
                    {
                      v12 = v46; /*0x14000c581*/
                      v7 = v28; /*0x14000c585*/
                      goto LABEL_76; /*0x14000c585*/
                    }
                  }
                  if ( dword_1400501CC > *v51 ) /*0x14000c60d*/
                  {
                    sub_140034330(a1: &dword_1400501CC); /*0x14000c616*/
                    if ( dword_1400501CC == -1 ) /*0x14000c622*/
                    {
                      xmmword_1400503C8 = 0; /*0x14000c627*/
                      sub_14000B820(); /*0x14000c62e*/
                      sub_140034700(Function: sub_140038DB0); /*0x14000c63a*/
                      sub_1400342C0(a1: &dword_1400501CC); /*0x14000c647*/
                    }
                  }
                  v24 = *((volatile signed __int32 **)&xmmword_1400503C8 + 1); /*0x14000c64c*/
                  if ( *((_QWORD *)&xmmword_1400503C8 + 1) != 0 ) /*0x14000c656*/
                  {
                    _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_1400503C8 + 1) + 8LL)); /*0x14000c658*/
                    v24 = *((volatile signed __int32 **)&xmmword_1400503C8 + 1); /*0x14000c65c*/
                  }
                  v38 = __PAIR128__((unsigned __int64)v24, xmmword_1400503C8); /*0x14000c66a*/
                  *(_QWORD *)&v30 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000c679*/
                  DWORD2(v30) = 175; /*0x14000c67e*/
                  HIDWORD(v30) = HIDWORD(v32); /*0x14000c689*/
                  v36 = &unk_14003E6B0; /*0x14000c694*/
                  v37 = 53; /*0x14000c698*/
                  v32 = v30; /*0x14000c6a5*/
                  v33 = "FindAndSelectCamera"; /*0x14000c6b0*/
                  sub_1400182C0( /*0x14000c6ea*/
                    a1: xmmword_1400503C8,
                    a2: (unsigned int)&v32,
                    a3: v22,
                    a4: (unsigned int)&v36,
                    a5: (__int64)v29,
                    a6: (__int64)&v35 + 4,
                    a7: (__int64)&v35,
                    a8: (__int64)&v43,
                    a9: (__int64)&v34);
                  if ( v24 != nullptr && _InterlockedExchangeAdd(v24 + 2, 0xFFFFFFFF) == 1 ) /*0x14000c700*/
                  {
                    (**(void (__fastcall ***)(volatile signed __int32 *))v24)(a1: v24); /*0x14000c708*/
                    if ( _InterlockedExchangeAdd(v24 + 3, 0xFFFFFFFF) == 1 ) /*0x14000c715*/
                      (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v24 + 8LL))(a1: v24); /*0x14000c71d*/
                  }
                  dword_140050160 = v15; /*0x14000c720*/
                  (*(void (__fastcall **)(__int64, void *, __int128 *))(*(_QWORD *)v57 + 80LL))( /*0x14000c73f*/
                    a1: v57,
                    a2: &unk_14003BED0,
                    a3: &xmmword_140050168);
                  dword_140050148 = v17; /*0x14000c742*/
                  dword_140050150 = v18; /*0x14000c748*/
                  v12 = v46; /*0x14000c755*/
                  qword_140050158 = *(__int64 *)((char *)pppSourceActivate + v46); /*0x14000c75d*/
                  (*(void (**)(void))(*(_QWORD *)qword_140050158 + 8LL))(); /*0x14000c767*/
                  v16 = 1; /*0x14000c76a*/
                  v7 = 1; /*0x14000c76d*/
                  v28 = 1; /*0x14000c771*/
                  (*(void (__fastcall **)(__int64))(*(_QWORD *)v57 + 16LL))(a1: v57); /*0x14000c780*/
LABEL_76:
                  v5 = v47; /*0x14000c58a*/
                  v8 = v29[0]; /*0x14000c58e*/
                }
                if ( ppSourceReader != nullptr ) /*0x14000c59c*/
                  ((void (__fastcall *)(IMFSourceReader *))ppSourceReader->lpVtbl->Release)(a1: ppSourceReader); /*0x14000c5a1*/
                if ( pMediaSource != nullptr ) /*0x14000c5ae*/
                  ((void (__fastcall *)(IMFMediaSource *))pMediaSource->lpVtbl->Release)(a1: pMediaSource); /*0x14000c5b3*/
                ((void (__fastcall *)(_QWORD))(*(IMFActivate **)((char *)pppSourceActivate + v12))->lpVtbl->ShutdownObject)(a1: *(IMFActivate **)((char *)pppSourceActivate + v12)); /*0x14000c5c4*/
                if ( v16 != 0 ) /*0x14000c5cd*/
                {
                  v9 = pcSourceActivate; /*0x14000c788*/
                  break; /*0x14000c788*/
                }
              }
              else
              {
                if ( dword_1400501CC > *(_DWORD *)(*(_QWORD *)NtCurrentTeb()->ThreadLocalStoragePointer + 8LL) ) /*0x14000c360*/
                {
                  sub_140034330(a1: &dword_1400501CC); /*0x14000c369*/
                  if ( dword_1400501CC == -1 ) /*0x14000c375*/
                  {
                    xmmword_1400503C8 = 0; /*0x14000c37a*/
                    sub_14000B820(); /*0x14000c381*/
                    sub_140034700(Function: sub_140038DB0); /*0x14000c38d*/
                    sub_1400342C0(a1: &dword_1400501CC); /*0x14000c39a*/
                  }
                }
                v14 = *((volatile signed __int32 **)&xmmword_1400503C8 + 1); /*0x14000c39f*/
                if ( *((_QWORD *)&xmmword_1400503C8 + 1) != 0 ) /*0x14000c3a9*/
                {
                  _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_1400503C8 + 1) + 8LL)); /*0x14000c3ab*/
                  v14 = *((volatile signed __int32 **)&xmmword_1400503C8 + 1); /*0x14000c3af*/
                }
                v48 = (const char *)xmmword_1400503C8; /*0x14000c3bd*/
                v49 = v14; /*0x14000c3c1*/
                *(_QWORD *)&v30 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000c3c5*/
                DWORD2(v30) = 152; /*0x14000c3ca*/
                HIDWORD(v30) = HIDWORD(v32); /*0x14000c3d5*/
                v54[0] = &unk_14003E688; /*0x14000c3e0*/
                v54[1] = 32; /*0x14000c3e4*/
                v32 = v30; /*0x14000c3f1*/
                v33 = "FindAndSelectCamera"; /*0x14000c3fc*/
                sub_140017FB0( /*0x14000c418*/
                  a1: xmmword_1400503C8,
                  a2: (unsigned int)&v32,
                  a3: 4,
                  a4: (unsigned int)v54,
                  a5: (__int64)v29);
                if ( v14 != nullptr && _InterlockedExchangeAdd(v14 + 2, 0xFFFFFFFF) == 1 ) /*0x14000c42e*/
                {
                  (**(void (__fastcall ***)(volatile signed __int32 *))v14)(a1: v14); /*0x14000c436*/
                  if ( _InterlockedExchangeAdd(v14 + 3, 0xFFFFFFFF) == 1 ) /*0x14000c443*/
                    (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v14 + 8LL))(a1: v14); /*0x14000c44b*/
                }
                ((void (__fastcall *)(IMFMediaSource *))pMediaSource->lpVtbl->Release)(a1: pMediaSource); /*0x14000c458*/
                ((void (__fastcall *)(IMFActivate *))pppSourceActivate[v8]->lpVtbl->ShutdownObject)(a1: pppSourceActivate[v8]); /*0x14000c46b*/
              }
            }
            else
            {
              if ( dword_1400501CC > *(_DWORD *)(*(_QWORD *)NtCurrentTeb()->ThreadLocalStoragePointer + 8LL) ) /*0x14000c219*/
              {
                sub_140034330(a1: &dword_1400501CC); /*0x14000c222*/
                if ( dword_1400501CC == -1 ) /*0x14000c22e*/
                {
                  xmmword_1400503C8 = 0; /*0x14000c233*/
                  sub_14000B820(); /*0x14000c23a*/
                  sub_140034700(Function: sub_140038DB0); /*0x14000c246*/
                  sub_1400342C0(a1: &dword_1400501CC); /*0x14000c253*/
                }
              }
              v13 = *((volatile signed __int32 **)&xmmword_1400503C8 + 1); /*0x14000c258*/
              if ( *((_QWORD *)&xmmword_1400503C8 + 1) != 0 ) /*0x14000c262*/
              {
                _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_1400503C8 + 1) + 8LL)); /*0x14000c264*/
                v13 = *((volatile signed __int32 **)&xmmword_1400503C8 + 1); /*0x14000c268*/
              }
              v54[4] = xmmword_1400503C8; /*0x14000c276*/
              v54[5] = v13; /*0x14000c27d*/
              *(_QWORD *)&v30 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000c284*/
              DWORD2(v30) = 145; /*0x14000c289*/
              HIDWORD(v30) = HIDWORD(v32); /*0x14000c294*/
              v53[0] = &unk_14003E670; /*0x14000c29f*/
              v53[1] = 18; /*0x14000c2a3*/
              v32 = v30; /*0x14000c2b0*/
              v33 = "FindAndSelectCamera"; /*0x14000c2bb*/
              sub_140017FB0( /*0x14000c2d7*/
                a1: xmmword_1400503C8,
                a2: (unsigned int)&v32,
                a3: 4,
                a4: (unsigned int)v53,
                a5: (__int64)v29);
              if ( v13 != nullptr && _InterlockedExchangeAdd(v13 + 2, 0xFFFFFFFF) == 1 ) /*0x14000c2ed*/
              {
                (**(void (__fastcall ***)(volatile signed __int32 *))v13)(a1: v13); /*0x14000c2f5*/
                if ( _InterlockedExchangeAdd(v13 + 3, 0xFFFFFFFF) == 1 ) /*0x14000c302*/
                  (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v13 + 8LL))(a1: v13); /*0x14000c30a*/
              }
              ((void (__fastcall *)(IMFActivate *))pppSourceActivate[v8]->lpVtbl->ShutdownObject)(a1: pppSourceActivate[v8]); /*0x14000c31d*/
            }
            v29[0] = ++v8; /*0x14000c5df*/
            v9 = pcSourceActivate; /*0x14000c5e3*/
            v10 = v44; /*0x14000c5eb*/
          }
          while ( v8 < pcSourceActivate ); /*0x14000c0c1*/
        }
        for ( i = 0; (unsigned int)i < v9; i = (unsigned int)(i + 1) ) /*0x14000c792*/
        {
          v26 = pppSourceActivate[i]; /*0x14000c7a7*/
          if ( v26 != nullptr ) /*0x14000c7ae*/
          {
            ((void (__fastcall *)(IMFActivate *))v26->lpVtbl->Release)(a1: v26); /*0x14000c7b3*/
            v9 = pcSourceActivate; /*0x14000c7b6*/
          }
        }
        CoTaskMemFree(pv: pppSourceActivate); /*0x14000c7c9*/
        if ( ppMFAttributes != nullptr ) /*0x14000c7d9*/
          ((void (__fastcall *)(IMFAttributes *))ppMFAttributes->lpVtbl->Release)(a1: ppMFAttributes); /*0x14000c7de*/
        result = 0; /*0x14000c7e1*/
        if ( v7 == 0 ) /*0x14000c7eb*/
          return -2147023728; /*0x14000c7eb*/
      }
      else
      {
        if ( dword_1400501CC > *(_DWORD *)(*(_QWORD *)NtCurrentTeb()->ThreadLocalStoragePointer + 8LL) ) /*0x14000c808*/
        {
          sub_140034330(a1: &dword_1400501CC); /*0x14000c811*/
          if ( dword_1400501CC == -1 ) /*0x14000c81d*/
          {
            xmmword_1400503C8 = 0; /*0x14000c822*/
            sub_14000B820(); /*0x14000c829*/
            sub_140034700(Function: sub_140038DB0); /*0x14000c835*/
            sub_1400342C0(a1: &dword_1400501CC); /*0x14000c842*/
          }
        }
        v27 = *((volatile signed __int32 **)&xmmword_1400503C8 + 1); /*0x14000c847*/
        if ( *((_QWORD *)&xmmword_1400503C8 + 1) != 0 ) /*0x14000c851*/
        {
          _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_1400503C8 + 1) + 8LL)); /*0x14000c853*/
          v27 = *((volatile signed __int32 **)&xmmword_1400503C8 + 1); /*0x14000c857*/
        }
        v38 = __PAIR128__((unsigned __int64)v27, xmmword_1400503C8); /*0x14000c865*/
        v36 = &unk_14003E628; /*0x14000c874*/
        v37 = 20; /*0x14000c878*/
        v48 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14000c880*/
        LODWORD(v49) = 126; /*0x14000c884*/
        HIDWORD(v49) = HIDWORD(v32); /*0x14000c88e*/
        v50 = "FindAndSelectCamera"; /*0x14000c891*/
        sub_140008E60(a1: xmmword_1400503C8, a2: &v48, a3: 4, a4: &v36); /*0x14000c8a3*/
        if ( v27 != nullptr && _InterlockedExchangeAdd(v27 + 2, 0xFFFFFFFF) == 1 ) /*0x14000c8b9*/
        {
          (**(void (__fastcall ***)(volatile signed __int32 *))v27)(a1: v27); /*0x14000c8c1*/
          if ( _InterlockedExchangeAdd(v27 + 3, 0xFFFFFFFF) == 1 ) /*0x14000c8cd*/
            (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v27 + 8LL))(a1: v27); /*0x14000c8d5*/
        }
        if ( pppSourceActivate != nullptr ) /*0x14000c8e2*/
          CoTaskMemFree(pv: pppSourceActivate); /*0x14000c8e4*/
        ((void (__fastcall *)(IMFAttributes *))ppMFAttributes->lpVtbl->Release)(a1: ppMFAttributes); /*0x14000c8f4*/
        return -2147023728; /*0x14000c8f7*/
      }
    }
    else
    {
      ((void (__fastcall *)(IMFAttributes *))ppMFAttributes->lpVtbl->Release)(a1: ppMFAttributes); /*0x14000bdd6*/
      return v1; /*0x14000bdd9*/
    }
  }
  return result; /*0x14000c8fc*/
}