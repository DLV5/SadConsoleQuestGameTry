namespace ConsoleStyleFantasy
{
    /// <summary>
    /// Component participant
    /// </summary>
    internal abstract class DialogComponent
    {
        protected readonly List<DialogComponent> _components = new List<DialogComponent>();

        public abstract string[] GetDialog();

        public void Add(DialogComponent component)
        {
            _components.Add(component);
        }
        
        public void Remove(DialogComponent component)
        {
            _components.Remove(component);
        }
    }
}
