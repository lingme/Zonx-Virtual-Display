#include "pch.h"
#include <iostream>
#include <vector>
#include <windows.h>
#include <swdevice.h>
#include <conio.h>
#include <wrl.h>

BOOL APIENTRY DllMain(HMODULE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}

VOID WINAPI
CreationCallback(_In_ HSWDEVICE hSwDevice, _In_ HRESULT hrCreateResult, _In_opt_ PVOID pContext,_In_opt_ PCWSTR pszDeviceInstanceId)
{
	HANDLE hEvent = *(HANDLE*)pContext;
	SetEvent(hEvent);
}



//extern "C" __declspec(dllexport) void Test();


