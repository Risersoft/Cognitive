using Bot.Builder.Community.Dialogs.Luis;
using Bot.Builder.Community.Dialogs.Luis.Models;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Extensions.Logging;
using risersoft.shared.bot;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Microsoft.Bot.Sample.LuisBot
{
    // For more information about this template visit http://aka.ms/azurebots-csharp-luis
    [Serializable]
    public class BasicLuisDialog : LuisDialog<object>
    {
        protected readonly ILogger Logger;

        public BasicLuisDialog(ILogger<BasicLuisDialog> logger) : base(nameof(BasicLuisDialog),new LuisService(new LuisModelAttribute(
            ConfigurationManager.AppSettings["LuisModelId"], 
            ConfigurationManager.AppSettings["LuisSubscriptionKey"], 
            domain: ConfigurationManager.AppSettings["LuisHostDomain"])))
        {
            Logger = logger;
        }

    [LuisIntent("None")]
        public async Task NoneIntent(DialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        // Go to https://luis.ai and create a new intent, then train/publish your luis app.
        // Finally replace "Gretting" with the name of your newly created intent in the following handler
        [LuisIntent("Greeting")]
        public async Task GreetingIntent(DialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        [LuisIntent("Cancel")]
        public async Task CancelIntent(DialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        [LuisIntent("Help")]
        public async Task HelpIntent(DialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        private async Task ShowLuisResult(DialogContext context, LuisResult result) 
        {
            await context.PostAsync($"You have reached {result.Intents[0].Intent}. You said: {result.Query}");
            //context.Wait(MessageReceived);
        }
    }

    [BotId(BotName="WordyBot",IdSettingName = "MicrosoftAppIdWordy", PasswordSettingName = "MicrosoftAppPasswordWordy")]
    public class BasicLuisBot : DialogBotBase<BasicLuisDialog>
    {
        public BasicLuisBot(ConversationState conversationState, UserState userState, BasicLuisDialog dialog, ILogger<BasicLuisBot> logger) : base(conversationState, userState, dialog, logger)
        {

        }
    }

}