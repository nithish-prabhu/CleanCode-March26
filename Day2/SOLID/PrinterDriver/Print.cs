public class Print : IInputDevice
{
    public void IsEndOfFile()
    {
        EOF();
    }
    public void Print()
    {
        // Print logic for print
    }

    private void EOF()
    {
        // Check if end of file
    }
}