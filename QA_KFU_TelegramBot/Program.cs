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
        static bool IsButton = false;
        static bool IsChange = false;
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

            var me = bot.GetMeAsync().Result;

            Console.WriteLine(me.FirstName);
            bot.StartReceiving();                          // Open stream 
            Console.ReadKey();
            bot.StopReceiving();                          // Close stream









        }

        private static async void BotOnCallbackQueryRecieved(object sender, CallbackQueryEventArgs e)
        {
            string buttonText = e.CallbackQuery.Data;
            string name = $"{e.CallbackQuery.From.FirstName} {e.CallbackQuery.From.LastName}";
            Console.WriteLine($"{name} press a key {buttonText}");
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
                                new KeyboardButton("\U0001F3AE Справка \U0001F3AE")
                            },
                            new []
                            {
                                new KeyboardButton("Календарь событий"),
                                new KeyboardButton("Если не нашёл(а) свой вопрос в FAQ")
                            }
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
                                InlineKeyboardButton.WithCallbackData("FAQ","questions"),
                                InlineKeyboardButton.WithCallbackData("КАКОЙ-то вопрос","Authors")
                            },
                            

                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Узнать погоду","Weather"),
                            InlineKeyboardButton.WithCallbackData("Пустая кнопка, пока...", "weater")
                        }
                    });
                    await bot.SendTextMessageAsync(message.From.Id, $"Выберите пункт меню {1F} :", replyMarkup: inlineKeyboard);
                    break;

                    bot.OnCallbackQuery += (object sender, CallbackQueryEventArgs e) =>
                    {
                        var message = e.CallbackQuery.Message;
                        if (e.CallbackQuery.Data == "FAQ")
                        {
                            
                        }
                        else
                        {

                        }

                    };


        }
        }
    }
}