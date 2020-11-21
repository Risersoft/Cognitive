using FitBitAssistantBot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using risersoft.shared.bot;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace risersoft.app.mxform.cog.BotFx.Fitbit
{


    public class FitBitDialog : TokenDialogBase
    {
        public FitBitDialog(IConfiguration configuration, ILogger logger) : base("fitbit", logger, "Fitbit")
        {
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[] { PromptStepAsync, LoginStepAsync, CommandStepAsync, ProcessStepAsync }));
            InitialDialogId = nameof(WaterfallDialog);
        }
        protected override async Task<DialogTurnResult> AfterTokenRecdAsync(WaterfallStepContext step, CancellationToken cancellationToken)
        {
            try
            {

                var bot = step.Context.TurnState.Get<ActivityBotBase>("bot");

                var conversationStateProp = bot.ConversationState.CreateProperty<int>("counter");
                var counter = await conversationStateProp.GetAsync(step.Context, () => 0, cancellationToken);
                if (counter == 0)
                {
                    var attachments = new List<Attachment>();

                    // Reply to the activity we received with an activity.
                    var reply = MessageFactory.Attachment(attachments);
                    reply.Attachments.Add(DialogHelpers.CreateBotFunctionsCard("This is what I can do for you", bot).ToAttachment());

                    await step.Context.SendActivityAsync(reply, cancellationToken);
                }
                counter++;
                await conversationStateProp.SetAsync(step.Context, counter);
            }
            catch (Exception ex)
            {
                await step.Context.SendActivityAsync(MessageFactory.Text("Error occured."), cancellationToken);
               Trace.WriteLine(ex.Message + "\r\n" + ex.StackTrace );

            }
            return Dialog.EndOfTurn;
        }


        private async Task<DialogTurnResult> ProcessStepAsync(WaterfallStepContext step, CancellationToken cancellationToken)
        {
            if (step.Result != null)
            {
                var bot = step.Context.TurnState.Get<ActivityBotBase>("bot");
                var tokenResponse = step.Result as TokenResponse;

                if (tokenResponse?.Token != null)
                {
                    var parts = (System.Convert.ToString(step.Values["command"]) ?? string.Empty).ToLowerInvariant().Split(' ');
                    var command = parts[0];

                    if (command == "myprofile")
                    {
                        FitBitApiHelper fitBitApiHelper = new FitBitApiHelper(tokenResponse.Token);
                        UserProfile userProfile = await fitBitApiHelper.GetUserProfileAsync();
                        var reply = step.Context.Activity.CreateReply();
                        reply.Attachments = new List<Attachment>()
                        {
                             DialogHelpers.CreateUserProfileAdaptiveCard(userProfile),
                             //DialogHelpers.CreateBotFunctionsCard("What would you like to do next? Select one", bot).ToAttachment()
                        };

                        await step.Context.SendActivityAsync(reply, cancellationToken);


                    }
                    else if (command == "mybadges")
                    {
                        FitBitApiHelper fitBitApiHelper = new FitBitApiHelper(tokenResponse.Token);
                        UserBadges userBadges = await fitBitApiHelper.GetUserBadgesAsync();
                        var reply = step.Context.Activity.CreateReply();
                        reply.Attachments = new List<Attachment>()
                        {
                             DialogHelpers.CreateUserBadgesCard(userBadges),
                             //DialogHelpers.CreateBotFunctionsCard("What would you like to do next? Select one", bot).ToAttachment()
                        };

                        await step.Context.SendActivityAsync(reply, cancellationToken);


                    }
                    else if (command == "myactivity")
                    {
                        FitBitApiHelper fitBitApiHelper = new FitBitApiHelper(tokenResponse.Token);
                        var userBadges = await fitBitApiHelper.GetUserActivityAsync(DateTime.Now);
                        var str1 = DialogHelpers.SummaryText(userBadges);
                        await step.Context.SendActivityAsync(MessageFactory.Text(str1), cancellationToken);


                    }
                    else if (command == "quit")
                    {
                        await step.Context.SendActivityAsync("Exiting Fitbit dialog.", cancellationToken: cancellationToken);
                        var conversationStateProp = bot.ConversationState.CreateProperty<int>("counter");
                        await conversationStateProp.SetAsync(step.Context, 0);
                        return await step.EndDialogAsync(cancellationToken: cancellationToken);


                    }
                    else
                    {
                        await step.Context.SendActivityAsync(MessageFactory.Text("I don't recognize that option."), cancellationToken);

                    }

                    //else
                    //{
                    //    var reply = step.Context.Activity.CreateReply();
                    //    reply.Attachments = new List<Attachment>()
                    //    {
                    //        DialogHelpers.CreateBotFunctionsHeroCard("This is what I can do for you.").ToAttachment()
                    //    };

                    //    await step.Context.SendActivityAsync(reply, cancellationToken);
                    //}

                    //else
                    //{
                    //    await step.Context.SendActivityAsync($"Your token is: {tokenResponse.Token}", cancellationToken: cancellationToken);
                    //}
                    return await step.ReplaceDialogAsync(nameof(WaterfallDialog), cancellationToken: cancellationToken);
                }
            }
            else
                await step.Context.SendActivityAsync(MessageFactory.Text("We couldn't log you in. Please try again later."), cancellationToken);

            return await step.EndDialogAsync(cancellationToken: cancellationToken);
        }
    }

}
//https://theserverlessspirit.wordpress.com/2018/11/05/creating-a-fitbitassistantbot-using-microsoft-bot-framework-and-fitbit-web-api-real-life-example-of-authentication-using-azure-bot-service/
//https://github.com/mandardhikari/FitBitAssistantBot
//https://github.com/MicrosoftDocs/bot-docs/issues/804
//https://www.c-sharpcorner.com/article/prompt-and-waterfall-dialog-in-bot-v4-framework-bot-builder-net-core/