using System.Data.Common;
using System.Diagnostics;

public class CExecutor : IExecutor{

    private readonly Sandbox _sandbox;

    public CExecutor(Sandbox sandbox)
    {
        _sandbox = sandbox;
    }

    public async Task Compile() {
        await _sandbox.ExecuteCommand("/usr/bin/gcc /box/code.c");
    }

    public async Task Execute(string stdin = null) {
        string output = await _sandbox.ExecuteCommand("/box/a.out", stdin);
        Console.WriteLine(output);
    }
}