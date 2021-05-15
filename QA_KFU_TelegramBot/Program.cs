using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.InputFiles;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace QA_KFU_TelegramBot
{
    class Program
    {
        static string TOKEN;
        static bool IsButton = false;
        static bool isQuestion = false;
        static TelegramBotClient bot;
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            string tok = File.ReadAllText("Token.txt");
            TOKEN = (String)tok;   
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
                string sdd = "\n Так как мы с тобой теперь не в школе, а в университете, ты должен понимать, " +
                                 "что твой учебный день теперь разделен не на уроки по 45 минут, а на длинннннные пары " +
                                 "по 1,5 часа, между которыми у тебя есть всего 10 минуток свободного времени. " +
                                 "Но университет не менее заботится о своих студентах, поэтому создал обеденную перемену аж в 40 минут!!!" +
                                 "\n Итак, запоминаем, время, когда ты сможешь покушать и набраться сил - перерыв между 3 и 4 парой(с 13:20 до 14:00). " +
                                 "Казалось бы, за 40 минут можно успеть многое, но по статистике почти все перваки попадаются на эту ловушку, конечно же, " +
                                 "опаздывая на пары. Чтобы вы не допустили этой ошибки, мы подготовили для вас столовые и магазинчики, где вы успеете " +
                                 "пообедать(пссс, столовые идут в порядке увеличения расстояния от университета или же в порядке увеличения очереди=времени, " +
                                 "которое вы потратите на обед в данном заведении):" +
                                 "\n" +
                                 "\n - СТОЛОВЫЕ:" +
                                 "\n" +
                                 "\n" +
                                 "\n 1. Столовая физического корпуса КФУ. Обычная студенческая столовая, цены не самые низки, качество 4/5, но находится достаточно " +
                                 "близко к двойке. Средний чек ~ 95-115 рублей(суп, второе и компот). ГДЕ? Выходим из двойки, поворачиваем направо к НАЦИОНАЛЬНОЙ БИБЛИОТЕКЕ КАЗАНИ" +
                                 "(или просто видим соседнюю высотку рядом - идём к ней)." +
                                 "\n" +
                                 "\n" +
                                 "\n 2. Токмач. Хорошая столовая с адекватными ценами и вкусной едой, качество 4,5/5, так же находится достаточно близко к двойке. Будь готов к тому, " +
                                 "что очередь в обеденную перемену за каждую минуту увеличатся в геометрической прогрессии, но и уменьшается примерно с такой же скоростью(бывает и такое, " +
                                 "что свободных столов может не быть). Средний чек ~ 75-100 рублей(суп, второе, чай). ГДЕ? Выходим из двойки, поворачиваем налево к УНИКСу и далее следует вниз " +
                                 "в сторону ул. Баумана, ГУМа, доходите до первого перекрестка и налево. Карта ниже" +
                                 "\n" +
                                 "\n" +
                                 "\n 3. Студенческая столовая в ГУМе. Хорошая столовая, цены немного выше среднего, но зато очереди в разы меньше. Оценка 3,8/5. Средний чек ~120-145 рублей" +
                                 "(суп, второе, чай). ГДЕ? Выходим из двойки, поворачиваем налево к УНИКСу и далее следует вниз в сторону ул. Баумана до ГУМа. Заходим в ГУМ, поднимаемся на 2 этаж." +
                                 " Кстати, там же вы можете отведать в соседнем отделе дико вкусную шаурму по приятной цене:)" +
                                 "\n" +
                                 "\n" +
                                 "\n 4. Тюбетей. Кафе татарской кухни, где вы можете отведать вкусную выпечку. Оценка 4/5. Средний чек ~150 рублей. Около двойки есть аж 2 точки, одна у УНИКСа, " +
                                 "другая на Баумана. ГДЕ? Выходим из двойки, поворачиваем налево к УНИКСу и далее следует вниз в сторону ул. Баумана." +
                                 "\n" +
                                 "\n" +
                                 "\n 5. Мастер-пицца. Название говорит само за себя, мини-пиццы на вынос. Оценка 3,5/5. Средний чек ~130 рублей. ГДЕ? Выходим из двойки, поворачиваем налево к УНИКСу" +
                                 " и далее следует вниз в сторону ул. Баумана, последний дом перед перекрёстком - наша точка.";
                await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, sdd);

            } else if(e.CallbackQuery.Data == "dormitory")
            {
                string s = "  Когда заселяться в ДУ?" +
           "\n<>Заселение происходит согласно графику.\n" +
           "\n  В ДУ есть столовая?" +
           "\n<>Да, и там можно вполне вкусно поесть, если вам не хочется готовить.\n" +
           "\n  Где можно отдохнуть в ДУ?" +
           "\n<>Специально для студентов было создано Art - пространство ComeIn, там можно попить чай с печеньками, поиграть в настольные игры.\n" +
           "\n  Сколько человек будет в комнате вместе со мной?" +
           "\n<>Комнаты рассчитаны на 2 - 5 человек.\n" +
           "\n  Сколько стоит проживание в ДУ?" +
           "\n<>За год надо будет заплатить 5600 рублей за проживание и в конце года оплатить электричество в соответствии с приборами, которыми вы будете пользоваться.\n";
                await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, s);
            } else if(e.CallbackQuery.Data == "howToGetThere")
            {
                string a = "Из Деревни Универсиады до учебы можно добраться несколькими способами:\n\n" +
                            "1) Троллейбус №8\n" + "2) Автобус №47\n" + "3) Метро\n" +
                            "Общее время: 40 - 60 мин \n Цена: 30 - 27 руб. \n\n" + "4) Такси\n" +
                            "Самый быстрый вариант. Общее время: 20 - 25 мин, зависит от пробок. \n" +
                            " Цена: на четырёх цена может быть от 20 до 50.  \n";
                await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, a);
            } else if(e.CallbackQuery.Data == "dean office")
            {
                string sdd = "\n Деканат находится по адресу: Казань, Кремлевская, 35, к. 1004" +
                                "\nРуководитель: Чикрин Дмитрий Евгеньевич" +
                                "\nТелефон: (843) 233-70-37" +
                                "\n" +
                                "\n" +
                                "В этом месте ты можешь получить важную для тебя информацию, а также получить необходимые для тебя документы";
                await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, sdd);
            } else if(e.CallbackQuery.Data =="two")
            {
                string sdd = "\n КАК УСТРОЕН ТВОЙ УЧЕБНЫЙ КОРПУС ?" +
                                 "\n Давайте по порядку…" +
                                 "\n 1. Твой учебный корпус называется ДВОЙКА. " +
                                 "ДВОЙКА - многоэтажное здание, которое является одни из учебных корпусов Казанского(Приволжского) Федерального Университета" +
                                 "(если быть точнее, то 14 корпус), расположенное по адресу улица Кремлевская, дом 35. " +
                                 "\n 2. Для того, чтобы попасть в ДВОЙКУ нам необходимо найти главный вход. " +
                                 "Главный вход расположен напротив главного корпуса КФУ:" +
                                 " подходим к двойке, если вы стоите лицом к ней, а спиной к главному зданию и наблюдаете перед собой лестницу," +
                                 " то вы на ВЕРНОМ пути! (Не путать со входом в библиотеку!!!)" +
                                 "\n 3. Поднимаемся по лестнице, показываем студенческий билет и вот ТЫ на 1 этаже нашей прекрасной двойки." +
                                 "\n 4. Смотрим на карту, пробуем ориентироваться. Как только ты зашёл в ДВОЙКУ, перед тобой стоят турникеты и охранники, " +
                                 "которые пристально проверяют наличие студенческого билета." +
                                 "\n 5. ЗАПОМНИ: этажи ИВМиИТа - с 8 по 12" +
                                 "\n 6. Мраморный зал(мраморка и другие вариации) - место, где ты можешь сделать дз, поговорить с одногруппниками и друзьями, " +
                                 "провести собрание группы и т.д.(кстати, здесь же проходят все масштабные мероприятия: день программиста, студбатл, выдача профкарт)" +
                                 "\n 7. Такс, ещё небольшой лайфхак :) Через мраморный зал можно напрямую попасть в здание, где находится библиотека. Для этого нужно " +
                                 "пройти через мраморный зал прямо до упора, под лестницей вы увидите деревянную дверь, которая приведет вас в нужное помещение." +
                                 "\n 8. Поехали наверх, на главный этаж нашего ИВМиИТа - 10 этаж." +
                                 "\n 9. ВАЖНО: номер кабинета имеет в себе огромный смысл, рассмотрим на примере кабинета 1006(10 - номер этажа, на котором расположен " +
                                 "кабинет, соответственно 06 - порядковый номера кабинета слева направо)" +
                                 "\n10. Здание двойки имеет свою особенность - технический этаж. ЧТО ЭТО ТАКОЕ? Технический этаж - этаж, расположенный между 1 и 2 этажом," +
                                 " к которому студенты не имеют доступ....поэтому если ты захочешь подниматься пешком по лестнице в аудиторию, то помни, что тебе всегда" +
                                 " придется подниматься на один этаж больше. Рассчитывай время грамотно!";
                await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, sdd);
            } else if(e.CallbackQuery.Data == "studentSov")
            {
                string s = "Деятели института:\n" +
             "\nТимур Хабибуллин - председатель студенческого совета" +
             "\nАлиса Пепеляева - профорг 1 курса" +
             "\nАнтон Приползин - председатель Профбюро, профорг 2 курса" +
             "\nРадик Газетдинов - профорг 3 курса" +
             "\nНикита Александров - профорг 4 курса" +
             "\nТимур Хабибуллин - профорг 4 курса" +
             "\n\nАзамат Имамов - профорг магистратуры" +
             "\nВладимир Михайлов - ответственный за социальное питание" +
             "\nКатя Покровская - культорг" +
             "\nДамир Гарипов - спорторг" +
             "\nКамилла Шангараева - председатель студенческого совета дома 3 / 1 Деревни Универсиады" +
             "\nДиляра Ахметшина - председатель студсовета ИВМиИТ в общежитии №9" +
             "\nТимур Ибрагимов - руководитель добровольческого центр";
                await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, s);
            } else if(e.CallbackQuery.Data == "schedule")
            {
                string s = "Расписание ИВМИИТа на второй семестр";
                await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, s);
                string filePath = @"Documents/sched2020_21_s2.xlsx";
                using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                await bot.SendDocumentAsync(e.CallbackQuery.From.Id, new InputOnlineFile(fileStream, filePath), "Расписание");
                s = "Если тебе нужно найти расписание не ИВМИИТа или же найти расписание какой-то конкретной группы, то перейди по этой ссылке: \n\n " +
                    "https://kpfu.ru/studentu/ucheba/raspisanie";
                await bot.SendTextMessageAsync(e.CallbackQuery.From.Id, s);
            } else if (e.CallbackQuery.Data =="liveindu")
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


            if (IsButton)
            {
                if (message.From.Id == 363450022 || message.From.Id == 806879827 || message.From.Id == 250899062)
                {
                    List<int> ID = new List<int>();
                    string response;
                    using (StreamReader stream = new StreamReader("DataUsers.json"))
                    {
                        while (!String.IsNullOrEmpty(response = stream.ReadLine()))
                        {
                            Users Response = JsonConvert.DeserializeObject<Users>(response);

                            //uSers.Add(Response);
                            ID.Add(Response.ID);
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

            if (isQuestion)
            {
                int[] Admins = new int[3];
                Admins[0] = 363450022;
                Admins[1] = 806879827;
                Admins[2] = 250899062;
                for (int i = 0; i < Admins.GetLength(0); i++)
                {
                    await bot.SendTextMessageAsync(Admins[i], $"" +
                        $" \n Вопрос : {message.Text} " +
                        $" \n id: {message.From.Id}" +
                        $" \n Username: {message.From.Username}" +
                        $" \n Имя: {message.From.FirstName}" +
                        $" \n Время(UTC): {message.Date.TimeOfDay}" +
                        $" \n Дата: {message.Date} ", ParseMode.Html, false, false, 0);
                }
                isQuestion = false;
            }


            switch (message.Text)     // Команды для бота
            {
                case "/start":

                    List<int> ID = new List<int>();
                    string response;
                    using (StreamReader stream = new StreamReader("DataUsers.json"))
                    {
                        while (!String.IsNullOrEmpty(response = stream.ReadLine()))
                        {
                            Users Response = JsonConvert.DeserializeObject<Users>(response);
                            ID.Add(Response.ID);
                        }
                    }
                    var sticker = new InputOnlineFile("CAACAgIAAxkBAALwi2BE04ZTyadk2ufx3khKlil1K7RjAAJ0AAM7YCQUs8te1W3kR_QeBA");
                    await bot.SendStickerAsync(message.Chat.Id, sticker);
                    string text = "\U0001F525 Добро пожаловать! \U0001F525" +
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

                    if (!ID.Contains(message.From.Id))
                    {
                        Users user = new Users(message.From.FirstName, message.From.Id);
                        var json = JsonConvert.SerializeObject(user);
                        using (StreamWriter stream = new StreamWriter("DataUsers.json", true))
                        {
                            stream.WriteLine(json);
                        }
                    }
                    else
                    {

                    }
                    break;
                    
                case "\U0001F4CB FAQ \U0001F4AC":
                    var inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Заселение","dormitory"), // готово
                            InlineKeyboardButton.WithCallbackData("Как добраться ?","howToGetThere") // готово
                        },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("Документы","documents"), // готово
                                InlineKeyboardButton.WithCallbackData("Деканат","dean office") //готово
                               
                               
                            },
                            new[]
                            {
                                InlineKeyboardButton.WithCallbackData("Расписание","schedule"), //готово
                                InlineKeyboardButton.WithCallbackData("Где покушать ?","whereEat")
                            },
                            

                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Студенческий совет","studentSov"), //готово
                            InlineKeyboardButton.WithCallbackData("Устройство двойки", "two") //готово
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

                case "Если не нашёл(а) свой вопрос в FAQ":
                    string q = "Жаль, что ты не смог(а) найти ответ" +
                        " на свой вопрос в разделе FAQ. \U0001F61E\n" +
                        "Напиши вопрос, который ты хочешь задать Админу : ";
                    isQuestion = true;
                    await bot.SendTextMessageAsync(message.From.Id, q, ParseMode.Html, false, false, 0);
                    break;

                    
            }
        }
    }
}
