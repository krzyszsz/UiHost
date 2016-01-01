using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using CommonLibUi.Dialogs;
using CommonLibUi.WaitingService;

namespace UiHost
{
	// TODO: Extract interface of this ViewModel & View for loose-coupling
	public class ShellWindowViewModel : BindableBase, IWaitingViewModel
	{
		private readonly IDialogService _dialogService;
		private readonly IWaitingService _waitingService;
		private readonly InteractionRequest<INotification> _interactionRequest;
		private string _waitingMessage;
		private bool _isBusy;

		public ShellWindowViewModel(
			IDialogService dialogService,
			InteractionRequest<INotification> interactionRequest,
			IWaitingService waitingService)
		{
			_dialogService = dialogService;
			_interactionRequest = interactionRequest;
			_waitingService = waitingService;
			ExitCommand = new DelegateCommand(ExitApplication);
			_waitingService.Init(this);
		}

		public InteractionRequest<INotification> InteractionRequest => _interactionRequest;

		public ICommand ExitCommand { get; private set; }

		public string WaitingMessage
		{
			get
			{
				return _waitingMessage;
			}
			set
			{
				_waitingMessage = value;
				OnPropertyChanged(() => WaitingMessage);
			}
		}

		public bool IsBusy
		{
			get
			{
				return _isBusy;
			}
			set
			{
				_isBusy = value;
				OnPropertyChanged(() => IsBusy);
			}
		}

		public ShellWindow Shell { get; set; }

		private async void ExitApplication()
		{
			var confirmed = await _dialogService.Confirm("Exit the application?");
			if (confirmed)
				Application.Current.Shutdown();
		}
	}
}
