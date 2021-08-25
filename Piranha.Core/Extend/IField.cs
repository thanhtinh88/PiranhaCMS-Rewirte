namespace Piranha.Extend
{
    public interface IField
    {
        object GetValue();
        void Init();
        void InitManager();
    }
}