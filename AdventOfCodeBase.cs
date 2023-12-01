namespace AdventOfCode2023;

public abstract class AdventOfCodeBase: IInputProvider
{
    protected List<string> Input;
    protected AdventOfCodeBase()
    {
        Input = (this as IInputProvider).GetInput();
    }
}

public interface IInputProvider
{
    public List<string> GetInput()
    {
        string dayOutOfNamespace = GetType().Namespace?[^2..] ?? throw new InvalidOperationException();
        return File.ReadLines($@"./Day{dayOutOfNamespace}/input").ToList();
    }

}
