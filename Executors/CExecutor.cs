using System.Data.Common;
using System.Diagnostics;

public class CRunner {

    private readonly Sandbox _sandbox;

    public CRunner(Sandbox sandbox)
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