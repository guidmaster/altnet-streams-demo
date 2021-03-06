﻿using System;
using System.Threading.Tasks;
using GrainInterfaces.Listeners;
using GrainInterfaces.Model;
using Orleans;
using Orleans.Streams;
using Utils;

namespace GrainImplementation.Listeners
{
	[ImplicitStreamSubscription(nameof(ChatMsg))]
	public class ChatMentionsAnalyzer : BaseListener<ChatMsg>, IChatMentionsAnalyzer
	{
		public ChatMentionsAnalyzer()
		{
			StreamNamespaces = new[] {nameof(ChatMsg)};
		}

		public override Task OnNextAsync(ChatMsg item, StreamSequenceToken token = null)
		{
			var textHasMention = item.Text.Contains("@");
			if (textHasMention)
			{
				PrettyConsole.Line($"==== MENTION DETECTED: '{item.Author}' mentions someone. Sending a notification email.", ConsoleColor.Green);
			}
			return Task.CompletedTask;
		}
	}
}