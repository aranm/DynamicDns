namespace Dynamic.Dns.UserInterface.Infrastructure.Windsor.Factories
{
    public interface IMainWindowFactory
    {
        MainWindow Create();
        void Release(MainWindow mainWindow);
    }
}