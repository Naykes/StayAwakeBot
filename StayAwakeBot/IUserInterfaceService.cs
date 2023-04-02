namespace StayAwakeBot;

internal interface IUserInterfaceService
{
    public void MoveMouse(int x, int y);
    public (int x, int y) GetCursorPossition();
    public (int i, int y) GetScreenResolution();
}