public class PrinterDriver
{
    private File file;
    private IInputDevice inputDevice;
    public PrinterDriver(File file, IInputDevice inputDevice){
        this.file = file;
        this.inputDevice = inputDevice;
    }
    public void Print(string text)
    {
        buffer page = file.readPage();
        while(!inputDevice.IsEndOfFile(page)){
            inputDevice.Print(page);
            page = file.readPage();
        }
    }
}





