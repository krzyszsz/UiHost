using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;
using SpellingMasterGame;

namespace SpellingMasterUI.Services
{
	[Export(typeof(ISynthesizer))]
	public class Synthesizer : ISynthesizer
	{
		private static readonly TimeSpan PollingPeriod = TimeSpan.FromSeconds(0.1);
		private static readonly TimeSpan Timeout = TimeSpan.FromSeconds(3);
		private static readonly string[] AcceptedLanguages = { "en-GB", "en-US" };

		public bool IsVoiceInstalled()
		{
			try
			{
				using (var synth = new SpeechSynthesizer())
				{
					var voices = synth.GetInstalledVoices();
					return voices.Any(v => AcceptedLanguages.Contains(v.VoiceInfo.Culture.Name));
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async void Say(string text)
		{
			await Task.Run(() =>
			{
				using (var synth = new SpeechSynthesizer())
				{
					var voices = synth.GetInstalledVoices();
					var newVoice = voices.First(v => AcceptedLanguages.Contains(v.VoiceInfo.Culture.Name));
					synth.SelectVoice(newVoice.VoiceInfo.Name);
					synth.SpeakAsync(text);
					var sw = new Stopwatch();
					sw.Start();
					while (synth.State == SynthesizerState.Ready && sw.Elapsed < Timeout)
						Thread.Sleep(PollingPeriod);
					while (synth.State != SynthesizerState.Ready && sw.Elapsed < Timeout)
						Thread.Sleep(PollingPeriod);
				}
			});
		}
	}
}
