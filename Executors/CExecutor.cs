using System.Data.Common;
using System.Diagnostics;

public class CExecutor : IExecutor{

    private readonly Sandbox _sandbox;

    public CExecutor(Sandbox sandbox)
    {
        _sandbox = sandbox;
    }

    public async Task<ExecuteCommandResult> Compile(string code) {
        _sandbox.StartSandbox(code);
        ExecuteCommandResult output = await _sandbox.ExecuteCommand("/usr/bin/gcc /box/code.c");
        return output;

    }
    public async Task<ExecuteCommandResult> Execute() {
        ExecuteCommandResult output = await _sandbox.ExecuteCommand("/box/a.out");
        _sandbox.Cleanup();
        return output;
    }
    public async Task<ExecuteCommandResult> Execute(string[] stdin = null) {
        ExecuteCommandResult output = await _sandbox.ExecuteCommand("/box/a.out", stdin);
        _sandbox.Cleanup();
        return output;
    }

}