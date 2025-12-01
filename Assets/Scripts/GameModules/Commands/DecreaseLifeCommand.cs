public class DecreaseLifeCommand : ICommand
{
    public string Name => nameof(DecreaseLifeCommand);
    public int DecreaseCount { get; set; }
    public string BirdId { get; set; }
}