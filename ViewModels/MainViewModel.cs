using Caliburn.Micro;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EmployeeDirectory.Caliburn.ViewModels
{
    public class MainViewModel : Conductor<object>, IHandle<Screen>,IHandle<string>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly HomeViewModel _homeVM;
        public MainViewModel(IEventAggregator eventAggregator, HomeViewModel homeVM)
        {
            _homeVM = homeVM;
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            ActivateItemAsync(_homeVM);
        }

        public Task HandleAsync(Screen screenToDisplay, CancellationToken cancellationToken)
        {
            if(screenToDisplay!=null)
                return ActivateItemAsync(screenToDisplay, cancellationToken);
            return null;
        }

        public Task HandleAsync(string message, CancellationToken cancellationToken)
        {
           return message.Equals("back", StringComparison.OrdinalIgnoreCase) ?  ActivateItemAsync(_homeVM, cancellationToken) :  null;
        }
    }
}
