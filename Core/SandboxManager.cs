public class SandboxManager() {
    private static int count = -1;

    private static readonly object _lock = new object();

    public static int GetBoxId() {
        lock (_lock)
        {
            return ++count;
        }
        
    }
}