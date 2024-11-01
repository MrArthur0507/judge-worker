using System.Data.Common;
using System.Diagnostics;

public class CExecutor : IExecutor{

    private readonly Sandbox _sandbox;

    public CExecutor(Sandbox sandbox)
    {
        _sandbox = sandbox;
    }

    public async Task Compile(string code) {
        _sandbox.StartSandbox(code);
        await _sandbox.ExecuteCommand("/usr/bin/gcc /box/code.c");
    }
    public async Task<string> Execute() {
        string output = await _sandbox.ExecuteCommand("/box/a.out");
        _sandbox.Cleanup();
        return output;
    }
    public async Task<string> Execute(string stdin = null) {
        string output = await _sandbox.ExecuteCommand("/box/a.out", stdin);
        _sandbox.Cleanup();
        return output;
    }
}