namespace FitBitAssistantBot
{
    #region References
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.IO;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Schema;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Newtonsoft.Json.Linq;
    using System.Text;
    using risersoft.shared;
    using System.Reflection;
    using risersoft.shared.bot;
    using risersoft.app.mxform.cog.BotFx.Fitbit.Models;
    #endregion

    public static class DialogHelpers
    {

      

        /// <summary>
        /// Creates a Hero Card Detailing the functions that the Bot can perfom
        /// </summary>
        /// <param name="cardHeading">Heading To Be sent to the card</param>
        /// <returns></returns>
        public static ThumbnailCard CreateBotFunctionsCard(string cardHeading,ActivityBotBase bot)
        {
            var attrib = bot.GetType().GetCustomAttribute<BotId>();
            var obj = myAssy.bytFromEmbed(Assembly.GetExecutingAssembly().GetName().Name, "BotPic.png",true);
            var str1 = Convert.ToBase64String(obj);
            var heroCard = new ThumbnailCard(cardHeading, attrib.BotName)
            {
                Buttons = new List<CardAction>
                {
                    //new CardAction(ActionTypes.ImBack, "LogIn", text:"LogIn", displayText: "LogIn", value: "LogIn"),
                    new CardAction(ActionTypes.ImBack, "MyProfile", text:"MyProfile", displayText: "MyProfile", value: "MyProfile"),
                    new CardAction(ActionTypes.ImBack, "MyBadges", text:"MyBadges", displayText: "MyBadges", value: "MyBadges"),
                    new CardAction(ActionTypes.ImBack, "MyActivity", text:"MyActivity", displayText: "MyActivity", value: "MyActivity"),
                    //new CardAction(ActionTypes.ImBack, "Help", text: "Help", displayText: "Help", value:"Help"),
                    new CardAction(ActionTypes.ImBack, "Quit", text: "Quit", displayText: "Quit", value: "Quit")
                },
                Images = new List<CardImage>()
                {
                    new CardImage()
                    {
                        //Url = string.Format(Constants.CardImageUrl, str1)
                         Url = "https://cognitive.risersoft.com/content/fitbit.png"
                    }
                }


            };

            return heroCard;

        }


        public static ThumbnailCard CreateErrorCard(ActivityBotBase bot)
        {
            var attrib = bot.GetType().GetCustomAttribute<BotId>();
            var obj = myAssy.bytFromEmbed(Assembly.GetExecutingAssembly().GetName().Name, "BrokenBot.png",true);
            var str1 = Convert.ToBase64String(obj);
            var heroCard = new ThumbnailCard($"Sorry, I have run into an issue. Please close the chat and try again", attrib.BotName)
            {
                Images = new List<CardImage>()
                {
                    new CardImage ()
                    {
                        //Reading Error Image from Local folders in solutiom
                        Url = string.Format(Constants.CardImageUrl, str1)
                    }
                }
            };

            return heroCard;

        }

        public static Attachment CreateUserProfileAdaptiveCard(UserProfile userProfile)
        {
            

            string jsonAdaptiveCard =myAssy.GetString(Assembly.GetExecutingAssembly().GetName().Name,"UserProfileAdaptiveCard.json");
            string fullName = userProfile.user.fullName;
            string age = userProfile.user.age.ToString();
            DateTime tempDateOfBirth = DateTime.ParseExact(userProfile.user.dateOfBirth, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            string dateOfBirth = tempDateOfBirth.ToString("dd MMM, yyyy");
            string gender = userProfile.user.gender;
            string height = $"{userProfile.user.height} cms";
            DateTime tempMemberSince= DateTime.ParseExact(userProfile.user.memberSince, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            string memberSince = tempMemberSince.ToString("dd MMM, yyyy");

            Dictionary<string, string> replaceMap = new Dictionary<string, string>()
            {
                { "{urlKey}", userProfile.user.avatar },
                { "{userNameKey}", fullName},
                { "{genderKey}", gender},
                { "{heightKey}", height},
                { "{ageKey}", age},
                { "{dateOfBirthKey}", dateOfBirth},
                { "{memberSinceKey}", memberSince }
            };

            foreach (var entry in replaceMap)
                jsonAdaptiveCard = jsonAdaptiveCard.Replace(entry.Key, entry.Value);
            



            //jsonAdaptiveCard = string.Format(jsonAdaptiveCard, userProfile.user.avatar, fullName, gender, height, age, dateOfBirth, memberSince);
            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(jsonAdaptiveCard),
            };
            return adaptiveCardAttachment;

        }

        public static Attachment CreateUserBadgesCard(UserBadges userBadges)
        {
            List<Dictionary<string, string>> dictlist = new List<Dictionary<string, string>>();

            string  badgeJsonSchema = myAssy.GetString(Assembly.GetExecutingAssembly().GetName().Name,"Badge.json");
            string badgeAdaptiveCardJsonSchema = myAssy.GetString(Assembly.GetExecutingAssembly().GetName().Name, "UserBadgesAdapativeCard.json");

            foreach (var badge in userBadges.badges)
            {
                Dictionary<string, string> dict = new Dictionary<string, string>()
                {
                    { "{badgeImage}", badge.image100px},
                    { "{badgeName}", badge.name},
                    { "{badgeType}", badge.category },
                    { "{earnedOn}", DateTime.ParseExact(badge.dateTime, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture).ToString("dd MMM, yyyy")},
                    { "{earnedMessage}", badge.marketingDescription},
                    { "{timesAchieved}", Convert.ToString(badge.timesAchieved)}
                };
                dictlist.Add(dict);
            }

            StringBuilder builder = new StringBuilder();
            
            foreach (var dict in dictlist)
            {
                string temp = badgeJsonSchema;
                foreach (var pair in dict)
                {
                    temp = temp.Replace(pair.Key, pair.Value);
                }
                builder.Append(temp);
                builder.Append(",");
            }

            badgeAdaptiveCardJsonSchema = badgeAdaptiveCardJsonSchema.Replace("\"{badges}\"", builder.ToString());

            var adaptiveCardAttachment = new Attachment()
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JsonConvert.DeserializeObject(badgeAdaptiveCardJsonSchema),
            };
            return adaptiveCardAttachment;
            
        }

        public static string SummaryText(UserActivity act)
        {
            var str1 = $"Summary: \r\n" +
                            $"**Calories**: {act.Summary.ActivityCalories}\r\n" +
                            $"Elevation: {act.Summary.Elevation}\r\n" +
                            $"SedentaryMinutes: {act.Summary.SedentaryMinutes}, " +
                            $"LightlyActiveMinutes: {act.Summary.LightlyActiveMinutes}, " +
                            $"FairlyActiveMinutes: {act.Summary.FairlyActiveMinutes}, " +
                            $"VeryActiveMinutes: {act.Summary.VeryActiveMinutes}\r\n" +
                            $"**Steps**: {act.Summary.Steps}, " +
                            $"Floors: {act.Summary.Floors}";
            return str1;

    }
    public static OAuthCard CreateOuathCard(string connectionName, string userName)
        {
            var oauthCard = new OAuthCard(string.Format(Constants.WelcomeMessage, userName))
            {
                ConnectionName = connectionName ?? throw new ArgumentNullException("Connection Name cannot be blank."),
                Buttons = new List<CardAction>
                {
                    new CardAction(ActionTypes.Signin, "SignIn",text: "SignIn", displayText: "SignIn", value: "SignIn" )

                }

            };

            return oauthCard;

        }

        public static OAuthPrompt OAuthPrompt(string connectionName)
        {
            var oauthPrompt = new OAuthPrompt(
                Constants.LoginPromtName, new OAuthPromptSettings
                {
                    ConnectionName = connectionName ?? throw new ArgumentNullException("Connection Name cannot be blank."),
                    Text = "Please Sign In",
                    Timeout = 300000,
                    Title = "SignIn"    
                }

            );

            return oauthPrompt;
            
        }

        //https://docs.microsoft.com/en-us/azure/bot-service/dotnet/bot-builder-dotnet-add-rich-card-attachments?view=azure-bot-service-3.0&viewFallbackFrom=azure-bot-service-4.0


        public static async Task SendErrorMessageAsync(ITurnContext turnContext, ActivityBotBase bot)
        {
            var reply = turnContext.Activity.CreateReply();
            reply.Attachments = new List<Attachment>()
            {
                CreateErrorCard(bot).ToAttachment()
            };

            await turnContext.SendActivityAsync(reply);
        }

    }
}
