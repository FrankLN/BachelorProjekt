// E01.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"


int _tmain(int argc, _TCHAR* argv[])
{
	SharedPtr<std::vector<int>> sP2;
	{
		SharedPtr<std::vector<int>> sP(new std::vector<int>());
		sP = sP2;
	}

	return 0;
}

