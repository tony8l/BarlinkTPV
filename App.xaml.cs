using Microsoft.Extensions.DependencyInjection;
using BarlinkTPV.Navigation;

namespace BarlinkTPV
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new InitialNavigation());
        }
        
        
    }
}