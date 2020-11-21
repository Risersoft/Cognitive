using Microsoft.Bot.Builder;
using Microsoft.BotBuilderSamples.Bots;
using risersoft.app.mxform.cog.Models;
using risersoft.shared;
using risersoft.shared.dal;
using risersoft.shared.portable.Models.Nav;
using risersoft.shared.web.mvc;
using System;

namespace CognitiveServiceRsMx
{

    public class clsExtendWebAppMxEnt : clsExtendWebAppBase

    {


        public override string NewDBName()
        {
            return "mxentdb";
        }


        public override string ProgramCode()
        {
            return "mxent";
        }

        public override string ProgramName()
        {
            return "Maximprise";

        }

        public override string StartupAppCode()
        {
            return "";
        }
        public override clsCollecString<Type> dicBotTypes()
        {
            if (dicBot == null){
                dicBot = new clsCollecString<Type>();
                this.AddTypeAssembly(dicBot, typeof(EchoBot).Assembly, typeof(IBot));

            }
            return dicBot;
        }

        public override ICosmosDbContext CreateObjNoSqlContext(IAppDataProvider provider)

        {
            var objNoSQLHelper = new CosmosDbContext(provider);
            objNoSQLHelper.AddEntity<BotUserOption>("BotUserOption");
            objNoSQLHelper.AddEntity<BotGroupOption>("BotGroupOption");
            objNoSQLHelper.AddEntity<BotGroupUser>("BotGroupUser");
            return objNoSQLHelper;
        }
        public override string HelpSite()
        {
            throw new NotImplementedException();
        }
    }
}
