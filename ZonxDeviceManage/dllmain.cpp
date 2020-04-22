#define _CRT_SECURE_NO_WARNINGS
#include "pch.h"
#include <iostream>
#include <vector>
#include <windows.h>
#include <swdevice.h>
#include <conio.h>
#include <wrl.h>
#include <map>

using namespace std;

map<int, HSWDEVICE> __DeviceList;
int __Vernier = 0;

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
CreationCallback(_In_ HSWDEVICE hSwDevice, _In_ HRESULT hrCreateResult, _In_opt_ PVOID pContext, _In_opt_ PCWSTR pszDeviceInstanceId)
{
	HANDLE hEvent = *(HANDLE*)pContext;
	SetEvent(hEvent);
}

extern "C" __declspec(dllexport) bool __stdcall CreateDevice(char* instanceId, char* deviceDescription, int* nDeviceStation);
extern "C" __declspec(dllexport) bool __stdcall CloseDevice(int* nDeviceStation);

bool __stdcall CloseDevice(int* nDeviceStation)
{
	if (__DeviceList.count(*nDeviceStation) > 0)
	{
		SwDeviceClose(__DeviceList[*nDeviceStation]);
		return true;
	}
	return false;
}

bool __stdcall CreateDevice(char* instanceId, char* deviceDescription, int* nDeviceStation)
{
	HANDLE hEvent = CreateEvent(nullptr, FALSE, FALSE, nullptr);
	HSWDEVICE hSwDevice;
	SW_DEVICE_CREATE_INFO createInfo = {};

	wchar_t instanceId_t[100];
	wchar_t deviceDescription_t[100];

	mbstowcs(instanceId_t, instanceId, strlen(instanceId) + 1);
	mbstowcs(deviceDescription_t, deviceDescription, strlen(deviceDescription) + 1);

	createInfo.cbSize = sizeof(createInfo);
	createInfo.pszzCompatibleIds = L"ZonxVirtualDevice\0\0";
	createInfo.pszInstanceId = instanceId_t;
	createInfo.pszzHardwareIds = L"ZonxVirtualDevice\0\0";
	createInfo.pszDeviceDescription = deviceDescription_t;
	createInfo.CapabilityFlags = SWDeviceCapabilitiesRemovable | SWDeviceCapabilitiesSilentInstall | SWDeviceCapabilitiesDriverRequired;

	HRESULT hr = SwDeviceCreate(
		L"ZonxVirtualDevice",
		L"HTREE\\ROOT\\0",
		&createInfo,
		0,
		nullptr,
		CreationCallback,
		&hEvent,
		&hSwDevice);


	if (FAILED(hr))
	{
		printf("SwDeviceCreate failed with 0x%lx\n", hr);
		return false;
	}

	DWORD waitResult = WaitForSingleObject(hEvent, 10 * 1000);

	if (waitResult != WAIT_OBJECT_0)
	{
		cout << "Wait for device creation failed" << endl;
		return false;
	}

	nDeviceStation = &++__Vernier;
	__DeviceList[*nDeviceStation] = hSwDevice;
	return true	;
}



