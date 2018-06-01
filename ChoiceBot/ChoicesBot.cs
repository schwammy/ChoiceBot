using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Bot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;

namespace ChoiceBot
{
    public class ChoicesBot : IBot
    {
        private DialogSet Dialogs { get; }

        public ChoicesBot(IChoicesExamplesDialog choicesExamplesDialog)
        {
            Dialogs = new DialogSet();

            Dialogs.Add("choicesExamplesDialog", choicesExamplesDialog);
        }
        public async Task OnTurn(ITurnContext context)
        {
            
            // This bot is only handling Messages
            if (context.Activity.Type == ActivityTypes.Message)
            {
                var conversationState = context.GetConversationState<Dictionary<string, object>>();


                // Establish dialog state from the conversation state.
                var dc = Dialogs.CreateContext(context, conversationState);

                // Continue any current dialog.
                await dc.Continue();

                if (!context.Responded)
                {
                    await dc.Begin("choicesExamplesDialog");
                }


            }
        }
    }    
}
