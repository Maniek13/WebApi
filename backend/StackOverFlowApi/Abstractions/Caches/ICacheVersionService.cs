namespace Abstractions.Caches;

public interface ICacheVersionService
{
    public string GetVersion();
    public void Invalidate();
}
