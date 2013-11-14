// TestCpu.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <windows.h>
#include <iostream>
using namespace std;

const DWORD busyTime = 10;
const DWORD idleTime = 10;

void MakeUsage()
{

	while(true)
	{
		DWORD startTime = GetTickCount();
		while(GetTickCount()-startTime<busyTime)
			;
		Sleep(idleTime);
	}
}

int _tmain(int argc, _TCHAR* argv[])
{
	SetThreadAffinityMask(GetCurrentProcess(), 0x00000001); 
	MakeUsage();
	return 0;
}

