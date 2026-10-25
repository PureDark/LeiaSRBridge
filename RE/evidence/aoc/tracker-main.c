int __fastcall main(int argc, const char **argv, const char **envp)
{
  UINT ACP; // eax
  UINT v4; // eax
  __int64 *ThreadLocalStoragePointer; // rax
  __int64 v6; // rsi
  volatile signed __int32 *v7; // rbx
  int v8; // edi
  volatile signed __int32 *v9; // rbx
  int v10; // r15d
  int v11; // eax
  int cbMultiByte; // r14d
  CHAR *lpMultiByteStr; // rax
  _QWORD *v14; // rax
  DWORD ModuleFileNameW; // eax
  LPSTR *v16; // r12
  wchar_t *v17; // rax
  __int64 v18; // rcx
  __int64 v19; // r8
  int v20; // eax
  int v21; // r13d
  unsigned __int64 v22; // rcx
  __int64 v23; // r15
  LPSTR *v24; // rax
  size_t v25; // r14
  LPSTR *v26; // rbx
  CHAR *v27; // rax
  HINSTANCE hInstance; // rbx
  HWND Window; // rax
  volatile signed __int32 *v30; // rbx
  HPOWERNOTIFY v31; // r15
  volatile signed __int32 *v32; // rbx
  int result; // eax
  _QWORD *v34; // rax
  __int64 v35; // rdx
  __int64 v36; // r8
  volatile signed __int32 *v37; // rbx
  volatile signed __int32 *v38; // rbx
  volatile signed __int32 *v39; // rbx
  volatile signed __int32 *v40; // rbx
  HANDLE Thread; // r14
  volatile signed __int32 *v42; // rbx
  HRESULT v43; // eax
  int v44; // ecx
  volatile signed __int32 *v45; // rbx
  volatile signed __int32 *v46; // rbx
  __int64 v47; // r8
  volatile signed __int32 *v48; // rbx
  LPVOID v49; // rcx
  volatile signed __int32 *v50; // rbx
  volatile signed __int32 *v51; // rbx
  void *v52; // rcx
  unsigned __int128 v53; // [rsp+60h] [rbp-A0h] BYREF
  const char *v54; // [rsp+70h] [rbp-90h]
  const char *v55; // [rsp+80h] [rbp-80h] BYREF
  __int64 v56; // [rsp+88h] [rbp-78h]
  unsigned __int128 v57; // [rsp+90h] [rbp-70h] BYREF
  const char *v58; // [rsp+A0h] [rbp-60h]
  _QWORD v59[2]; // [rsp+B0h] [rbp-50h] BYREF
  __int128 v60; // [rsp+C0h] [rbp-40h] BYREF
  __m128i v61; // [rsp+D0h] [rbp-30h]
  _BYTE v62[32]; // [rsp+E0h] [rbp-20h] BYREF
  WNDCLASSW WndClass; // [rsp+100h] [rbp+0h] BYREF
  LPSTR v64[2]; // [rsp+150h] [rbp+50h] BYREF
  __m128i si128; // [rsp+160h] [rbp+60h]
  void *Block[2]; // [rsp+170h] [rbp+70h] BYREF
  void *v67[3]; // [rsp+180h] [rbp+80h] BYREF
  unsigned __int64 v68; // [rsp+198h] [rbp+98h]
  struct tagMSG Msg; // [rsp+1A0h] [rbp+A0h] BYREF
  wchar_t Filename[264]; // [rsp+1D0h] [rbp+D0h] BYREF
  WCHAR Buffer[264]; // [rsp+3E0h] [rbp+2E0h] BYREF

  ACP = GetACP(); /*0x14001117b*/
  SetConsoleOutputCP(wCodePageID: ACP); /*0x140011183*/
  v4 = GetACP(); /*0x140011189*/
  SetConsoleCP(wCodePageID: v4); /*0x140011191*/
  ThreadLocalStoragePointer = (__int64 *)NtCurrentTeb()->ThreadLocalStoragePointer; /*0x14001119d*/
  v6 = *ThreadLocalStoragePointer; /*0x1400111a6*/
  if ( dword_14005073C > *(_DWORD *)(*ThreadLocalStoragePointer + 8) ) /*0x1400111b3*/
  {
    sub_1400341D0(a1: &dword_14005073C); /*0x1400111bc*/
    if ( dword_14005073C == -1 ) /*0x1400111c8*/
    {
      __wind /*0xf1c000000000026c*/
      {
        xmmword_140050938 = 0; /*0x1400111cd*/
        sub_14000BA40(); /*0x1400111d4*/
        atexit(a1: sub_140038CF0); /*0x1400111e0*/
        Init_thread_footer(a1: &dword_14005073C); /*0x1400111ed*/
      }
      __unwind
      {
        sub_140034128(a1: &dword_14005073C); /*0x1400375f7*/
      }
    }
  }
  __wind /*0xf1c0000000000234*/
  {
    v7 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x1400111f2*/
    if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x1400111fc*/
    {
      _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x1400111fe*/
      v7 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x140011202*/
    }
    v55 = (const char *)xmmword_140050938; /*0x140011210*/
    v56 = (__int64)v7; /*0x140011214*/
    *(_QWORD *)&v57 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14001121f*/
    DWORD2(v57) = 921; /*0x140011223*/
    HIDWORD(v57) = HIDWORD(v64[1]); /*0x14001122d*/
    v58 = "main"; /*0x140011237*/
    v59[0] = &unk_14003ED80; /*0x140011242*/
    v59[1] = 8; /*0x140011246*/
    v53 = v57; /*0x140011252*/
    v54 = "main"; /*0x14001125c*/
    sub_140008FF0(a1: xmmword_140050938, a2: &v53, a3: 2, a4: v59); /*0x140011271*/
    v8 = -1; /*0x140011277*/
    if ( v7 != nullptr && _InterlockedExchangeAdd(v7 + 2, 0xFFFFFFFF) == 1 ) /*0x14001128d*/
    {
      (**(void (__fastcall ***)(volatile signed __int32 *))v7)(a1: v7); /*0x140011295*/
      if ( _InterlockedExchangeAdd(v7 + 3, 0xFFFFFFFF) == 1 ) /*0x1400112a1*/
        (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v7 + 8LL))(a1: v7); /*0x1400112a9*/
    }
    GetCurrentDirectoryW(nBufferLength: 0x104u, lpBuffer: Buffer); /*0x1400112b8*/
    SetCurrentDirectoryW(lpPathName: Buffer); /*0x1400112c5*/
  }
  __unwind
  {
    sub_140016910(a1: &v55); /*0xf1c0000000000244*/
  }
  __wind /*0xf1c000000000024c*/
  {
    if ( dword_14005073C > *(_DWORD *)(v6 + 8) ) /*0x1400112d5*/
    {
      sub_1400341D0(a1: &dword_14005073C); /*0x1400112de*/
      if ( dword_14005073C == -1 ) /*0x1400112e9*/
      {
        xmmword_140050938 = 0; /*0x1400112ee*/
        sub_14000BA40(); /*0x1400112f5*/
        atexit(a1: sub_140038CF0); /*0x140011301*/
        Init_thread_footer(a1: &dword_14005073C); /*0x14001130e*/
      }
    }
  }
  __unwind
  {
    sub_140034128(a1: &dword_14005073C); /*0x14003760f*/
  }
  __eh34_enter_wind_state(-1, 3); /*0xf1c0000000000264*/
  v9 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x140011313*/
  if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x14001131d*/
  {
    _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x14001131f*/
    v9 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x140011323*/
  }
  v10 = xmmword_140050938; /*0x14001132a*/
  *(_QWORD *)&v57 = xmmword_140050938; /*0x140011331*/
  *((_QWORD *)&v57 + 1) = v9; /*0x140011335*/
  v11 = WideCharToMultiByte( /*0x14001135e*/
          CodePage: 0xFDE9u,
          dwFlags: 0,
          lpWideCharStr: Buffer,
          cchWideChar: -1,
          lpMultiByteStr: nullptr,
          cbMultiByte: 0,
          lpDefaultChar: nullptr,
          lpUsedDefaultChar: nullptr);
  cbMultiByte = v11; /*0x140011364*/
  if ( v11 > 0 ) /*0x140011369*/
  {
    sub_140017650(a1: v64, a2: v11 - 1); /*0x14001138e*/
    lpMultiByteStr = (CHAR *)v64; /*0x140011393*/
    if ( si128.m128i_i64[1] > 0xFuLL ) /*0x14001139c*/
      lpMultiByteStr = v64[0]; /*0x14001139c*/
    WideCharToMultiByte( /*0x1400113c6*/
      CodePage: 0xFDE9u,
      dwFlags: 0,
      lpWideCharStr: Buffer,
      cchWideChar: -1,
      lpMultiByteStr,
      cbMultiByte,
      lpDefaultChar: nullptr,
      lpUsedDefaultChar: nullptr);
    v60 = *(_OWORD *)v64; /*0x1400113d0*/
    v61 = si128; /*0x1400113d8*/
    si128 = _mm_load_si128((const __m128i *)&xmmword_14003F410); /*0x1400113e4*/
    LOBYTE(v64[0]) = 0; /*0x1400113e9*/
    sub_140003EE0(a1: v64); /*0x1400113f1*/
  }
  else
  {
    v60 = 0; /*0x14001136e*/
    v61 = _mm_load_si128((const __m128i *)&xmmword_14003F410); /*0x14001137a*/
    LOBYTE(v60) = 0; /*0x14001137f*/
  }
  __wind /*0xf1c0000000000228*/
  {
    v14 = (_QWORD *)sub_140005920(a1: v62, a2: &v60); /*0x1400113ff*/
    if ( v14[3] > 0xFu ) /*0x14001140a*/
      v14 = (_QWORD *)*v14; /*0x14001140c*/
    __wind /*0xf1c00000000001f8*/
    {
      v59[0] = v14; /*0x14001140f*/
      *(_QWORD *)&v53 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14001141a*/
      DWORD2(v53) = 936; /*0x14001141f*/
      HIDWORD(v53) = HIDWORD(v64[1]); /*0x14001142a*/
      v54 = "main"; /*0x14001142e*/
      v55 = (const char *)&unk_14003ED98; /*0x14001143a*/
      v56 = 12; /*0x14001143e*/
      *(_OWORD *)v64 = v53; /*0x14001144b*/
      si128.m128i_i64[0] = (__int64)"main"; /*0x140011455*/
      sub_14001A370(a1: v10, a2: (unsigned int)v64, a3: 2, a4: (unsigned int)&v55, a5: (__int64)v59); /*0x140011474*/
      sub_140003EE0(a1: v62); /*0x14001147e*/
      sub_140003EE0(a1: &v60); /*0x140011488*/
      if ( v9 != nullptr && _InterlockedExchangeAdd(v9 + 2, 0xFFFFFFFF) == 1 ) /*0x14001149d*/
      {
        (**(void (__fastcall ***)(volatile signed __int32 *))v9)(a1: v9); /*0x1400114a5*/
        if ( _InterlockedExchangeAdd(v9 + 3, 0xFFFFFFFF) == 1 ) /*0x1400114b1*/
          (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v9 + 8LL))(a1: v9); /*0x1400114b9*/
      }
      memset(a1: Filename, Val: 0, Size: 0x20Au); /*0x1400114cb*/
      ModuleFileNameW = GetModuleFileNameW(hModule: nullptr, lpFilename: Filename, nSize: 0x104u); /*0x1400114df*/
    }
    __unwind
    {
      sub_1400029D0(a1: v62); /*0xf1c0000000000208*/
    }
  }
  __unwind
  {
    sub_1400029D0(a1: &v60); /*0xf1c0000000000214*/
  }
  if ( __eh34_unwind(3) ) /*0xf1c000000000021c*/
  {
unwind_state_3:
    sub_140016910(a1: &v57); /*0xf1c0000000000090*/
    __eh34_propagate_exception_into_caller(3, -1); /*0xf1c0000000000094*/
  }
  __eh34_exit_wind_state(3, -1); /*0xf1c0000000000220*/
  v16 = &::lpMultiByteStr; /*0x1400114e5*/
  if ( ModuleFileNameW != 0 ) /*0x1400114ee*/
  {
    v17 = wcsrchr(Str: Filename, Ch: 0x5Cu); /*0x140011500*/
    if ( v17 != nullptr ) /*0x140011509*/
      *v17 = 0; /*0x14001150b*/
    v19 = -1; /*0x140011516*/
    do /*0x140011529*/
      ++v19; /*0x140011520*/
    while ( Filename[v19] != 0 ); /*0x140011529*/
    sub_1400225C0(a1: v18, a2: Filename); /*0x140011532*/
    v20 = WideCharToMultiByte( /*0x14001155c*/
            CodePage: 0xFDE9u,
            dwFlags: 0,
            lpWideCharStr: Filename,
            cchWideChar: -1,
            lpMultiByteStr: nullptr,
            cbMultiByte: 0,
            lpDefaultChar: nullptr,
            lpUsedDefaultChar: nullptr);
    v21 = v20; /*0x140011562*/
    if ( v20 > 0 ) /*0x140011567*/
    {
      v22 = v20 - 1LL; /*0x14001156d*/
      v23 = qword_14004D160; /*0x140011571*/
      if ( v22 > qword_14004D160 ) /*0x14001157b*/
      {
        v25 = v22 - qword_14004D160; /*0x1400115a0*/
        if ( v22 - qword_14004D160 > qword_14004D168 - qword_14004D160 ) /*0x1400115b3*/
        {
          sub_140021240(Src: &::lpMultiByteStr); /*0x1400115ed*/
        }
        else
        {
          qword_14004D160 = v20 - 1LL; /*0x1400115b5*/
          v26 = &::lpMultiByteStr; /*0x1400115bc*/
          if ( (unsigned __int64)qword_14004D168 > 0xF ) /*0x1400115c3*/
            v26 = (LPSTR *)::lpMultiByteStr; /*0x1400115c3*/
          memset(a1: (char *)v26 + v23, Val: 0, Size: v25); /*0x1400115d4*/
          *((_BYTE *)v26 + v25 + v23) = 0; /*0x1400115dd*/
        }
      }
      else
      {
        qword_14004D160 = v20 - 1LL; /*0x14001157d*/
        v24 = &::lpMultiByteStr; /*0x140011584*/
        if ( (unsigned __int64)qword_14004D168 > 0xF ) /*0x14001158f*/
          v24 = (LPSTR *)::lpMultiByteStr; /*0x14001158f*/
        *((_BYTE *)v24 + v22) = 0; /*0x140011597*/
      }
      v27 = (CHAR *)&::lpMultiByteStr; /*0x1400115f9*/
      if ( (unsigned __int64)qword_14004D168 > 0xF ) /*0x140011604*/
        v27 = ::lpMultiByteStr; /*0x140011604*/
      WideCharToMultiByte( /*0x140011633*/
        CodePage: 0xFDE9u,
        dwFlags: 0,
        lpWideCharStr: Filename,
        cchWideChar: -1,
        lpMultiByteStr: v27,
        cbMultiByte: v21,
        lpDefaultChar: nullptr,
        lpUsedDefaultChar: nullptr);
    }
  }
  hInstance = GetModuleHandleW(lpModuleName: nullptr); /*0x140011644*/
  *(_QWORD *)&WndClass.style = 0; /*0x140011647*/
  *(_QWORD *)&WndClass.cbClsExtra = 0; /*0x14001164b*/
  memset(&WndClass.hIcon, 0, 32); /*0x140011652*/
  WndClass.lpfnWndProc = (WNDPROC)&sub_140010F70; /*0x140011666*/
  WndClass.hInstance = hInstance; /*0x14001166a*/
  WndClass.lpszClassName = L"PowerMonitorWindow"; /*0x140011675*/
  RegisterClassW(lpWndClass: &WndClass); /*0x14001167d*/
  Window = CreateWindowExW( /*0x1400116bf*/
             dwExStyle: 0,
             lpClassName: WndClass.lpszClassName,
             lpWindowName: L"HiddenPowerWindow",
             dwStyle: 0,
             X: 0,
             Y: 0,
             nWidth: 0,
             nHeight: 0,
             hWndParent: HWND_MESSAGE,
             hMenu: nullptr,
             hInstance,
             lpParam: nullptr);
  if ( Window == nullptr ) /*0x1400116c8*/
  {
    if ( dword_14005073C > *(_DWORD *)(v6 + 8) ) /*0x1400116dc*/
    {
      sub_1400341D0(a1: &dword_14005073C); /*0x1400116e5*/
      if ( dword_14005073C == -1 ) /*0x1400116f1*/
      {
        __wind /*0xf1c00000000001ec*/
        {
          xmmword_140050938 = 0; /*0x1400116f6*/
          sub_14000BA40(); /*0x1400116fd*/
          atexit(a1: sub_140038CF0); /*0x140011709*/
        }
        __unwind
        {
          sub_140034128(a1: &dword_14005073C); /*0x14003763f*/
        }
        Init_thread_footer(a1: &dword_14005073C); /*0x140011716*/
      }
    }
    v30 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14001171b*/
    if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x140011725*/
    {
      _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x140011727*/
      v30 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x14001172b*/
    }
    __eh34_enter_wind_state(-1, 3); /*0xf1c00000000001d8*/
    v57 = __PAIR128__((unsigned __int64)v30, xmmword_140050938); /*0x140011739*/
    *(_QWORD *)&v53 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x140011741*/
    DWORD2(v53) = 979; /*0x140011746*/
    HIDWORD(v53) = HIDWORD(v64[1]); /*0x140011751*/
    v54 = "main"; /*0x14001175c*/
    v55 = "CreateWindow failed"; /*0x140011768*/
    v56 = 19; /*0x14001176c*/
    *(_OWORD *)v64 = v53; /*0x140011779*/
    si128.m128i_i64[0] = (__int64)"main"; /*0x140011783*/
    sub_140008FF0(a1: xmmword_140050938, a2: v64, a3: 4, a4: &v55); /*0x140011796*/
    if ( v30 != nullptr && _InterlockedExchangeAdd(v30 + 2, 0xFFFFFFFF) == 1 ) /*0x1400117af*/
    {
      (**(void (__fastcall ***)(volatile signed __int32 *))v30)(a1: v30); /*0x1400117bb*/
      if ( _InterlockedExchangeAdd(v30 + 3, 0xFFFFFFFF) == 1 ) /*0x1400117c7*/
        (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v30 + 8LL))(a1: v30); /*0x1400117d3*/
    }
LABEL_187:
    result = v8; /*0x1400126fc*/
    goto LABEL_188; /*0x1400126fc*/
  }
  __eh34_enter_wind_state(-1, 7); /*0xf1c00000000001d0*/
  v31 = RegisterPowerSettingNotification(hRecipient: Window, PowerSettingGuid: &Buf2, Flags: 0); /*0x1400117ee*/
  if ( v31 != nullptr ) /*0x1400117f4*/
  {
    if ( __eh34_unwind(7) ) /*0xf1c00000000001bc*/
    {
unwind_state_7:
      sub_140034128(a1: &dword_14005073C); /*0xf1c00000000001c0*/
      __eh34_propagate_exception_into_caller(7, -1); /*0xf1c00000000001c4*/
    }
    __eh34_exit_wind_state(7, -1); /*0xf1c00000000001c8*/
    sub_140015B30(a1: v67); /*0x14001190c*/
    __wind /*0xf1c00000000001b0*/
    {
      Block[0] = nullptr; /*0x140011919*/
      Block[1] = nullptr; /*0x14001191d*/
      v34 = operator new(Size: 0x50u); /*0x140011926*/
      __wind /*0xf1c0000000000184*/
      {
        *v34 = v34; /*0x14001192b*/
        v34[1] = v34; /*0x14001192e*/
        v34[2] = v34; /*0x140011932*/
        *((_WORD *)v34 + 12) = 257; /*0x140011936*/
        Block[0] = v34; /*0x14001193c*/
        if ( (unsigned __int8)sub_140001400(a1: Block, a2: v67) == 0 ) /*0x140011952*/
        {
          if ( dword_14005073C > *(_DWORD *)(v6 + 8) ) /*0x140011966*/
          {
            sub_1400341D0(a1: &dword_14005073C); /*0x14001196f*/
            if ( dword_14005073C == -1 ) /*0x14001197b*/
            {
              __wind /*0xf1c000000000016c*/
              {
                xmmword_140050938 = 0; /*0x140011980*/
                sub_14000BA40(); /*0x140011987*/
                atexit(a1: sub_140038CF0); /*0x140011993*/
                Init_thread_footer(a1: &dword_14005073C); /*0x1400119a0*/
              }
              __unwind
              {
                sub_140034128(a1: &dword_14005073C); /*0x140037687*/
              }
            }
          }
          __eh34_enter_wind_state(9, 11); /*0xf1c0000000000164*/
          v37 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x1400119a5*/
          if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x1400119af*/
          {
            _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x1400119b1*/
            v37 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x1400119b5*/
          }
          v57 = __PAIR128__((unsigned __int64)v37, xmmword_140050938); /*0x1400119c3*/
          if ( (unsigned __int64)qword_14004D168 > 0xF ) /*0x1400119d3*/
            v16 = (LPSTR *)::lpMultiByteStr; /*0x1400119d3*/
          v59[0] = v16; /*0x1400119db*/
          *(_QWORD *)&v53 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x1400119df*/
          DWORD2(v53) = 1003; /*0x1400119e4*/
          HIDWORD(v53) = HIDWORD(v64[1]); /*0x1400119ef*/
          v54 = "main"; /*0x1400119fa*/
          v55 = (const char *)&unk_14003EE68; /*0x140011a06*/
          v56 = 54; /*0x140011a0a*/
          *(_OWORD *)v64 = v53; /*0x140011a17*/
          si128.m128i_i64[0] = (__int64)"main"; /*0x140011a21*/
          sub_14001A370(a1: xmmword_140050938, a2: (unsigned int)v64, a3: 4, a4: (unsigned int)&v55, a5: (__int64)v59); /*0x140011a3d*/
          if ( v37 != nullptr && _InterlockedExchangeAdd(v37 + 2, 0xFFFFFFFF) == 1 ) /*0x140011a52*/
          {
            (**(void (__fastcall ***)(volatile signed __int32 *))v37)(a1: v37); /*0x140011a5a*/
            if ( _InterlockedExchangeAdd(v37 + 3, 0xFFFFFFFF) == 1 ) /*0x140011a66*/
              (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v37 + 8LL))(a1: v37); /*0x140011a6e*/
          }
          if ( __eh34_unwind(11) ) /*0xf1c0000000000158*/
          {
unwind_state_11:
            sub_140016910(a1: &v57); /*0xf1c00000000000fc*/
            __eh34_continue_unwinding(11, 9); /*0xf1c0000000000100*/
          }
          __eh34_exit_wind_state(11, 9); /*0xf1c000000000015c*/
        }
        LODWORD(v59[0]) = 1032134328; /*0x140011a71*/
        sub_140001FA0(a1: Block, a2: v35, a3: v36, a4: v59); /*0x140011a80*/
        LODWORD(v59[0]) = 1; /*0x140011a85*/
        dword_1400506B4 = sub_140001CD0(a1: Block, a2: "show_kps", a3: "show_img", a4: v59); /*0x140011aa7*/
        LODWORD(v59[0]) = 1280; /*0x140011aad*/
        dword_14004D12C = sub_140001CD0(a1: Block, a2: "camera_info", a3: "width", a4: v59); /*0x140011acf*/
        LODWORD(v59[0]) = 480; /*0x140011ad5*/
        dword_14004D128 = sub_140001CD0(a1: Block, a2: "camera_info", a3: "height", a4: v59); /*0x140011af7*/
        LODWORD(v59[0]) = 90; /*0x140011afd*/
        dword_14004D120 = sub_140001CD0(a1: Block, a2: "camera_info", a3: "fps", a4: v59); /*0x140011b1f*/
        if ( dword_14005073C > *(_DWORD *)(v6 + 8) ) /*0x140011b35*/
        {
          sub_1400341D0(a1: &dword_14005073C); /*0x140011b3e*/
          if ( dword_14005073C == -1 ) /*0x140011b4a*/
          {
            __wind /*0xf1c000000000014c*/
            {
              xmmword_140050938 = 0; /*0x140011b4f*/
              sub_14000BA40(); /*0x140011b56*/
              atexit(a1: sub_140038CF0); /*0x140011b62*/
            }
            __unwind
            {
              sub_140034128(a1: &dword_14005073C); /*0x14003769f*/
            }
            Init_thread_footer(a1: &dword_14005073C); /*0x140011b6f*/
          }
        }
        v38 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x140011b74*/
        if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x140011b7e*/
        {
          _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x140011b80*/
          v38 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x140011b84*/
        }
        __eh34_enter_wind_state(9, 11); /*0xf1c0000000000138*/
        v57 = __PAIR128__((unsigned __int64)v38, xmmword_140050938); /*0x140011b92*/
        *(_QWORD *)&v53 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x140011b9a*/
        DWORD2(v53) = 1015; /*0x140011b9f*/
        HIDWORD(v53) = HIDWORD(v64[1]); /*0x140011baa*/
        v54 = "main"; /*0x140011bb5*/
        v55 = (const char *)&unk_14003EF00; /*0x140011bc1*/
        v56 = 13; /*0x140011bc5*/
        *(_OWORD *)v64 = v53; /*0x140011bd2*/
        si128.m128i_i64[0] = (__int64)"main"; /*0x140011bdc*/
        sub_140008FF0(a1: xmmword_140050938, a2: v64, a3: 2, a4: &v55); /*0x140011bef*/
        if ( v38 != nullptr && _InterlockedExchangeAdd(v38 + 2, 0xFFFFFFFF) == 1 ) /*0x140011c04*/
        {
          (**(void (__fastcall ***)(volatile signed __int32 *))v38)(a1: v38); /*0x140011c0c*/
          if ( _InterlockedExchangeAdd(v38 + 3, 0xFFFFFFFF) == 1 ) /*0x140011c18*/
            (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v38 + 8LL))(a1: v38); /*0x140011c20*/
        }
        if ( CoInitializeEx(pvReserved: nullptr, dwCoInit: 6u) >= 0 ) /*0x140011c32*/
        {
          if ( CoCreateInstance(rclsid: &rclsid, pUnkOuter: nullptr, dwClsContext: 1u, riid: &riid, ppv: &ppv) >= 0 ) /*0x140011d31*/
          {
            if ( dword_14005073C > *(_DWORD *)(v6 + 8) ) /*0x140011e50*/
            {
              sub_1400341D0(a1: &dword_14005073C); /*0x140011e59*/
              if ( dword_14005073C == -1 ) /*0x140011e65*/
              {
                if ( __eh34_unwind(11) ) /*0xf1c0000000000310*/
                  goto unwind_state_11; /*0xf1c0000000000310*/
                __eh34_exit_wind_state(11, 9); /*0xf1c0000000000314*/
                __wind /*0xf1c0000000000318*/
                {
                  xmmword_140050938 = 0; /*0x140011e6a*/
                  sub_14000BA40(); /*0x140011e71*/
                  atexit(a1: sub_140038CF0); /*0x140011e7d*/
                  Init_thread_footer(a1: &dword_14005073C); /*0x140011e8a*/
                }
                __unwind
                {
                  sub_140034128(a1: &dword_14005073C); /*0x1400376e7*/
                }
                __eh34_enter_wind_state(9, 11); /*0xf1c0000000000308*/
              }
            }
            v40 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x140011e8f*/
            if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x140011e99*/
            {
              _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x140011e9b*/
              v40 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x140011e9f*/
            }
            v57 = __PAIR128__((unsigned __int64)v40, xmmword_140050938); /*0x140011ead*/
            *(_QWORD *)&v53 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x140011eb5*/
            DWORD2(v53) = 1029; /*0x140011eba*/
            HIDWORD(v53) = HIDWORD(v64[1]); /*0x140011ec5*/
            v54 = "main"; /*0x140011ed0*/
            v55 = (const char *)&unk_14003EF40; /*0x140011edc*/
            v56 = 14; /*0x140011ee0*/
            *(_OWORD *)v64 = v53; /*0x140011eed*/
            si128.m128i_i64[0] = (__int64)"main"; /*0x140011ef7*/
            sub_140008FF0(a1: xmmword_140050938, a2: v64, a3: 2, a4: &v55); /*0x140011f0a*/
            if ( v40 != nullptr && _InterlockedExchangeAdd(v40 + 2, 0xFFFFFFFF) == 1 ) /*0x140011f1f*/
            {
              (**(void (__fastcall ***)(volatile signed __int32 *))v40)(a1: v40); /*0x140011f27*/
              if ( _InterlockedExchangeAdd(v40 + 3, 0xFFFFFFFF) == 1 ) /*0x140011f33*/
                (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v40 + 8LL))(a1: v40); /*0x140011f3b*/
            }
            Thread = CreateThread( /*0x140011f5c*/
                       lpThreadAttributes: nullptr,
                       dwStackSize: 0,
                       lpStartAddress: sub_14000FFC0,
                       lpParameter: nullptr,
                       dwCreationFlags: 0,
                       lpThreadId: nullptr);
            if ( Thread != nullptr ) /*0x140011f62*/
            {
              if ( dword_14005073C > *(_DWORD *)(v6 + 8) ) /*0x140011f72*/
              {
                sub_1400341D0(a1: &dword_14005073C); /*0x140011f7b*/
                if ( dword_14005073C == -1 ) /*0x140011f87*/
                {
                  if ( __eh34_unwind(11) ) /*0xf1c00000000002e8*/
                    goto unwind_state_11; /*0xf1c00000000002e8*/
                  __eh34_exit_wind_state(11, 9); /*0xf1c00000000002ec*/
                  __wind /*0xf1c00000000002f0*/
                  {
                    xmmword_140050938 = 0; /*0x140011f8c*/
                    sub_14000BA40(); /*0x140011f93*/
                    atexit(a1: sub_140038CF0); /*0x140011f9f*/
                    Init_thread_footer(a1: &dword_14005073C); /*0x140011fac*/
                  }
                  __unwind
                  {
                    sub_140034128(a1: &dword_14005073C); /*0x1400376ff*/
                  }
                  __eh34_enter_wind_state(9, 11); /*0xf1c00000000002e0*/
                }
              }
              v42 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x140011fb1*/
              if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x140011fbb*/
              {
                _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x140011fbd*/
                v42 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x140011fc1*/
              }
              v57 = __PAIR128__((unsigned __int64)v42, xmmword_140050938); /*0x140011fcf*/
              *(_QWORD *)&v53 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x140011fde*/
              DWORD2(v53) = 1068; /*0x140011fe3*/
              HIDWORD(v53) = HIDWORD(v64[1]); /*0x140011fee*/
              v54 = "main"; /*0x140011ff9*/
              v55 = (const char *)&unk_14003EF50; /*0x140012005*/
              v56 = 26; /*0x140012009*/
              *(_OWORD *)v64 = v53; /*0x140012016*/
              si128.m128i_i64[0] = (__int64)"main"; /*0x140012020*/
              sub_140008FF0(a1: xmmword_140050938, a2: v64, a3: 2, a4: &v55); /*0x140012033*/
              if ( v42 != nullptr && _InterlockedExchangeAdd(v42 + 2, 0xFFFFFFFF) == 1 ) /*0x140012048*/
              {
                (**(void (__fastcall ***)(volatile signed __int32 *))v42)(a1: v42); /*0x140012050*/
                if ( _InterlockedExchangeAdd(v42 + 3, 0xFFFFFFFF) == 1 ) /*0x14001205c*/
                  (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v42 + 8LL))(a1: v42); /*0x140012064*/
              }
              v43 = MFStartup(Version: 0x20070u, dwFlags: 0); /*0x14001206e*/
              v44 = *(_DWORD *)(v6 + 8); /*0x140012074*/
              if ( v43 >= 0 ) /*0x14001207a*/
              {
                if ( dword_14005073C > v44 ) /*0x140012192*/
                {
                  sub_1400341D0(a1: &dword_14005073C); /*0x14001219b*/
                  if ( dword_14005073C == -1 ) /*0x1400121a7*/
                  {
                    if ( __eh34_unwind(11) ) /*0xf1c0000000000298*/
                      goto unwind_state_11; /*0xf1c0000000000298*/
                    __eh34_exit_wind_state(11, 9); /*0xf1c000000000029c*/
                    __wind /*0xf1c00000000002a0*/
                    {
                      xmmword_140050938 = 0; /*0x1400121ac*/
                      sub_14000BA40(); /*0x1400121b3*/
                      atexit(a1: sub_140038CF0); /*0x1400121bf*/
                      Init_thread_footer(a1: &dword_14005073C); /*0x1400121cc*/
                    }
                    __unwind
                    {
                      sub_140034128(a1: &dword_14005073C); /*0x14003772f*/
                    }
                    __eh34_enter_wind_state(9, 11); /*0xf1c0000000000290*/
                  }
                }
                v46 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x1400121d1*/
                if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x1400121db*/
                {
                  _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x1400121dd*/
                  v46 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x1400121e1*/
                }
                v57 = __PAIR128__((unsigned __int64)v46, xmmword_140050938); /*0x1400121ef*/
                *(_QWORD *)&v53 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x1400121fe*/
                DWORD2(v53) = 1077; /*0x140012203*/
                HIDWORD(v53) = HIDWORD(v64[1]); /*0x14001220e*/
                v54 = "main"; /*0x140012219*/
                v55 = (const char *)&unk_14003EF90; /*0x140012225*/
                v56 = 21; /*0x140012229*/
                *(_OWORD *)v64 = v53; /*0x140012236*/
                si128.m128i_i64[0] = (__int64)"main"; /*0x140012240*/
                sub_140008FF0(a1: xmmword_140050938, a2: v64, a3: 2, a4: &v55); /*0x140012253*/
                if ( v46 != nullptr && _InterlockedExchangeAdd(v46 + 2, 0xFFFFFFFF) == 1 ) /*0x140012268*/
                {
                  (**(void (__fastcall ***)(volatile signed __int32 *))v46)(a1: v46); /*0x140012270*/
                  if ( _InterlockedExchangeAdd(v46 + 3, 0xFFFFFFFF) == 1 ) /*0x14001227c*/
                    (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v46 + 8LL))(a1: v46); /*0x140012284*/
                }
                if ( __eh34_unwind(11) ) /*0xf1c000000000012c*/
                  goto unwind_state_11; /*0xf1c000000000012c*/
                __eh34_exit_wind_state(11, 9); /*0xf1c0000000000130*/
                if ( (int)sub_14000C190() >= 0 && qword_1400506B8 != 0 ) /*0x14001229c*/
                {
                  if ( dword_14005073C > *(_DWORD *)(v6 + 8) ) /*0x1400122ac*/
                  {
                    sub_1400341D0(a1: &dword_14005073C); /*0x1400122b5*/
                    if ( dword_14005073C == -1 ) /*0x1400122c1*/
                    {
                      __wind /*0xf1c0000000000114*/
                      {
                        xmmword_140050938 = 0; /*0x1400122c6*/
                        sub_14000BA40(); /*0x1400122cd*/
                        atexit(a1: sub_140038CF0); /*0x1400122d9*/
                        Init_thread_footer(a1: &dword_14005073C); /*0x1400122e6*/
                      }
                      __unwind
                      {
                        sub_140034128(a1: &dword_14005073C); /*0x14003775f*/
                      }
                    }
                  }
                  __eh34_enter_wind_state(9, 11); /*0xf1c000000000010c*/
                  v48 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x1400122eb*/
                  if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x1400122f5*/
                  {
                    _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x1400122f7*/
                    v48 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x1400122fb*/
                  }
                  v57 = __PAIR128__((unsigned __int64)v48, xmmword_140050938); /*0x140012309*/
                  *(_QWORD *)&v53 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x140012318*/
                  DWORD2(v53) = 1095; /*0x14001231d*/
                  HIDWORD(v53) = HIDWORD(v64[1]); /*0x140012328*/
                  v54 = "main"; /*0x140012333*/
                  v55 = (const char *)&unk_14003E7C8; /*0x14001233f*/
                  v56 = 43; /*0x140012343*/
                  *(_OWORD *)v64 = v53; /*0x140012350*/
                  si128.m128i_i64[0] = (__int64)"main"; /*0x14001235a*/
                  sub_140008FF0(a1: xmmword_140050938, a2: v64, a3: 2, a4: &v55); /*0x14001236d*/
                  if ( v48 != nullptr && _InterlockedExchangeAdd(v48 + 2, 0xFFFFFFFF) == 1 ) /*0x140012382*/
                  {
                    (**(void (__fastcall ***)(volatile signed __int32 *))v48)(a1: v48); /*0x14001238a*/
                    if ( _InterlockedExchangeAdd(v48 + 3, 0xFFFFFFFF) == 1 ) /*0x140012396*/
                      (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v48 + 8LL))(a1: v48); /*0x14001239e*/
                  }
                  while ( GetMessageW(lpMsg: &Msg, hWnd: nullptr, wMsgFilterMin: 0, wMsgFilterMax: 0) ) /*0x1400123b8*/
                  {
                    TranslateMessage(lpMsg: &Msg); /*0x1400123c7*/
                    DispatchMessageW(lpMsg: &Msg); /*0x1400123d4*/
                  }
                  UnregisterPowerSettingNotification(Handle: v31); /*0x1400123f6*/
                  if ( __eh34_unwind(11) ) /*0xf1c00000000000f8*/
                    goto unwind_state_11; /*0xf1c00000000000f8*/
                  __eh34_exit_wind_state(11, 9); /*0xf1c0000000000104*/
                  if ( qword_1400506F0 != 0 ) /*0x140012406*/
                    (*(void (__fastcall **)(__int64))(*(_QWORD *)qword_1400506F0 + 16LL))(a1: qword_1400506F0); /*0x14001240b*/
                  if ( qword_1400506B8 != 0 ) /*0x140012418*/
                  {
                    (*(void (__fastcall **)(__int64))(*(_QWORD *)qword_1400506B8 + 272LL))(a1: qword_1400506B8); /*0x14001241d*/
                    (*(void (__fastcall **)(__int64))(*(_QWORD *)qword_1400506B8 + 16LL))(a1: qword_1400506B8); /*0x14001242d*/
                    qword_1400506B8 = 0; /*0x140012430*/
                  }
                  MFShutdown(); /*0x140012437*/
                  __wind /*0xf1c00000000000ec*/
                  {
                    v49 = ppv; /*0x14001243e*/
                    if ( ppv != nullptr ) /*0x140012448*/
                    {
                      ppv = nullptr; /*0x14001244a*/
                      (*(void (__fastcall **)(LPVOID))(*(_QWORD *)v49 + 16LL))(a1: v49); /*0x140012454*/
                    }
                  }
                  __unwind
                  {
                    __std_terminate(); /*0x140036170*/
                  }
                  CoUninitialize(); /*0x140012458*/
                  WaitForSingleObject(hHandle: Thread, dwMilliseconds: 0xFFFFFFFF); /*0x140012466*/
                  CloseHandle(hObject: Thread); /*0x14001246f*/
                  if ( dword_14005073C > *(_DWORD *)(v6 + 8) ) /*0x14001247f*/
                  {
                    sub_1400341D0(a1: &dword_14005073C); /*0x140012488*/
                    if ( dword_14005073C == -1 ) /*0x140012494*/
                    {
                      __wind /*0xf1c00000000000c8*/
                      {
                        xmmword_140050938 = 0; /*0x140012499*/
                        sub_14000BA40(); /*0x1400124a0*/
                        atexit(a1: sub_140038CF0); /*0x1400124ac*/
                        Init_thread_footer(a1: &dword_14005073C); /*0x1400124b9*/
                      }
                      __unwind
                      {
                        sub_140034128(a1: &dword_14005073C); /*0x140037777*/
                      }
                    }
                  }
                  __eh34_enter_wind_state(9, 11); /*0xf1c00000000000c0*/
                  v50 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x1400124be*/
                  if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x1400124c8*/
                  {
                    _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x1400124ca*/
                    v50 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x1400124ce*/
                  }
                  v57 = __PAIR128__((unsigned __int64)v50, xmmword_140050938); /*0x1400124dc*/
                  *(_QWORD *)&v53 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x1400124eb*/
                  DWORD2(v53) = 1137; /*0x1400124f0*/
                  HIDWORD(v53) = HIDWORD(v64[1]); /*0x1400124fb*/
                  v54 = "main"; /*0x140012506*/
                  v55 = (const char *)&unk_14003EFA8; /*0x140012512*/
                  v56 = 8; /*0x140012516*/
                  *(_OWORD *)v64 = v53; /*0x140012523*/
                  si128.m128i_i64[0] = (__int64)"main"; /*0x14001252d*/
                  sub_140008FF0(a1: xmmword_140050938, a2: v64, a3: 2, a4: &v55); /*0x140012540*/
                  if ( v50 != nullptr && _InterlockedExchangeAdd(v50 + 2, 0xFFFFFFFF) == 1 ) /*0x140012555*/
                  {
                    (**(void (__fastcall ***)(volatile signed __int32 *))v50)(a1: v50); /*0x14001255d*/
                    if ( _InterlockedExchangeAdd(v50 + 3, 0xFFFFFFFF) == 1 ) /*0x140012567*/
                      (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v50 + 8LL))(a1: v50); /*0x14001256f*/
                  }
                  v8 = 0; /*0x140012572*/
                  goto LABEL_182; /*0x140012575*/
                }
                __wind /*0xf1c00000000000a0*/
                {
                  if ( dword_14005073C > *(_DWORD *)(v6 + 8) ) /*0x140012584*/
                  {
                    sub_1400341D0(a1: &dword_14005073C); /*0x14001258d*/
                    if ( dword_14005073C == -1 ) /*0x140012599*/
                    {
                      xmmword_140050938 = 0; /*0x14001259e*/
                      sub_14000BA40(); /*0x1400125a5*/
                      atexit(a1: sub_140038CF0); /*0x1400125b1*/
                      Init_thread_footer(a1: &dword_14005073C); /*0x1400125be*/
                    }
                  }
                  v51 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x1400125c3*/
                  if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x1400125cd*/
                  {
                    _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x1400125cf*/
                    v51 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x1400125d3*/
                  }
                }
                __unwind
                {
                  sub_140034128(a1: &dword_14005073C); /*0x140037747*/
                }
                __eh34_enter_wind_state(9, 11); /*0xf1c00000000000b8*/
                v57 = __PAIR128__((unsigned __int64)v51, xmmword_140050938); /*0x1400125e1*/
                *(_QWORD *)&v53 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x1400125f0*/
                DWORD2(v53) = 1090; /*0x1400125f5*/
                HIDWORD(v53) = HIDWORD(v64[1]); /*0x140012600*/
                v54 = "main"; /*0x14001260b*/
                v55 = (const char *)&unk_14003E7A8; /*0x140012617*/
                v56 = 31; /*0x14001261b*/
                *(_OWORD *)v64 = v53; /*0x140012628*/
                si128.m128i_i64[0] = (__int64)"main"; /*0x140012632*/
                sub_140019A20(a1: xmmword_140050938, a2: v64, a3: v47, a4: &v55); /*0x14001263f*/
                if ( v51 != nullptr && _InterlockedExchangeAdd(v51 + 2, 0xFFFFFFFF) == 1 ) /*0x140012654*/
                {
                  (**(void (__fastcall ***)(volatile signed __int32 *))v51)(a1: v51); /*0x14001265c*/
                  if ( _InterlockedExchangeAdd(v51 + 3, 0xFFFFFFFF) == 1 ) /*0x140012668*/
                    (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v51 + 8LL))(a1: v51); /*0x140012670*/
                }
                MFShutdown(); /*0x140012673*/
              }
              else
              {
                if ( dword_14005073C > v44 ) /*0x140012086*/
                {
                  sub_1400341D0(a1: &dword_14005073C); /*0x14001208f*/
                  if ( dword_14005073C == -1 ) /*0x14001209b*/
                  {
                    if ( __eh34_unwind(11) ) /*0xf1c00000000002c0*/
                      goto unwind_state_11; /*0xf1c00000000002c0*/
                    __eh34_exit_wind_state(11, 9); /*0xf1c00000000002c4*/
                    __wind /*0xf1c00000000002c8*/
                    {
                      xmmword_140050938 = 0; /*0x1400120a0*/
                      sub_14000BA40(); /*0x1400120a7*/
                      atexit(a1: sub_140038CF0); /*0x1400120b3*/
                      Init_thread_footer(a1: &dword_14005073C); /*0x1400120c0*/
                    }
                    __unwind
                    {
                      sub_140034128(a1: &dword_14005073C); /*0x140037717*/
                    }
                    __eh34_enter_wind_state(9, 11); /*0xf1c00000000002b8*/
                  }
                }
                v45 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x1400120c5*/
                if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x1400120cf*/
                {
                  _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x1400120d1*/
                  v45 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x1400120d5*/
                }
                v57 = __PAIR128__((unsigned __int64)v45, xmmword_140050938); /*0x1400120e3*/
                *(_QWORD *)&v53 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x1400120f2*/
                DWORD2(v53) = 1072; /*0x1400120f7*/
                HIDWORD(v53) = HIDWORD(v64[1]); /*0x140012102*/
                v54 = "main"; /*0x14001210d*/
                v55 = (const char *)&unk_14003EF70; /*0x140012119*/
                v56 = 27; /*0x14001211d*/
                *(_OWORD *)v64 = v53; /*0x14001212a*/
                si128.m128i_i64[0] = (__int64)"main"; /*0x140012134*/
                sub_140008FF0(a1: xmmword_140050938, a2: v64, a3: 4, a4: &v55); /*0x140012147*/
                if ( v45 != nullptr && _InterlockedExchangeAdd(v45 + 2, 0xFFFFFFFF) == 1 ) /*0x140012160*/
                {
                  (**(void (__fastcall ***)(volatile signed __int32 *))v45)(a1: v45); /*0x14001216c*/
                  if ( _InterlockedExchangeAdd(v45 + 3, 0xFFFFFFFF) == 1 ) /*0x140012178*/
                    (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v45 + 8LL))(a1: v45); /*0x140012184*/
                }
              }
            }
            CoUninitialize(); /*0x140012679*/
LABEL_182:
            sub_140003AE0(a1: Block, a2: Block, a3: *((_QWORD *)Block[0] + 1)); /*0x140012680*/
            j_j_free(Block: Block[0]); /*0x14001269e*/
            if ( v68 > 7 ) /*0x1400126af*/
            {
              v52 = v67[0]; /*0x1400126b9*/
              if ( 2 * v68 + 2 >= 0x1000 ) /*0x1400126ca*/
              {
                v52 = *((void **)v67[0] - 1); /*0x1400126d0*/
                if ( (unsigned __int64)((char *)v67[0] - (char *)v52 - 8) > 0x1F ) /*0x1400126df*/
                  invoke_watson(Expression: nullptr, FunctionName: nullptr, FileName: nullptr, LineNo: 0, Reserved: 0); /*0x1400126f0*/
              }
              j_j_free(Block: v52); /*0x1400126f7*/
            }
            if ( __eh34_unwind(11) ) /*0xf1c0000000000384*/
              goto unwind_state_11; /*0xf1c0000000000384*/
            __eh34_exit_wind_state(11, 9); /*0xf1c0000000000388*/
            goto LABEL_231; /*0xf1c000000000038c*/
          }
          CoUninitialize(); /*0x140011d37*/
          if ( dword_14005073C > *(_DWORD *)(v6 + 8) ) /*0x140011d47*/
          {
            sub_1400341D0(a1: &dword_14005073C); /*0x140011d50*/
            if ( dword_14005073C == -1 ) /*0x140011d5c*/
            {
              if ( __eh34_unwind(11) ) /*0xf1c0000000000338*/
                goto unwind_state_11; /*0xf1c0000000000338*/
              __eh34_exit_wind_state(11, 9); /*0xf1c000000000033c*/
              __wind /*0xf1c0000000000340*/
              {
                xmmword_140050938 = 0; /*0x140011d61*/
                sub_14000BA40(); /*0x140011d68*/
                atexit(a1: sub_140038CF0); /*0x140011d74*/
                Init_thread_footer(a1: &dword_14005073C); /*0x140011d81*/
              }
              __unwind
              {
                sub_140034128(a1: &dword_14005073C); /*0x1400376cf*/
              }
              __eh34_enter_wind_state(9, 11); /*0xf1c0000000000330*/
            }
          }
          v39 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x140011d86*/
          if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x140011d90*/
          {
            _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x140011d92*/
            v39 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x140011d96*/
          }
          v57 = __PAIR128__((unsigned __int64)v39, xmmword_140050938); /*0x140011da4*/
          *(_QWORD *)&v53 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x140011dac*/
          DWORD2(v53) = 1025; /*0x140011db1*/
          HIDWORD(v53) = HIDWORD(v64[1]); /*0x140011dbc*/
          v54 = "main"; /*0x140011dc7*/
          v55 = (const char *)&unk_14003EF20; /*0x140011dd3*/
          v56 = 29; /*0x140011dd7*/
          *(_OWORD *)v64 = v53; /*0x140011de4*/
          si128.m128i_i64[0] = (__int64)"main"; /*0x140011dee*/
          sub_140008FF0(a1: xmmword_140050938, a2: v64, a3: 4, a4: &v55); /*0x140011e01*/
        }
        else
        {
          if ( dword_14005073C > *(_DWORD *)(v6 + 8) ) /*0x140011c42*/
          {
            sub_1400341D0(a1: &dword_14005073C); /*0x140011c4b*/
            if ( dword_14005073C == -1 ) /*0x140011c57*/
            {
              if ( __eh34_unwind(11) ) /*0xf1c0000000000360*/
                goto unwind_state_11; /*0xf1c0000000000360*/
              __eh34_exit_wind_state(11, 9); /*0xf1c0000000000364*/
              __wind /*0xf1c0000000000368*/
              {
                xmmword_140050938 = 0; /*0x140011c5c*/
                sub_14000BA40(); /*0x140011c63*/
                atexit(a1: sub_140038CF0); /*0x140011c6f*/
                Init_thread_footer(a1: &dword_14005073C); /*0x140011c7c*/
              }
              __unwind
              {
                sub_140034128(a1: &dword_14005073C); /*0x1400376b7*/
              }
              __eh34_enter_wind_state(9, 11); /*0xf1c0000000000358*/
            }
          }
          v39 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x140011c81*/
          if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x140011c8b*/
          {
            _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x140011c8d*/
            v39 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x140011c91*/
          }
          v57 = __PAIR128__((unsigned __int64)v39, xmmword_140050938); /*0x140011c9f*/
          *(_QWORD *)&v53 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x140011ca7*/
          DWORD2(v53) = 1018; /*0x140011cac*/
          HIDWORD(v53) = HIDWORD(v64[1]); /*0x140011cb7*/
          v54 = "main"; /*0x140011cc2*/
          v55 = (const char *)&unk_14003EF10; /*0x140011cce*/
          v56 = 15; /*0x140011cd2*/
          *(_OWORD *)v64 = v53; /*0x140011cdf*/
          si128.m128i_i64[0] = (__int64)"main"; /*0x140011ce9*/
          sub_140008FF0(a1: xmmword_140050938, a2: v64, a3: 4, a4: &v55); /*0x140011cfc*/
        }
        if ( v39 != nullptr && _InterlockedExchangeAdd(v39 + 2, 0xFFFFFFFF) == 1 ) /*0x140011e1a*/
        {
          (**(void (__fastcall ***)(volatile signed __int32 *))v39)(a1: v39); /*0x140011e26*/
          if ( _InterlockedExchangeAdd(v39 + 3, 0xFFFFFFFF) == 1 ) /*0x140011e32*/
            (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v39 + 8LL))(a1: v39); /*0x140011e3e*/
        }
        goto LABEL_182; /*0x140011e41*/
      }
      __unwind
      {
        sub_1400012F0(a1: Block); /*0xf1c0000000000194*/
      }
    }
    __unwind
    {
      sub_1400174C0(a1: v67); /*0xf1c00000000001a0*/
    }
LABEL_231:
    __eh34_enter_wind_state(-1, 3); /*0xf1c00000000001a8*/
    goto LABEL_187; /*0xf1c00000000001ac*/
  }
  if ( dword_14005073C > *(_DWORD *)(v6 + 8) ) /*0x140011808*/
  {
    sub_1400341D0(a1: &dword_14005073C); /*0x140011811*/
    if ( dword_14005073C == -1 ) /*0x14001181d*/
    {
      xmmword_140050938 = 0; /*0x140011822*/
      sub_14000BA40(); /*0x140011829*/
      atexit(a1: sub_140038CF0); /*0x140011835*/
      Init_thread_footer(a1: &dword_14005073C); /*0x140011842*/
    }
  }
  v32 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x140011847*/
  if ( *((_QWORD *)&xmmword_140050938 + 1) != 0 ) /*0x140011851*/
  {
    _InterlockedIncrement((volatile signed __int32 *)(*((_QWORD *)&xmmword_140050938 + 1) + 8LL)); /*0x140011853*/
    v32 = *((volatile signed __int32 **)&xmmword_140050938 + 1); /*0x140011857*/
  }
  if ( __eh34_unwind(7) ) /*0xf1c0000000000374*/
    goto unwind_state_7; /*0xf1c0000000000374*/
  __eh34_exit_wind_state(7, -1); /*0xf1c0000000000378*/
  __eh34_enter_wind_state(-1, 3); /*0xf1c000000000037c*/
  v57 = __PAIR128__((unsigned __int64)v32, xmmword_140050938); /*0x140011865*/
  *(_QWORD *)&v53 = "K:\\Project\\VSProject\\RedMagic\\FaceTracking\\MMFaceTracking2\\main.cpp"; /*0x14001186d*/
  DWORD2(v53) = 991; /*0x140011872*/
  HIDWORD(v53) = HIDWORD(v64[1]); /*0x14001187d*/
  v54 = "main"; /*0x140011888*/
  v55 = "RegisterPowerSettingNotification failed"; /*0x140011894*/
  v56 = 39; /*0x140011898*/
  *(_OWORD *)v64 = v53; /*0x1400118a5*/
  si128.m128i_i64[0] = (__int64)"main"; /*0x1400118af*/
  sub_140008FF0(a1: xmmword_140050938, a2: v64, a3: 4, a4: &v55); /*0x1400118c2*/
  if ( v32 != nullptr && _InterlockedExchangeAdd(v32 + 2, 0xFFFFFFFF) == 1 ) /*0x1400118d7*/
  {
    (**(void (__fastcall ***)(volatile signed __int32 *))v32)(a1: v32); /*0x1400118df*/
    if ( _InterlockedExchangeAdd(v32 + 3, 0xFFFFFFFF) == 1 ) /*0x1400118e9*/
      (*(void (__fastcall **)(volatile signed __int32 *))(*(_QWORD *)v32 + 8LL))(a1: v32); /*0x1400118f1*/
  }
  result = -2; /*0x1400118f4*/
  if ( __eh34_unwind(3) ) /*0xf1c000000000008c*/
    goto unwind_state_3; /*0xf1c000000000008c*/
LABEL_188:
  __eh34_exit_wind_state(3, -1); /*0xf1c0000000000098*/
  return result; /*0x1400126fe*/
}