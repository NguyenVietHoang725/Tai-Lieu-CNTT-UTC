#include <Windows.h>
int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, PSTR cmdLine, int nShowCmd)
{
	int Result;

	do {
		Result = MessageBox(NULL, TEXT("Do you want to stop the program"), TEXT("Question"), MB_YESNO | MB_ICONQUESTION);
	} while (Result == IDNO);
	
	return 0;
}