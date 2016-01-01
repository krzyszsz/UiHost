namespace CommonLibUi.WaitingService
{
	public interface IWaitingViewModel
	{
		bool IsBusy { get; set; }

		string WaitingMessage { get; set; }
	}
}