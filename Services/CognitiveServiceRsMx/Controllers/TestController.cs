using Alexa.NET.Request;
using Alexa.NET.Response;
using CognitiveServiceRsMx.Speech.Quiz;
using CognitiveServiceRsMx.Speech.Wordy;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CognitiveServiceRsMx.Controllers
{
    public class TestController : ApiController
    {
       

        [Route("api/menu")]
        [HttpPost]
        public async Task<SkillResponse> Menu([FromBody] SkillRequest input)
        {
            var speechlet = new MenuSpeechlet();
            return await speechlet.GetResponseAsync(input);
        }
        [Route("api/quizus")]
        [HttpPost]
        public async Task<SkillResponse> QuizUS([FromBody] SkillRequest input)
        {
            var speechlet = new USQuizSpeechlet();
            return await speechlet.GetResponseAsync(input);
        }
        [Route("api/quizin")]
        [HttpPost]
        public async Task<SkillResponse> QuizIN([FromBody] SkillRequest input)
        {
            var speechlet = new INQuizSpeechlet();
            return await speechlet.GetResponseAsync(input);
        }

        [Route("api/wordy")]
        [HttpPost]
        public async Task<SkillResponse> Wordy([FromBody] SkillRequest input)
        {
            var speechlet = new WordySpeechlet();
            return await speechlet.GetResponseAsync(input);
        }
    }
}
