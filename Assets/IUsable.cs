internal interface IUsable
{
    public bool IsUsing { get; set; }
    public abstract bool Use();
}