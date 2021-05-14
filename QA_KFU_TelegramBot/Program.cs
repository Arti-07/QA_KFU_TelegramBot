﻿using System;
using Telegram.Bot;
using Telegram;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.InputFiles;

namespace QA_KFU_TelegramBot
{
    class Program
    {
        static string TOKEN;   //   1879411043:AAFmjjG_6iThq-Ugh2nWGpCIgQJm9ReRdz8
        //static bool IsButton = false;
        //static bool IsChange = false;
        static TelegramBotClient bot;

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine("Введи токен : ");
            //TOKEN = Convert.ToString(Console.ReadLine());
            TOKEN= "1879411043:AAFmjjG_6iThq-Ugh2nWGpCIgQJm9ReRdz8";
            bot = new TelegramBotClient(TOKEN);                    // Подключили бота

            bot.OnMessage += BotOnMessageRecieved;
            bot.OnCallbackQuery += BotOnCallbackQueryRecieved;
            bot.OnCallbackQuery += MenuCallBackQuerry;
            //SendMessage();
            var me = bot.GetMeAsync().Result;

            Console.WriteLine(me.FirstName);
            bot.StartReceiving();                          // Open stream 
            Console.ReadKey();
            bot.StopReceiving();                          // Close stream
        }

        private static void BotOnCallbackQueryRecieved(object sender, CallbackQueryEventArgs e)
        {
            string buttonText = e.CallbackQuery.Data;
            string name = $"{e.CallbackQuery.From.FirstName} {e.CallbackQuery.From.LastName}";
            Console.WriteLine($"{name} press a key {buttonText}");
        }

        private static void SendMessage()
        {
            bot.SendTextMessageAsync(363450022, "Hello WORLd");
        }



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
            else if(e.CallbackQuery.Data == "AdminDir")
            {
                string a = "Директор института: Чикрин Дмитрий Евгеньевич.\n\n" +
                    "Адрес: Адрес: 420008, г. Казань, ул. Кремлевская, 35, каб.1004\n" +
                    "Телефон: (843) 233-70-37\n" +
                    "Email: dmitry.kfu@gmail.com";
                //await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, a);
                await bot.SendPhotoAsync(e.CallbackQuery.From.Id, "https://kpfu.ru/docs/F16347701418/img8152402.jpg", a);
                a = "Заместитель директора по цифровизации: Егорчев Антон Александрович.\n\n" +
                   "Адрес: Адрес: 420008, г. Казань, ул. Кремлевская, 35, каб.1004\n" +
                   "Телефон: (843) 233-70-37\n" +
                   "Email: anton.egorchev.kfu@gmail.com";
                //await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, a);
                await bot.SendPhotoAsync(e.CallbackQuery.From.Id, "https://kpfu.ru/docs/F21520534037/img703043329.jpg", a);
                a = "Заместитель директора по научной деятельности: Тумаков Дмитрий Николаевич.\n\n" +
                   "Адрес: Адрес: 420008, г. Казань, ул. Кремлевская, 35, каб.1202\n" +
                   "Телефон: (843) 233-70-35\n" +
                   "Email: dtumakov@kpfu.ru";
                //await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, a);
                await bot.SendPhotoAsync(e.CallbackQuery.From.Id, "https://shelly.kpfu.ru/e-ksu/docs/F353093089/IMAG0084_1.jpg?rnd=6943", a);
                a = "Заместитель директора по образовательной деятельности: Панкратова Ольга Владиславна.\n\n" +
                   "Адрес: Адрес: 420008, г. Казань, ул. Кремлевская, 35, каб.1002\n" +
                   "Телефон: (843) 233-71-55\n" +
                   "Email: olga_pankratova@rambler.ru";
                //await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, a);
                await bot.SendPhotoAsync(e.CallbackQuery.From.Id, "http://shelly.kpfu.ru/e-ksu/docs/F55574275/m__74_.jpg?rnd=787", a);
                a = "Заместитель директора по воспитательной и социальной работе и связям с общественностью: Новенькова Аида Зуфаровна.\n\n" +
   "Адрес: Адрес: 420008, г. Казань, ул. Кремлевская, 35, каб.1001\n" +
   "Телефон: (843) 233-77-74\n" +
   "Email: followaida@gmail.com";
                //await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, a);
                await bot.SendPhotoAsync(e.CallbackQuery.From.Id, "https://kpfu.ru/portal/docs/F1491305294/Novenkova.jpg", a);
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




                    ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup
                    {
                        Keyboard = new[]
                        {
                            new[]
                            {
                                new KeyboardButton("\U0001F4C3 FAQ \U0001F4C3"),
                                new KeyboardButton("Справка")
                            },
                            new []
                            {
                                new KeyboardButton("Календарь событий"),
                                new KeyboardButton("Если не нашёл(а) свой вопрос в FAQ")
                            },
                            new[]
                            {
                                new KeyboardButton("Кафедры"),
                                new KeyboardButton("Администрация")
                            },
                        }
                    };
                    await bot.SendTextMessageAsync(message.From.Id, text, ParseMode.Html, false, false, 0, keyboard);
                    break;
                case "\U0001F4C3 FAQ \U0001F4C3":
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

                case "Календарь событий":
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
                case "Администрация":
                    var AdminKeyboard = new InlineKeyboardMarkup(new[]{
                        new []
                        {
                           InlineKeyboardButton.WithCallbackData("Директорат","AdminDir")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("Ученый совет","https://kpfu.ru/computing-technology/struktura/uchenyj-sovet")
                        },
                        new[]
                        {
                            InlineKeyboardButton.WithUrl("Сотрудники","https://kpfu.ru/computing-technology/sotrudniki")
                        }
                    });
                    await bot.SendTextMessageAsync(message.From.Id, $"Информация об ИВМИИТ:", replyMarkup: AdminKeyboard);
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
