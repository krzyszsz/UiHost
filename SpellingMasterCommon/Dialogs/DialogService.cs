using System;
using System.Threading.Tasks;
using Prism.Interactivity.InteractionRequest;

namespace SpellingMasterCommon.Dialogs
{
	public class DialogService : IDialogService
	{
		private readonly InteractionRequest<INotification> _notificationRequest;

		public DialogService(InteractionRequest<INotification> notificationRequest)
		{
			_notificationRequest = notificationRequest;
		}

		public async Task<bool> Confirm(string question)
		{
			var tcs = new TaskCompletionSource<bool>();

			_notificationRequest.Raise(
				new Confirmation
				{
					Title = "Confirmation",
					Content = question
				}, result => tcs.SetResult(((IConfirmation)result).Confirmed));

			var finalResult = await tcs.Task;
			return finalResult;
		}

		public async Task Notify(string notification)
		{
			var tcs = new TaskCompletionSource<int>();

			_notificationRequest.Raise(
				new Notification
				{
					Title = "Confirmation",
					Content = notification
				}, result => tcs.SetResult(default(int)));

			await tcs.Task;
		}

		public Task<TR> CustomDialog<TVm, TR>(TVm viewModel, Func<object> viewFactory)
		{
			throw new NotImplementedException();
		}
	}
}