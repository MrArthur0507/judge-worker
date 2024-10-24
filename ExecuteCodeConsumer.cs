using Docker.DotNet;
using Docker.DotNet.Models;
using JudgeContracts;
using MassTransit;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace JudgeWorker
{
    public class ExecuteCodeConsumer : IConsumer<ExecuteCode>
    {
        private readonly DockerClient _dockerClient;

        public ExecuteCodeConsumer()
        {
            // Initialize DockerClient to interact with Docker Daemon
            _dockerClient = new DockerClientConfiguration(new Uri("unix:///var/run/docker.sock")).CreateClient();
        }

        public async Task Consume(ConsumeContext<ExecuteCode> context)
        {
            var message = context.Message;

            Console.WriteLine($"Processing Code: {message.Code} in Language: {message.Language}");
            

            // Execute the code in a new container for each request
            string result = await ExecuteCode(message.Code);

            Console.WriteLine(result);

            await Task.CompletedTask;
        }

        public async Task<string> ExecuteCode(string submission)
        {
            return await ExecuteCodeInDocker(submission);
        }

        private async Task<string> ExecuteCodeInDocker(string input)
        {


            var processInfo = new ProcessStartInfo
            {
                FileName = "docker",
                Arguments = $"run --rm  --cap-drop=ALL --tmpfs /tmp --user limiteduser --memory=256m --cpus=0.5 --pids-limit=10 -i mrarthur0507/c_language_executor:1.0",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = false,
            };

            using (var process = new Process { StartInfo = processInfo })
            {
                var sw = new Stopwatch();
                process.Start();
                sw.Stop();
                Console.WriteLine(sw.ElapsedMilliseconds);
                var task = process.WaitForExitAsync();

                using (var writer = process.StandardInput)
                {
                    if (writer.BaseStream.CanWrite)
                    {
                        await writer.WriteAsync(input);
                        await writer.WriteAsync(Environment.NewLine);
                    }
                }

                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();
                return output;
            }
        }
    }
}
