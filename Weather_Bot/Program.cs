using NewAbhWeatherBot;
using Parsering;
using System.Configuration;
using System.Data.SqlClient;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Weather_Bot;

var botClient = new TelegramBotClient("5508535639:AAEloOE7dOKW2JjaqWHA73l_3vsQmQxMezc");
using var cts = new CancellationTokenSource();
var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = { }
};
var me = await botClient.GetMeAsync();
Console.WriteLine(receiverOptions);
Console.WriteLine($"Запущен бот {me.Username}");
botClient.StartReceiving(
        AbhWeather.HandleUpdatesAsync,
        AbhWeather.HandleErrorAsync,
        receiverOptions,
        cancellationToken: cts.Token);
Console.ReadLine();
cts.Cancel();

namespace Weather_Bot
{
    public class AbhWeather
    {

        public static async Task HandleUpdatesAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update?.Message?.Text != null)
            {
                await AbhWeather.HandleMessage(botClient, update.Message);
                return;
            }
        }
        static async Task HandleMessage(ITelegramBotClient botClient, Message message)
        {

            string url;
            string city = "";
            string temp = "";
            string feels = "";
            string wind = "";
            string humidity = "";
            string pressure = "";
            string water = "";

            Console.WriteLine($"Пришел запрос от {message.Chat.Id}");
            Console.WriteLine($"Выбрана команда: {message.Text}");
            ReplyKeyboardMarkup keyboard = new(new[]
            {
                 new KeyboardButton[] {"Гагра", "Гал", "Гудаута", "Гулрыпш", "Новый Афон"},
                 new KeyboardButton[] {"Очамчыра", "Пицунда", "Сухум", "Ткуарчал", "Цандрипш"}
    })
            {
                ResizeKeyboard = true
            };

            switch (message.Text)
            {
                case "/start":
                    {
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Вас приветствует бот мониторинга погоды в Абхазии. Начнем?", replyMarkup: keyboard);
                        for (int i = 0; i < 10; i++)
                        {
                            if (i == 0)
                            {
                                url = "https://prognoz3.ru/abkhazia/gagra/ghagra";
                                DataBase.AddToDB(url, ref city, ref temp, ref feels, ref wind, ref humidity, ref pressure, ref water);
                            }
                            else if (i == 1)
                            {
                                url = "https://prognoz3.ru/abkhazia/gali/galle";
                                DataBase.AddToDB(url, ref city, ref temp, ref feels, ref wind, ref humidity, ref pressure, ref water);
                            }
                            else if (i == 2)
                            {
                                url = "https://prognoz3.ru/abkhazia/gudauta/gudauta";
                                DataBase.AddToDB(url, ref city, ref temp, ref feels, ref wind, ref humidity, ref pressure, ref water);
                            }
                            else if (i == 3)
                            {
                                url = "https://prognoz3.ru/abkhazia/gulrypsh/gulrypsh";
                                DataBase.AddToDB(url, ref city, ref temp, ref feels, ref wind, ref humidity, ref pressure, ref water);
                            }
                            else if (i == 4)
                            {
                                url = "https://prognoz3.ru/abkhazia/gudauta/novyj-afon";
                                DataBase.AddToDB(url, ref city, ref temp, ref feels, ref wind, ref humidity, ref pressure, ref water);
                            }
                            else if (i == 5)
                            {
                                url = "https://prognoz3.ru/abkhazia/ochamchyrsky/ochamchyra";
                                DataBase.AddToDB(url, ref city, ref temp, ref feels, ref wind, ref humidity, ref pressure, ref water);
                            }
                            else if (i == 6)
                            {
                                url = "https://prognoz3.ru/abkhazia/gagra/pitsunda";
                                DataBase.AddToDB(url, ref city, ref temp, ref feels, ref wind, ref humidity, ref pressure, ref water);
                            }
                            else if (i == 7)
                            {
                                url = "https://prognoz3.ru/abkhazia/sukhumi/suhum";
                                DataBase.AddToDB(url, ref city, ref temp, ref feels, ref wind, ref humidity, ref pressure, ref water);
                            }
                            else if (i == 8)
                            {
                                url = "https://prognoz3.ru/abkhazia/tkuarchal/tkuarchal";
                                DataBase.AddToDB(url, ref city, ref temp, ref feels, ref wind, ref humidity, ref pressure, ref water);
                            }
                            else if (i == 9)
                            {
                                url = "https://prognoz3.ru/abkhazia/gagra/tsandrypsh";
                                DataBase.AddToDB(url, ref city, ref temp, ref feels, ref wind, ref humidity, ref pressure, ref water);
                            }
                        }
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Parser"].ConnectionString);
                        string query = "INSERT INTO AbhWeather (User_Id) VALUES(@User_Id)";
                        SqlCommand command = new SqlCommand(query, con);
                        command.Parameters.AddWithValue("@User_Id", message.Chat.Id);
                        try
                        {
                            con.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (SqlException e)
                        {
                            Console.WriteLine("Произошла ошибка при передаче данных в базу. Код ошибки: " + e.ToString());
                        }
                    }
                    break;
                case "Гагра":
                    {
                        url = "https://prognoz3.ru/abkhazia/gagra/ghagra";
                        Parser.Parse(url, ref city, ref temp, ref feels, ref wind, ref humidity, ref pressure, ref water);
                        await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels + "\n" + wind + "\n" + humidity + "\n" + pressure + "\n" + water + "\n", replyMarkup: keyboard);
                    }
                    break;
                case "Гал":
                    {
                        url = "https://prognoz3.ru/abkhazia/gali/galle";
                        Parser.Parse(url, ref city, ref temp, ref feels, ref wind, ref humidity, ref pressure, ref water);
                        await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels + "\n" + wind + "\n" + humidity + "\n" + pressure + "\n" + water + "\n", replyMarkup: keyboard);
                    }
                    break;
                case "Гудаута":
                    {
                        url = "https://prognoz3.ru/abkhazia/gudauta/gudauta";
                        Parser.Parse(url, ref city, ref temp, ref feels, ref wind, ref humidity, ref pressure, ref water);
                        await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels + "\n" + wind + "\n" + humidity + "\n" + pressure + "\n" + water + "\n", replyMarkup: keyboard);
                    }
                    break;
                case "Гулрыпш":
                    {
                        url = "https://prognoz3.ru/abkhazia/gulrypsh/gulrypsh";
                        Parser.Parse(url, ref city, ref temp, ref feels, ref wind, ref humidity, ref pressure, ref water);
                        await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels + "\n" + wind + "\n" + humidity + "\n" + pressure + "\n" + water + "\n", replyMarkup: keyboard);
                    }
                    break;
                case "Новый Афон":
                    {
                        url = "https://prognoz3.ru/abkhazia/gudauta/novyj-afon";
                        Parser.Parse(url, ref city, ref temp, ref feels, ref wind, ref humidity, ref pressure, ref water);
                        await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels + "\n" + wind + "\n" + humidity + "\n" + pressure + "\n" + water + "\n", replyMarkup: keyboard);
                    }
                    break;
                case "Очамчыра":
                    {
                        url = "https://prognoz3.ru/abkhazia/ochamchyrsky/ochamchyra";
                        Parser.Parse(url, ref city, ref temp, ref feels, ref wind, ref humidity, ref pressure, ref water);
                        await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels + "\n" + wind + "\n" + humidity + "\n" + pressure + "\n" + water + "\n", replyMarkup: keyboard);
                    }
                    break;
                case "Пицунда":
                    {
                        url = "https://prognoz3.ru/abkhazia/gagra/pitsunda";
                        Parser.Parse(url, ref city, ref temp, ref feels, ref wind, ref humidity, ref pressure, ref water);
                        await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels + "\n" + wind + "\n" + humidity + "\n" + pressure + "\n" + water + "\n", replyMarkup: keyboard);
                    }
                    break;
                case "Сухум":
                    {
                        url = "https://prognoz3.ru/abkhazia/sukhumi/suhum";
                        Parser.Parse(url, ref city, ref temp, ref feels, ref wind, ref humidity, ref pressure, ref water);
                        await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels + "\n" + wind + "\n" + humidity + "\n" + pressure + "\n" + water + "\n", replyMarkup: keyboard);
                    }
                    break;
                case "Ткуарчал":
                    {
                        url = "https://prognoz3.ru/abkhazia/tkuarchal/tkuarchal";
                        Parser.Parse(url, ref city, ref temp, ref feels, ref wind, ref humidity, ref pressure, ref water);
                        await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels + "\n" + wind + "\n" + humidity + "\n" + pressure + "\n" + water + "\n", replyMarkup: keyboard);
                    }
                    break;
                case "Цандрипш":
                    {
                        url = "https://prognoz3.ru/abkhazia/gagra/tsandrypsh";
                        Parser.Parse(url, ref city, ref temp, ref feels, ref wind, ref humidity, ref pressure, ref water);
                        await botClient.SendTextMessageAsync(message.Chat.Id, city + "\nТемпература: " + temp + "°С" + "\n" + feels + "\n" + wind + "\n" + humidity + "\n" + pressure + "\n" + water + "\n", replyMarkup: keyboard);
                    }
                    break;
                default:
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Выберите команду: ", replyMarkup: keyboard);
                    break;
            }
        }


        public static Task HandleErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Ошибка телеграм API:\n{apiRequestException.ErrorCode}\n{apiRequestException.Message}",
                _ => exception.ToString()
            };
            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }

}

