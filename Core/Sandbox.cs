using System.Diagnostics;
using JudgeContracts;

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
        File.WriteAllText(path + "code.c", code);
    }


    public async Task<ExecuteCommandResult> ExecuteCommand(string command, string[] stdin = null) {
        
        ExecuteCommandResult executeCommandResult = new ExecuteCommandResult();

        ProcessStartInfo processStartInfo = new ProcessStartInfo {
            FileName = "isolate",
            Arguments = $"-b {id} -p " + 
                    "--env=PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin " +
                    "--stdout=out.txt " +
                    "--stderr-to-stdout " +
                    $"--meta={path}/meta.txt " +
                    $"--run -- {command}",
            RedirectStandardInput = true,
        };

        using (Process process = new Process())
        {
            process.StartInfo = processStartInfo;
            process.Start();
            if (stdin != null) {

                using (StreamWriter sw = process.StandardInput)
                {
                    foreach (var item in stdin)
                    {

                        sw.WriteLine(item);

                    }
                }
            }
            
            process.WaitForExit();
            using (StreamReader sr = new StreamReader(path + "out.txt"))
            {
                
                executeCommandResult.Output = await sr.ReadToEndAsync();
            };
            executeCommandResult.Meta = ExtractMeta();

            return executeCommandResult;
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



    private List<string?> ExtractMeta() {
        List<string?> metaData = new List<string?>();
        using (StreamReader sr = new StreamReader(path + "meta.txt"))
        {
            while (!sr.EndOfStream)
            {
                metaData.Add(sr.ReadLine());            
            }
        }

        return metaData;
    }

}