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
        //static bool IsChange = false;
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
            //SendMessage();
            var me = bot.GetMeAsync().Result;
            Console.WriteLine(me.FirstName);
            bot.StartReceiving();                          // Open stream 
            Console.ReadKey();
            bot.StopReceiving();                          // Close stream
            
        }
       //private void ReadData()
       // {
       //     string response;
       //     using (StreamReader stream = new StreamReader("DataUsers.json"))
       //     {
       //         while (!String.IsNullOrEmpty(response = stream.ReadLine()))
       //         {
       //             Users Response = JsonConvert.DeserializeObject<Users>(response);
       //             uSers.Add(Response);
       //             foreach(var usr in uSers)
       //             {
       //                 ID.Add(usr.ID);
       //             }
       //         }
       //     }

       // }
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
            } else if(e.CallbackQuery.Data == "mathelp")
            {
                string filePath = @"Documents/MatHelp.pdf";
                using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                await bot.SendDocumentAsync(e.CallbackQuery.From.Id, new InputOnlineFile(fileStream, filePath), "Документ на мат. помощь");
            } else if(e.CallbackQuery.Data == "documents")
            {
                var documentsKeyboard = new InlineKeyboardMarkup(new[]
                        {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Материальная помощь","mathelp"),
                            InlineKeyboardButton.WithCallbackData("Социальное питание","socialfood")
                        },
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Заселение в ДУ","liveindu"),
                            InlineKeyboardButton.WithCallbackData("Устройство двойки", "two")
                        }
                        });
                await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, $"Выбери документ, который тебе нужен : ", replyMarkup: documentsKeyboard);
            }else if (e.CallbackQuery.Data == "socialfood")
            {
                string filePath = @"Documents/1. Заявление на питание.pdf";
                using var fileStream0 = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                await bot.SendDocumentAsync(e.CallbackQuery.From.Id, new InputOnlineFile(fileStream0, filePath), "1. Заявление на питание");
                filePath = @"Documents/2. Заявление-распоряжение.pdf";
                using var fileStream1 = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                await bot.SendDocumentAsync(e.CallbackQuery.From.Id, new InputOnlineFile(fileStream1, filePath), "2.Заявление - распоряжение");
                filePath = @"Documents/3. Договор текущего счета.pdf";
                using var fileStream2 = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                await bot.SendDocumentAsync(e.CallbackQuery.From.Id, new InputOnlineFile(fileStream2, filePath), "3. Договор текущего счета");
                filePath = @"Documents/4. Анкета.pdf";
                using var fileStream3 = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                await bot.SendDocumentAsync(e.CallbackQuery.From.Id, new InputOnlineFile(fileStream3, filePath), "4. Анкета");
                filePath = @"Documents/5. Опрос-анкета.pdf";
                using var fileStream4 = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                await bot.SendDocumentAsync(e.CallbackQuery.From.Id, new InputOnlineFile(fileStream4, filePath), "5. Опрос-анкета");
                filePath = @"Documents/6. Согласие на обработку.pdf";
                using var fileStream5 = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                await bot.SendDocumentAsync(e.CallbackQuery.From.Id, new InputOnlineFile(fileStream5, filePath), "6. Согласие на обработку");
                await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "Для получения социальнного питания тебе нужно заполнить данные документы в соотвествии с образцом ниже");
                filePath = @"Documents/Образец заполнения.pdf";
                using var fileStream6 = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                await bot.SendDocumentAsync(e.CallbackQuery.From.Id, new InputOnlineFile(fileStream6, filePath), "Образец заполнения");

            } else if(e.CallbackQuery.Data == "whereEat")
            {

            } else if(e.CallbackQuery.Data == "liveindu")
            {
                string filePath = @"Documents/Пакет документов для заселения в ДУ.docx";
                using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                await bot.SendDocumentAsync(e.CallbackQuery.From.Id, new InputOnlineFile(fileStream, filePath), "Тут ты можешь найти документы, которые тебе потребуются для заселения");
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

            //Program p = new Program();

            
            if(IsButton)
            {
                if (message.From.Id == 363450022 || message.From.Id == 806879827 || message.From.Id == 250899062)
                {
                    //List<Users> uSers = new List<Users>();
                    List<int> ID = new List<int>();
                    string response;
                    using (StreamReader stream = new StreamReader("DataUsers.json"))
                    {
                        while (!String.IsNullOrEmpty(response = stream.ReadLine()))
                        {
                            Users Response = JsonConvert.DeserializeObject<Users>(response);

                            //uSers.Add(Response);
                            ID.Add(Response.ID);
                            //foreach (var usr in uSers)
                            //{
                            //    ID.Add(usr.ID);
                            //}
                        }
                    }
                    foreach (var usr in ID)
                    {
                        //string t = e.Message;
                        await bot.SendTextMessageAsync(usr, message.Text, ParseMode.Html, false, false, 0);
                    }
                    IsButton = false;
                }
            }
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
                    //ПРОВЕРИТЬ НА НАЛИЧИЕ ЮЗЕРА В ДЖСОН
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
                                new KeyboardButton(" Календарь событий \U0001F4C5 ")
                            },
                            new []
                            {
                                new KeyboardButton("\U0001F4BB Кафедры \U0001F393"),
                                new KeyboardButton("Администрация \U0001F5C4")
                            },
                            new[]
                            {
                                new KeyboardButton("Если не нашёл(а) свой вопрос в FAQ"),
                                new KeyboardButton("Справка \U0001F4CC")
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
                            InlineKeyboardButton.WithCallbackData("Заселение","dormitory"),
                            InlineKeyboardButton.WithCallbackData("Как добраться ?","howToGetThere")
                        },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("Документы","documents"),
                                InlineKeyboardButton.WithCallbackData("Деканат","dean office")
                               
                               
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("Расписание","schedule"),
                                InlineKeyboardButton.WithCallbackData("Где покушать ?","whereEat")
                            },
                            

                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Студенческий совет","studentSov"),
                            InlineKeyboardButton.WithCallbackData("Устройство двойки", "two")
                        }
                    });
                    await bot.SendTextMessageAsync(message.From.Id, $"Выбери интересующий тебя вопрос : ", replyMarkup: inlineKeyboard);
                    break;

                case "Справка \U0001F4CC":
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
                    await bot.SendTextMessageAsync(message.From.Id, $"Справка : ", replyMarkup: Keyboard_Note);
                    break;

                case "Календарь событий \U0001F4C5":
                    string mes = "Ой, пока запланированных событий нет \U0001F61E";
                    await bot.SendTextMessageAsync(message.From.Id, mes, ParseMode.Html, false, false, 0);
                    break;


                case "\U0001F4BB Кафедры \U0001F393":
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
                    await bot.SendTextMessageAsync(message.From.Id, $"Выбери кафедру, о которой ты хочешь узнать : ", replyMarkup: Keyboard_Department);
                    break;
                case "Администрация \U0001F5C4":
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
                case "/MessageToEveryone":
                    string m = "Администратор, мы рады видеть тебя!" +
                        "Напиши сообщение, которое мы отправим всем!";
                        IsButton = true;
                    await bot.SendTextMessageAsync(message.From.Id, m, ParseMode.Html, false, false, 0);
                    break;
                
            }
        }
    }
}
