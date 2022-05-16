using Caliburn.Micro;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeDirectory.Caliburn.ViewModels
{
    public class MainViewModel : Conductor<object>, IHandle<Screen>
    {
        private readonly IEventAggregator _eventAggregator;

        public MainViewModel(IEventAggregator eventAggregator, HomeViewModel homevm)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            ActivateItemAsync(homevm);
        }

        public Task HandleAsync(Screen screenToDisplay, CancellationToken cancellationToken)
        {
            if(screenToDisplay!=null)
                return ActivateItemAsync(screenToDisplay, cancellationToken);
            return null;
        }
    }
}
