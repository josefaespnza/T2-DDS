namespace Server;

public abstract class View
{
    protected abstract void Write(string message);
    protected abstract string ReadLine();
    public virtual void Close() {}
    public void Pause() => ReadLine();
    public void Welcome() =>Write("¡Bienvenido a la escoba!");
    

}