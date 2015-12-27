namespace SpellingMasterGame
{
	public interface ISynthesizer
	{
		bool IsVoiceInstalled();

		void Say(string text);
	}
}