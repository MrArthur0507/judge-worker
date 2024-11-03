namespace JudgeContracts
{
    public class ExecuteCodeResult {

    public string? CompileResult { get; set; }

    public List<string?> CompileMeta { get; set; }
    public string? Output { get; set; }
    public List<string?> Meta { get; set; }
    }
    
}
