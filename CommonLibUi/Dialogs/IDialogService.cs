using System;
using System.Threading.Tasks;

namespace SpellingMasterCommon.Dialogs
{
	public interface IDialogService
	{
		Task<bool> Confirm(string question);

		Task Notify(string notification);

		Task<TR> CustomDialog<TVm, TR>(TVm viewModel, Func<object> viewFactory);
	}
}
