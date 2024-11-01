using System.Diagnostics;

public class Sandbox : IDisposable{
    private int id;
    private string path;
    public Sandbox()
    {
        this.id = SandboxManager.GetBoxId();
    }
    

    public void StartSandbox(string code) {
        
        Cleanup();
        ProcessStartInfo processStartInfo = new ProcessStartInfo {
            FileName = "isolate",
            Arguments = $"-b {id} --init"
        };

        using (Process process = new Process())
        {
            process.StartInfo = processStartInfo;
            process.Start();
            process.WaitForExit();
        }
        path = $"/var/local/lib/isolate/{id}/box/";
        File.Copy(code, path + "code.c");
    }


    public async Task<string> ExecuteCommand(string command, string stdin = null) {
        ProcessStartInfo processStartInfo = new ProcessStartInfo {
            FileName = "isolate",
            Arguments = $"-b {id} -p " + 
                    "--env=PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin " +
                    "--stdout=out.txt " +
                    $"--run -- {command}",
            RedirectStandardInput = true,
        };

        using (Process process = new Process())
        {
            process.StartInfo = processStartInfo;
            process.Start();
            using (StreamWriter sw = process.StandardInput)
            {
                sw.WriteLine(stdin);
            }
            process.WaitForExit();
            using (StreamReader sr = new StreamReader(path + "out.txt"))
            {
                return await sr.ReadToEndAsync();
            }
        }
    }

    public void Cleanup() {
        ProcessStartInfo processStartInfo = new ProcessStartInfo() {
            FileName = "isolate",
            Arguments = $"-b {id} --cleanup"
        };

        using (Process process = new Process())
        {
            process.StartInfo = processStartInfo;
            process.Start();
            process.WaitForExit();
        }
    }

    public void Dispose()
    {
        Cleanup();
    }


}