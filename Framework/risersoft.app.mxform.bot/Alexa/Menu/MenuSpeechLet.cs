using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using CognitiveServiceRsMx.Helpers;
using CognitiveServiceRsMx.Models;
using Newtonsoft.Json;
using risersoft.shared.bot;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
namespace CognitiveServiceRsMx
{

    public class MenuSpeechlet : RSSpeechletBase
    {
        
        private static MenuSchedule _menuSchedule;
        protected override string WELCOME_MESSAGE { get; set; } = "Welcome";
        protected override string EXIT_SKILL_MESSAGE { get; set; } = "Bye";
        protected override string HELP_MESSAGE { get; set; } = "I am very helpful";
        protected override string START_QUIZ_MESSAGE { get; set; } = "Starting Now";

        protected override string SkillName { get; set; } = "School Menu";
        private const string DateKey = "Date";
        private const string MenuForDateIntent = "MenuForDateIntent";

        public override Task<SkillResponse> OnLaunchAsync(LaunchRequest launchRequest, Session session, Context context)
        {
            return Task.FromResult(GetWelcomeResponse());
        }

        protected override SkillResponse GetWelcomeResponse()
        {
            var output = "Welcome to the English International School menu app. Please request the menu for a given date.";
            return BuildSkillResponse("Welcome", output, false);
        }

        public override async  Task<SkillResponse> OnIntentAsync(IntentRequest intentRequest, Session session, Context context)
        {
            // Get intent from the request object.
            var intent = intentRequest.Intent;
            var intentName = intent?.Name;

            // Note: If the session is started with an intent, no welcome message will be rendered;
            // rather, the intent specific response will be returned.
            if (MenuForDateIntent.Equals(intentName))
            {
                return await GetMenuResponse(intent, session);
            }

            return ResponseBuilder.Tell("Invalid Intent");
        }

   
        public override Task<SkillResponse> OnSessionEndedAsync(SessionEndedRequest sessionEndedRequest, Session session, Context context)
        {
            return Task.FromResult<SkillResponse>(null);
        }

        private async Task<SkillResponse> GetMenuResponse(Intent intent, Session session)
        {
            // Retrieve date from the intent slot
            var dateSlot = intent.Slots[DateKey];

            // Create response
            string output;
            DateTime date;
            var endSession = false;
            if (dateSlot != null && DateTime.TryParse(dateSlot.Value, out date))
            {
                // Retrieve and return the menu response
                if (_menuSchedule == null)
                {
                    _menuSchedule = await LoadMenuSchedule();
                }

                var menu = _menuSchedule.GetMenuForDate(date);
                if (menu != null)
                {
                    output = _menuSchedule.GetMenuForDate(date).ToString(date);
                    endSession = true;
                }
                else
                {
                    output = $"Sorry, no menu is available for {date.ToStringWithSuffix("d MMMM")}.  Please try another date or say quit to exit";
                }
            }
            else
            {
                // Render an error since we don't know what the date requested is
                output = "I'm not sure which date you require, please try again or say quit to exit.";
            }

            // Return response, passing flag for whether to end the conversation
            return BuildSkillResponse(intent.Name, output, endSession);
        }

        private async Task<MenuSchedule> LoadMenuSchedule()
        {
            var filePath = HttpContext.Current.Server.MapPath("/App_Data/menu.json");
            using (var reader = new StreamReader(filePath))
            {
                var json = await reader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<MenuSchedule>(json);
            }
        }

        /// <summary>
        /// Creates and returns the visual and spoken response with shouldEndSession flag
        /// </summary>
        /// <param name="title">Title for the companion application home card</param>
        /// <param name="output">Output content for speech and companion application home card</param>
        /// <param name="shouldEndSession">Should the session be closed</param>
        /// <returns>SkillResponse spoken and visual response for the given input</returns>
        private SkillResponse BuildSkillResponse(string title, string output, bool shouldEndSession)
        {
            // Create the Simple card content
            var card = new SimpleCard
            {
                Title = $"School Menu - {title}",
                Content = $"School Menu - {output}"
            };


            // Create the plain text output
            var speech = new PlainTextOutputSpeech { Text = output };

            return this.BuildResponse(card, speech, shouldEndSession);
        }


    }
}
