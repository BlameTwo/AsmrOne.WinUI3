namespace AsmrOne.WinUI3.Common
{
    public interface IItem<T, VM>
    {
        public VM ViewModel { get; set; }
    }
}
