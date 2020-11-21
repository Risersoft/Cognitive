// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using risersoft.app.mxform.cog.Bot.Models;
using risersoft.shared.bot;

namespace Microsoft.BotBuilderSamples.Bots
{
    [BotId(BotName="KakaBot",IdSettingName = "MicrosoftAppIdKaka", PasswordSettingName = "MicrosoftAppPasswordKaka")]
    public class EchoBot : ActivityBotBase
    {

        public EchoBot(ConversationState conversationState, UserState userState, ILogger<EchoBot> logger):base(conversationState,userState,logger)
        {
        }
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var conversationStateProp = ConversationState.CreateProperty<CustomState>("customState");
            var convProp = await conversationStateProp.GetAsync(turnContext, () => new CustomState { Value = 0 }, cancellationToken);

            var userStateProp = UserState.CreateProperty<CustomState>("customState");
            var userProp = await userStateProp.GetAsync(turnContext, () => new CustomState { Value = 0 }, cancellationToken);

            await turnContext.SendActivityAsync(MessageFactory.Text($"Echo: {turnContext.Activity.Text} conversation state: {convProp.Value} user state: {userProp.Value}"), cancellationToken);

            convProp.Value++;
            userProp.Value++;

//            await turnContext.SendActivityAsync(MessageFactory.Text($"Echo: {turnContext.Activity.Text}"), cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Hello and welcome!"), cancellationToken);
                }
            }
        }
    }
}
