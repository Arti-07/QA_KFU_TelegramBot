using System;
using Telegram.Bot;
using Telegram;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.InputFiles;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Collections;
using Newtonsoft.Json;

namespace QA_KFU_TelegramBot
{
    class Program
    {
        static string TOKEN;   //   1879411043:AAFmjjG_6iThq-Ugh2nWGpCIgQJm9ReRdz8
        static bool IsButton = false;
        static bool IsChange = false;
        static TelegramBotClient bot;
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine("Введи токен : ");
            //TOKEN = Convert.ToString(Console.ReadLine());
            
            TOKEN = "1879411043:AAFmjjG_6iThq-Ugh2nWGpCIgQJm9ReRdz8";
            bot = new TelegramBotClient(TOKEN);                    // Подключили бота

            bot.OnMessage += BotOnMessageRecieved;
            bot.OnCallbackQuery += BotOnCallbackQueryRecieved;
            bot.OnCallbackQuery += MenuCallBackQuerry;

            var me = bot.GetMeAsync().Result;
            Console.WriteLine(me.FirstName);
            bot.StartReceiving();                          // Open stream 
            Console.ReadKey();
            bot.StopReceiving();                          // Close stream

        }
        private void ReadData()
        {
            string response;
            using (StreamReader stream = new StreamReader("DataUsers.json"))
            {
                while (!String.IsNullOrEmpty(response = stream.ReadLine()))
                {
                    Users Response = JsonConvert.DeserializeObject<Users>(response);
                    uSers.Add(Response);
                    foreach(var usr in uSers)
                    {
                        ID.Add(usr.ID);
                    }
                }
            }

        }
        private static void BotOnCallbackQueryRecieved(object sender, CallbackQueryEventArgs e)
        {
            string buttonText = e.CallbackQuery.Data;
            string name = $"{e.CallbackQuery.From.FirstName} {e.CallbackQuery.From.LastName}";
            Console.WriteLine($"{name} press a key {buttonText}");
        }

        List<Users> uSers = new List<Users>();
        List<int> ID = new List<int>();


        private static async void MenuCallBackQuerry(object sender, CallbackQueryEventArgs e)
        {
            var message = e.CallbackQuery.Message;
            if (e.CallbackQuery.Data == "ab")
            {
                string mmess = " Данный бот помогает абитуриенту разобраться с вопросами," +
                " которые могут возникнуть при поступлении. А также" +
                " поможет студентам в поиске нужной информации";
                await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, mmess);
            }
            else
            {

            }
        }
        private static async void BotOnMessageRecieved(object sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (message == null || message.Type != MessageType.Text)
            {
                return;
            }
            string name = $"{message.From.FirstName} {message.From.LastName}";
            Console.WriteLine(name + ": " + message.Text);

            






            switch (message.Text)     // Команды для бота
            {
                case "/start":
                    var sticker = new InputOnlineFile("CAACAgIAAxkBAALwi2BE04ZTyadk2ufx3khKlil1K7RjAAJ0AAM7YCQUs8te1W3kR_QeBA");
                    await bot.SendStickerAsync(message.Chat.Id, sticker);
                    string text = "\U0001F525Добро пожаловать! \U0001F525" +
                        "\n" +
                        "\n \U0001F527 Спасибо, что подключили бота! \U0001F527" +
                        "\n " +
                        "\n \U0001F381 Лови кнопочки! \U0001F381";
                    Users user = new Users(message.From.FirstName, message.From.Id);

                    //var jsonFormater = new DataContractJsonSerializer(typeof(List<Users>));
                    //using (var file = new FileStream("Users.json", FileMode.Append))
                    //{
                    //    jsonFormater.WriteObject(file, user);
                    //}

                    var json = JsonConvert.SerializeObject(user);
                    using (StreamWriter stream = new StreamWriter("DataUsers.json", true))
                    {
                        stream.WriteLine(json);
                    }


                    ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup
                    {
                        Keyboard = new[]
                        {
                            new[]
                            {
                                new KeyboardButton("\U0001F4CB FAQ \U0001F4AC"),
                                new KeyboardButton("Справка")
                            },
                            new []
                            {
                                new KeyboardButton("\U0001F4C5 Календарь событий \U0001F4C5"),
                                new KeyboardButton("Если не нашёл(а) свой вопрос в FAQ")
                            },
                            new[]
                            {
                                new KeyboardButton("\U0001F4BB Кафедры \U0001F393")
                            },
                        }
                    };
                    await bot.SendTextMessageAsync(message.From.Id, text, ParseMode.Html, false, false, 0, keyboard);
                    break;
                case "\U0001F4CB FAQ \U0001F4AC":
                    var inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("КАКОЙ-то вопрос","Authors"),
                            InlineKeyboardButton.WithCallbackData("FAQ","questions")
                        },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("КАКОЙ-то вопрос","Authors"),
                                InlineKeyboardButton.WithCallbackData("FAQ","questions")
                               
                               
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("Что-то написано","questions"),
                                InlineKeyboardButton.WithCallbackData("Что-то напсиано","Authors")
                            },
                            

                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Что-то написано","Weather"),
                            InlineKeyboardButton.WithCallbackData("Что-то написано", "weater")
                        }
                    });
                    await bot.SendTextMessageAsync(message.From.Id, $"Выберите пункт меню {1F} :", replyMarkup: inlineKeyboard);
                    break;

                case "Справка":
                    var Keyboard_Note = new InlineKeyboardMarkup(new[]{
                        new []
                        {
                           InlineKeyboardButton.WithUrl("Автор","https://t.me/ar_gin"),
                        },
                        new[]
                        {
                                InlineKeyboardButton.WithUrl("Автор","https://t.me/auter1"),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithUrl("Автор","https://t.me/ROwaGO")
                        },
                        new []
                        {
                           InlineKeyboardButton.WithCallbackData("О функциях бота","ab")
                        }


                    });
                    await bot.SendTextMessageAsync(message.From.Id, $"Выберите пункт 'Справки' {1F} :", replyMarkup: Keyboard_Note);
                    break;

                case "\U0001F4C5 Календарь событий \U0001F4C5":
                    string mes = "Ой, пока запланированных событий нет \U0001F61E";
                    await bot.SendTextMessageAsync(message.From.Id, mes, ParseMode.Html, false, false, 0);
                    break;


                case "Кафедры":
                    var Keyboard_Department = new InlineKeyboardMarkup(new[]{
                        new []
                        {
                           InlineKeyboardButton.WithUrl("Кафедра прикладной математики","https://kpfu.ru/computing-technology/struktura/kafedry/kafedra-prikladnoj-matematiki"),
                        },
                        new[]
                        {
                                InlineKeyboardButton.WithUrl("Кафедра вычислительной математики","https://kpfu.ru/computing-technology/struktura/kafedry/kafedra-vychislitelnoj-matematiki"),
                        },
                        new []
                        {
                            InlineKeyboardButton.WithUrl("Кафедра теоретической кибернетики","https://kpfu.ru/computing-technology/struktura/kafedry/kafedra-teoreticheskoj-kibernetiki")
                        },
                        new []
                        {
                           InlineKeyboardButton.WithUrl("Кафедра анализа данных и исследования операций","https://kpfu.ru/computing-technology/struktura/kafedry/kafedra-analiza-dannyh-i-issledovaniya-operacij")
                        },
                        new []
                        {
                            InlineKeyboardButton.WithUrl("Кафедра технологий программирования","https://kpfu.ru/computing-technology/struktura/kafedry/kafedra-tehnologij-programmirovaniya")
                        },
                        new []
                        {
                           InlineKeyboardButton.WithUrl("Кафедра системного анализа и информационных технологий","https://kpfu.ru/computing-technology/struktura/kafedry/kafedra-sistemnogo-analiza-i-informacionnyh")
                        },
                        new []
                        {
                            InlineKeyboardButton.WithUrl("Кафедра информационных систем","https://kpfu.ru/computing-technology/struktura/kafedry/kafedra-informacionnyh-sistem")
                        },
                        
                    });
                    await bot.SendTextMessageAsync(message.From.Id, $"Выберите пункт 'Кафедра' {1F} :", replyMarkup: Keyboard_Department);
                    break;
            }


            //bot.OnCallbackQuery += (object sender, CallbackQueryEventArgs e) =>
            //{
            //    //var message = e.CallbackQuery.Message;
            //    //if (e.CallbackQuery.Data == "ab")
            //    //{
            //    //    string mmess = " Данный бот помогает абитуриенту разобраться с вопросами," +
            //    //    " которые могут возникнуть при поступлении. А также" +
            //    //    " поможет студентам в поиске нужной информации";
            //    //     bot.SendTextMessageAsync(message.From.Id, mmess);
            //    //}
            //    //else
            //    //{

            //    //}

            //};
        }
    }
}
