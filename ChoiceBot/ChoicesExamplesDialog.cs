using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Prompts;
using Microsoft.Bot.Builder.Prompts.Choices;
using Microsoft.Recognizers.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChoicePrompt = Microsoft.Bot.Builder.Dialogs.ChoicePrompt;

namespace ChoiceBot
{
    public interface IChoicesExamplesDialog : IDialog, IDialogContinue
    {
        
    }

    public class ChoicesExamplesDialog : DialogContainer, IChoicesExamplesDialog
    {
        public const string Id = "choicesExamplesDialog";
        private readonly ChoicePromptOptions _choicePromptOptions;

        public ChoicesExamplesDialog() : base(Id)
        {
            Dialogs.Add(Id, new WaterfallStep[]
            {
                AutoPrompt,
                SuggestedActionPrompt,
                InlinePrompt,
                ListPrompt,
                NonePrompt,
                DonePrompt
            });

            var auto = new ChoicePrompt(Culture.English) { Style = ListStyle.Auto };
            Dialogs.Add("auto", auto);

            var suggestedAction = new ChoicePrompt(Culture.English) {Style = ListStyle.SuggestedAction};
            Dialogs.Add("suggestedAction", suggestedAction);

            var inline = new ChoicePrompt(Culture.English) { Style = ListStyle.Inline };
            Dialogs.Add("inline", inline);

            var list = new ChoicePrompt(Culture.English) { Style = ListStyle.List,};
            Dialogs.Add("list", list);

            var none = new ChoicePrompt(Culture.English) { Style = ListStyle.None };
            Dialogs.Add("none", none);

            var choices = new List<Choice>();
            choices.Add(new Choice { Value = "Plain Pizza", Synonyms = new List<string> { "plain" } });
            choices.Add(new Choice { Value = "Pizza with Pepperoni", Synonyms = new List<string> { "4 Day", "workshop", "full" } });
            choices.Add(new Choice { Value = "Pizza with Mushrooms", Synonyms = new List<string> { "mushroom", "mushrooms", "shrooms" } });
            choices.Add(new Choice { Value = "Pizza with Peppers, Mushrooms and Brocolli", Synonyms = new List<string> { "vegtable", "veggie" } });
            choices.Add(new Choice { Value = "Pizza with Anchovies" });

            _choicePromptOptions = new ChoicePromptOptions { Choices = choices, RetryPromptString = "Sorry, that isn't on the list. Please pick again."};
        }

        private async Task Respond(DialogContext dc, IDictionary<string, object> args)
        {
            var choiceResult = args as ChoiceResult;
            if (choiceResult != null)
            {
                await dc.Context.SendActivity("you picked: " + choiceResult.Value.Value);
            }
        }
        private async Task AutoPrompt(DialogContext dc, IDictionary<string, object> args, SkipStepFunction next)
        {
            await Respond(dc, args);
            await dc.Prompt("auto", "What kind of Pizza would you like? (auto) ", _choicePromptOptions);
        }

        private async Task SuggestedActionPrompt(DialogContext dc, IDictionary<string, object> args, SkipStepFunction next)
        {
            await Respond(dc, args);
            await dc.Prompt("suggestedAction", "What kind of Pizza would you like? (suggestedAction)", _choicePromptOptions);
        }

        private async Task InlinePrompt(DialogContext dc, IDictionary<string, object> args, SkipStepFunction next)
        {
            await Respond(dc, args);
            await dc.Prompt("inline", "What kind of Pizza would you like? (inline)", _choicePromptOptions);
        }

        private async Task ListPrompt(DialogContext dc, IDictionary<string, object> args, SkipStepFunction next)
        {
            await Respond(dc, args);
            await dc.Prompt("list", "What kind of Pizza would you like? (list)", _choicePromptOptions);
        }

        private async Task NonePrompt(DialogContext dc, IDictionary<string, object> args, SkipStepFunction next)
        {
            await Respond(dc, args);
            await dc.Prompt("none", "What kind of Pizza would you like? (none)", _choicePromptOptions);
        }

        private async Task DonePrompt(DialogContext dc, IDictionary<string, object> args, SkipStepFunction next)
        {
            await Respond(dc, args);
            await dc.Context.SendActivity("The End");
            await dc.End();
        }
    }
}
