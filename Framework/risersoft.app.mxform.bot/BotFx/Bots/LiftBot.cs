// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using risersoft.app.mxform.cog.BotFx.Fitbit;
using risersoft.shared;
using risersoft.shared.bot;
using risersoft.shared.portable.Models.Auth;
using System.Linq;
using System.Reflection;
using risersoft.app.mxform.cog.Models;

namespace Microsoft.BotBuilderSamples
{
    [BotId(BotName = "LiftBot", CallerInfo = true)]
    public class LiftBot : ActivityBotBase
    {
        private DialogSet _dialogs;
        private IStatePropertyAccessor<DialogState> _accessor;
        public LiftBot(IConfiguration configuration, ConversationState conversationState, UserState userState, ILogger<LiftBot> logger)
            : base(conversationState, userState, logger)
        {
            _accessor = conversationState.CreateProperty<DialogState>(nameof(DialogState));
            _dialogs = new DialogSet(_accessor);
            var _dialog1 = new RSAuthDialog(configuration, logger);
            _dialog1.UserAdded += _dialog1_UserAdded;
            _dialogs.Add(_dialog1);
            var _dialog2 = new RSGroupDialog(configuration, logger);
            _dialog2.GroupAdded += _dialog2_GroupAdded;
            _dialogs.Add(_dialog2);
            _dialogs.Add(new FitBitDialog(configuration, logger));
        }
        public override void Initialize(ITurnContext turnContext)
        {
            base.Initialize(turnContext);
            var attrib = this.GetType().GetCustomAttribute<BotId>();
            GreetMessage = $"Welcome to **{attrib.BotName} chat bot from {this.myWebController.CalcPublisher()}**.\n Please enter /register or /fitbit.";

        }
        private async void _dialog2_GroupAdded(object sender, BotChatInfo e)
        {
            //insert in BotGroup and BotGroupUser
            if (await this.myWebController.CheckInitModel(this.myWebController.AppInfo(), false))
            {
                try
                {
                    var _ctx = await GetDBContext();

                    var _grp = (await _ctx.GetRepo<BotGroupOption>().WhereAsync(obj => (obj.BotId == e.BotId && obj.ChannelKey == e.ChannelKey && obj.ConversationId == e.ConversationId))).FirstOrDefault();
                    if (_grp == null)
                    {
                        _grp = new BotGroupOption { BotId = e.BotId, ChannelKey = e.ChannelKey, ConversationId = e.ConversationId };
                        await _ctx.GetRepo<BotGroupOption>().AddOrUpdateAsync(_grp);
                    }

                    var _ugrp = (await _ctx.GetRepo<BotGroupUser>().WhereAsync(obj => (obj.BotId == e.BotId && obj.ChannelKey == e.ChannelKey && obj.ConversationId == e.ConversationId && obj.UserId == e.UserId))).FirstOrDefault();
                    if (_ugrp == null)
                    {
                        _ugrp = new BotGroupUser { BotId = e.BotId, ChannelKey = e.ChannelKey, ConversationId = e.ConversationId, UserId = e.UserId };
                        await _ctx.GetRepo<BotGroupUser>().AddOrUpdateAsync(_ugrp);
                    }
                }
                catch (Exception ex)
                {
                    this.myWebController.Logger.Information(ex.Message);
                }
            }
        }

        private async void _dialog1_UserAdded(object sender, BotChatInfo e)
        {
            //insert in BotUser
            if (await this.myWebController.CheckInitModel(this.myWebController.AppInfo(), false))
            {
                try
                {
                    var _ctx = await GetDBContext();
                       var _user = (await _ctx.GetRepo<BotUserOption>().WhereAsync(obj => (obj.BotId == e.BotId && obj.ChannelKey == e.ChannelKey && obj.UserID == e.UserId))).FirstOrDefault();

                        if (_user == null)
                        {
                            _user = new BotUserOption { BotId = e.BotId, ChannelKey = e.ChannelKey, UserID = e.UserId };
                            await _ctx.GetRepo<BotUserOption>().AddOrUpdateAsync(_user);
                        }

                }
                catch (Exception ex)
                {
                    this.myWebController.Logger.Information(ex.Message);
                }
            }
        }
        protected async Task<ICosmosDbContext> GetDBContext()
        {
            this.myWebController.DataProvider.DBConnectionInfo = this.myWebController.CalcConnStringAppCode("mxcogdb", "task");
            var info = this.myWebController.GetCallerInfo();

            return await this.myWebController.DataProvider.objNoSQLContext();
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in turnContext.Activity.MembersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(GreetMessage), cancellationToken);
                }
            }
        }

        protected override async Task OnTokenResponseEventAsync(ITurnContext<IEventActivity> turnContext, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Running dialog with Token Response Event Activity.");

            // Run the Dialog with the new Token Response Event Activity.
            await _dialogs.Run(string.Empty, turnContext, cancellationToken);
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var convProp = await _accessor.GetAsync(turnContext, () => new DialogState(), cancellationToken);

            if (convProp.DialogStack.Count == 0)
            {
                var text = turnContext.Activity.Text.ToLowerInvariant();
                if (myUtils.StartsWith(text, "/register"))
                {
                    if (turnContext.Activity.Conversation.IsGroup.GetValueOrDefault())
                    {
                        //register group
                        await _dialogs.Run("rsgroup", turnContext, cancellationToken);
                    }
                    else
                    {
                        await _dialogs.Run("rsauth", turnContext, cancellationToken);
                    }
                }
                else if (myUtils.StartsWith(text, "/fitbit"))
                {
                    if (turnContext.Activity.Conversation.IsGroup.GetValueOrDefault())
                    {
                        await turnContext.SendActivityAsync(MessageFactory.Text($"This command cannot be run in group context."), cancellationToken);
                    }
                    else
                    {
                        await _dialogs.Run("fitbit", turnContext, cancellationToken);
                    }
                }
                else if (myUtils.StartsWith(text, "/score"))
                {
                    if (turnContext.Activity.Conversation.IsGroup.GetValueOrDefault())
                    {
                        //get tokens of all users in this group and generate HTML
                    }
                    else
                    {
                        await turnContext.SendActivityAsync(MessageFactory.Text($"This command cannot be run in user context."), cancellationToken);
                    }
                }
                else if (myUtils.StartsWith(text, "/start"))
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(GreetMessage), cancellationToken);
                }
                else
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text($"Do not recognize that command {text}."), cancellationToken);

                }


            }
            else
            {
                Logger.LogInformation("Running dialog with Message Activity.");
                await _dialogs.Run(string.Empty, turnContext, cancellationToken);

            }
        }
    }
}


//


//References:
//1. https://www.c-sharpcorner.com/blogs/build-an-enterprise-chat-bot-for-service-now-integrations-and-qna-based-faqs
//2. FItbitAssistantBot https://github.com/mandardhikari/FitBitAssistantBot/blob/master/FitBitAssistantBot/FitBitAssistantBot.cs
//3. ContosoHelpLinev3v4
//4. Form Flow sample Hotel
//5. Dotnet core samples
//3. 