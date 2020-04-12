using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Args;



namespace TripToEuropeBot
{
    class Program
    {

        static TelegramBotClient bot;


        /*
            Объявление бота
             */
        static void Main(string[] args)
        {
            bot = new TelegramBotClient("1143924365:AAF8zUIQWgwYKt0megx_BgRg21TYxIreCbk") { Timeout = TimeSpan.FromSeconds(10) };
            var me = bot.GetMeAsync().Result;

            bot.OnMessage += BotOnMessageReceived;
            bot.OnCallbackQuery += BotOnCallBackReceived;

            Console.WriteLine($"bot id:{me.Id }.bot name:{me.FirstName}");
            bot.StartReceiving();
            Console.ReadLine();
        }

        /*
            CALL BACK
             */
        private static async void BotOnCallBackReceived(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            string buttonText = e.CallbackQuery.Data;
            string name = $"{e.CallbackQuery.From.FirstName} {e.CallbackQuery.From.LastName}";
            Console.WriteLine($"{name} нажал кнопку {buttonText}");

            await bot.AnswerCallbackQueryAsync(e.CallbackQuery.Id, $"вы нажали{buttonText}");
            //var tripgoal= BotOnMessageReceived(ReplyKeyboardMarku,tripgoal);

        }

        /*
            ON MESSAGE
             */



        private static async void BotOnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var msg = e.Message;
            var mesg = e.Message;

            if ( msg.Type != MessageType.Text ) { await bot.SendTextMessageAsync(msg.From.Id, "На жаль я не розумію цей текст, виберіть значення знизу"); }
                


            string name = $"{msg.From.FirstName}{msg.From.LastName}";

            Console.WriteLine($"{name} отправил смс {msg.Text}");

            var start = new ReplyKeyboardMarkup(new[] {new KeyboardButton("/start") }, resizeKeyboard: true, oneTimeKeyboard: true);
            
            //Обработка команд!
            //Приветствие
            switch (msg.Text)
            {
                /*case "/start":
                    string text1 =
@"Привіт!
  Я - TripToEuropeBot!

  Я допоможу тобі обрати країну для подорожі.
  Мої основні команди:
  /start-мій запуск
  /menu-вивід меню
  /keyboard-вивід клавіатури";
                    await bot.SendTextMessageAsync(msg.From.Id, text1);
                    break;*/


                //ПЕРВОЕ МЕНЮ
                /*case "/menu":
                    var firstinlinemenu = new InlineKeyboardMarkup(new[] {
                         new[]{InlineKeyboardButton.WithCallbackData("Обрати країну для подорожі") },
                         new[]{InlineKeyboardButton.WithCallbackData("Переглянути попередні результати") },
                         new[]{InlineKeyboardButton.WithUrl("Обрати готель","https://booking.com") },
                         new[]{InlineKeyboardButton.WithCallbackData("Вийти") }
                      });
                    await bot.SendTextMessageAsync(msg.From.Id, "Выберите пункт меню",
                        replyMarkup: firstinlinemenu);
                    break;*/

                //Первый вопрос анкеты
                case "/start":

                    var tripgoal = new ReplyKeyboardMarkup(new[]
                    {
                          new[]
                          {
                              new KeyboardButton("Гірськолижній курорт \U0001F3BF")
                          },
                          new[]
                          {
                              new KeyboardButton("Шопінг \U0001F45C"),
                              new KeyboardButton("Конференції/освіта \U0001F393"),
                              new KeyboardButton("Екскурсії \U0001F3A1")
                          },
                          new[]
                          {
                              new KeyboardButton("Пляжний відпочинок \U0001F3CA")
                          }
                      }, resizeKeyboard: true, oneTimeKeyboard: true);
                    await bot.SendTextMessageAsync(msg.From.Id, "Яка ціль Вашої подорожі?", ParseMode.Html, replyMarkup: tripgoal);
                    
                    break;

                default:
                    break;


            }

            //ГІРСЬКОЛИЖНІЙ КУРОРТ//

            switch (msg.Text)
            {
                case ("Гірськолижній курорт \U0001F3BF"):
                    var mova = new ReplyKeyboardMarkup(new[]
{
                    new[]{new KeyboardButton("Не володію англійською \U0001F613")  },
                    new[]{new KeyboardButton("Можу сказати декілька елементарних фраз \U0001F609") },
                    new[]{new KeyboardButton("Знаю на базовому рівні \U0001F60A") }
                }, resizeKeyboard: true, oneTimeKeyboard: true);
                    await bot.SendTextMessageAsync(msg.From.Id, "Як добре Ви володієте англійською?", ParseMode.Html, replyMarkup: mova);
                    break;
                case ("Можу сказати декілька елементарних фраз \U0001F609"):
                    {
                        var child = new ReplyKeyboardMarkup(new[]
                        {
                                new[]{
                                    new KeyboardButton("Є діти \U0001F46A"),
                                    new KeyboardButton("Немає дітей \U0001F46B") }
                             }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Чи є у Вас діти?", ParseMode.Html, replyMarkup: child);
                    }
                    break;

                case ("Є діти \U0001F46A"): await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Італія", ParseMode.Html);
                                            await bot.SendTextMessageAsync(e.Message.From.Id, 
                                            "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start); break;
                case ("Немає дітей \U0001F46B"): await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Австрія", ParseMode.Html);
                                            await bot.SendTextMessageAsync(e.Message.From.Id,
                                             "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start); break;
                case ("Не володію англійською \U0001F613"): await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Україна", ParseMode.Html);
                                            await bot.SendTextMessageAsync(e.Message.From.Id,
                                            "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start); break;
                case ("Знаю на базовому рівні \U0001F60A"): await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Андорра", ParseMode.Html); 
                                              await bot.SendTextMessageAsync(e.Message.From.Id,
                                             "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start); break;
                default: break;
            }


            //ШОПІНГ//
            switch (msg.Text)
            {
                case ("Шопінг \U0001F45C"):
                    {
                        var yeartime = new ReplyKeyboardMarkup(new[]
                            {
                                new[]{
                                    new KeyboardButton("Осінь \U0001F342"),
                                    new KeyboardButton("Зима \U00002744") },
                            new[]
                            {
                                new KeyboardButton("Весна \U0001F331"),
                                new KeyboardButton("Літо \U0001F338")}
                            }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Яка пора року Вас цікавить?", ParseMode.Html, replyMarkup: yeartime);
                    }
                    break;
                case ("Зима \U00002744"): await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Андорра", ParseMode.Html); 
                                          await bot.SendTextMessageAsync(e.Message.From.Id,
                                          "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start); break;

                case ("Літо \U0001F338"):
                    {
                        var mova = new ReplyKeyboardMarkup(new[]
                        {
                            new[]{new KeyboardButton("Можу сказати декілька елементарних слів \U0001F609") },
                            new[]{new KeyboardButton("Знаю на базовому рівні \U0001F609") }
                        }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Як добре Ви володієте англійською?", ParseMode.Html, replyMarkup: mova);
                    } break;
                case ("Знаю на базовому рівні \U0001F609"): await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Італія", ParseMode.Html); 
                                                            await bot.SendTextMessageAsync(e.Message.From.Id,
                                                            "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start); break;
                case ("Можу сказати декілька елементарних слів \U0001F609"):
                    {
                        var tourist = new ReplyKeyboardMarkup(new[]
                        {
                            new KeyboardButton("Багато туристів \U00002795"),
                            new KeyboardButton("Мало туристів \U00002796")
                        }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Яка кількість туристів переважає для Вас?", ParseMode.Html, replyMarkup: tourist);
                    } break;
                case ("Багато туристів \U00002795"): await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Іспанія", ParseMode.Html); 
                                                     await bot.SendTextMessageAsync(e.Message.From.Id,
                                                     "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start); break;
                case ("Мало туристів \U00002796"): await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Німеччина", ParseMode.Html); 
                                                   await bot.SendTextMessageAsync(e.Message.From.Id,
                                                   "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start); break;

                case ("Осінь \U0001F342"):
                    {
                        var mova = new ReplyKeyboardMarkup(new[]
  {
                            new[]{new KeyboardButton("Можу сказати декілька елементарних слів \U0001F61C") },
                            new[]{new KeyboardButton("Знаю на базовому рівні \U0001F60F") }
                        }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Як добре Ви володієте англійською?", ParseMode.Html, replyMarkup: mova);
                    }
                    break;
                case ("Можу сказати декілька елементарних слів \U0001F61C"):
                    { var tourist = new ReplyKeyboardMarkup(new[]
{
                            new KeyboardButton("Багато туристів \U0001F64C"),
                            new KeyboardButton("Мало туристів \U00002757")
                        }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Яка кількість туристів переважає для Вас?", ParseMode.Html, replyMarkup: tourist);
                    }
                    break;
                case ("Багато туристів \U0001F64C"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Іспанія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start); break;
                case ("Мало туристів \U00002757"): await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Чехія", ParseMode.Html); 
                                                   await bot.SendTextMessageAsync(e.Message.From.Id,
                                                   "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start); break;
                case ("Знаю на базовому рівні \U0001F60F"): await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Нідерланди", ParseMode.Html); 
                                                            await bot.SendTextMessageAsync(e.Message.From.Id,
                                                             "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start); break;
                default: break;
            }

            switch (msg.Text)
            {
                case ("Весна \U0001F331"):
                    {
                        var price = new ReplyKeyboardMarkup(new[]
{
                            new KeyboardButton("Так \U0001F609"),
                            new KeyboardButton("Ні \U0001F60A")
                        }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Чи важлива ціна для Вас?", ParseMode.Html, replyMarkup: price);
                    }
                    break;
                case ("Ні \U0001F60A"): {
                        var child = new ReplyKeyboardMarkup(new[]
                             {
                                new[]{
                                    new KeyboardButton("Є діти \U0001F37C"),
                                    new KeyboardButton("Немає дітей \U0001F600") }
                             }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Чи є у Вас діти?", ParseMode.Html, replyMarkup: child);
                    } break;
                case ("Є діти \U0001F37C"):
                    {
                        var mova = new ReplyKeyboardMarkup(new[]
  {
                            new[]{new KeyboardButton("Можу сказати декілька елементарних слів \U0001F606") },
                            new[]{new KeyboardButton("Знаю на базовому рівні \U0001F606") }
                        }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Як добре Ви володієте англійською?", ParseMode.Html, replyMarkup: mova);
                    }
                    break;
                case ("Можу сказати декілька елементарних слів \U0001F606"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Грузія", ParseMode.Html); 
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start); break;
                case ("Знаю на базовому рівні \U0001F606"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Бельгія", ParseMode.Html); 
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start); break;

                case ("Немає дітей \U0001F600"):
                    {
                        var mova = new ReplyKeyboardMarkup(new[]
                        {
                            new[]{new KeyboardButton("Можу сказати декілька елементарних слів \U0001F603") },
                            new[]{new KeyboardButton("Знаю на базовому рівні \U0001F440") }
                        }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Як добре Ви володієте англійською?", ParseMode.Html, replyMarkup: mova);
                    }
                    break;
                case ("Можу сказати декілька елементарних слів \U0001F603"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Австрія", ParseMode.Html); 
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start); break;
                case ("Знаю на базовому рівні \U0001F440"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Ірландія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start); break;



                case ("Так \U0001F609"):
                    {
                        var tourist = new ReplyKeyboardMarkup(new[]
                        {
                           new[]{ new KeyboardButton("Багато туристів \U0001F646") },
                           new[]{ new KeyboardButton("Мало туристів \U0001F645") },
                           new[]{ new KeyboardButton("Кількість туристів не має значення \U0001F604") }
                        }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Яка кількість туристів переважає для Вас?", ParseMode.Html, replyMarkup: tourist);
                    }
                    break;
                case ("Багато туристів \U0001F646"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Туреччина ", ParseMode.Html); 
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start); break;
                case ("Мало туристів \U0001F645"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Польща", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start); break;
                case ("Кількість туристів не має значення \U0001F604"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Австрія", ParseMode.Html); 
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start); break;
            }

            switch (msg.Text)
            {
                case ("Пляжний відпочинок \U0001F3CA"):
                    {
                        var yeartime = new ReplyKeyboardMarkup(new[]
                        {
                            new[]{new KeyboardButton("Весна \U00002600") },
                            new[]{new KeyboardButton("Літо \U0001F335") },
                            new[]{new KeyboardButton("Осінь \U00002614") }
                        },resizeKeyboard:true,oneTimeKeyboard:true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Яка пора року Вас цікавить? ",ParseMode.Html,replyMarkup:yeartime);
                    }break;
                case ("Осінь \U00002614"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Іспанія", ParseMode.Html); 
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Весна \U00002600"):
                    {
                        var tourist = new ReplyKeyboardMarkup(new[]
                        {
                            new[]{new KeyboardButton("Багато туристів \U0001F607") },
                           new[]{ new KeyboardButton("Мало туристів \U0001F466") },
                           new[]{ new KeyboardButton("Кількість туристів не має значення \U0001F463") }
                        }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Яка кількість туристів переважає для Вас?", ParseMode.Html, replyMarkup: tourist);
                        break;
                    }
                case ("Багато туристів \U0001F607"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Туреччина", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Мало туристів \U0001F466"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Грузія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Кількість туристів не має значення \U0001F463"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Хорватія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Літо \U0001F335"):
                    {
                        var mova = new ReplyKeyboardMarkup(new[]
                        {
                            new[]{new KeyboardButton("Не знаю англійської \U0001F481") },
                            new[]{new KeyboardButton("Можу сказати декілька елементарних слів \U0001F601") },
                            new[]{new KeyboardButton("Знаю на базовому рівні \U0001F603") },
                            new[]{new KeyboardButton("Знаю дуже добре \U0001F612") }
                        }, resizeKeyboard:true,oneTimeKeyboard:true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Як добре Ви володієте англійською", ParseMode.Html, replyMarkup: mova);
                    }break;
                case ("Знаю дуже добре \U0001F612"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Албанія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;

                case ("Не знаю англійської \U0001F481"):
                    {
                        var tourist = new ReplyKeyboardMarkup(new[]
                        {
                            new KeyboardButton("Багато туристів \U0001F60D"),
                            new KeyboardButton("Мало туристів \U0001F60A")
                        },resizeKeyboard:true,oneTimeKeyboard:true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Яка кількість туристів переважає для Вас?", ParseMode.Html, replyMarkup: tourist);
                    }
                    break;
                case ("Багато туристів \U0001F60D"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Чорногорія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Мало туристів \U0001F60A"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Україна", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;


                case ("Можу сказати декілька елементарних слів \U0001F601"):
                    {
                        var tourist = new ReplyKeyboardMarkup(new[]
                        {
                            new KeyboardButton("Багато туристів \U0001F649"),
                            new KeyboardButton("Мало туристів \U0001F648")
                        }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Яка кількість туристів переважає для Вас?", ParseMode.Html, replyMarkup: tourist);
                    }
                    break;
                case ("Багато туристів \U0001F649"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Болгарія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Мало туристів \U0001F648"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Греція", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;

                case ("Знаю на базовому рівні \U0001F603"):
                    {
                        var triptype = new ReplyKeyboardMarkup(new[]
                        {
                            new KeyboardButton("Активний \U0001F6B2"),
                            new KeyboardButton("Пасивний \U0001F6C0")
                        }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Якому типу відпочинку Ви надаєте перевагу?", ParseMode.Html, replyMarkup: triptype);
                    }
                    break;
                case ("Активний \U0001F6B2"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Італія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Пасивний \U0001F6C0"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Кіпр", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
            }
           
            switch(msg.Text)
            {
                case ("Конференції/освіта \U0001F393"):
                    {
                        var mova = new ReplyKeyboardMarkup(new[]
                        {
                            new[]{new KeyboardButton("Not English \U0000263A"),
                                  new KeyboardButton("Elementary \U00002615")},
                            new[]{new KeyboardButton("Intermediate \U00002B50"),
                                  new KeyboardButton("Advanced \U0000261D") },
                            new[]{ new KeyboardButton("Upper-Intermediate \U000026A1") } 
                        },resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Як добре Ви володієте англійською?",ParseMode.Html,replyMarkup:mova);
                    }
                    break;

                case ("Intermediate \U00002B50"):
                    {
                        var price = new ReplyKeyboardMarkup(new[]
                        {
                            new[]{new KeyboardButton("Дешево \U0001F4B8"),
                                  new KeyboardButton("Нижче середнього \U0001F4B5")},
                            new[]{new KeyboardButton("Середня ціна \U0001F4B4") },
                            new[]{new KeyboardButton("Дорого \U0001F4B3"),
                                  new KeyboardButton("Дуже дорого \U0001F4B0")}
                        },resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Яка ціна для Вас можлива?", ParseMode.Html, replyMarkup: price);
                    };
                    break;

                case ("Дорого \U0001F4B3"):
                    {
                        var tourist = new ReplyKeyboardMarkup(new[]
                        {
                            new KeyboardButton("Багато туристів \U0001F4A5"),
                            new KeyboardButton("Мало туристів \U0001F44D")
                        },resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Яка кількість туристів переважає для Вас?", ParseMode.Html, replyMarkup: tourist);
                    }break;
                case ("Багато туристів \U0001F4A5"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Австрія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Мало туристів \U0001F44D"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Ірландія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;

                case ("Дуже дорого \U0001F4B0"):
                    {
                        var tourist = new ReplyKeyboardMarkup(new[]
                        {
                            new[]{new KeyboardButton("Багато туристів \U0001F44D") },
                            new[]{new KeyboardButton("Мало туристів \U0001F446") },
                            new[]{new KeyboardButton("Кількість туристів не важлива \U0001F44D") }
                        },resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Яка кількість туристів переважає для Вас?", ParseMode.Html, replyMarkup: tourist);
                    }
                    break;
                case ("Багато туристів \U0001F44D"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Данія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Мало туристів \U0001F446"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Ісландія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Кількість туристів не важлива \U0001F44D"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Німеччина", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;



                case ("Дешево \U0001F4B8"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Румунія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Нижче середнього \U0001F4B5"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Естонія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Середня ціна \U0001F4B4"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Чехія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;

                case ("Upper-Intermediate \U000026A1"):
                    {
                        var price = new ReplyKeyboardMarkup(new[]
                        {
                            new KeyboardButton("Так \U00002795"),
                            new KeyboardButton("Ні \U00002796")
                        },resizeKeyboard: true, oneTimeKeyboard: true) ;
                        await bot.SendTextMessageAsync(msg.From.Id, "Чи важлива для Вас ціна? ", ParseMode.Html, replyMarkup:price);
                    }break;
                case ("Так \U00002795"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Італія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Ні \U00002796"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Швеція", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;


                case ("Not English \U0000263A"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Україна", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Elementary \U00002615"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Польща", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Advanced \U0000261D"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Іспанія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;

            }
            switch(msg.Text)
            {
                case ("Екскурсії \U0001F3A1"):
                    {
                        var yeartime = new ReplyKeyboardMarkup(new[]
                        {
                            new[]{new KeyboardButton("Весна \U0001F427") },
                            new[]{new KeyboardButton("Літо \U0001F42C") },
                            new[]{new KeyboardButton("Осінь \U0001F425") }
                        }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Яка пора року Вас цікавить ? ", ParseMode.Html, replyMarkup: yeartime);
                    }
                    break;
                    
                case ("Весна \U0001F427"):
                    {
                        var price = new ReplyKeyboardMarkup(new[]
                        {
                            new KeyboardButton("Так \U0001F4B0"),
                            new KeyboardButton("Ні \U0001F4B3"),
                            new KeyboardButton("Ще не знаю \U0001F4B8")
                        },resizeKeyboard:true,oneTimeKeyboard:true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Чи важлива для Вас ціна?", ParseMode.Html, replyMarkup:price);
                    } break;
                case ("Так \U0001F4B0"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Румунія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;

                case ("Ще не знаю \U0001F4B8"):
                    {
                        var tourist = new ReplyKeyboardMarkup(new[]
                        {
                            new[]{new KeyboardButton("Багато туристів \U0001F4AA") },
                            new[]{new KeyboardButton("Мало туристів \U0001F4AB") },
                            new[]{new KeyboardButton("Кількість туристів не важлива \U0001F4A3") }
                        }, resizeKeyboard: true, oneTimeKeyboard: true); 
                        await bot.SendTextMessageAsync(msg.From.Id, "Яка кількість туристів переважає для Вас?", ParseMode.Html, replyMarkup:tourist);
                    }
                    break;
                case ("Багато туристів \U0001F4AA"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Туреччина", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Мало туристів \U0001F4AB"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Угорщина", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Кількість туристів не важлива \U0001F4A3"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Австрія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Ні \U0001F4B3"):
                    {
                        var tourist = new ReplyKeyboardMarkup(new[]
                        {
                            new[]{new KeyboardButton("Багато туристів \U0001F4A3") },
                            new[]{new KeyboardButton("Мало туристів \U0001F4AA") },
                            new[]{new KeyboardButton("Кількість туристів не важлива \U0001F4AB") }
                        }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Яка кількість туристів переважає для Вас?", ParseMode.Html, replyMarkup: tourist);
                    }
                    break;
                case ("Багато туристів \U0001F4A3"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Італія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Мало туристів \U0001F4AA"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Ірландія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Кількість туристів не важлива \U0001F4AB"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Бельгія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;



                case ("Літо \U0001F42C"):
                    {
                        var price  = new ReplyKeyboardMarkup(new[]
                        {
                            new KeyboardButton("Так \U0001F4B3"),
                            new KeyboardButton("Ні \U0001F4B0")
                        }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Чи важлива для Вас ціна? ", ParseMode.Html, replyMarkup:price);
                    }
                    break;
                case ("Так \U0001F4B3"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Албанія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Ні \U0001F4B0"):
                    {
                        var mova = new ReplyKeyboardMarkup(new[]
                        {
                             new[]{new KeyboardButton("Не знаю англійської \U0001F4AC") },
                            new[]{new KeyboardButton("Можу сказати декілька елементарних слів \U0001F4AC") },
                            new[]{new KeyboardButton("Знаю на базовому рівні \U0001F4AC") },
                            new[]{new KeyboardButton("Знаю дуже добре \U0001F4AC") }
                        }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id,"Як добре Ви володієте англійською?", ParseMode.Html, replyMarkup:mova);
                    }
                    break;
                case ("Знаю на базовому рівні \U0001F4AC"):
                    {
                        var tourist = new ReplyKeyboardMarkup(new[]
                        {
                            new[]{new KeyboardButton("Багато туристів \U0001F6B4") },
                            new[]{new KeyboardButton("Мало туристів \U0001F6B4") },
                            new[]{new KeyboardButton("Кількість туристів не важлива \U0001F6B4") }
                        }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Яка кількість туристів переважає для Вас?", ParseMode.Html, replyMarkup:tourist);
                    }
                    break;

                case ("Не знаю англійської \U0001F4AC"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Туреччина", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Можу сказати декілька елементарних слів \U0001F4AC"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Греція", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Знаю дуже добре \U0001F4AC"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Італія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Багато туристів \U0001F6B4"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Іспанія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Мало туристів \U0001F6B4"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Кіпр", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Кількість туристів не важлива \U0001F6B4"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Франція", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;






                case ("Осінь \U0001F425"):
                    {
                        var price = new ReplyKeyboardMarkup(new[]
                        {
                            new[]{new KeyboardButton("Дешево \U0001F4B0"),
                                  new KeyboardButton("Нижче середнього \U0001F4B0")},
                           
                            new[]{new KeyboardButton("Середня ціна \U0001F4B0"),
                                new KeyboardButton("Дорого \U0001F4B0")}
                        }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Чи важлива для Вас ціна?", ParseMode.Html, replyMarkup:price);
                    }
                    break;

                case ("Середня ціна \U0001F4B0"):
                    {
                        var tourist = new ReplyKeyboardMarkup(new[]
                        {
                             new KeyboardButton("Багато туристів \U0001F440"),
                             new KeyboardButton("Мало туристів \U0001F440")
                        }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Яка кількість туристів переважає для Вас?", ParseMode.Html, replyMarkup: tourist);
                    }
                    break;
                case ("Дорого \U0001F4B0"):
                    {
                        var child = new ReplyKeyboardMarkup(new[]
                        {
                             new KeyboardButton("Є діти \U0001F4AA"),
                             new KeyboardButton("Немає дітей \U0001F4AB")
                        }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Чи є у Вас діти? ", ParseMode.Html, replyMarkup:child);
                    }
                    break;
                case ("Немає дітей \U0001F4AB"):
                    {
                        var tourist = new ReplyKeyboardMarkup(new[]
                        {
                             new KeyboardButton("Багато туристів \U0001F46A"),
                             new KeyboardButton("Мало туристів \U0001F46A")
                        }, resizeKeyboard: true, oneTimeKeyboard: true);
                        await bot.SendTextMessageAsync(msg.From.Id, "Яка кількість туристів переважає для Вас?", ParseMode.Html, replyMarkup:tourist);
                    }
                    break;

                case ("Дешево \U0001F4B0"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Білорусь", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Нижче середнього \U0001F4B0"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Естонія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Мало туристів \U0001F440"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Чехія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Багато туристів \U0001F440"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Іспанія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Є діти \U0001F4AA"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Данія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Багато туристів \U0001F46A"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Нідерланди", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
                case ("Мало туристів \U0001F46A"):
                    await bot.SendTextMessageAsync(msg.From.Id, "Ваша країна для подорожі: Ісландія", ParseMode.Html);
                    await bot.SendTextMessageAsync(e.Message.From.Id,
                    "Щоб ще раз підібрати країну - натисніть /start \U0001F447", ParseMode.Html, replyMarkup: start);
                    break;
            }
        }
    }
}
