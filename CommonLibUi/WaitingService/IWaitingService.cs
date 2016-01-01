using System.Threading.Tasks;

namespace CommonLibUi.WaitingService
{
	public interface IWaitingService
	{
		void Init(IWaitingViewModel viewModel);

		Task<T> ExecuteLongTask<T>(Task<T> task, string waitingMessage = "Please wait...");

		Task ExecuteLongTask(Task task, string waitingMessage = "Please wait...");
	}
}
