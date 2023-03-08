//Tool für die Protokollierung von Pings
//Erstellt von: Jan-Hendrik Focke
//https://github.com/Endergamer1


using System.Net.NetworkInformation;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

[DllImport("kernel32.dll", SetLastError = true)]
static extern bool AllocConsole();

[DllImport("kernel32.dll")]
static extern IntPtr GetConsoleWindow();

[DllImport("user32.dll")]
static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

const int SW_HIDE = 0;
const int SW_SHOW = 5;

static void ShowConsoleWindow()
{
    var handle = GetConsoleWindow();

    if (handle == IntPtr.Zero)
    {
        AllocConsole();
    }
    else
    {
        ShowWindow(handle, SW_SHOW);
    }
}

static void HideConsoleWindow()
{
    var handle = GetConsoleWindow();
    ShowWindow(handle, SW_HIDE);
}

string FileLocation = @"C:\Service\Ping\PingLog.txt";

Ping p = new Ping();
PingReply r;
ShowConsoleWindow();
Console.WriteLine("Bitte Ping Addresse eingeben: ");
string PingAddress = Console.ReadLine();
StringBuilder sb = new StringBuilder("", 100);
HideConsoleWindow();
while (true)
{
    r = p.Send(PingAddress);
    if (r.Status == IPStatus.Success)
    {
    }
    else if (r.Status == IPStatus.TimedOut || r.Status == IPStatus.DestinationNetworkUnreachable)
    {
        using (StreamWriter sw = File.AppendText(FileLocation))
        {
            Console.WriteLine("Boop");
            sw.WriteLine("Fehler ab: " + DateTime.Now + ":" + DateTime.Now.Millisecond + " Status: " + r.Status);
            while (true)
            {
                r = p.Send(PingAddress);
                if (r.Status == IPStatus.Success)
                {
                    Console.WriteLine("Boop");
                    sw.WriteLine("Erfolgreich ab: " + DateTime.Now + ":" + DateTime.Now.Millisecond  + " Status : " + r.Status);
                    break;
                }
            }

        }
    }
}
