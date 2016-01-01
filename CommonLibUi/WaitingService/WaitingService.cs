using System.Threading.Tasks;

namespace CommonLibUi.WaitingService
{
	public class WaitingService : IWaitingService
	{
		private IWaitingViewModel _viewModel;

		public void Init(IWaitingViewModel viewModel)
		{
			_viewModel = viewModel;
		}

		public async Task<T> ExecuteLongTask<T>(Task<T> task, string waitingMessage = "Please wait...")
		{
			_viewModel.IsBusy = true;
			_viewModel.WaitingMessage = waitingMessage;
			try
			{
				return await task;
			}
			finally
			{
				_viewModel.IsBusy = false;
			}
		}

		public async Task ExecuteLongTask(Task task, string waitingMessage = "Please wait...")
		{
			_viewModel.IsBusy = true;
			_viewModel.WaitingMessage = waitingMessage;
			try
			{
				await task;
			}
			finally
			{
				_viewModel.IsBusy = false;
			}
		}
	}
}