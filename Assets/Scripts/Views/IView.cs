public interface IView
{
    public void Show();
    public void Hide();
    public void Refresh();
    public bool IsShowing { get; }
}
