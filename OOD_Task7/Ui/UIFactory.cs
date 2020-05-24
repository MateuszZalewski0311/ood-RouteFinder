// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// Mateusz Zalewski
namespace BigTask2.Ui
{
    interface IAbstractUIFactory
    {
        public IForm GetForm();

        public IDisplay GetDisplay();
        public ISystem GetSystem(IForm form, IDisplay display);
    }

    class XmlFactory : IAbstractUIFactory
    {
        public IDisplay GetDisplay() => new XmlDisplay();

        public IForm GetForm() => new XmlForm();

        public ISystem GetSystem(IForm form, IDisplay display) => new XmlSystem(form, display);
    }

    class KeyValueFactory : IAbstractUIFactory
    {
        public IDisplay GetDisplay() => new KeyValueDisplay();

        public IForm GetForm() => new KeyValueForm();

        public ISystem GetSystem(IForm form, IDisplay display) => new KeyValueSystem(form, display);
    }

}
