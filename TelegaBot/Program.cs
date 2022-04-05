using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegaBot
{
    internal class Program
    {
        public static ITelegramBotClient bot = new TelegramBotClient("5260914849:AAEbueMl50UkBmB5do9bJ4nKopHUQghxVrc");
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower() == "/start")
                {
                    var text = "sdasda";
                    var ikm = new InlineKeyboardMarkup(new[]
                     {
                        new[]
                            {
                              InlineKeyboardButton.WithCallbackData("🍞Седан", "callbackSedan"),
                              InlineKeyboardButton.WithCallbackData("🥛Универсал", "callbackStation"),
                              InlineKeyboardButton.WithCallbackData("🐟Хэтчбек "),
                            },                        
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🍖Лифтбек"),
                            InlineKeyboardButton.WithCallbackData("⚱️Купе"),
                            InlineKeyboardButton.WithCallbackData("🐓Лимузин"),
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("🍦Кабриолет"),
                            InlineKeyboardButton.WithCallbackData("🏮Внедорожник"),
                        },
                    });
                    await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать выберите категорию");
                    await botClient.SendTextMessageAsync(message.Chat.Id, text, replyMarkup: ikm);
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
            }
        }

        

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }

        static void Main(string[] args)
        {

            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            
            Console.ReadLine();
        }
    }

    
}
